Namespace AutoWikiBrowser.Plugins.SDKSoftware.Kingbotk.Plugins
    Friend NotInheritable Class WPAlbumsSettings
        Implements IGenericSettings

        Private Const conAutoStubParm As String = "AlbumAutoStub"
        Private Const conStubClassParm As String = "AlbumStubClass"

        ' Properties:
        Public Property StubClass() As Boolean Implements IGenericSettings.StubClass
            Get
                Return StubClassCheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                StubClassCheckBox.Checked = value
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
        WriteOnly Property StubClassModeAllowed() As Boolean Implements IGenericSettings.StubClassModeAllowed
            Set(ByVal value As Boolean)
                StubClassCheckBox.Enabled = value
            End Set
        End Property
        Friend ReadOnly Property TextInsertContextMenuStripItems() As ToolStripItemCollection _
        Implements IGenericSettings.TextInsertContextMenuStripItems
            Get
                Return TextInsertContextMenuStrip.Items
            End Get
        End Property

#Region "XML interface"
        Public Sub ReadXML(ByVal Reader As System.Xml.XmlTextReader) Implements IGenericSettings.ReadXML
            AutoStub = PluginManager.XMLReadBoolean(Reader, conAutoStubParm, AutoStub)
            StubClass = PluginManager.XMLReadBoolean(Reader, conStubClassParm, StubClass)
        End Sub
        Public Sub WriteXML(ByVal Writer As System.Xml.XmlTextWriter) Implements IGenericSettings.WriteXML
            Writer.WriteAttributeString(conAutoStubParm, AutoStub.ToString)
            Writer.WriteAttributeString(conStubClassParm, StubClass.ToString)
        End Sub
        Public Sub Reset() Implements IGenericSettings.XMLReset
            AutoStub = False
            StubClass = False
        End Sub
#End Region

        ' Event handlers:
        Private Sub InsertTemplateCallMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AlbumToolStripMenuItem.Click
            PluginManager.EditBoxInsert("{{album}}")
        End Sub
        Private Sub LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles LinkLabel1.LinkClicked
            Tools.OpenENArticleInBrowser("Template:Album", False)
        End Sub
        Private Sub AutoStubCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AutoStubCheckBox.CheckedChanged
            If AutoStubCheckBox.Checked Then StubClassCheckBox.Checked = False
        End Sub
        Private Sub StubClassCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles StubClassCheckBox.CheckedChanged
            If StubClassCheckBox.Checked Then AutoStubCheckBox.Checked = False
        End Sub
    End Class

    Friend NotInheritable Class WPAlbums
        Inherits PluginBase

        ' Regular expressions:
        Private ReadOnly InfoboxRegex As New Regex("\{\{ *(template *: *|) *Needsinfobox *\}\} *", _
           RegexOptions.IgnoreCase Or RegexOptions.Compiled Or RegexOptions.ExplicitCapture)

        ' Settings:
        Private OurTab As New TabPage("Albums")
        Private WithEvents OurSettingsControl As New WPAlbumsSettings
        Private Const conEnabled As String = "AlbumEnabled"
        Protected Friend Overrides ReadOnly Property PluginShortName() As String
            Get
                Return "Albums"
            End Get
        End Property
        Protected Overrides ReadOnly Property PreferredTemplateName() As String
            Get
                Return "Album"
            End Get
        End Property
        Protected Overrides ReadOnly Property ParameterBreak() As String
            Get
                Return ""
            End Get
        End Property
        Protected Overrides Sub ImportanceParameter(ByVal Importance As Importance)
            Template.NewOrReplaceTemplateParm("importance", Importance.ToString, Me.Article, False, False)
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
                Return "NA"
            End Get
        End Property
        Protected Overrides ReadOnly Property TemplateTalkClassParm() As String
            Get
                Return "NA"
            End Get
        End Property
        'Protected Overrides ReadOnly Property PreferredTemplateNameRegexString() As String
        '    Get
        '        Return "^[Aa]lbum$"
        '    End Get
        'End Property

        ' Initialisation:
        Friend Sub New(ByVal Manager As PluginManager)
            MyBase.New("Album[s]?", True)
        End Sub
        Protected Friend Overrides Sub Initialise()
            OurMenuItem = New ToolStripMenuItem("Albums Plugin")
            MyBase.InitialiseBase() ' must set menu item object first
            OurTab.UseVisualStyleBackColor = True
            OurTab.Controls.Add(OurSettingsControl)
        End Sub

        ' Article processing:
        Protected Overrides Function SkipIfContains() As Boolean
            ' Skip if contains {{WPBeatles}} or {{KLF}}
            Return (BeatlesKLFSkipRegex.Matches(Article.AlteredArticleText).Count > 0)
        End Function
        Protected Overrides Sub ProcessArticleFinish()
            StubClass()
            ReplaceATemplateWithAYesParameter(InfoboxRegex, "needs-infobox", _
                "{{[[Template:Needsinfobox|Needsinfobox]]}}")
        End Sub
        Protected Overrides Function TemplateFound() As Boolean
            ' Nothing to do here
        End Function
        Protected Overrides Sub GotTemplateNotPreferredName(ByVal TemplateName As String)
            ' Currently only WPBio does anything here (if {{musician}} add to musician-work-group)
        End Sub
        Protected Overrides Function WriteTemplateHeader(ByRef PutTemplateAtTop As Boolean) As String
            WriteTemplateHeader = "{{album" & WriteOutParameterToHeader("class") & _
               WriteOutParameterToHeader("importance")
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

        ' Misc:
        Protected Overrides ReadOnly Property InspectUnsetParameters() As Boolean
            Get
                Return False
            End Get
        End Property
        Protected Overrides Sub InspectUnsetParameter(ByVal Param As String)
        End Sub ' will never be called
        Friend Overrides ReadOnly Property HasSharedLogLocation() As Boolean
            Get
                Return False
            End Get
        End Property
        Friend Overrides ReadOnly Property SharedLogLocation() As String
            Get
                Return ""
            End Get
        End Property
        Friend Overrides ReadOnly Property HasReqPhotoParam() As Boolean
            Get
                Return False
            End Get
        End Property
        Friend Overrides Sub ReqPhoto()
        End Sub
    End Class
End Namespace