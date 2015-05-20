Imports System
Imports System.Xml
Public Class Form2

    Dim checkstat As Boolean
    Public path As String = (My.Computer.FileSystem.SpecialDirectories.MyDocuments) & "\p4r.xml" '(Mid(Environment.SystemDirectory, 1, 3) & "p4r.xml")
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim settings As New XmlWriterSettings()
            settings.Indent = True
        Dim XmlWrt As XmlWriter = XmlWriter.Create(path, settings)
            With XmlWrt
                .WriteStartDocument()
                .WriteStartElement("Data")

                .WriteStartElement("Settings")

                .WriteStartElement("htbval")
                .WriteString(htb.Value)
                .WriteEndElement()

                .WriteStartElement("mtbval")
                .WriteString(mtb.Value)
                .WriteEndElement()

                .WriteStartElement("stbval")
                .WriteString(stb.Value)
                .WriteEndElement()

                .WriteStartElement("relax4val")
                .WriteString(NumericUpDown1.Value)
                .WriteEndElement()

                .WriteStartElement("cb1state")
                .WriteString(CheckBox1.CheckState)
                .WriteEndElement()

                .WriteStartElement("cb2state")
                .WriteString(CheckBox2.CheckState)
            .WriteEndElement()

            .WriteStartElement("cb3state")
            .WriteString(CheckBox3.CheckState)
            .WriteEndElement()

                .WriteStartElement("notib4val")
                .WriteString(NumericUpDown2.Value)
                .WriteEndElement()

                .WriteEndElement()

                .WriteStartElement("Notifications")

                .WriteStartElement("filepath")
                .WriteString(TextBox2.Text)
                .WriteEndElement()

            .WriteEndElement()

            .WriteStartElement("startup")

            .WriteStartElement("loadatstart")
            .WriteString(Loadstart.CheckState)
            .WriteEndElement()

            .WriteEndElement()
                .WriteEndDocument()
                .Close()
            End With
        MessageBox.Show("Settings saved!", "Pause 4 Relax", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If MsgBox("Are you sure that you want to set default settings?", vbYesNo, "Pause 4 Relax") = vbYes Then
            htb.Value = 0
            mtb.Value = 30
            stb.Value = 0
            NumericUpDown1.Value = 5
            CheckBox1.Checked = True
            NumericUpDown2.Value = 30
            CheckBox2.Checked = False
            TextBox2.Clear()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            NumericUpDown2.Enabled = True
        Else
            NumericUpDown2.Enabled = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            TextBox2.Enabled = True
            Button5.Enabled = True
            playstop.Enabled = True
        Else
            TextBox2.Enabled = False
            Button5.Enabled = False
            playstop.Enabled = False
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        OpenFileDialog1.ShowDialog()
        TextBox2.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub playstop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles playstop.CheckedChanged
        If playstop.Checked Then
            playstop.Text = "█"
            On Error GoTo errmsg
            My.Computer.Audio.Play(TextBox2.Text)
            Exit Sub
errmsg:
            MsgBox(Err.Description)
            playstop.Checked = False
        Else
            My.Computer.Audio.Stop()
            playstop.Text = "►"
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text <> "" Then
            playstop.Enabled = True
        Else
            playstop.Enabled = False
        End If
    End Sub

    Private Sub Form2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.VisibleChanged
        If IO.File.Exists(Splash_screen.path) = False Then
revert:     'Revert back the settings when the file has been corrupted
            Dim settings As New XmlWriterSettings()
            settings.Indent = True
            Dim XmlWrt As XmlWriter = XmlWriter.Create(Splash_screen.path, settings)
            With XmlWrt
                .WriteStartDocument()
                .WriteStartElement("data")
                .WriteStartElement("Settings")

                .WriteStartElement("htbval")
                .WriteString(0)
                .WriteEndElement()

                .WriteStartElement("mtbval")
                .WriteString(30)
                .WriteEndElement()

                .WriteStartElement("stbval")
                .WriteString(0)
                .WriteEndElement()

                .WriteStartElement("relax4val")
                .WriteString(5)
                .WriteEndElement()

                .WriteStartElement("cb1state")
                .WriteString(1)
                .WriteEndElement()

                .WriteStartElement("cb2state")
                .WriteString(0)
                .WriteEndElement()

                .WriteStartElement("cb3state")
                .WriteString(CheckBox3.CheckState)
                .WriteEndElement()

                .WriteStartElement("notib4val")
                .WriteString(5)
                .WriteEndElement()

                .WriteEndElement()

                .WriteStartElement("Notifications")

                .WriteStartElement("filepath")
                .WriteString("")
                .WriteEndElement()

                .WriteEndElement()

                .WriteStartElement("startup")

                .WriteStartElement("loadatstart")
                .WriteString(Loadstart.CheckState)
                .WriteEndElement()

                .WriteEndElement()

                .WriteEndDocument()
                .Close()
            End With
            htb.Value = 0
            mtb.Value = 30
            stb.Value = 0
            NumericUpDown1.Value = 5
            CheckBox1.Checked = True
            NumericUpDown2.Value = 30
            CheckBox2.Checked = False
            Splash_screen.Hide()
        Else
            Dim document As XmlReader = New XmlTextReader(Splash_screen.path)
            Try
                While (document.Read())
                    Dim type = document.NodeType
                    If (type = XmlNodeType.Element) Then
                        If (document.Name = "htbval") Then
                            htb.Value = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "mtbval") Then
                            mtb.Value = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "stbval") Then
                            stb.Value = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "relax4val") Then
                            NumericUpDown1.Value = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "cb1state") Then
                            CheckBox1.Checked = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "cb2state") Then
                            CheckBox2.Checked = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "cb3state") Then
                            CheckBox3.Checked = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "notib4val") Then
                            NumericUpDown2.Value = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "filepath") Then
                            TextBox2.Text = document.ReadInnerXml.ToString()
                        End If
                        If (document.Name = "loadatstart") Then
                            Loadstart.Checked = document.ReadInnerXml.ToString()
                            checkstat = Loadstart.CheckState
                        End If
                    End If
                End While
                document.Close()
            Catch ex As Exception
                MessageBox.Show("Error occurred to the saved database or file is corrupted!" & vbNewLine & "Reverting back the settings to original",
                                "Pause 4 Relax : Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                document.Close()
                GoTo revert
            End Try

        End If
        'Label4.Text = Format(htb.Value, "00") & ":" & Format(mtb.Value, "00") & ":" & Format(stb.Value, "00")
    End Sub

    Private Sub loadstart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Loadstart.Click
        Try
            If Loadstart.Checked Then
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
            Else
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
            End If

        Catch exc As Exception
            MessageBox.Show("This feature is available for only administrators." & vbNewLine & vbNewLine & "To enable this feature, run the file as Administrator " & vbNewLine & " or copy the shortcut file of Pause 4 Relax to" & vbNewLine & " Start -> All Programs -> Startup",
                             "Pause 4 Relax : Feature not available", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Loadstart.Checked = checkstat
            Loadstart.Enabled = False
        End Try
    End Sub
End Class