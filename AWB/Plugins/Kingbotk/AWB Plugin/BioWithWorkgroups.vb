﻿Friend Class BioWithWorkgroups
    Inherits GenericWithWorkgroups

    Public Sub New(ByVal template As String, ByVal prefix As String, ByVal autoStubEnabled As Boolean, _
                   ByVal inspectUnsetEnabled As Boolean, ByVal ParamArray params() As TemplateParameters)
        MyBase.New(template, prefix, autoStubEnabled, inspectUnsetEnabled, params)

        'AddHandler ListView1.ItemChecked, AddressOf ListView1_ItemChecked
    End Sub

    Private Sub ListView1_ItemChecked(ByVal sender As Object, ByVal e As ItemCheckedEventArgs) _
        Handles ListView1.ItemChecked

        If e.Item.Text = "Living" Then
            Dim lvi As ListViewItem = ListView1.FindItemWithText("Not Living")

            If lvi IsNot Nothing AndAlso lvi.Checked Then
                lvi.Checked = False
            End If
        ElseIf e.Item.Text = "Not Living" Then
            Dim lvi As ListViewItem = ListView1.FindItemWithText("Living")

            If lvi IsNot Nothing AndAlso lvi.Checked Then
                lvi.Checked = False
            End If
        End If
    End Sub
End Class