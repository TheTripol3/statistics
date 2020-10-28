'Form1 File
Public Class Form1
    Private Sub Panel1_DragDrop(sender As Object, e As DragEventArgs) Handles Panel1.DragDrop
        Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop, False), String())

        For Each file As String In files

            Dim fi As New IO.FileInfo(file)
            Dim extn As String = fi.Extension

            'If there is no file or it is not txt or csv
            If String.IsNullOrWhiteSpace(file) Or Not checkFilter(extn) Then
                MessageBox.Show("You have to choose a csv or txt file", "Error", MessageBoxButtons.OK)
            Else
                Me.PictureBox1.Visible = True
                Me.Label4.Text = file
                Me.Button2.Enabled = True
            End If
        Next
    End Sub

    Private Sub Panel1_DragEnter(sender As Object, e As DragEventArgs) Handles Panel1.DragEnter
        e.Effect = DragDropEffects.All
    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        Dim opD As New OpenFileDialog
        opD.Filter = "csv files (*.csv)|*.txt |txt files (*.txt)|*.txt"
        opD.Title = "Choose a CSV file . . ."
        opD.ShowDialog()
        If (opD.FileName IsNot Nothing) Then
            If String.IsNullOrWhiteSpace(opD.FileName) Then
                Exit Sub
            End If
            Me.PictureBox1.Visible = True
            Me.Label4.Text = opD.FileName
            Me.Button2.Enabled = True
        End If
    End Sub


    'Check file extension
    Function checkFilter(ext As String)

        Dim flag As Boolean = False

        If ext = ".txt" Or ext = ".csv" Then
            flag = True
        End If

        Return flag
    End Function

    'Show Form2 (Initial Setup)
    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        Me.Hide()
        Form2.Show()
    End Sub



End Class
