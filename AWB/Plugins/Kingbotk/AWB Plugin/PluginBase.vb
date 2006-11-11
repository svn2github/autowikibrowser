Namespace AWB.Plugins.SDKSoftware.Kingbotk
    ''' <summary>
    ''' SDK Software's base class for template-manipulating AWB plugins
    ''' </summary>
    Friend MustInherit Class PluginBase
        ' Settings:
        Protected Friend MustOverride ReadOnly Property conPluginShortName() As String
        Protected MustOverride ReadOnly Property InspectUnsetParameters() As Boolean
        Protected Const ForceAddition As Boolean = True ' we might want to parameterise this later
        Protected MustOverride ReadOnly Property ParameterBreak() As String
        Protected Friend Const conTemplatePlaceholder As String = "{{xxxTEMPLATExxx}}"
        Protected MustOverride ReadOnly Property OurTemplateHasAlternateNames() As Boolean
        Protected Friend MustOverride ReadOnly Property GenericSettings() As GenericSettingsClass
        Protected MustOverride ReadOnly Property CategoryTalkClassParm() As String
        Protected MustOverride ReadOnly Property TemplateTalkClassParm() As String

        ' Objects:
        Protected WithEvents OurMenuItem As ToolStripMenuItem
        Protected Manager As PluginManager
        Protected Article As Article
        Protected Template As Templating

        ' Regular expressions:
        Protected MainRegex As Regex
        Protected SecondChanceRegex As Regex
        Protected Const conRegexpLeft As String = "\{\{\s*(?<tl>template\s*:)?\s*(?<tlname>"
        'Protected Const conRegexpRight As String = _
        '   ")[\s\n\r]*(([\s\n\r]*\|[\s\n\r]*(?<parm>[-a-z0-9&]*)[\s\n\r]*)+(=[\s\n\r]*(?<val>[-a-z0-9]*)[\s\n\r]*)?)*\}\}[\s\n\r]*"
        Protected Const conRegexpRight As String = _
           ")[\s\n\r]*(([\s\n\r]*\|[\s\n\r]*(?<parm>[^}{|\s\n\r=]*)[\s\n\r]*)+(=[\s\n\r]*" & _
           "(?<val>[^}{|\s\n\r]*)[\s\n\r]*)?)*\}\}[\s\n\r]*"
        Protected Const conRegexpRightNotStrict As String = ")[^}]*"
        Protected Const conRegexpOptions As RegexOptions = RegexOptions.Compiled Or RegexOptions.Multiline Or _
           RegexOptions.IgnoreCase Or RegexOptions.ExplicitCapture
        Protected PreferredTemplateNameRegex As Regex
        Protected MustOverride ReadOnly Property PreferredTemplateNameWiki() As String
        Private Shared StubClassTemplateRegex As New Regex(conRegexpLeft & "Stubclass" & _
           ")[\s\n\r]*(([\s\n\r]*\|[\s\n\r]*(?<parm>[^}{|\s\n\r=]*)[\s\n\r]*)+(=[\s\n\r]*" & _
           "(?<val>[^|\n\r]*)[\s\n\r]*)?)*\}\}[\s\n\r]*", conRegexpOptions) ' value might contain {{!}} and spaces

        ' Enum:
        Friend Enum ProcessTalkPageMode As Integer
            Normal
            ManualAssessment
            NonStandardTalk
        End Enum

        Protected Friend Sub New(ByVal PM As PluginManager)
            Manager = PM
        End Sub

        ' AWB pass through:
        Protected Sub InitialiseBase(ByVal AWBPluginsMenu As ToolStripMenuItem, ByVal txt As TextBox)
            With OurMenuItem
                .CheckOnClick = True
                .Checked = False
            End With
            AWBPluginsMenu.DropDownItems.Add(OurMenuItem)

            GenericSettings.EditTextBox = txt

            Try
                DirectCast(txt.ContextMenuStrip.Items("insertTagToolStripMenuItem"), ToolStripMenuItem). _
                   DropDownItems.AddRange(GenericSettings.TextInsertContextMenuStripItems)
            Catch
            End Try
        End Sub
        Protected Friend MustOverride Sub Initialise(ByVal AWBPluginsMenu As ToolStripMenuItem, _
           ByVal txt As TextBox)
        Protected Friend MustOverride Sub ReadXML(ByVal Reader As XmlTextReader)
        Protected Friend MustOverride Sub Reset()
        Protected Friend MustOverride Sub WriteXML(ByVal Writer As XmlTextWriter)
        Protected Friend Sub ProcessTalkPage(ByVal A As Article)
            ProcessTalkPage(A, Classification.Code, Importance.Code, False, False, False, _
               ProcessTalkPageMode.Normal)
        End Sub
        Protected Friend Sub ProcessTalkPage(ByVal A As Article, ByVal Classification As Classification, _
        ByVal Importance As Importance, ByVal ForceNeedsInfobox As Boolean, _
        ByVal ForceNeedsAttention As Boolean, ByVal RemoveAutoStub As Boolean, _
        ByVal ProcessTalkPageMode As ProcessTalkPageMode)

            Me.Article = A

            If SkipIfContains() Then
                A.PluginIHaveFinished(SkipResults.SkipMiscellaneous, conPluginShortName)
            Else
                ' MAIN
                Dim OriginalArticleText As String = A.AlteredArticleText

                Template = New Templating
                A.AlteredArticleText = MainRegex.Replace(A.AlteredArticleText, AddressOf Me.MatchEvaluator)

                If Template.BadTemplate Then
                    GoTo BadTemplate
                ElseIf Template.FoundTemplate Then
                    TemplateFound()
                Else
                    If SecondChanceRegex.IsMatch(OriginalArticleText) Then
                        GoTo BadTemplate
                    Else
                        If ForceAddition Then TemplateNotFound()
                    End If
                End If

                ProcessArticleFinish()
                If Not ProcessTalkPageMode = PluginBase.ProcessTalkPageMode.Normal Then
                    ProcessArticleFinishNonStandardMode(Classification, Importance, ForceNeedsInfobox, _
                       ForceNeedsAttention, RemoveAutoStub, ProcessTalkPageMode)
                End If

                If Article.ProcessIt Then
                    TemplateWritingAndPlacement()
                Else
                    A.AlteredArticleText = OriginalArticleText ' New: Hopefully fixes a bug but keep an eye on this
                    A.PluginIHaveFinished(SkipResults.SkipNoChange, conPluginShortName)
                End If
            End If

ExitMe:
            Article = Nothing
            Exit Sub

BadTemplate:
            A.PluginIHaveFinished(SkipResults.SkipBadTag, conPluginShortName) ' TODO: We could get the template placeholder here
            Article = Nothing
            Exit Sub
        End Sub

        ' Article processing:
        Protected MustOverride Sub InspectUnsetParameter(ByVal Param As String)
        Protected MustOverride Function SkipIfContains() As Boolean
        Protected MustOverride Sub TemplateFound()
        Protected MustOverride Sub ProcessArticleFinish()
        Protected MustOverride Function CreateTemplateHeader(ByRef PutTemplateAtTop As Boolean) As String
        Protected MustOverride Sub ImportanceParameter(ByVal Importance As Importance)
        Protected Function MatchEvaluator(ByVal match As Match) As String
            If Not match.Groups("parm").Captures.Count = match.Groups("val").Captures.Count Then
                Template.BadTemplate = True
            Else
                Template.FoundTemplate = True
                Article.PluginCheckTemplateCall(match.Groups("tl").Value, conPluginShortName)

                If OurTemplateHasAlternateNames Then
                    Article.PluginCheckTemplateName(match.Groups("tlname").Value, PreferredTemplateNameRegex, _
                       PreferredTemplateNameWiki, conPluginShortName)
                End If

                If match.Groups("parm").Captures.Count > 0 Then
                    For i As Integer = 0 To match.Groups("parm").Captures.Count - 1

                        Dim value As String = match.Groups("val").Captures(i).Value
                        Dim parm As String = match.Groups("parm").Captures(i).Value

                        If value = "" Then
                            If InspectUnsetParameters Then InspectUnsetParameter(parm)
                        Else
                            Template.AddTemplateParmFromExistingTemplate(parm, value)
                        End If
                    Next
                End If
                End If

                Return conTemplatePlaceholder
        End Function
        Protected Overridable Sub TemplateNotFound()
            Article.ArticleHasAMajorChange()
            Template.NewTemplateParm("class", "")
            Article.TemplateAdded(PreferredTemplateNameWiki, conPluginShortName)
        End Sub
        Private Sub TemplateWritingAndPlacement()
            Dim PutTemplateAtTop As Boolean
            Dim TemplateHeader As String = CreateTemplateHeader(PutTemplateAtTop)

            For Each o As KeyValuePair(Of String, Templating.TemplateParametersObject) _
            In Template.Parameters
                With o
                    TemplateHeader += "|" + .Key + "=" + .Value.Value + ParameterBreak
                End With
            Next

            TemplateHeader += "}}" + Microsoft.VisualBasic.vbCrLf

            With Me.Article
                If Not Template.FoundTemplate Then
                    .AlteredArticleText = TemplateHeader + .AlteredArticleText
                    .HavePlacedATemplateAtTop()
                ElseIf PutTemplateAtTop Then
                    .AlteredArticleText = TemplateHeader + .AlteredArticleText.Replace(conTemplatePlaceholder, "")
                    .HavePlacedATemplateAtTop()
                    ' TODO: If this seems slow could use regex.replace with a count of 1. (But not for bad tags, _
                    ' where we might have multiple instances of the same template... that's done in
                    ' PluginManager.FinaliseArticleProcessingReturnSkip())
                Else
                    .AlteredArticleText = .AlteredArticleText.Replace(conTemplatePlaceholder, TemplateHeader)
                End If
            End With
        End Sub
        Protected Sub AddAndLogNewParamWithAYesValue(ByVal ParamName As String)
            Template.NewOrReplaceTemplateParm(ParamName, "yes", Article, True, conPluginShortName)
        End Sub
        Protected Sub ProcessArticleFinishNonStandardMode(ByVal Classification As Classification, _
        ByVal Importance As Importance, ByVal ForceNeedsInfobox As Boolean, _
        ByVal ForceNeedsAttention As Boolean, ByVal RemoveAutoStub As Boolean, _
        ByVal ProcessTalkPageMode As ProcessTalkPageMode)
            Select Case Classification
                Case Kingbotk.Classification.Code
                    If ProcessTalkPageMode = PluginBase.ProcessTalkPageMode.NonStandardTalk Then
                        Select Case Me.Article.Namespace
                            Case Namespaces.CategoryTalk
                                Template.NewOrReplaceTemplateParm( _
                                   "class", CategoryTalkClassParm, Me.Article, True, conPluginShortName)
                            Case Namespaces.TemplateTalk
                                Template.NewOrReplaceTemplateParm( _
                                   "class", TemplateTalkClassParm, Me.Article, True, conPluginShortName)
                            Case Namespaces.ImageTalk, Namespaces.PortalTalk, Namespaces.ProjectTalk
                                Template.NewOrReplaceTemplateParm( _
                                   "class", "NA", Me.Article, True, conPluginShortName)
                        End Select
                    End If
                Case Kingbotk.Classification.Unassessed
                Case Else
                    Template.NewOrReplaceTemplateParm("class", Classification.ToString, Me.Article, False)
            End Select

            Select Case Importance
                Case Kingbotk.Importance.Code, Kingbotk.Importance.Unassessed
                Case Else
                    ImportanceParameter(Importance)
            End Select

            If ForceNeedsInfobox Then AddAndLogNewParamWithAYesValue("needs-infobox")

            If ForceNeedsAttention Then AddAndLogNewParamWithAYesValue("attention")

            If RemoveAutoStub Then
                With Me.Article
                    If Template.Parameters.ContainsKey("auto") Then
                        Template.Parameters.Remove("auto")
                        .ArticleHasAMajorChange()
                    End If

                    If StubClassTemplateRegex.IsMatch(.AlteredArticleText) Then
                        .AlteredArticleText = StubClassTemplateRegex.Replace(.AlteredArticleText, "")
                        .ArticleHasAMajorChange()
                    End If
                End With
            End If
        End Sub
        Protected Function WriteOutClassHeader() As String
            With Template
                WriteOutClassHeader = "|class="
                If .Parameters.ContainsKey("class") Then
                    WriteOutClassHeader += .Parameters("class").Value + ParameterBreak
                    .Parameters.Remove("class")
                Else
                    WriteOutClassHeader += ParameterBreak
                End If
            End With
        End Function
        Protected Sub StubClass()
            If Me.Article.Namespace = Namespaces.Talk Then
                If GenericSettings.StubClass Then Template.NewOrReplaceTemplateParm("class", "Stub", Article, _
                   True, conPluginShortName, True)
                If GenericSettings.AutoStub Then
                    If Template.NewOrReplaceTemplateParm("class", "Stub", Article, True, conPluginShortName, True) _
                       Then AddAndLogNewParamWithAYesValue("auto")
                    ' If add class=Stub (we don't change if set) add auto
                End If
            Else
                PluginSettingsControl.MyTrace.WriteArticleActionLine1( _
                   "Ignoring Stub-Class and Auto-Stub options; not a mainspace talk page", conPluginShortName, True)
            End If
        End Sub

        ' Interraction with manager:
        Friend Property Enabled() As Boolean
            Get
                Return OurMenuItem.Checked
            End Get
            Set(ByVal IsEnabled As Boolean)
                OurMenuItem.Checked = IsEnabled
                ShowHideOurObjects(IsEnabled)
                Manager.PluginEnabledStateChanged(Me, IsEnabled)
            End Set
        End Property
        Protected Friend Overridable Sub BotModeChanged(ByVal BotMode As Boolean)
            If BotMode And GenericSettings.StubClass Then
                GenericSettings.AutoStub = True
                GenericSettings.StubClass = False
            End If
            GenericSettings.StubClassModeAllowed = Not BotMode
        End Sub
        Protected Friend Overridable ReadOnly Property IAmReady() As Boolean
            Get
                Return True
            End Get
        End Property
        Protected Friend Overridable ReadOnly Property IAmGeneric() As Boolean
            Get
                Return False
            End Get
        End Property

        ' User interface:
        Protected MustOverride Sub ShowHideOurObjects(ByVal Visible As Boolean)
        'Protected Friend ReadOnly Property TextInsertContextMenuItems(ByVal txt As TextBox) _
        'As ToolStripItemCollection
        '    Get

        '    End Get
        'End Property

        ' Event handlers:
        Private Sub ourmenuitem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles OurMenuItem.CheckedChanged
            Enabled = OurMenuItem.Checked
        End Sub
    End Class

    Friend Interface GenericSettingsClass
        Property AutoStub() As Boolean
        Property StubClass() As Boolean
        WriteOnly Property StubClassModeAllowed() As Boolean
        WriteOnly Property EditTextBox() As TextBox
        ReadOnly Property TextInsertContextMenuStripItems() As ToolStripItemCollection
    End Interface
End Namespace