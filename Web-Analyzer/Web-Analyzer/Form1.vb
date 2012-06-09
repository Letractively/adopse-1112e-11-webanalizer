Imports System.Runtime.InteropServices
Imports System.Math

Public Class Form1
    Dim flag As Boolean = False
    Dim strFileName As String = ""
    Dim path As String = "C:\"
    Dim fileNamesList As List(Of String) = New List(Of String)
    Dim list As List(Of String) = New List(Of String)
    Dim monthList As List(Of String) = New List(Of String)
    Dim statusList As List(Of String) = New List(Of String)
    Dim methodList As List(Of String) = New List(Of String)
    Dim daysList As List(Of String) = New List(Of String)

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
        Me.MaximumSize = New Size(1240, 520)
        Me.MinimumSize = New Size(750, 520)
        Disloading()
        If flag <> True Then
            HideChart()
            DoVisible()
            NewDataToolStripMenuItem.Enabled = False
        Else
            NewDataToolStripMenuItem.Enabled = True
            fillData()
            Me.Size = New Point(900, 520)
            AdvLDL.Visible = True
            loadData()
            'vreiskei posoun mines exei to arxeio
            For Each strX1 As String In New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
                For m As Integer = 0 To list.Count - 1 Step 1
                    If list.ElementAt(m).Contains("/" & strX1 & "/") Then
                        If Not monthList.Contains(strX1) Then
                            monthList.Add(strX1)
                        End If
                    End If
                Next m
            Next strX1

            'dhmiougei ta linklabel
            For Each month As String In monthList
                ComboBox1.Items.Add(month)
            Next
            ComboBox1.Text = monthList.ElementAt(0)

            '-------------------------------------------
            updateChart1()
            updateChart2()
            updateChart3()
            updateChart4(ComboBox1.Items(0))
        End If
    End Sub

    Private Sub updateChart1()
        '-----------------------------------------------------------------------------------------------------------
        ' chart 1
        With Me.Chart1
            .Series.Clear()
            .Titles.Clear()
            Dim chart13 As String() = New String() {"Visits", "Sites"}
            Dim title As String = ""
            For Each rt In chart13
                title += rt + " "
            Next
            GroupBox1.Text = title + "by Month"
            Dim tr As Integer = 0
            Dim usersPerMonth As List(Of String) = New List(Of String)
            Dim sitesPerMonth As List(Of String) = New List(Of String)
            For Each m In chart13
                With .Series.Add(m)
                    For Each u In monthList
                        Dim countY As Integer = 0
                        For m3 = 0 To list.Count - 1 Step 1


                            If .Name.Equals("Visits") Then
                                Dim p As Integer = list.ElementAt(m3).IndexOf(" ")
                                Dim user As String = list.ElementAt(m3).Substring(0, p)
                                If list.ElementAt(m3).Contains(u) And Not usersPerMonth.Contains(user) Then
                                    usersPerMonth.Add(user)
                                    countY += 1
                                End If
                            End If


                            If .Name.Equals("Sites") Then
                                Dim site As String = getData2(list.ElementAt(m3), methodList, m3)
                                If list.ElementAt(m3).Contains(u) And Not sitesPerMonth.Contains(site) Then
                                    sitesPerMonth.Add(site)
                                    countY += 1
                                End If
                            End If


                        Next m3
                        tr += 1
                        .Points.AddXY(tr, countY)
                        countY = 0
                    Next u
                    tr = 0
                    .ChartType = DataVisualization.Charting.SeriesChartType.Column
                    .IsVisibleInLegend = False
                    .Label = "#VALY"
                End With

                .Series(m)("DrawingStyle") = "Cylinder"

            Next

            For intMonth As Integer = 1 To monthList.Count
                Dim cl As New DataVisualization.Charting.CustomLabel
                cl.FromPosition = (intMonth - 1) + 0.5
                cl.ToPosition = intMonth + 0.5
                cl.Text += monthList(intMonth - 1).ToString
                .ChartAreas(0).AxisX.CustomLabels.Add(cl)
            Next

        End With

    End Sub


    Private Sub updateChart2()
        '-----------------------------------------------
        'chart 2
        With Me.Chart2
            .Series.Clear()
            .Titles.Clear()

            Dim chart14 As String() = New String() {"Bytes per Month"}
            GroupBox2.Text = chart14(0)
            Dim tr As Integer = 0
            Dim usersPerMonth As List(Of String) = New List(Of String)

            For Each m In chart14
                With .Series.Add(m)
                    For Each u In monthList
                        Dim countY As Integer = 0
                        For m3 = 0 To list.Count - 1 Step 1
                            If list.ElementAt(m3).Contains(u) Then
                                countY += Val(getData2(list.ElementAt(m3), statusList, m3))
                            End If
                        Next m3
                        tr += 1
                        .Points.AddXY(tr, countY)
                        countY = 0
                    Next u
                    tr = 0
                    .IsVisibleInLegend = False
                    .Label = "#VALY"
                End With
                .Series(m)("DrawingStyle") = "Cylinder" '"Wedge"
            Next

            For intMonth As Integer = 1 To monthList.Count
                Dim cl As New DataVisualization.Charting.CustomLabel
                cl.FromPosition = (intMonth - 1) + 0.5
                cl.ToPosition = intMonth + 0.5
                cl.Text += monthList(intMonth - 1).ToString
                .ChartAreas(0).AxisX.CustomLabels.Add(cl)
            Next
        End With
    End Sub

    Private Sub updateChart3()
        '-------------------------------
        'chart 3
        With Me.Chart3
            .Series.Clear()
            .Titles.Clear()
            GroupBox3.Text = "Sites Status"
            With .Series.Add("Series1")
                .ChartType = DataVisualization.Charting.SeriesChartType.Doughnut
                For Each strX As String In New String() {"200", "206", "301", "304", "404", "405"}
                    Dim counterPie As Integer = 0
                    For m = 0 To list.Count - 1 Step 1
                        If list.ElementAt(m).Contains(strX) Then
                            counterPie += 1
                        End If
                    Next
                    .Points.AddXY(strX, counterPie)
                Next

                .IsVisibleInLegend = False
                .Label = "(#VALX)\n#PERCENT"
            End With
            Chart3.ChartAreas(0).Area3DStyle.Enable3D = True
            Chart3.Series(0)("PieLabelStyle") = "Outside"
        End With

    End Sub

    Private Sub updateChart4(ByVal myMonth As String)
        With Me.Chart4
            .Series.Clear()
            .Titles.Clear()
            GroupBox5.Text = "Visits per Day"
            Dim chart14 As String() = New String() {"Visits"}
            Dim tr As Integer = 0
            Dim usersPerDay As List(Of String) = New List(Of String)
            For Each m In chart14
                With .Series.Add(m)
                    For Each u In daysList
                        Dim countY As Integer = 0
                        For m3 = 0 To list.Count - 1 Step 1
                            Dim p As Integer = list.ElementAt(m3).IndexOf(" ")
                            Dim user As String = list.ElementAt(m3).Substring(0, p)
                            If list.ElementAt(m3).Contains("[" & u & "/") And list.ElementAt(m3).Contains(myMonth) And Not usersPerDay.Contains(user) Then
                                usersPerDay.Add(user)
                                countY += 1
                            End If
                        Next m3

                        tr += 1
                        .Points.AddXY(tr, countY)
                        countY = 0
                    Next u
                    tr = 0
                    .IsVisibleInLegend = False
                    .Label = "#VALX"
                End With
                .Series(m)("DrawingStyle") = "Wedge"
            Next
            usersPerDay.Clear()
            For intMonth As Integer = 1 To daysList.Count
                Dim cl As New DataVisualization.Charting.CustomLabel
                cl.FromPosition = (intMonth - 1) + 0.5
                cl.ToPosition = intMonth + 0.5
                cl.Text += daysList(intMonth - 1).ToString
                .ChartAreas(0).AxisX.CustomLabels.Add(cl)
            Next
        End With
    End Sub

    Private Sub OpenfileTextBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenfileTextBox.Click
        loading()
        UnVisible()
        openFileDialog(sender, e)
    End Sub

    Private Sub fillData()
        statusList.Add(" 200 ")
        statusList.Add(" 201 ")
        statusList.Add(" 202 ")
        statusList.Add(" 301 ")
        statusList.Add(" 302 ")
        statusList.Add(" 304 ")
        statusList.Add(" 400 ")
        statusList.Add(" 401 ")
        statusList.Add(" 403 ")
        statusList.Add(" 404 ")
        statusList.Add(" 405 ")
        statusList.Add(" 500 ")
        statusList.Add(" 501 ")
        statusList.Add(" 503 ")
        statusList.Add(" 504 ")

        methodList.Add("GET ")
        methodList.Add("POST ")
        methodList.Add("OPTIONS ")
        methodList.Add("POST ")
        methodList.Add("PROPFIND ")
        For Day As Integer = 1 To 31
            If Day < 10 Then
                daysList.Add("0" & CStr(Day))
            Else
                daysList.Add(CStr(Day))
            End If
        Next
        Return
    End Sub

    Private Function getData2(ByVal str As String, ByVal lista As List(Of String), ByVal i As Integer) As String
        Dim po As Integer = 0
        For Each u In lista
            If list.ElementAt(i).Contains(u) Then
                po = str.IndexOf(u)
                Dim po2 As Integer = po + 4
                Dim po3 As Integer = str.IndexOf(" "c, po2 + 1) - po2
                If po3 > 0 Then
                    Return list.ElementAt(i).Substring(po2, po3)
                End If
            End If
        Next
        Return ""
    End Function

    'get data from log file into a list
    Private Sub loadData()
        Dim index As Integer = 0
        Dim lineoftext As String = ""
        Dim myLines(0) As String
        Dim countload As Integer = 0
        For Each s As String In fileNamesList
            Using sr As New System.IO.StreamReader(s)
                While Not sr.EndOfStream
                    lineoftext = sr.ReadLine
                    If lineoftext <> "" Then
                        ReDim Preserve myLines(index)
                        myLines(index) = lineoftext
                        index += 1
                    End If
                End While
            End Using
        Next s
        'Array.Sort(myLines)
        For Each s As String In myLines
            list.Add(s)
        Next
        showChart()
        Disloading()
    End Sub

    Private Sub openFileDialog(ByVal sender As Object, ByVal e As System.EventArgs)
        resetAll()
        Dim TotalFile As String = ""
        OpenFD.Multiselect = True
        OpenFD.Title = "Open File"
        OpenFD.Filter = "All Files (*.*)| *.*"
        OpenFD.FileName = "Open File(s)"
        If OpenFD.ShowDialog = DialogResult.OK Then
            For Each s As String In OpenFD.FileNames
                fileNamesList.Add(s)
            Next
            flag = True
        End If
        Form1_Load(sender, E)
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        firstLBL.Left -= 10
        If firstLBL.Left <= -Width + 350 Then
            firstLBL.Left = Width + 10
        End If
    End Sub

    Sub resetAll()
        flag = False
        fileNamesList.Clear()
        list.Clear()
        monthList.Clear()
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

    Private Sub HideChart()
        GroupBox1.Hide()
        GroupBox2.Hide()
        GroupBox3.Hide()
    End Sub

    Private Sub showChart()
        GroupBox1.Show()
        GroupBox2.Show()
        GroupBox3.Show()
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

    Private Sub NewDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewDataToolStripMenuItem.Click
        UnVisible()
        loading()
        openFileDialog(sender, e)
    End Sub

    Private Sub AdvLDL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdvLDL.Click
        AdvLDL.Hide()
        closeLBL.Show()
        Timer1.Enabled = True
        If Timer2.Enabled = True Then
            Timer2.Enabled = False
        End If
    End Sub

    Private Sub ClsLBL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles closeLBL.Click
        AdvLDL.Show()
        Timer2.Enabled = True
        If Timer1.Enabled = True Then
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub AdvLDL_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdvLDL.MouseHover
        AdvLDL.Font = New Font("Verdana", 16, FontStyle.Bold)
        AdvLDL.Location = New Point(779, 460)
    End Sub

    Private Sub AdvLDL_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdvLDL.MouseLeave
        AdvLDL.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
        AdvLDL.Location = My.Settings.adcLBLLocation
    End Sub


    Private Sub closeLBL_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles closeLBL.MouseHover
        closeLBL.Font = New Font("Verdana", 16, FontStyle.Bold)
        closeLBL.Location = New Point(1155, 18)
    End Sub

    Private Sub closeLBL_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles closeLBL.MouseLeave
        closeLBL.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
        closeLBL.Location = My.Settings.closeLBLLocation
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Width += 10
        If Me.Width = 1240 Then
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.Width -= 10
        If Me.Width = 900 Then
            Timer2.Enabled = False
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If My.Settings.Voice Then
            Dim SAPI
            SAPI = CreateObject("sapi.spvoice")
            SAPI.Speak("Bye, see you next time.")
        End If
        My.Settings.Voice = True
    End Sub

    Private Sub EnableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableToolStripMenuItem.Click
        My.Settings.Voice = True
    End Sub

    Private Sub DisableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem.Click
        My.Settings.Voice = False
    End Sub
End Class
