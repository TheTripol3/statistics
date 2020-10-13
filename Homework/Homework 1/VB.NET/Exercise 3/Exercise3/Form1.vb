Public Class Form1
    Private Sub Panel1_DragEnter(sender As Object, e As DragEventArgs) Handles Panel1.DragEnter

        'Insert effects to the cursor when moving files around the panel
        e.Effect = DragDropEffects.All

    End Sub

    Private Sub Panel1_DragDrop(sender As Object, e As DragEventArgs) Handles Panel1.DragDrop

        'An array of strings is initialized with a specific format (CType), i.e. an array of strings, of the data extracted from the FileDrop operation
        Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop, False), String())

        'For each files, we extrapolate the data and store it in the file variable and
        For Each file As String In files
            Me.Label4.Text = file
        Next
    End Sub
End Class
