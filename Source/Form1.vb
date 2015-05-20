Public Class Form1
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Splash_screen.Timer1.Enabled = False
        Splash_screen.initializing()
        Splash_screen.skiprevert()
        Splash_screen.totaltime.Enabled = True
        Splash_screen.TakeRelaxationNowToolStripMenuItem.Text = "Take Relaxation now"
        My.Computer.Audio.Stop()
        Me.Close()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Opacity = 0.9
        Label1.Location = New Point(My.Computer.Screen.WorkingArea.Width / 3, My.Computer.Screen.WorkingArea.Height / 2.5)
        Label4.Location = New Point(My.Computer.Screen.WorkingArea.Width - 155, My.Computer.Screen.WorkingArea.Height - 30)
        ProgressBar1.Location = New Point(0, My.Computer.Screen.WorkingArea.Height + 10)
        ProgressBar1.Size = New Point(My.Computer.Screen.WorkingArea.Width, 10)
        ProgressBar1.Maximum = Splash_screen.mr * 60
        ProgressBar1.Value = Splash_screen.mr * 60
        Button1.Visible = Form2.CheckBox3.CheckState
    End Sub
End Class