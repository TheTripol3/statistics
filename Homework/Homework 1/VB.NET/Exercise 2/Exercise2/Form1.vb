Public Class Form1


    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick


        'Reference 1
        Me.RichTextBox1.AppendText("Example 1" & Environment.NewLine)
        Dim objX As New System.Text.StringBuilder("Tommaso Gastaldi")
        Dim objY As System.Text.StringBuilder
        objY = objX
        objX.Replace("Tommaso", "Andrea")
        Me.RichTextBox1.AppendText(objY.ToString() & Environment.NewLine)
        Me.RichTextBox1.AppendText("--------------------------------------------------------------------------------" & Environment.NewLine)




        'Value 1
        Me.RichTextBox1.AppendText("Example 2" & Environment.NewLine)
        Dim m As Integer = 5
        Dim n As Integer = m
        m = 3
        Me.RichTextBox1.AppendText("m=" & m & Environment.NewLine)
        Me.RichTextBox1.AppendText("n=" & n & Environment.NewLine)
        Me.RichTextBox1.AppendText("--------------------------------------------------------------------------------" & Environment.NewLine)


        'Value 2
        Me.RichTextBox1.AppendText("Example 3" & Environment.NewLine)
        Dim a As Integer = 56
        Dim b As Integer = 28
        Dim u As User = New User("James Brown", 28)
        Dim v As User = New User("Jack White", 32)
        Me.RichTextBox1.AppendText("a:  " & a & Environment.NewLine & "b:  " & b & Environment.NewLine & "u:  " & u.ToString & Environment.NewLine & "v:  " & v.ToString & Environment.NewLine)
        Me.RichTextBox1.AppendText("--------------------------------------------------------------------------------" & Environment.NewLine)


        'Reference 2
        Me.RichTextBox1.AppendText("Example 4" & Environment.NewLine)
        a = b
        u = v

        v.name = "Test"
        Me.RichTextBox1.AppendText("a:  " & a & Environment.NewLine & "b:  " & b & Environment.NewLine & "u:  " & u.ToString & Environment.NewLine & "v:  " & v.ToString & Environment.NewLine)
        Me.RichTextBox1.AppendText("--------------------------------------------------------------------------------" & Environment.NewLine)

    End Sub

    Public Class User

        Public age As Integer
        Public name As String

        Public Sub New(name As String, age As Integer)
            Me.name = name
            Me.age = age
        End Sub

        Public Overrides Function ToString() As String
            Return name
        End Function

    End Class


End Class
