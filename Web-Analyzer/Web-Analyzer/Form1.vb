Imports System.Runtime.InteropServices
Imports System.Math

Public Class Form1
    Dim flag As Boolean = False
    Dim strFileName As String = "" '''''
    Dim path As String = "C:\"
    Dim fileNamesList As List(Of String) = New List(Of String)

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MARGINS
        Public Left As Integer
        Public Right As Integer
        Public Top As Integer
        Public Bottom As Integer
    End Structure
    Declare Auto Function DwmIsCompositionEnabled Lib "dwmapi.dll" Alias "DwmIsCompositionEnabled" (ByRef pfEnable As Boolean) As Integer
    Declare Auto Function DwmExtendFrameIntoClientArea Lib "dwmapi.dll" Alias "DwmExtendFrameIntoClientArea" (ByVal hwnd As IntPtr, ByRef pMargin As MARGINS) As Integer
    Dim pMargins As New MARGINS With {.Top = -1, .Right = -1, .Left = -1, .Bottom = -1}

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim en As Boolean = False
        DwmIsCompositionEnabled(en)
        If en Then
            DwmExtendFrameIntoClientArea(Me.Handle, pMargins)
        End If
        Me.TransparencyKey = Color.FromKnownColor(KnownColor.AliceBlue)
        Me.BackColor = Me.TransparencyKey
        Disloading()
        If flag <> True Then
            DoVisible()
        Else
        End If
    End Sub

    Private Sub OpenfileTextBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenfileTextBox.Click
        loading()
        UnVisible()
        openFileDialog(sender, e) ''''
    End Sub

    Private Sub openFileDialog(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TotalFile As String = ""
        OpenFD.Multiselect = True
        OpenFD.Title = "Open File"
        OpenFD.Filter = "All Files (*.*)| *.*"
        OpenFD.FileName = "Open File(s)"
        If OpenFD.ShowDialog = DialogResult.OK Then
            For Each s As String In OpenFD.FileNames
                fileNamesList.Add(s)
            Next
        End If
        Form1_Load(sender, e)
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        firstLBL.Left -= 10
        If firstLBL.Left <= -Width + 350 Then
            firstLBL.Left = Width + 10
        End If
        'khjkj
    End Sub

    Private Sub UnVisible()
        OpenfileTextBox.Hide()
        firstLBL.Hide()
        fileName.Hide()
    End Sub


    Private Sub DoVisible()
        OpenfileTextBox.Show()
        firstLBL.Show()
        fileName.Show()
    End Sub

    Private Sub loading()
        Label1.Show()
        Label2.Show()
        Timer3.Enabled = True
    End Sub

    Private Sub Disloading()
        Label1.Hide()
        Label2.Hide()
        Timer3.Enabled = False
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        If Label2.Text = "." Then
            Label2.Text = ".."
        ElseIf Label2.Text = ".." Then
            Label2.Text = "..."
        ElseIf Label2.Text = "..." Then
            Label2.Text = "."
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        WebAnalyzerAboutBox.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class
