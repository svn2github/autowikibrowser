using System;
using System.Collections.Generic;
using System.Text;

using WikiFunctions;
using WikiFunctions.Logging;

namespace WikiFunctions.Logging
{
	/// <summary>
	/// An inheritable implementation of a Logging manager, built around a generic collection of IMyTraceListener objects and String keys
	/// </summary>
    public class TraceManager : IMyTraceListener
	{

		// Listeners:
        protected Dictionary<string, IMyTraceListener> Listeners = new Dictionary<string, IMyTraceListener>();

		public virtual void AddListener(string Key, IMyTraceListener Listener)
		{
			// Override this if you want to programatically add an event handler
			Listeners.Add(Key, Listener);
		}
		public virtual void RemoveListener(string Key)
		{
			// Override this if you want to programatically remove an event handler
			Listeners[Key].Close();
			Listeners.Remove(Key);
		}
		public bool ContainsKey(string Key)
		{
			return Listeners.ContainsKey(Key);
		}
		public bool ContainsValue(IMyTraceListener Listener)
		{
			return Listeners.ContainsValue(Listener);
		}

		// IMyTraceListener:
		public virtual void Close()
		{
			foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
			{
				t.Value.Close();
			}
		}
		public virtual void Flush()
		{
			foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
			{
				t.Value.Flush();
			}
		}
		public virtual void ProcessingArticle(string FullArticleTitle, Namespaces NS)
		{
			foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
			{
				t.Value.ProcessingArticle(FullArticleTitle, NS);
			}
		}
		public virtual void WriteBulletedLine(string Line, bool Bold, bool VerboseOnly, bool DateStamp)
		{
			foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
			{
				t.Value.WriteBulletedLine(Line, Bold, VerboseOnly, DateStamp);
			}
		}
		public virtual void WriteBulletedLine(string Line, bool Bold, bool VerboseOnly)
		{
			WriteBulletedLine(Line, Bold, VerboseOnly, false);
		}
		public virtual void SkippedArticle(string SkippedBy, string Reason)
		{
			foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
			{
				t.Value.SkippedArticle(SkippedBy, Reason);
			}
		}
		public virtual void SkippedArticleBadTag(string SkippedBy, string FullArticleTitle, Namespaces NS)
		{
			foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
			{
				t.Value.SkippedArticleBadTag(SkippedBy, FullArticleTitle, NS);
			}
		}

        public virtual void SkippedArticleRedlink(string SkippedBy, string FullArticleTitle, Namespaces NS)
        {
            foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
            {
                t.Value.SkippedArticleRedlink(SkippedBy, FullArticleTitle, NS);
            }
        }
        public virtual void WriteArticleActionLine(string Line, string PluginName)
        {
            foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
            {
                t.Value.WriteArticleActionLine(Line, PluginName);
            }
        }
        public virtual void WriteTemplateAdded(string Template, string PluginName)
        {
            foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
            {
                t.Value.WriteTemplateAdded(Template, PluginName);
            }
        }
        public virtual void WriteArticleActionLine(string Line, string PluginName, bool VerboseOnly)
        {
            WriteArticleActionLine1(Line, PluginName, VerboseOnly);
        }
        public virtual void WriteArticleActionLine1(string Line, string PluginName, bool VerboseOnly)
        {
            if (VerboseOnly)
            {
                foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
                {
                    t.Value.WriteArticleActionLine(Line, PluginName, true);
                }
            }
            else
            {
                foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
                {
                    t.Value.WriteArticleActionLine(Line, PluginName);
                }
            }
        }
        public virtual bool Uploadable
        {
            get
            {
                foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
                {
                    if (t.Value.Uploadable)
                    {
                        return true;
                    }
                }
                //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
                return false;
            }
        }
        public virtual void Write(string Text)
        {
            foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
            {
                t.Value.Write(Text);
            }
        }
        public virtual void WriteComment(string Line)
        {
            foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
            {
                t.Value.WriteComment(Line);
            }
        }
        public virtual void WriteCommentAndNewLine(string Line)
        {
            foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
            {
                t.Value.WriteCommentAndNewLine(Line);
            }
        }
        public virtual void WriteLine(string Line)
        {
            foreach (KeyValuePair<string, IMyTraceListener> t in Listeners)
            {
                t.Value.WriteLine(Line);
            }
        }
    }
}

