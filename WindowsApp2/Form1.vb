Public Class Form1
    Public IsFreightTrain As Boolean = False
    Public ToSandTownEastFreight As New List(Of String) From {"18cf79c3-d581-42ec-b11e-29a87ba01320"}

    Public StopsOnRoute As New List(Of String)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IO.File.Exists(Application.StartupPath & "\WorldPath.txt") Then
            TextBox3.Text = IO.File.ReadAllText(Application.StartupPath & "\WorldPath.txt")
        End If

        Dim I As Int64 = 0

        Dim dir1 = Application.StartupPath & "\Routes"
        Dim files() As System.IO.FileInfo
        Dim dirinfo As New System.IO.DirectoryInfo(dir1)
        files = dirinfo.GetFiles("*", IO.SearchOption.TopDirectoryOnly)
        For Each file In files
            ListBox1.Items.Add(file.Name.Replace(".route", ""))
            'ListBox1.SetSelected(I, )
            'I = I + 1
        Next



    End Sub

    Private Sub SetSwitch(station As String, turnOn As Boolean)



        'Dim computerID As String = station
        MsgBox(station)

        Dim homePath As String = TextBox3.Text & "\opencomputers\" &
            station & "\home\"
        'Debug.WriteLine(station)

        Dim filePath As String = homePath & "On.txt"
        'Dim filePath As String = IO.Path.Combine(homePath, "On.txt")
        Try
            If turnOn Then
                MsgBox(filePath)
                IO.File.WriteAllText(filePath, "ON")
            Else
                If IO.File.Exists(filePath) Then
                    IO.File.Delete(filePath)
                End If
            End If
        Catch ex As Exception
            'SetSwitch(station, turnOn)
        End Try


    End Sub

    Public Sub ResetAllSwitches()
        Dim dir1 = TextBox3.Text & "\opencomputers"
        Dim files() As System.IO.DirectoryInfo
        Dim dirinfo As New System.IO.DirectoryInfo(dir1)
        files = dirinfo.GetDirectories("*", IO.SearchOption.TopDirectoryOnly)
        For Each file In files
            If IO.File.Exists(file.FullName & "\home\On.txt") Then
                IO.File.Delete(file.FullName & "\home\On.txt")
            End If
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ResetAllSwitches()

        For Each s As String In ToSandTownEastFreight
            SetSwitch(s, True)
        Next
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Try
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\WorldPath.txt", TextBox3.Text, False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ResetAllSwitches()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ResetAllSwitches()

        Dim Text As String() = IO.File.ReadAllText(Application.StartupPath & "\Routes\" & ListBox1.SelectedItems(ListBox1.SelectedIndex) & ".route").Split(";"c)

        For Each s As String In Text
            If s.Contains(vbCrLf) Then
                s.Replace(vbCrLf, "")
            End If
            Debug.WriteLine(s)
            SetSwitch(s, True)
        Next

    End Sub
End Class
