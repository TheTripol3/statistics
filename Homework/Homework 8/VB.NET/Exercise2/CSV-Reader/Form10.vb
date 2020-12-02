Public Class Form10
    Public b As Bitmap
    Public g As Graphics
    Public SmallFont As New Font("Calibri", 15, FontStyle.Regular, GraphicsUnit.Pixel)

    Public ViewPort As New Rectangle

    Public MinX_windows As Double = Nothing
    Public MaxX_windows As Double = Nothing
    Public MinY_windows As Double = Nothing
    Public MaxY_windows As Double = Nothing
    Public RangeX As Double = Nothing
    Public RangeY As Double = Nothing
    Public countIntervalColumn As Double = Nothing
    Public countIntervalRow As Double = Nothing

    Dim DistinctValues_FirstVariable As HashSet(Of intervals) = Form8.DistinctValues_FirstVariable
    Dim DistinctValues_SecondVariable As HashSet(Of intervals) = Form8.DistinctValues_SecondVariable


    Dim semiBlackPen As New Pen(Color.FromArgb(20, Color.Black), 0.5)
    Dim semiBlackBorder As New Pen(Color.FromArgb(70, Color.Black), 0.7)
    Dim BlackPen As New Pen(Color.Black, 1)

    Dim semiBrushYellow As New SolidBrush(Color.FromArgb(40, Color.Yellow))
    Dim semiBrushBlue As New SolidBrush(Color.FromArgb(40, Color.Cyan))
    Dim semiBrushRed As New SolidBrush(Color.FromArgb(40, Color.OrangeRed))


    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGraphics()


    End Sub



    Sub InitializeGraphics()


        Me.b = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
        Me.g = Graphics.FromImage(b)
        Me.g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias


        MinX_windows = 0
        MaxX_windows = PictureBox1.Width - 10
        MinY_windows = 0
        MaxY_windows = PictureBox1.Height - 10

        RangeX = MaxX_windows - MinX_windows
        RangeY = MaxY_windows - MinY_windows

        ViewPort.Width = Math.Abs(MinX_windows) + Math.Abs(MaxX_windows)
        ViewPort.Height = Math.Abs(MinY_windows) + Math.Abs(MaxY_windows)
        ViewPort.X = 0
        ViewPort.Y = 0


        'ViewPort

        Me.DrawScene()

    End Sub



    Sub DrawScene()

        g.Clear(Color.White)


        'ViewPort
        Me.g.DrawRectangle(Pens.Transparent, ViewPort)






        Dim s1 As New SortedSet(Of intervals)(DistinctValues_FirstVariable)
        Dim s2 As New SortedSet(Of intervals)(DistinctValues_SecondVariable)
        countIntervalColumn = s2.Count + 2
        countIntervalRow = s1.Count + 1

        For s As Integer = 0 To s2.Count + 1

            If s = 0 Then
                Dim text As New StringFormat
                text.Alignment = StringAlignment.Center
                text.LineAlignment = StringAlignment.Center
                Dim X_Text As Single = Me.X_ViewPort(s * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                Dim Y_Text As Single = Me.Y_ViewPort(MaxY_windows, ViewPort, MinY_windows, RangeY)

                Dim resize As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                Dim rect1 = New Rectangle(X_Text, Y_Text, resize, (ViewPort.Height) / (countIntervalRow + 1))

                g.DrawRectangle(Pens.Black, rect1)
                g.FillRectangle(semiBrushYellow, rect1)
                g.DrawString("X\Y", SmallFont, Brushes.Black, rect1, text)
            ElseIf (s > 0) AndAlso (s <= s2.Count) Then
                Dim text As New StringFormat
                text.Alignment = StringAlignment.Center
                text.LineAlignment = StringAlignment.Center
                Dim X_Text As Single = Me.X_ViewPort(s * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                Dim Y_Text As Single = Me.Y_ViewPort(MaxY_windows, ViewPort, MinY_windows, RangeY)

                Dim resize As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                Dim rect1 = New Rectangle(X_Text, Y_Text, resize, (ViewPort.Height) / (countIntervalRow + 1))
                g.DrawString("(" & s2.ElementAt(s - 1).lowerPoint & " - " & s2.ElementAt(s - 1).upperPoint & "]", SmallFont, Brushes.Black, rect1, text)
                g.DrawRectangle(Pens.Black, rect1)
                g.DrawRectangle(Pens.Black, rect1)
                g.FillRectangle(semiBrushYellow, rect1)
            Else
                Dim text As New StringFormat
                text.Alignment = StringAlignment.Center
                text.LineAlignment = StringAlignment.Center
                Dim X_Text As Single = Me.X_ViewPort(s * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                Dim Y_Text As Single = Me.Y_ViewPort(MaxY_windows, ViewPort, MinY_windows, RangeY)

                Dim resize As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                Dim rect1 = New Rectangle(X_Text, Y_Text, resize, (ViewPort.Height) / (countIntervalRow + 1))
                g.DrawString("Marginal X", SmallFont, Brushes.Black, rect1, text)
                g.DrawRectangle(Pens.Black, rect1)
                g.DrawRectangle(Pens.Black, rect1)
                g.FillRectangle(semiBrushRed, rect1)
            End If
        Next







        For s As Integer = 0 To s1.Count

            If s < s1.Count Then
                Dim text As New StringFormat
                text.Alignment = StringAlignment.Center
                text.LineAlignment = StringAlignment.Center
                Dim X_Text As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
                Dim Y_Text As Single = Me.Y_ViewPort(MaxY_windows - (((ViewPort.Height) / (countIntervalRow + 1)) * (s + 1)), ViewPort, MinY_windows, RangeY)

                Dim resize As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                Dim rect1 = New Rectangle(X_Text, Y_Text, resize, ViewPort.Height / (countIntervalRow + 1))
                g.DrawString("(" & s1.ElementAt(s).lowerPoint & " - " & s1.ElementAt(s).upperPoint & "]", SmallFont, Brushes.Black, rect1, text)
                g.DrawRectangle(Pens.Black, rect1)
                g.DrawRectangle(Pens.Black, rect1)
                g.FillRectangle(semiBrushYellow, rect1)

                For k As Integer = 0 To s2.Count - 1
                    Dim t As New Tuple(Of intervals, intervals)(s1.ElementAt(s), s2.ElementAt(k))
                    Dim c As Integer

                    If Form8.FrequencyDistributionBivariateContinous.ContainsKey(t) Then
                        c = Form8.FrequencyDistributionBivariateContinous(t).countFrequencies     'joint frequency of X and Y
                    Else
                        c = 0    'joint frequency of X and Y
                    End If


                    Dim text1 As New StringFormat
                    text1.Alignment = StringAlignment.Center
                    text1.LineAlignment = StringAlignment.Center
                    Dim X_Text1 As Single = Me.X_ViewPort((k + 1) * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                    Dim Y_Text1 As Single = Me.Y_ViewPort(MaxY_windows - ((ViewPort.Height) / (countIntervalRow + 1) * (s + 1)), ViewPort, MinY_windows, RangeY)

                    Dim resize1 As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                    Dim rect11 = New Rectangle(X_Text1, Y_Text1, resize1, (ViewPort.Height) / (countIntervalRow + 1))
                    g.DrawString(c.ToString, SmallFont, Brushes.Black, rect11, text1)
                    g.DrawRectangle(Pens.Black, rect11)

                Next


            ElseIf s = s1.Count Then
                Dim text As New StringFormat
                text.Alignment = StringAlignment.Center
                text.LineAlignment = StringAlignment.Center
                Dim X_Text As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
                Dim Y_Text As Single = Me.Y_ViewPort(MaxY_windows - (((ViewPort.Height) / (countIntervalRow + 1)) * (s + 1)), ViewPort, MinY_windows, RangeY)

                Dim resize As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
                Dim rect1 = New Rectangle(X_Text, Y_Text, resize, ((ViewPort.Height) / (countIntervalRow + 1)))
                g.DrawString("Marginal Y", SmallFont, Brushes.Black, rect1, text)
                g.DrawRectangle(Pens.Black, rect1)
                g.DrawRectangle(Pens.Black, rect1)
                g.FillRectangle(semiBrushRed, rect1)
            End If

        Next





        For s As Integer = 0 To Form8.FrequencyDistributionContinousX.Count - 1

            Dim a = Form8.FrequencyDistributionContinousX.ElementAt(s).Value.countFrequencies


            Dim text As New StringFormat
            text.Alignment = StringAlignment.Center
            text.LineAlignment = StringAlignment.Center
            Dim X_Text As Single = Me.X_ViewPort((s2.Count + 1) * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
            Dim Y_Text As Single = Me.Y_ViewPort(MaxY_windows - ((ViewPort.Height) / (countIntervalRow + 1) * (s + 1)), ViewPort, MinY_windows, RangeY)

            Dim resize As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
            Dim rect1 = New Rectangle(X_Text, Y_Text, resize, (ViewPort.Height) / (countIntervalRow + 1))
            g.DrawString(a.ToString, SmallFont, Brushes.Black, rect1, text)
            g.DrawRectangle(Pens.Black, rect1)
            g.FillRectangle(semiBrushRed, rect1)
        Next

        Dim count As Integer = 0
        For s As Integer = 0 To Form8.FrequencyDistributionContinousY.Count - 1

            Dim a = Form8.FrequencyDistributionContinousY.ElementAt(s).Value.countFrequencies


            Dim text As New StringFormat
            text.Alignment = StringAlignment.Center
            text.LineAlignment = StringAlignment.Center
            Dim X_Text As Single = Me.X_ViewPort((s + 1) * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
            Dim Y_Text As Single = Me.Y_ViewPort(MaxY_windows - ((ViewPort.Height) / (countIntervalRow + 1) * (s1.Count + 1)), ViewPort, MinY_windows, RangeY)

            Dim resize As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
            Dim rect1 = New Rectangle(X_Text, Y_Text, resize, (ViewPort.Height) / (countIntervalRow + 1))
            g.DrawString(a.ToString, SmallFont, Brushes.Black, rect1, text)
            g.DrawRectangle(Pens.Black, rect1)
            g.FillRectangle(semiBrushRed, rect1)

            count += a

        Next


        Dim text2 As New StringFormat
        text2.Alignment = StringAlignment.Center
        text2.LineAlignment = StringAlignment.Center
        Dim X_Text2 As Single = Me.X_ViewPort((s2.Count + 1) * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
        Dim Y_Text2 As Single = Me.Y_ViewPort(MaxY_windows - ((ViewPort.Height) / (countIntervalRow + 1) * (s1.Count + 1)), ViewPort, MinY_windows, RangeY)

        Dim resize2 As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
        Dim rect2 = New Rectangle(X_Text2, Y_Text2, resize2, (ViewPort.Height) / (countIntervalRow + 1))
        g.DrawString(count.ToString, SmallFont, Brushes.Black, rect2, text2)
        g.DrawRectangle(Pens.Black, rect2)
        g.FillRectangle(semiBrushBlue, rect2)

        Me.PictureBox1.Image = b

    End Sub

    Function Y_ViewPort(Y_World As Double, ViewPort As Rectangle, MinY As Double, RangeY As Double) As Integer

        Return CInt(ViewPort.Top + ViewPort.Height - ViewPort.Height * (Y_World - MinY) / RangeY)

    End Function

    'Sub to transformm X and Y

    Function X_ViewPort(X_World As Double, ViewPort As Rectangle, MinX As Double, RangeX As Double) As Integer

        Return CInt(ViewPort.Left + ViewPort.Width * (X_World - MinX) / RangeX)

    End Function



    Private Sub Form10_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        g.Dispose()
        InitializeGraphics()

    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        Form11.Show()
        Form11.BringToFront()
    End Sub


    Private Sub Form11Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Form11.Close()
    End Sub
End Class