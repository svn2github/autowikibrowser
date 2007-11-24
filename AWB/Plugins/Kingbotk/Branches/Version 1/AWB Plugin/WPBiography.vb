Namespace AutoWikiBrowser.Plugins.SDKSoftware.Kingbotk.Plugins

    Friend Enum Living
        Unknown
        Living
        Dead
    End Enum

    Friend NotInheritable Class WPBiographySettings
        Implements IGenericSettings

        Private Const conLivingParm As String = "BioLivingPerson"
        Private Const conDeadParm As String = "BioNotLivingPerson"
        Private Const conAutoStubParm As String = "BioAutoStub"
        Private Const conStubClassParm As String = "BioStubClass"
        Private Const conActivePolParm As String = "BioActivePol"
        Private Const conArtsEntsWGParm As String = "BioArtsEntsWG"
        Private Const conFilmWGParm As String = "BioFilmWG"
        Private Const conMilitaryWGParm As String = "BioMilitaryWG"
        Private Const conBritishRoyaltyWGParm As String = "BioBritishRoyaltyWG"
        Private Const conRoyaltyWGParm As String = "BioRoyaltyWG"
        Private Const conMusiciansWGParm As String = "BioMusiciansWG"
        Private Const conScienceWGParm As String = "BioScienceWG"
        Private Const conSportWGParm As String = "BioSportWG"
        Private Const conPoliticianWGParm As String = "BioPoliticianWG"
        Private Const conPeerageWGParm As String = "BioPeerageWG"
        Private Const conBaronetsWGParm As String = "BioBaronetsWG"
        Private Const conCategoryTalkParm As String = "BioCategoryTalk"
        Private Const conForcePriorityParm As String = "BioForcePriorityParm"
        Private Const conNonBioParm As String = "BioNonBio"
        Private Const conForceListasParm As String = "BioForceListasParm"

#Region "XML interface"
        Public Sub ReadXML(ByVal Reader As System.Xml.XmlTextReader) Implements IGenericSettings.ReadXML
            Living = PluginManager.XMLReadBoolean(Reader, conLivingParm, Living)
            Dead = PluginManager.XMLReadBoolean(Reader, conDeadParm, Dead)
            AutoStub = PluginManager.XMLReadBoolean(Reader, conAutoStubParm, AutoStub)
            StubClass = PluginManager.XMLReadBoolean(Reader, conStubClassParm, StubClass)
            ActivePol = PluginManager.XMLReadBoolean(Reader, conActivePolParm, ActivePol)
            ArtsEntsWG = PluginManager.XMLReadBoolean(Reader, conArtsEntsWGParm, ArtsEntsWG)
            MilitaryWG = PluginManager.XMLReadBoolean(Reader, conMilitaryWGParm, MilitaryWG)
            RoyaltyWG = PluginManager.XMLReadBoolean(Reader, conRoyaltyWGParm, RoyaltyWG)
            PoliticianWG = PluginManager.XMLReadBoolean(Reader, conPoliticianWGParm, PoliticianWG)
            ForcePriorityParm = PluginManager.XMLReadBoolean(Reader, conForcePriorityParm, ForcePriorityParm)
            ForceListAsParm = PluginManager.XMLReadBoolean(Reader, conForceListasParm, ForceListAsParm)
            BritishRoyaltyWG = PluginManager.XMLReadBoolean(Reader, conBritishRoyaltyWGParm, BritishRoyaltyWG)
            PeerageWG = PluginManager.XMLReadBoolean(Reader, conPeerageWGParm, PeerageWG)
            BaronetsWG = PluginManager.XMLReadBoolean(Reader, conBaronetsWGParm, BaronetsWG)
            NonBio = PluginManager.XMLReadBoolean(Reader, conNonBioParm, NonBio)
            MusiciansWG = PluginManager.XMLReadBoolean(Reader, conMusiciansWGParm, MusiciansWG)
            ScientistWG = PluginManager.XMLReadBoolean(Reader, conScienceWGParm, ScientistWG)
            SportsWG = PluginManager.XMLReadBoolean(Reader, conSportWGParm, SportsWG)
            FilmWG = PluginManager.XMLReadBoolean(Reader, conFilmWGParm, FilmWG)
        End Sub
        Public Sub WriteXML(ByVal Writer As System.Xml.XmlTextWriter) Implements IGenericSettings.WriteXML
            With Writer
                .WriteAttributeString(conLivingParm, Living.ToString)
                .WriteAttributeString(conDeadParm, Dead.ToString)
                .WriteAttributeString(conAutoStubParm, AutoStub.ToString)
                .WriteAttributeString(conStubClassParm, StubClass.ToString)
                .WriteAttributeString(conActivePolParm, ActivePol.ToString)
                .WriteAttributeString(conArtsEntsWGParm, ArtsEntsWG.ToString)
                .WriteAttributeString(conMilitaryWGParm, MilitaryWG.ToString)
                .WriteAttributeString(conRoyaltyWGParm, RoyaltyWG.ToString)
                .WriteAttributeString(conPoliticianWGParm, PoliticianWG.ToString)
                .WriteAttributeString(conForcePriorityParm, ForcePriorityParm.ToString)
                .WriteAttributeString(conForceListasParm, ForceListAsParm.ToString)
                .WriteAttributeString(conBritishRoyaltyWGParm, BritishRoyaltyWG.ToString)
                .WriteAttributeString(conNonBioParm, NonBio.ToString)
                .WriteAttributeString(conMusiciansWGParm, MusiciansWG.ToString)
                .WriteAttributeString(conSportWGParm, SportsWG.ToString)
                .WriteAttributeString(conScienceWGParm, ScientistWG.ToString)
                .WriteAttributeString(conPeerageWGParm, PeerageWG.ToString)
                .WriteAttributeString(conBaronetsWGParm, BaronetsWG.ToString)
                .WriteAttributeString(conFilmWGParm, FilmWG.ToString)
            End With
        End Sub
        Public Sub Reset() Implements IGenericSettings.XMLReset
            Living = False
            Dead = False
            AutoStub = False
            StubClass = False
            ActivePol = False
            ArtsEntsWG = False
            MilitaryWG = False
            RoyaltyWG = False
            PoliticianWG = False
            ForcePriorityParm = False
            ForceListAsParm = False
            BritishRoyaltyWG = False
            NonBio = False
            MusiciansWG = False
            SportsWG = False
            ScientistWG = False
            BaronetsWG = False
            PeerageWG = False
            FilmWG = False
        End Sub
#End Region

#Region "Properties"
        Friend WriteOnly Property BotMode() As Boolean
            Set(ByVal BotModeEnabled As Boolean)
                ' an alternative is to have IGenericSettings declare this, and PluginBase to call it
                ForceListasCheckbox.Enabled = Not BotModeEnabled
                If BotModeEnabled Then ForceListAsParm = False
            End Set
        End Property
        Public ReadOnly Property DeadOrAlive() As Living
            Get
                If Living Then Return Plugins.Living.Living
                If Dead Then Return Plugins.Living.Dead
                Return Plugins.Living.Unknown
            End Get
        End Property
        Private Property Living() As Boolean
            Get
                Return LivingCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                LivingCheckBox.Checked = value
                If value Then Dead = False
            End Set
        End Property
        Private Property Dead() As Boolean
            Get
                Return DeadPersonCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                DeadPersonCheckBox.Checked = value
                If value Then Living = False
            End Set
        End Property
        Public Property StubClass() As Boolean Implements IGenericSettings.StubClass
            Get
                Return StubClassCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                StubClassCheckBox.Checked = value
            End Set
        End Property
        Public Property ActivePol() As Boolean
            Get
                Return ActivePoliticianCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                ActivePoliticianCheckBox.Checked = value
            End Set
        End Property
        Public Property ArtsEntsWG() As Boolean
            Get
                Return ArtsEntsCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                ArtsEntsCheckBox.Checked = value
            End Set
        End Property
        Public Property FilmWG() As Boolean
            Get
                Return FilmCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                FilmCheckBox.Checked = value
            End Set
        End Property
        Public Property MilitaryWG() As Boolean
            Get
                Return MilitaryCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                MilitaryCheckBox.Checked = value
            End Set
        End Property
        Public Property BritishRoyaltyWG() As Boolean
            Get
                Return BritishRoyaltyCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                BritishRoyaltyCheckBox.Checked = value
            End Set
        End Property
        Public Property RoyaltyWG() As Boolean
            Get
                Return RoyaltyCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                RoyaltyCheckBox.Checked = value
            End Set
        End Property
        Public Property PoliticianWG() As Boolean
            Get
                Return PoliticianCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                PoliticianCheckBox.Checked = value
            End Set
        End Property
        Public Property MusiciansWG() As Boolean
            Get
                Return MusiciansCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                MusiciansCheckBox.Checked = value
            End Set
        End Property
        Public Property SportsWG() As Boolean
            Get
                Return SportsCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                SportsCheckBox.Checked = value
            End Set
        End Property
        Public Property ScientistWG() As Boolean
            Get
                Return ScienceAcademiaCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                ScienceAcademiaCheckBox.Checked = value
            End Set
        End Property
        Public Property PeerageWG() As Boolean
            Get
                Return PeerageCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                PeerageCheckBox.Checked = value
            End Set
        End Property
        Public Property BaronetsWG() As Boolean
            Get
                Return BaronetsCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                BaronetsCheckBox.Checked = value
            End Set
        End Property
        Public Property ForcePriorityParm() As Boolean
            Get
                Return ForcePriorityParmCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                ForcePriorityParmCheckBox.Checked = value
            End Set
        End Property
        Public Property ForceListAsParm() As Boolean
            Get
                Return ForceListasCheckbox.Checked
            End Get
            Set(ByVal value As Boolean)
                ForceListasCheckbox.Checked = value
            End Set
        End Property
        Public Property AutoStub() As Boolean Implements IGenericSettings.AutoStub
            Get
                Return AutoStubCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                AutoStubCheckBox.Checked = value
            End Set
        End Property
        Public Property NonBio() As Boolean
            Get
                Return NonBiographyCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                NonBiographyCheckBox.Checked = value
            End Set
        End Property
        WriteOnly Property StubClassModeAllowed() As Boolean Implements IGenericSettings.StubClassModeAllowed
            Set(ByVal value As Boolean)
                StubClassCheckBox.Enabled = value
            End Set
        End Property
        Public ReadOnly Property TextInsertContextMenuStripItems() As ToolStripItemCollection _
        Implements IGenericSettings.TextInsertContextMenuStripItems
            Get
                Return TextInsertContextMenuStrip.Items
            End Get
        End Property
#End Region

#Region "Misc event handlers"
        Private Sub LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
            Tools.OpenENArticleInBrowser("Template:WPBiography", False)
        End Sub
        Private Sub ActivePoliticianCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ActivePoliticianCheckBox.CheckedChanged
            If ActivePoliticianCheckBox.Checked Then PoliticianCheckBox.Checked = True
        End Sub
        Private Sub AutoStubCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AutoStubCheckBox.CheckedChanged
            If AutoStubCheckBox.Checked Then StubClassCheckBox.Checked = False
        End Sub
        Private Sub StubClassCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles StubClassCheckBox.CheckedChanged
            If StubClassCheckBox.Checked Then AutoStubCheckBox.Checked = False
        End Sub
        Private Sub BritishRoyaltyCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles BritishRoyaltyCheckBox.CheckedChanged
            If BritishRoyaltyCheckBox.Checked Then RoyaltyCheckBox.Checked = False
        End Sub
        Private Sub RoyaltyCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles RoyaltyCheckBox.CheckedChanged
            If RoyaltyCheckBox.Checked Then BritishRoyaltyCheckBox.Checked = False
        End Sub
        Private Sub ArtsEntsCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles ArtsEntsCheckBox.CheckedChanged
            If ArtsEntsCheckBox.Checked Then
                MusiciansCheckBox.Checked = False
                FilmCheckBox.Checked = False
            End If
        End Sub
        Private Sub ArtsSubGroupCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MusiciansCheckBox.CheckedChanged, FilmCheckBox.CheckedChanged
            If DirectCast(sender, CheckBox).Checked Then ArtsEntsCheckBox.Checked = False
        End Sub
        Private Sub LivingCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles LivingCheckBox.CheckedChanged
            If LivingCheckBox.Checked Then DeadPersonCheckBox.Checked = False
        End Sub
        Private Sub DeadPersonCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles DeadPersonCheckBox.CheckedChanged
            If DeadPersonCheckBox.Checked Then LivingCheckBox.Checked = False
        End Sub
#End Region

#Region "TextInsertHandlers"
        Private Sub ArtsEntertainmentToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArtsEntertainmentToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("a&e-work-group")
        End Sub
        Private Sub BritishRoyaltyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BritishRoyaltyToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("british-royalty")
        End Sub
        Private Sub MilitaryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MilitaryToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("military-work-group")
        End Sub
        Private Sub PoliticsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PoliticsToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("politician-work-group")
        End Sub
        Private Sub RoyaltyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RoyaltyToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("royalty-work-group")
        End Sub
        Private Sub AttentionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AttentionToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("attention")
        End Sub
        Private Sub InfoboxToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles InfoboxToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("needs-infobox")
        End Sub
        Private Sub CollabCandidateToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CollabCandidateToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("collaboration-candidate")
        End Sub
        Private Sub PastCollabToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PastCollabToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("past-collaboration")
        End Sub
        Private Sub PeerReviewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PeerReviewToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("peer-review")
        End Sub
        Private Sub OldPeerReviewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OldPeerReviewToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("old-peer-review")
        End Sub
        Private Sub LivingPersonToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LivingPersonToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("living")
        End Sub
        Private Sub ActivePoliticianToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ActivePoliticianToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("activepol")
        End Sub
        Private Sub CoreArticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CoreArticleToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("core")
        End Sub
        Private Sub NonbiographyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NonbiographyToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("non-bio")
        End Sub
        Private Sub AutotaggedToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AutotaggedToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("auto")
        End Sub
        Private Sub WPBiographyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WPBiographyToolStripMenuItem.Click
            PluginManager.EditBoxInsert("{{WPBiography}}")
        End Sub
        Private Sub MusiciansToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MusiciansToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("musician-work-group")
        End Sub
        Private Sub ScienceAcademiaToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ScienceAcademiaToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("s&a-work-group")
        End Sub
        Private Sub SportsGamesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SportsGamesToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("sports-work-group")
        End Sub
        Private Sub BaronetsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BaronetsToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("baronets-work-group")
        End Sub
        Private Sub PeerageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PeerageToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("peerage-work-group")
        End Sub
        Private Sub DeceasedPersonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeceasedPersonToolStripMenuItem.Click
            PluginManager.EditBoxInsert("|living=no")
        End Sub
        Private Sub FilmToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FilmToolStripMenuItem.Click
            PluginManager.EditBoxInsertYesParam("filmbio-work-group")
        End Sub
#End Region
    End Class

    Friend NotInheritable Class WPBiography
        Inherits PluginBase

        ' Regular expressions:
        Private Shared ReadOnly BLPRegex As New Regex("\{\{\s*(template\s*:\s*|)\s*blp\s*\}\}[\s\n\r]*", _
           RegexOptions.IgnoreCase Or RegexOptions.Compiled Or RegexOptions.ExplicitCapture)
        Private Shared ReadOnly BioCOTWRegex As New Regex("\{\{\s*(template\s*:\s*|)\s*BioCOTWs\s*\}\}[\s\n\r]*", _
           RegexOptions.IgnoreCase Or RegexOptions.Compiled Or RegexOptions.ExplicitCapture)
        Private Shared ReadOnly ActivepolRegex As New Regex( _
           "\{\{\s*(template\s*:\s*|)\s*(Activepolitician|Activepol)\s*\}\}[\s\n\r]*", _
           RegexOptions.IgnoreCase Or RegexOptions.Compiled Or RegexOptions.ExplicitCapture)
        'Private Shared ReadOnly InfoboxNeededRegex As New Regex( _
        '   "\{\{\s*(template\s*:\s*|)\s*(Infoboxrequested|Infoboxneeded)\s*\}\}[\s\n\r]*", _
        '   RegexOptions.IgnoreCase Or RegexOptions.Compiled Or RegexOptions.ExplicitCapture)
        ' No use because the template takes a parameter

        ' Strings:
        Private Const conStringsRoyaltyWorkGroup As String = "royalty-work-group"
        Private Const conStringsBritishRoyaltyWorkGroup As String = "british-royalty"
        Private Const conStringsMusicianWorkGroup As String = "musician-work-group"
        Private Const conStringsArtsWorkGroup As String = "a&e-work-group"
        Private Const conStringsFilmWorkGroup As String = "filmbio-work-group"

        ' Settings:
        Private OurTab As New TabPage(Constants.Biography)
        Private WithEvents OurSettingsControl As New WPBiographySettings
        Private Const conEnabled As String = "BioEnabled"
        Protected Friend Overrides ReadOnly Property PluginShortName() As String
            Get
                Return Constants.Biography
            End Get
        End Property
        Protected Overrides ReadOnly Property PreferredTemplateNameWiki() As String
            Get
                Return "WPBiography"
            End Get
        End Property
        Protected Overrides ReadOnly Property ParameterBreak() As String
            Get
                Return Microsoft.VisualBasic.vbCrLf
            End Get
        End Property
        Protected Overrides Sub ImportanceParameter(ByVal Importance As Importance)
            Template.NewOrReplaceTemplateParm("priority", Importance.ToString, Me.Article, False, False)
        End Sub
        Protected Overrides ReadOnly Property OurTemplateHasAlternateNames() As Boolean
            Get
                Return True
            End Get
        End Property
        Protected Friend Overrides ReadOnly Property GenericSettings() As IGenericSettings
            Get
                Return OurSettingsControl
            End Get
        End Property
        Protected Overrides ReadOnly Property CategoryTalkClassParm() As String
            Get
                Return "Cat"
            End Get
        End Property
        Protected Overrides ReadOnly Property TemplateTalkClassParm() As String
            Get
                Return "Template"
            End Get
        End Property
        Friend Overrides ReadOnly Property HasSharedLogLocation() As Boolean
            Get
                Return True
            End Get
        End Property
        Friend Overrides ReadOnly Property SharedLogLocation() As String
            Get
                Return "Wikipedia:WikiProject Biography/Automation/Logs"
            End Get
        End Property
        Friend Overrides ReadOnly Property HasReqPhotoParam() As Boolean
            Get
                Return True
            End Get
        End Property
        Friend Overrides Sub ReqPhoto()
            AddNewParamWithAYesValue("needs-photo")
        End Sub

        ' Initialisation:
        Friend Sub New(ByVal Manager As PluginManager)
            MyBase.New()
            Const RegexpMiddle As String = "WPBiography|BioWikiProject|Musician|WikiProject Biography|WikiProjectBiography|broy|bio"
            MainRegex = CreateStandardRegex(RegexpMiddle)
            PreferredTemplateNameRegex = New Regex("^[Ww]PBiography$", RegexOptions.Compiled)
            SecondChanceRegex = CreateSecondChanceRegex(RegexpMiddle)
        End Sub
        Protected Friend Overrides Sub Initialise()
            OurMenuItem = New ToolStripMenuItem("Biography Plugin")
            MyBase.InitialiseBase() ' must set menu item object first
            OurTab.UseVisualStyleBackColor = True
            OurTab.Controls.Add(OurSettingsControl)
        End Sub

        ' Article processing:
        Protected Overrides ReadOnly Property InspectUnsetParameters() As Boolean
            Get
                Return OurSettingsControl.ForcePriorityParm
            End Get
        End Property
        Protected Overrides Sub InspectUnsetParameter(ByVal Param As String)
            ' We only get called if InspectUnsetParameters is True
            If String.Equals(Param, "importance", StringComparison.CurrentCultureIgnoreCase) Then
                Template.NewTemplateParm("priority", "")
                Article.DoneReplacement("importance", "priority", True, PluginShortName)
            End If
        End Sub
        Protected Overrides Function SkipIfContains() As Boolean
            ' Skip if contains {{WPBeatles}} or {{KLF}}
            Return (BeatlesKLFSkipRegex.Matches(Article.AlteredArticleText).Count > 0)
        End Function
        Private Function Dead() As Boolean
            ' A little hack to ensure we don't change no to No as our only edit
            If Template.Parameters.ContainsKey("living") Then
                Select Case Template.Parameters("living").Value
                    Case "no", "No"
                        Return True
                End Select
            End If
        End Function
        Protected Overrides Sub ProcessArticleFinish()
            Dim Living As Living = OurSettingsControl.DeadOrAlive, LivingAlreadyAddedToEditSummary As Boolean
            Const conContainsDefaultSortKeyword As String = "Page contains DEFAULTSORT keyword: "

            With Article
                If BLPRegex.Matches(.AlteredArticleText).Count > 0 Then
                    .AlteredArticleText = BLPRegex.Replace(.AlteredArticleText, "")
                    .DoneReplacement("{{[[Template:Blp|Blp]]}}", "living=yes", True, PluginShortName)
                    Living = Plugins.Living.Living
                    LivingAlreadyAddedToEditSummary = True
                    .ArticleHasAMinorChange()
                End If

                If ActivepolRegex.Matches(.AlteredArticleText).Count > 0 Then
                    .AlteredArticleText = ActivepolRegex.Replace(.AlteredArticleText, "")
                    .DoneReplacement("{{[[Template:Activepolitician|Activepolitician]]}}", "activepol=yes", _
                       True, PluginShortName)
                    AddNewParamWithAYesValue("activepol")
                    .ArticleHasAMinorChange()
                End If
            End With

            Select Case Living
                Case Plugins.Living.Living
                    If LivingAlreadyAddedToEditSummary Then AddNewParamWithAYesValue("living") Else _
                       AddAndLogNewParamWithAYesValue("living")
                Case Plugins.Living.Dead
                    If Not Dead() Then
                        Template.NewOrReplaceTemplateParm("living", "no", Article, True, False, False, _
                        "", PluginShortName, True)
                    End If
            End Select

            StubClass()

            With OurSettingsControl
                If .ActivePol Then AddAndLogNewParamWithAYesValue("activepol")
                If .NonBio Then AddAndLogNewParamWithAYesValue("non-bio")
                If .ArtsEntsWG Then AddAndLogNewParamWithAYesValue(conStringsArtsWorkGroup)
                If .MilitaryWG Then AddAndLogNewParamWithAYesValue("military-work-group")
                If .RoyaltyWG Then AddAndLogNewParamWithAYesValue(conStringsRoyaltyWorkGroup)
                If .ScientistWG Then AddAndLogNewParamWithAYesValue("s&a-work-group")
                If .PoliticianWG Then AddAndLogNewParamWithAYesValue("politician-work-group")
                If .SportsWG Then AddAndLogNewParamWithAYesValue("sports-work-group")
                If .BaronetsWG Then AddAndLogNewParamWithAYesValue("baronets-work-group")
                If .PeerageWG Then AddAndLogNewParamWithAYesValue("peerage-work-group")
                Template.RemoveParentWorkgroup(conStringsBritishRoyaltyWorkGroup, conStringsRoyaltyWorkGroup, _
                   .BritishRoyaltyWG, Article, PluginShortName)
                Template.RemoveParentWorkgroup(conStringsMusicianWorkGroup, conStringsArtsWorkGroup, _
                   .MusiciansWG, Article, PluginShortName)
                Template.RemoveParentWorkgroup(conStringsFilmWorkGroup, conStringsArtsWorkGroup, _
                   .FilmWG, Article, PluginShortName)
            End With

            With Article

                If .Namespace = Namespaces.Talk AndAlso (.ProcessIt OrElse OurSettingsControl.ForceListAsParm) Then
                    If WikiFunctions.TalkPages.TalkPageHeaders.ContainsDefaultSortKeywordOrTemplate( _
                    .AlteredArticleText) Then
                        If Template.Parameters.ContainsKey("listas") Then
                            Template.Parameters.Remove("listas")
                            PluginManager.AWBForm.TraceManager.WriteArticleActionLine(conContainsDefaultSortKeyword & _
                               "removing listas parameter", PluginShortName)
                            .ArticleHasAMajorChange()
                        ElseIf Not PluginManager.BotMode Then
                            PluginManager.AWBForm.TraceManager.WriteArticleActionLine1(conContainsDefaultSortKeyword & _
                               "not adding listas parameter", PluginShortName, True)
                        End If
                    ElseIf Not PluginManager.BotMode Then
                        ' Since we're dealing with talk pages, we want a listas= even if it's the same as the
                        ' article title without namespace (otherwise it sorts to namespace)
                        Template.NewOrReplaceTemplateParm("listas", _
                        WikiFunctions.Tools.MakeHumanCatKey(Article.FullArticleTitle), Article, _
                        True, False, True, "", PluginShortName)
                    End If
                End If
            End With

            ReplaceATemplateWithAYesParameter(BioCOTWRegex, "collaboration-candidate", _
                "{{[[Template:BioCOTWs|BioCOTWs]]}}")
        End Sub
        Protected Overrides Function TemplateFound() As Boolean
            With Template
                If .Parameters.ContainsKey("importance") Then
                    If .Parameters.ContainsKey("priority") Then
                        Article.EditSummary += "rm importance param, has priority=, "
                        PluginManager.AWBForm.TraceManager.WriteArticleActionLine( _
                           "importance parameter removed, has priority=", PluginShortName)
                    Else
                        .Parameters.Add("priority", _
                           New Templating.TemplateParametersObject("priority", _
                           .Parameters("importance").Value))
                        Article.DoneReplacement("importance", "priority", True, PluginShortName)
                    End If
                    .Parameters.Remove("importance")
                    Article.ArticleHasAMinorChange()
                End If
            End With
        End Function
        Protected Overrides Sub GotTemplateNotPreferredName(ByVal TemplateName As String)
            If TemplateName.ToLower = "musician" Then AddAndLogNewParamWithAYesValue("musician-work-group")
        End Sub
        Protected Overrides Function WriteTemplateHeader(ByRef PutTemplateAtTop As Boolean) As String
            WriteTemplateHeader = "{{WPBiography" & Microsoft.VisualBasic.vbCrLf

            With Template
                If .Parameters.ContainsKey("living") Then
                    WriteTemplateHeader += "|living=" + .Parameters("living").Value + Microsoft.VisualBasic.vbCrLf

                    If .Parameters("living").Value = "yes" Then
                        PluginManager.AWBForm.TraceManager.WriteArticleActionLine1( _
       "Template contains living=yes, placing at top", PluginShortName, True)
                        PutTemplateAtTop = True ' otherwise, leave as False
                    End If

                    .Parameters.Remove("living")
                End If

                WriteTemplateHeader += WriteOutParameterToHeader("class") & _
                   WriteOutParameterToHeader("priority")
            End With
        End Function

        'User interface:
        Protected Overrides Sub ShowHideOurObjects(ByVal Visible As Boolean)
            PluginManager.ShowHidePluginTab(OurTab, Visible)
        End Sub

        ' XML settings:
        Protected Friend Overrides Sub ReadXML(ByVal Reader As System.Xml.XmlTextReader)
            Dim blnNewVal As Boolean = PluginManager.XMLReadBoolean(Reader, conEnabled, Enabled)
            If Not blnNewVal = Enabled Then Enabled = blnNewVal ' Mustn't set if the same or we get extra tabs
            OurSettingsControl.ReadXML(Reader)
        End Sub
        Protected Friend Overrides Sub Reset()
            OurSettingsControl.Reset()
        End Sub
        Protected Friend Overrides Sub WriteXML(ByVal Writer As System.Xml.XmlTextWriter)
            Writer.WriteAttributeString(conEnabled, Enabled.ToString)
            OurSettingsControl.WriteXML(Writer)
        End Sub

        ' Other overrides:
        Protected Friend Overrides Sub BotModeChanged(ByVal BotMode As Boolean)
            MyBase.BotModeChanged(BotMode)
            OurSettingsControl.BotMode = BotMode
        End Sub
    End Class
End Namespace