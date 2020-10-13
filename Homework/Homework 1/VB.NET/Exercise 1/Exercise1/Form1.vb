Public Class Form1

    'Click on the button to insert the text
    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick

        'Initialize a variable of type String
        Dim text As String = "Hi Tommaso Gastaldi!"

        'Set the text of the richtextbox element with the contents of the variable text
        Me.RichTextBox1.Text = text
    End Sub

    'Click on the button to clean the text
    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick

        'The element is completely cleaned and is not set with " " (null)
        Me.RichTextBox1.Clear()
    End Sub

    'Mouse access in the richtextbox element
    Private Sub RichTextBox1_MouseEnter(sender As Object, e As EventArgs) Handles RichTextBox1.MouseEnter

        'Changing the background color from default to orange
        Me.RichTextBox1.BackColor = Color.Orange

        'Changing the text color from default to white
        Me.RichTextBox1.ForeColor = Color.White
    End Sub

    'Mouse leave from the richtextbox element
    Private Sub RichTextBox1_MouseLeave(sender As Object, e As EventArgs) Handles RichTextBox1.MouseLeave

        'Changing the background color from orange to default
        Me.RichTextBox1.BackColor = DefaultBackColor

        'Changing the text color from white to default
        Me.RichTextBox1.ForeColor = DefaultForeColor
    End Sub
End Class
