Public Class Splash_screen
    Dim n As Integer = 0

    Public path As String = (My.Computer.FileSystem.SpecialDirectories.MyDocuments) & "\p4r.xml" ' (Mid(Environment.SystemDirectory, 1, 3) & "p4r.xml")

    Dim hh, mm, ss As Integer   'Timer to check the status and refreshed every h:m:s
    Dim h, m, s As Integer      'Relax every h:m:s (user defined)
    Public mr As Integer           'Relaxation time (relax for mr min)
    Dim noti As Boolean         'Show notification or not
    Dim notitim As Integer      'Show notification before notitim seconds
    Dim flag As Boolean = True    'To overcome the initail display and for stop / start relaxation time
    Dim nrflag As Boolean = True       'Flag for no relaxation
    Dim soundnoti As Boolean        'Play sound or not
    Dim soundfil As String          'Sound file path to play

    'System running time calculation
    Dim nTicks As Double
    Dim nDays As Integer
    Dim nHours As Integer
    Dim nMin As Integer
    Dim nSec As Integer

    Private Sub TimerStart_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerStart.Tick
        n += 1
        Label1.Text += "."
        If n = 10 Then
            
            Me.Hide()
            TimerStart.Enabled = False
            NotifyIcon1.BalloonTipText = "Software is loaded and minimized to tray" & vbNewLine & "Right click for the settings"
            NotifyIcon1.ShowBalloonTip(2)
        End If
    End Sub

    Private Sub Splash_screen_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Timer1.Enabled = False
        totaltime.Enabled = False
        NotifyIcon1.Dispose()
    End Sub

    Private Sub Splash_screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Form2.Show()
        Form2.Hide()
        NotifyIcon1.BalloonTipTitle = "Pause 4 Relax"
        initializing()
    End Sub

    Public Sub initializing()
        h = Form2.htb.Value
        m = Form2.mtb.Value
        s = Form2.stb.Value
        mr = Form2.NumericUpDown1.Value
        noti = Form2.CheckBox1.CheckState
        notitim = Form2.NumericUpDown2.Value
        soundnoti = Form2.CheckBox2.CheckState
        soundfil = Form2.TextBox2.Text
        ss = s
        mm = m
        hh = h
    End Sub
    Private Sub SkipOnceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkipOnceToolStripMenuItem.Click
        flag = False
        SkipOnceToolStripMenuItem.Text = "Skipped Once"
        SkipOnceToolStripMenuItem.Enabled = False
        After5MinToolStripMenuItem.Enabled = False
        After10MinToolStripMenuItem.Enabled = False
        After15MinToolStripMenuItem.Enabled = False
        NotifyIcon1.BalloonTipText = "Skipped relaxation Once"
        NotifyIcon1.ShowBalloonTip(5)
    End Sub

    Private Sub After5MinToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles After5MinToolStripMenuItem.Click
        mm += 5
        After5MinToolStripMenuItem.Checked = True
        SkipOnceToolStripMenuItem.Enabled = False
        After5MinToolStripMenuItem.Enabled = False
        After10MinToolStripMenuItem.Enabled = False
        After15MinToolStripMenuItem.Enabled = False
        NotifyIcon1.BalloonTipText = "5 mins added to Relaxation time"
        NotifyIcon1.ShowBalloonTip(5)
    End Sub

    Private Sub After10MinToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles After10MinToolStripMenuItem.Click
        mm += 10
        After10MinToolStripMenuItem.Checked = True
        SkipOnceToolStripMenuItem.Enabled = False
        After5MinToolStripMenuItem.Enabled = False
        After10MinToolStripMenuItem.Enabled = False
        After15MinToolStripMenuItem.Enabled = False
        NotifyIcon1.BalloonTipText = "10 mins added to Relaxation time"
        NotifyIcon1.ShowBalloonTip(5)
    End Sub

    Private Sub After15MinToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles After15MinToolStripMenuItem.Click
        mm += 15
        After15MinToolStripMenuItem.Checked = True
        SkipOnceToolStripMenuItem.Enabled = False
        After5MinToolStripMenuItem.Enabled = False
        After10MinToolStripMenuItem.Enabled = False
        After15MinToolStripMenuItem.Enabled = False
        NotifyIcon1.BalloonTipText = "15 mins added to Relaxation time"
        NotifyIcon1.ShowBalloonTip(5)
    End Sub

    Public Sub skiprevert()
        After5MinToolStripMenuItem.Checked = False
        After10MinToolStripMenuItem.Checked = False
        After15MinToolStripMenuItem.Checked = False
        SkipOnceToolStripMenuItem.Enabled = True
        After5MinToolStripMenuItem.Enabled = True
        After10MinToolStripMenuItem.Enabled = True
        After15MinToolStripMenuItem.Enabled = True
    End Sub

    Private Sub NoRelaxationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoRelaxationToolStripMenuItem.Click
        If nrflag Then
            totaltime.Enabled = False
            Timer1.Enabled = False
            Timer2.Enabled = True
            Form2.Label1.Text = "Disabled"
            NxtRlx.Text = "Next Relaxation : Disabled"
            NxtRlx.Enabled = False
            nrflag = False
            NoRelaxationToolStripMenuItem.Text = "Enable Relaxation"
            NotifyIcon1.BalloonTipText = "Relaxations disabled!"
            NotifyIcon1.ShowBalloonTip(1)
            TakeRelaxationNowToolStripMenuItem.Enabled = False
        Else
            Timer2.Enabled = False
            totaltime.Enabled = True
            NxtRlx.Enabled = True
            NoRelaxationToolStripMenuItem.Text = "Disable Relaxation"
            NotifyIcon1.BalloonTipText = "Relaxations enabled!"
            NotifyIcon1.ShowBalloonTip(1)
            nrflag = True
            TakeRelaxationNowToolStripMenuItem.Enabled = True
        End If

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        TotRuntim.Text = "System running for :- " & Format(nHours, "00") & ":" & Format(nMin, "00") & ":" & Format(nSec, "00")
        systimcalc()
        Form2.Label4.Text = Format(nHours, "00") & ":" & Format(nMin, "00") & ":" & Format(nSec, "00")
    End Sub

    Private Sub systimcalc()
        'System running time calculation
        nTicks = Environment.TickCount
        nTicks = nTicks / 1000
        nDays = Int(nTicks / (3600 * 24))
        nTicks = nTicks - (Int(nTicks / (3600 * 24)) * (3600 * 24))
        nHours = Int(nTicks / 3600)
        nTicks = nTicks - (Int(nTicks / 3600) * 3600)
        nMin = Int(nTicks / 60)
        nTicks = nTicks - (Int(nTicks / 60) * 60)
        nSec = nTicks
        Form1.Label2.Text = "System running for :- " & Format(nHours, "00") & ":" & Format(nMin, "00") & ":" & Format(nSec, "00")
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        Form1.Show()
        If ss > 0 Then
            ss -= 1
        Else
            ss = 59
            If mm > 0 Then
                mm -= 1
            Else
                mm = 0
            End If
        End If
        If mm = 0 And ss = 0 Then
            Form1.Close()
            TakeRelaxationNowToolStripMenuItem.Text = "Take Relaxation now"
            My.Computer.Audio.Stop()
            Timer1.Enabled = False
            initializing()
            skiprevert()
            totaltime.Enabled = True
        End If
        systimcalc()
        Form1.Label3.Text = "Relaxation time left :- " & Format(mm, "00") & ":" & Format(ss, "00")
        Form2.Label4.Text = Format(nHours, "00") & ":" & Format(nMin, "00") & ":" & Format(nSec, "00")
        Form2.Label1.Text = Format(hh, "00") & ":" & Format(mm, "00") & ":" & Format(ss, "00")
        Try
            Form1.ProgressBar1.Value -= 1
        Catch ex As Exception

        End Try

    End Sub

    Private Sub totaltime_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles totaltime.Tick
        'Time calculation
        If ss > 0 Then
            ss -= 1
        Else
            ss = 59
            If mm > 0 Then
                mm -= 1
            Else
                mm = 59
                If hh > 0 Then
                    hh -= 1
                Else
                    hh = 12
                End If
            End If
        End If

        If noti And hh = 0 And mm = 0 And ss < notitim Then
            NotifyIcon1.BalloonTipText = "Relaxation in " & ss & " seconds"
            NotifyIcon1.ShowBalloonTip(1)
        End If

        If hh = 0 And mm = 0 And ss = 0 And flag Then
            totaltime.Enabled = False
            If soundnoti Then
                My.Computer.Audio.Play(soundfil)
            End If
            ss = 59
            mm = mr - 1
            Form1.Show()
            Timer1.Enabled = True
        ElseIf hh = 0 And mm = 0 And ss = 0 Then    'If skip once is clicked
            totaltime.Enabled = False
            initializing()
            skiprevert()
            flag = True
            SkipOnceToolStripMenuItem.Text = "Skip Once"
            totaltime.Enabled = True
        End If

        'Label2.Text = "Next relaxation time :- " & Format(hh, "00") & ":" & Format(mm, "00") & ":" & Format(ss, "00")
        systimcalc()
        TotRuntim.Text = "System running for :- " & Format(nHours, "00") & ":" & Format(nMin, "00") & ":" & Format(nSec, "00")
        NxtRlx.Text = "Next relaxation :- " & Format(hh, "00") & ":" & Format(mm, "00") & ":" & Format(ss, "00")
        Form2.Label4.Text = Format(nHours, "00") & ":" & Format(nMin, "00") & ":" & Format(nSec, "00")
        Form2.Label1.Text = Format(hh, "00") & ":" & Format(mm, "00") & ":" & Format(ss, "00")
    End Sub

    Private Sub RelaxationModificationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RelaxationModificationToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub TimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub TakeRelaxationNowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TakeRelaxationNowToolStripMenuItem.Click
        TakeRelaxationNowToolStripMenuItem.Text = "Relaxation in progress!"
        NotifyIcon1.BalloonTipText = "Relaxation taken now!"
        NotifyIcon1.ShowBalloonTip(5)
        totaltime.Enabled = False
        hh = 0
        ss = 59
        mm = mr - 1
        Timer1.Enabled = True
    End Sub
End Class