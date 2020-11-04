Imports System.IO


Public Class Form1

    Public LoadEvents As Integer

    Public b As Bitmap
    Public g As Graphics

    Public ViewPort As New Rectangle(0, 0, 450, 450)
    Public colorsArray() As Color = New Color() {Color.FromArgb(255, 0, 0), Color.FromArgb(0, 255, 0), Color.FromArgb(0, 0, 255)}
    Dim R As New Random


    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        Dim url2 As String = "https://www.ilgiornale.it/"

        WebBrowser1.ScriptErrorsSuppressed = True
        WebBrowser1.Url = New Uri(url2)
    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        Try
            WebBrowser1.Document.ExecCommand("SelectAll", False, Nothing)
            WebBrowser1.Document.ExecCommand("Copy", False, Nothing)
            WebBrowser1.Document.ExecCommand("Unselect", False, Type.Missing)
        Catch ex As Exception
            MessageBox.Show("You have to click on the setup!")
        End Try
        Me.RichTextBox2.Text = Clipboard.GetText()
        Clipboard.Clear()


        Dim stopWords As New HashSet(Of String)(StringComparer.InvariantCultureIgnoreCase)
        Dim FileParoleFiltrate As String = My.Resources.Resource1.StopList_Ita

        Using s As New StringReader(FileParoleFiltrate)

            Do
                Dim Line As String = s.ReadLine
                If Line Is Nothing Then Exit Do


                Dim j As Integer = Line.IndexOf("|")
                If j > -1 Then
                    Line = Line.Substring(0, j).Trim
                Else
                    Line = Line.Trim
                End If


                If Not String.IsNullOrWhiteSpace(Line) Then
                    stopWords.Add(Line)
                End If

            Loop

        End Using


        Dim Text As New Text.StringBuilder(RichTextBox2.Text)
        Dim wordCount As Integer = 0, index As Integer = 0
        Dim CurrentWord As New Text.StringBuilder
        Dim wordsDistribution As New Dictionary(Of String, Integer)(StringComparer.InvariantCultureIgnoreCase)



        'skip whitespace until first word
        While index < Text.Length AndAlso Char.IsWhiteSpace(Text(index))
            index += 1
        End While




        While index < Text.Length

            While index < Text.Length AndAlso Not Char.IsWhiteSpace(Text(index))
                CurrentWord.Append(Text(index))
                index += 1
            End While

            Dim Parola As String = CurrentWord.ToString.Replace("?", "").Replace("!", "").Replace("|", "").Replace("-", "").Replace("*", "").Replace("(", "")

            CurrentWord.Length = 0

            If Not stopWords.Contains(Parola) Then

                If Not wordsDistribution.ContainsKey(Parola) Then
                    wordsDistribution.Add(Parola, 1)
                Else
                    wordsDistribution(Parola) += 1
                End If
            End If
            wordCount += 1


            ' skip whitespace until next word
            While index < Text.Length AndAlso Char.IsWhiteSpace(Text(index))
                index += 1
            End While

        End While


        Me.InitializeGraphics()
        Me.g.Clear(Color.White)


        printWords(wordsDistribution)


    End Sub






    Private Sub printWords(dict As Dictionary(Of String, Integer))


        Dim ListOfKV As List(Of KeyValuePair(Of String, Integer)) = dict.OrderByDescending(Function(p) p.Value).ToList

        Dim ListaRect As New List(Of Rectangle)

        For Each kvp As KeyValuePair(Of String, Integer) In ListOfKV
            Dim TryRect As Rectangle
            Dim f As New Font("Arial", kvp.Value * 2)
            Dim s As Size = Size.Truncate(g.MeasureString(kvp.Key, f))

            Dim Tries As Integer
            Do
                Dim x = R.Next(ViewPort.Left, ViewPort.Right + 1)
                Dim y = R.Next(ViewPort.Top, ViewPort.Bottom + 1)

                TryRect = New Rectangle(New Point(x, y), s)
                If Not SpotIsAlreadyOccupied(TryRect, ListaRect) Then Exit Do
                Tries += 1

                If Tries > 1000 Then Continue For

            Loop

            g.DrawString(kvp.Key, f, New SolidBrush(GiveRandomColor), New Point(TryRect.X, TryRect.Y))
            ListaRect.Add(TryRect)

            If kvp.Value < 3 Then Exit For

        Next
        Me.PictureBox1.Image = b

    End Sub





    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        LoadEvents += 1
        Me.Label1.Text = "Load" & LoadEvents
    End Sub


    Function SpotIsAlreadyOccupied(TargetSpot As Rectangle, ListaRect As List(Of Rectangle)) As Boolean

        If ListaRect.Count = 0 Then Return False

        For Each ExistingSpot As Rectangle In ListaRect

            If ExistingSpot.IntersectsWith(TargetSpot) Then Return True
            If ExistingSpot.IntersectsWith(TargetSpot) Then Return True
            If TargetSpot.Contains(ExistingSpot) Then Return True
        Next

        Return False


    End Function



    Sub InitializeGraphics()
        Me.b = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
        Me.g = Graphics.FromImage(b)
        Me.g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
    End Sub


    Function GiveRandomColor() As Color

        Dim colorsArray As Array = [Enum].GetValues(GetType(KnownColor))
        Dim k As Integer = colorsArray(R.Next(0, colorsArray.Length))

        Return Color.FromKnownColor(k)

    End Function

End Class
