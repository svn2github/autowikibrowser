﻿/*
(C) 2007 Stephen Kennedy (Kingboyk) http://www.sdk-software.com/

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

/* Some of this is currently only suitable for enwiki. */

using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace WikiFunctions.TalkPages
{
    public enum DEFAULTSORT
    {
        NoChange,
        MoveToTop,
        MoveToBottom
    }

    internal class Processor
    {
        public string DefaultSortKey;
        public bool FoundDefaultSort;
        public bool FoundSkipToTalk;
        public bool FoundTalkHeader;

        // Match evaluators:
        public string DefaultSortMatchEvaluator(Match match)
        {
            FoundDefaultSort = true;
            if (match.Groups["key"].Captures.Count > 0)
                DefaultSortKey = match.Groups["key"].Captures[0].Value.Trim();
            return "";
        }

        public string SkipTOCMatchEvaluator(Match match)
        {
            FoundSkipToTalk = true;
            return "";
        }

        public string TalkHeaderMatchEvaluator(Match match)
        {
            FoundTalkHeader = true;
            return "";
        }
    }

    public static class TalkPageHeaders
    {
        public static bool ContainsDefaultSortKeywordOrTemplate(string articleText)
        {
            return WikiRegexes.Defaultsort.IsMatch(articleText);
        }

        /// <summary>
        /// Processes talk pages: moves any talk page header template, moves any default, adds a section heading if missing
        /// </summary>
        /// <param name="articleText">The talk page text</param>
        /// <param name="moveDefaultsort">The action to take over any defaultsort on the page</param>
        /// <returns>The updated talk page text</returns>
        public static bool ProcessTalkPage(ref string articleText, DEFAULTSORT moveDefaultsort)
        {
            Processor pr = new Processor();

            articleText = WikiRegexes.SkipTOCTemplateRegex.Replace(articleText, new MatchEvaluator(pr.SkipTOCMatchEvaluator), 1);
            
            // move talk page header to the top
            articleText = MoveTalkHeader(articleText);

            if (pr.FoundSkipToTalk)
                WriteHeaderTemplate("Skip to talk", ref articleText);

            if (moveDefaultsort != DEFAULTSORT.NoChange)
            {
                articleText = WikiRegexes.Defaultsort.Replace(articleText, 
                    new MatchEvaluator(pr.DefaultSortMatchEvaluator), 1);
                if (pr.FoundDefaultSort)
                {
                    if (!string.IsNullOrEmpty(pr.DefaultSortKey))
                    {
                        articleText = SetDefaultSort(pr.DefaultSortKey, moveDefaultsort, articleText);
                    }
                }
            }
            
            articleText = AddMissingFirstCommentHeader(articleText);
            
            return pr.FoundTalkHeader || pr.FoundSkipToTalk || pr.FoundDefaultSort;
        }

        public static string FormatDefaultSort(string articleText)
        {
            return WikiRegexes.Defaultsort.Replace(articleText, "{{DEFAULTSORT:${key}}}");
        }
        
        // Helper routines:
        private static string SetDefaultSort(string key, DEFAULTSORT location, string articleText)
        {
            switch (location)
            {
                case DEFAULTSORT.MoveToTop:
                    return "{{DEFAULTSORT:" + key + "}}\r\n" + articleText;

                case DEFAULTSORT.MoveToBottom:
                    return articleText + "\r\n{{DEFAULTSORT:" + key + "}}";
            }

            return "";
        }

        /// <summary>
        /// Writes the input template to the top of the input page; updates the input edit summary
        /// </summary>
        /// <param name="name">template name to write</param>
        /// <param name="articleText">article text to update</param>
        private static void WriteHeaderTemplate(string name, ref string articleText)
        {
            articleText = "{{" + name + "}}\r\n" + articleText;
        }
        
        /// <summary>
        /// Moves the {{talk header}} template to the top of the talk page
        /// </summary>
        /// <param name="articleText">the talk page text</param>
        /// <returns>the update talk page text</returns>
        private static string MoveTalkHeader(string articleText)
        {
            Match m = WikiRegexes.TalkHeaderTemplate.Match(articleText);
            
            if(m.Success && m.Index > 0)
            {
                // remove existing talk header – handle case where not at beginning of line
                articleText = articleText.Replace(m.Value, articleText.Contains("\r\n" + m.Value) ? "" : "\r\n");

                // write existing talk header to top
                articleText = m.Value.TrimEnd() + "\r\n" + articleText.TrimStart();
                
                // ensure template is now named {{talk header}}
                articleText = articleText.Replace(m.Groups[1].Value, "Talk header");
            }
            
            return articleText;
        }
        
        private static readonly Regex FirstComment = new Regex(@"^ {0,4}[:\*\w'""](?<!_)", RegexOptions.Compiled | RegexOptions.Multiline);
        
        /// <summary>
        /// Adds a section 2 heading before the first comment if the talk page does not have one
        /// </summary>
        /// <param name="articleText">The talk page text</param>
        /// <returns>The updated article text</returns>
        private static string AddMissingFirstCommentHeader(string articleText)
        {
            // don't match on lines within templates
            string articleTextTemplatesSpaced = Tools.ReplaceWithSpaces(articleText, WikiRegexes.NestedTemplates.Matches(articleText));
            
            if(FirstComment.IsMatch(articleTextTemplatesSpaced))
            {
                int firstCommentIndex = FirstComment.Match(articleTextTemplatesSpaced).Index;
                
                int firstLevelTwoHeading = WikiRegexes.HeadingLevelTwo.IsMatch(articleText) ? WikiRegexes.HeadingLevelTwo.Match(articleText).Index : 99999999;
                
                if (firstCommentIndex < firstLevelTwoHeading)
                {
                    // is there a heading level 3? If yes, change to level 2
                    string articletexttofirstcomment = articleText.Substring(0, firstCommentIndex);

                    articleText = WikiRegexes.HeadingLevelThree.IsMatch(articletexttofirstcomment) ? WikiRegexes.HeadingLevelThree.Replace(articleText, @"==$1==", 1) : articleText.Insert(firstCommentIndex, "\r\n==Untitled==\r\n");
                }
            }
            
            return articleText;
        }
        
        private static List<string> BannerShellRedirects = new List<string>(new [] { "WikiProject Banners", "WikiProjectBanners", "WPBS", "WPB", "Wpb", "Wpbs"});
        
        /// <summary>
        /// Performs fixes to the WikiProjectBannerShells template
        /// </summary>
        /// <param name="articletext">The talk page text</param>
        /// <returns>The updated talk page text</returns>
        public static string WikiProjectBannerShell(string articletext)
        {
            // rename redirects
            foreach(string redirect in BannerShellRedirects)
                articletext = Tools.RenameTemplate(articletext, redirect, "WikiProjectBannerShell");
            
            return articletext;
        }
    }
}

