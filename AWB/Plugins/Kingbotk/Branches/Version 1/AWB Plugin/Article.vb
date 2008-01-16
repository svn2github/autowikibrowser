Namespace AutoWikiBrowser.Plugins.SDKSoftware.Kingbotk
    ''' <summary>
    ''' An object representing an article which may or may not contain the targetted template
    ''' </summary>
    Friend NotInheritable Class Article
        ' Properties:
        Private mArticleText As String, mFullArticleTitle As String
        Private mArticleTitle As String, mNamespace As Namespaces
        Private mEditSummary As String = PluginManager.conWikiPluginBrackets, mMajor As Boolean

        ' Plugin-state:
        Private mSkipResults As SkipResults = SkipResults.NotSet
        Private mProcessIt As Boolean ' gets set by ArticleHasAMajorChange/ArticleHasAMinorChange
        Private WeFoundBannerShells As BannerShellsEnum

        ' Regex:
        Private Shared ReadOnly WikiProjectBannerShellRegex As New Regex(PluginBase.conRegexpLeft & PluginBase.WikiProjectBannerShell & _
           ")\b[\s\n\r]*\|1=(?<body>.*}}[^{]*?)(?<end>\|[^{]*)?}}", RegexOptions.Compiled Or RegexOptions.IgnoreCase Or _
           RegexOptions.Singleline Or RegexOptions.ExplicitCapture)
        Private Shared ReadOnly WikiProjectBannersRegex As New Regex("")

        ' Enum:
        Private Enum BannerShellsEnum
            NotChecked
            NoneFound
            FoundWikiProjectBannerShell
            FoundWikiProjectBanners ' if both are present, well, tough titty!
        End Enum

        ' New:
        Friend Sub New(ByVal ArticleText As String, ByVal vFullArticleTitle As String, _
        ByVal vNamespace As Namespaces)
            mArticleText = ArticleText
            mFullArticleTitle = vFullArticleTitle
            mNamespace = vNamespace
            'mFullArticleTitle = GetArticleName(mNamespace, mArticleTitle)
        End Sub

        ' Public properties:
        Public Property AlteredArticleText() As String
            Get
                Return mArticleText
            End Get
            Set(ByVal value As String)
                mArticleText = value
            End Set
        End Property
        Public ReadOnly Property FullArticleTitle() As String
            Get
                Return mFullArticleTitle
            End Get
        End Property
        Public ReadOnly Property [Namespace]() As Namespaces
            Get
                Return mNamespace
            End Get
        End Property
        Public Property EditSummary() As String
            Get
                Return mEditSummary
            End Get
            Set(ByVal value As String)
                mEditSummary = value
            End Set
        End Property
        Public Sub ArticleHasAMinorChange()
            mProcessIt = True
        End Sub
        Public Sub ArticleHasAMajorChange()
            mProcessIt = True
            mMajor = True
        End Sub
        Public ReadOnly Property ChangesAreMinor() As Boolean
            Get
                Return Not mMajor
            End Get
        End Property
        Public ReadOnly Property ProcessIt() As Boolean
            Get
                Return mProcessIt
            End Get
        End Property

        ' For calling by plugin:
        Friend Sub PluginCheckTemplateCall(ByVal TemplateCall As String, ByVal PluginName As String)
            If Not TemplateCall = "" Then ' we have "template:"
                mProcessIt = True
                'EditSummary += "Remove ""template:"", "
                PluginManager.AWBForm.TraceManager.WriteArticleActionLine("Remove ""template:"" call", PluginName, True)
            End If
        End Sub
        Friend Sub PluginIHaveFinished(ByVal Result As SkipResults, ByVal PluginName As String)
            Select Case Result
                Case SkipResults.SkipBadTag
                    mSkipResults = SkipResults.SkipBadTag
                    PluginManager.AWBForm.TraceManager.SkippedArticleBadTag(PluginName, mFullArticleTitle, mNamespace)
                Case SkipResults.SkipRegex
                    If mSkipResults = SkipResults.NotSet Then mSkipResults = SkipResults.SkipRegex
                    PluginManager.AWBForm.TraceManager.SkippedArticle(PluginName, _
                       "Article text matched skip regex")
                Case SkipResults.SkipNoChange
                    PluginManager.AWBForm.TraceManager.SkippedArticle(PluginName, "No change")
                    mSkipResults = SkipResults.SkipNoChange
            End Select
        End Sub

        ' For calling by manager:
        Friend ReadOnly Property PluginManagerGetSkipResults() As SkipResults
            Get
                Return mSkipResults
            End Get
        End Property
        Friend Sub PluginManagerLookForHeaderTemplatesAndFinaliseEditSummary()
            TalkPages.TalkPageHeaders.ProcessTalkPage(AlteredArticleText, PluginManager.AWBForm.TraceManager, _
               TalkPages.DEFAULTSORT.NoChange, PluginManager.PluginName)

            EditSummary = Regex.Replace(EditSummary, ", $", ".")
            PluginManager.AWBForm.WebControl.SetMinor(ChangesAreMinor)
        End Sub
        Friend Sub PluginManagerEditSummaryTaggingCategory(ByVal CategoryName As String)
            If Not CategoryName = "" Then EditSummary += "Tag [[Category:" + CategoryName + "]]. "
        End Sub

        ' Match evaluators:
        Private Function WikiProjectBannerShellRegexMatchEvaluator(ByVal match As Match) As String
            Const templatename As String = "WikiProjectBannerShell"

            ' Does the shell contain template: ?
            PluginCheckTemplateCall(match.Groups("tl").Value, templatename)
            ' Does the template have it's primary name:
            If Not match.Groups("template").Value = templatename Then RenamedATemplate(match.Groups("template").Value, _
               templatename, templatename)


            'PluginManager.AWBForm.TraceManager.WriteArticleActionLine()
        End Function
        Private Function WikiProjectBannersRegexMatchEvaluator(ByVal match As Match) As String

        End Function

        ' General:
        Friend Sub RenamedATemplate(ByVal OldName As String, ByVal NewName As String, ByVal Caller As String)
            DoneReplacement(OldName, NewName, False)
            PluginManager.AWBForm.TraceManager.WriteArticleActionLine( _
               String.Format("Rename template [[Template:{0}|{0}]]→[[Template:{1}|{1}]]", OldName, NewName), Caller)
        End Sub
        Friend Sub DoneReplacement(ByVal Old As String, ByVal Replacement As String, _
        ByVal LogIt As Boolean, Optional ByVal PluginName As String = "")
            mProcessIt = True
            EditSummary += Old + "→" + Replacement + ", "
            If LogIt Then PluginManager.AWBForm.TraceManager.WriteArticleActionLine("Replacement: " + Old + "→" + _
               Replacement, PluginName)
        End Sub
        Friend Sub TemplateAdded(ByVal Template As String, ByVal PluginName As String)
            mEditSummary += String.Format("Added {{{{[[Template:{0}|{0}]]}}}}, ", Template)
            PluginManager.AWBForm.TraceManager.WriteTemplateAdded(Template, PluginName)
            ArticleHasAMajorChange()
        End Sub
        Friend Sub ParameterAdded(ByVal ParamName As String, ByVal ParamValue As String, _
        ByVal PluginName As String, Optional ByVal MinorEdit As Boolean = False)
            mEditSummary += ParamName & "=" & ParamValue & ", "
            PluginManager.AWBForm.TraceManager.WriteArticleActionLine(ParamName & "=" & ParamValue, PluginName)

            If MinorEdit Then ArticleHasAMinorChange() Else ArticleHasAMajorChange()
        End Sub
        Friend Sub RestoreTemplateToPlaceholderSpot(ByVal TemplateHeader As String)
            ' just write one instance of template even if have multiple conTemplatePlaceholder's
            Static strPlaceholder As String = Regex.Escape(PluginBase.conTemplatePlaceholder)
            Static RestoreTemplateToPlaceholderSpotRegex As New Regex(strPlaceholder)

            AlteredArticleText = RestoreTemplateToPlaceholderSpotRegex.Replace(AlteredArticleText, TemplateHeader, 1)
            AlteredArticleText = RestoreTemplateToPlaceholderSpotRegex.Replace(AlteredArticleText, "")
        End Sub
        'Friend Function ReplaceReqphotoWithTemplateParams(ByVal PluginName As String) As Boolean
        '    If ReqPhotoNoParamsRegex.IsMatch(AlteredArticleText) Then
        '        AlteredArticleText = ReqPhotoNoParamsRegex.Replace(AlteredArticleText, "")
        '        DoneReplacement("{{reqphoto}}", "template param(s)", True, PluginName)
        '        ArticleHasAMajorChange()
        '        Return True
        '    End If
        'End Function
        Friend Sub AddReqPhoto(ByVal PluginName As String)
            AlteredArticleTextPrependLine("{{reqphoto}}")
            ArticleHasAMajorChange()
            PluginManager.AWBForm.TraceManager.WriteArticleActionLine1("Added {{[[Template:Reqphoto|Reqphoto]]}}", PluginName, True)
        End Sub
        Friend Sub PrependTemplateOrWriteIntoShell(ByVal Text As String, ByVal Living As Boolean, ByVal ActivePol As Boolean)
            If WeFoundBannerShells = BannerShellsEnum.NotChecked Then
                If WikiProjectBannerShellRegex.IsMatch(AlteredArticleText) Then
                    WeFoundBannerShells = BannerShellsEnum.FoundWikiProjectBannerShell
                ElseIf WikiProjectBannersRegex.IsMatch(AlteredArticleText) Then
                    WeFoundBannerShells = BannerShellsEnum.FoundWikiProjectBanners
                Else
                    WeFoundBannerShells = BannerShellsEnum.NoneFound
                End If
            End If

            Select Case WeFoundBannerShells
                Case BannerShellsEnum.FoundWikiProjectBanners
                    AlteredArticleText = WikiProjectBannersRegex.Replace(AlteredArticleText, _
                       AddressOf Me.WikiProjectBannersRegexMatchEvaluator, 1)
                Case BannerShellsEnum.FoundWikiProjectBannerShell
                    AlteredArticleText = WikiProjectBannerShellRegex.Replace(AlteredArticleText, _
                       AddressOf Me.WikiProjectBannerShellRegexMatchEvaluator, 1)
                Case BannerShellsEnum.NoneFound
                    AlteredArticleText = Text + AlteredArticleText
                Case Else
                    Throw New ArgumentException
            End Select
        End Sub
        Friend Sub AlteredArticleTextPrependLine(ByVal Text As String)
            AlteredArticleText = Text + Microsoft.VisualBasic.vbCrLf + AlteredArticleText
        End Sub

        'Public Sub OpenInBrowser()
        '    Tools.OpenENArticleInBrowser(FullArticleTitle, False)
        'End Sub
        Public Sub EditInBrowser()
            Tools.EditArticleInBrowser(FullArticleTitle)
        End Sub
    End Class

    ''' <summary>
    ''' An object which wraps around a collection of template parameters
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class Templating
        Friend FoundTemplate As Boolean = False, BadTemplate As Boolean = False
        Friend Parameters As New Dictionary(Of String, TemplateParametersObject)

        Friend Sub AddTemplateParmFromExistingTemplate(ByVal ParameterName As String, ByVal ParameterValue As String)
            If Parameters.ContainsKey(ParameterName) Then
                If Not Parameters.Item(ParameterName).Value = ParameterValue Then BadTemplate = True
            Else
                Parameters.Add(ParameterName, New TemplateParametersObject(ParameterName, ParameterValue))
            End If
        End Sub
        Friend Sub NewTemplateParm(ByVal ParameterName As String, ByVal ParameterValue As String)
            Parameters.Add(ParameterName, New TemplateParametersObject(ParameterName, ParameterValue))
        End Sub
        Friend Sub NewTemplateParm(ByVal ParameterName As String, ByVal ParameterValue As String, _
        ByVal LogItAndUpdateEditSummary As Boolean, ByVal TheArticle As Article, ByVal PluginName As String, _
        Optional ByVal MinorEdit As Boolean = False)
            NewTemplateParm(ParameterName, ParameterValue)
            If LogItAndUpdateEditSummary Then TheArticle.ParameterAdded(ParameterName, ParameterValue, _
               PluginName, MinorEdit)
        End Sub
        ''' <summary>
        ''' Checks for the existence of a parameter and adds it if missing/optionally changes it
        ''' </summary>
        ''' <returns>True if made a change</returns>
        Friend Function NewOrReplaceTemplateParm(ByVal ParameterName As String, ByVal ParameterValue As String, _
        ByVal TheArticle As Article, ByVal LogItAndUpdateEditSummary As Boolean, _
        ByVal ParamHasAlternativeName As Boolean, Optional ByVal DontChangeIfSet As Boolean = False, _
        Optional ByVal ParamAlternativeName As String = "", Optional ByVal PluginName As String = "", _
        Optional ByVal MinorEditOnlyIfAdding As Boolean = False) As Boolean

            If Parameters.ContainsKey(ParameterName) Then
                NewOrReplaceTemplateParm = ReplaceTemplateParm(ParameterName, ParameterValue, TheArticle, _
                   LogItAndUpdateEditSummary, DontChangeIfSet, PluginName)
            ElseIf ParamHasAlternativeName AndAlso Parameters.ContainsKey(ParamAlternativeName) Then
                NewOrReplaceTemplateParm = ReplaceTemplateParm(ParamAlternativeName, ParameterValue, TheArticle, _
                   LogItAndUpdateEditSummary, DontChangeIfSet, PluginName)
            Else ' Doesn't contain parameter
                NewTemplateParm(ParameterName, ParameterValue, LogItAndUpdateEditSummary, TheArticle, PluginName, _
                   MinorEditOnlyIfAdding)

                If MinorEditOnlyIfAdding Then TheArticle.ArticleHasAMinorChange() _
                   Else TheArticle.ArticleHasAMajorChange()
                Return True
            End If

            If NewOrReplaceTemplateParm Then TheArticle.ArticleHasAMajorChange()
        End Function
        Private Function ReplaceTemplateParm(ByVal ParameterName As String, ByVal ParameterValue As String, _
        ByVal TheArticle As Article, ByVal LogItAndUpdateEditSummary As Boolean, ByVal DontChangeIfSet As Boolean, _
        ByVal PluginName As String) As Boolean
            Dim ExistingValue As String = _
               WikiFunctions.WikiRegexes.Comments.Replace(Parameters(ParameterName).Value, "").Trim

            If Not ExistingValue = ParameterValue Then ' Contains parameter with a different value
                If ExistingValue = "" OrElse Not DontChangeIfSet _
                Then ' Contains parameter with a different value, and _
                    ' we want to change it; or contains an empty parameter
                    Parameters(ParameterName).Value = ParameterValue
                    TheArticle.ArticleHasAMajorChange()
                    If LogItAndUpdateEditSummary Then
                        If ExistingValue = "" Then
                            TheArticle.ParameterAdded(ParameterName, ParameterValue, PluginName)
                        Else
                            TheArticle.DoneReplacement(ParameterName & "=" & ExistingValue, ParameterValue, _
                               True, PluginName)
                        End If
                    End If
                    ReplaceTemplateParm = True
                Else ' Contains param with a different value, and we don't want to change it
                    PluginManager.AWBForm.TraceManager.WriteArticleActionLine( _
                       String.Format("{0} not changed, has existing value of {1}", _
                       ParameterName, ParameterValue), PluginName)
                End If
            End If ' Else: Already contains parameter and correct value; no need to change
        End Function
        Friend Sub RemoveParentWorkgroup(ByVal ChildWorkGroupParm As String, ByVal ParentWorkGroupParm As String, _
        ByVal AddChildWorkGroupParm As Boolean, ByVal Article As Article, ByVal PluginName As String)
            Dim AddedParm As Boolean

            If AddChildWorkGroupParm AndAlso Not Parameters.ContainsKey(ChildWorkGroupParm) Then
                NewTemplateParm(ChildWorkGroupParm, "yes")
                AddedParm = True
            End If

            If Parameters.ContainsKey(ChildWorkGroupParm) AndAlso Parameters.ContainsKey(ParentWorkGroupParm) _
            AndAlso Parameters(ChildWorkGroupParm).Value = "yes" Then
                Parameters.Remove(ParentWorkGroupParm)
                Article.DoneReplacement(ParentWorkGroupParm, ChildWorkGroupParm, True, PluginName)
            ElseIf AddedParm Then
                Article.ParameterAdded(ChildWorkGroupParm, "yes", PluginName)
            End If
        End Sub

        ''' <summary>
        ''' An object which represents a template parameter
        ''' </summary>
        ''' <remarks></remarks>
        Friend NotInheritable Class TemplateParametersObject
            Public Name As String
            Public Value As String

            Friend Sub New(ByVal ParameterName As String, ByVal ParameterValue As String)
                Name = ParameterName
                Value = ParameterValue
            End Sub
        End Class
    End Class
End Namespace