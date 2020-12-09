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

    Dim RelativeF As Boolean = False
    Dim Hyst As Boolean = True

    Dim count As Integer = 0


    'Left PictureBox
    Public b2 As Bitmap
    Public g2 As Graphics
    Public ViewPort2 As New Rectangle

    Public MinX_windows2 As Double = Nothing
    Public MaxX_windows2 As Double = Nothing
    Public MinY_windows2 As Double = Nothing
    Public MaxY_windows2 As Double = Nothing
    Public RangeX2 As Double = Nothing
    Public RangeY2 As Double = Nothing
    Public countIntervalColumn2 As Double = Nothing
    Public countIntervalRow2 As Double = Nothing


    'Bottom
    Public b3 As Bitmap
    Public g3 As Graphics
    Public ViewPort3 As New Rectangle

    Public MinX_windows3 As Double = Nothing
    Public MaxX_windows3 As Double = Nothing
    Public MinY_windows3 As Double = Nothing
    Public MaxY_windows3 As Double = Nothing
    Public RangeX3 As Double = Nothing
    Public RangeY3 As Double = Nothing
    Public countIntervalColumn3 As Double = Nothing
    Public countIntervalRow3 As Double = Nothing

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


        'PictureBox2


        Me.b2 = New Bitmap(Me.PictureBox2.Width, Me.PictureBox2.Height)
        Me.g2 = Graphics.FromImage(b2)
        Me.g2.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g2.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias


        MinX_windows2 = 0
        MaxX_windows2 = PictureBox2.Width - 10
        MinY_windows2 = 0
        MaxY_windows2 = PictureBox2.Height - 10

        RangeX2 = MaxX_windows2 - MinX_windows2
        RangeY2 = MaxY_windows2 - MinY_windows2

        ViewPort2.Width = Math.Abs(MinX_windows2) + Math.Abs(MaxX_windows2)
        ViewPort2.Height = Math.Abs(MinY_windows2) + Math.Abs(MaxY_windows2)
        ViewPort2.X = 0
        ViewPort2.Y = 0



        'PictureBox3


        Me.b3 = New Bitmap(Me.PictureBox3.Width, Me.PictureBox3.Height)
        Me.g3 = Graphics.FromImage(b3)
        Me.g3.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g3.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias


        MinX_windows3 = 0
        MaxX_windows3 = PictureBox3.Width - 10
        MinY_windows3 = 0
        MaxY_windows3 = PictureBox3.Height - 10

        RangeX3 = MaxX_windows3 - MinX_windows3
        RangeY3 = MaxY_windows3 - MinY_windows3

        ViewPort3.Width = Math.Abs(MinX_windows3) + Math.Abs(MaxX_windows3)
        ViewPort3.Height = Math.Abs(MinY_windows3) + Math.Abs(MaxY_windows3)
        ViewPort3.X = 0
        ViewPort3.Y = 0

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



        count = 0
        For s As Integer = 0 To Form8.FrequencyDistributionContinousY.Count - 1

            Dim a = Form8.FrequencyDistributionContinousY.ElementAt(s).Value.countFrequencies
            count += a

        Next



        For s As Integer = 0 To Form8.FrequencyDistributionContinousY.Count - 1

            Dim a = Form8.FrequencyDistributionContinousY.ElementAt(s).Value.countFrequencies


            Dim text As New StringFormat
            text.Alignment = StringAlignment.Center
            text.LineAlignment = StringAlignment.Center
            Dim X_Text As Single = Me.X_ViewPort((s + 1) * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
            Dim Y_Text As Single = Me.Y_ViewPort(MaxY_windows - ((ViewPort.Height) / (countIntervalRow + 1) * (s1.Count + 1)), ViewPort, MinY_windows, RangeY)

            Dim resize As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
            Dim rect1 = New Rectangle(X_Text, Y_Text, resize, (ViewPort.Height) / (countIntervalRow + 1))
            If RelativeF Then
                g.DrawString(a / count.ToString, SmallFont, Brushes.Black, rect1, text)
            Else
                g.DrawString(a.ToString, SmallFont, Brushes.Black, rect1, text)
            End If
            g.DrawRectangle(Pens.Black, rect1)
            g.FillRectangle(semiBrushRed, rect1)

        Next




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

                    If RelativeF Then
                        Dim freq As Double = c / count
                        g.DrawString(freq.ToString, SmallFont, Brushes.Black, rect11, text1)
                        g.DrawRectangle(Pens.Black, rect11)
                    Else
                        g.DrawString(c.ToString, SmallFont, Brushes.Black, rect11, text1)
                        g.DrawRectangle(Pens.Black, rect11)
                    End If


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

            If RelativeF Then
                g.DrawString(a / count.ToString, SmallFont, Brushes.Black, rect1, text)
            Else
                g.DrawString(a.ToString, SmallFont, Brushes.Black, rect1, text)
            End If
            g.DrawRectangle(Pens.Black, rect1)
            g.FillRectangle(semiBrushRed, rect1)
        Next


        Dim text2 As New StringFormat
        text2.Alignment = StringAlignment.Center
        text2.LineAlignment = StringAlignment.Center
        Dim X_Text2 As Single = Me.X_ViewPort((s2.Count + 1) * (ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
        Dim Y_Text2 As Single = Me.Y_ViewPort(MaxY_windows - ((ViewPort.Height) / (countIntervalRow + 1) * (s1.Count + 1)), ViewPort, MinY_windows, RangeY)

        Dim resize2 As Single = Me.X_ViewPort((ViewPort.Width) / countIntervalColumn, ViewPort, MinX_windows, RangeX)
        Dim rect2 = New Rectangle(X_Text2, Y_Text2, resize2, (ViewPort.Height) / (countIntervalRow + 1))

        If RelativeF Then
            g.DrawString((count / count).ToString, SmallFont, Brushes.Black, rect2, text2)
        Else
            g.DrawString(count.ToString, SmallFont, Brushes.Black, rect2, text2)
        End If


        g.DrawRectangle(Pens.Black, rect2)
        g.FillRectangle(semiBrushBlue, rect2)

        Me.PictureBox1.Image = b

    End Sub









    Sub DrawHist()

        g2.Clear(Color.White)
        g3.Clear(Color.White)

        'ViewPort
        Me.g2.DrawRectangle(Pens.Transparent, ViewPort2)
        Me.g3.DrawRectangle(Pens.Transparent, ViewPort3)



        Dim s1 As New SortedSet(Of intervals)(DistinctValues_FirstVariable)
        Dim s2 As New SortedSet(Of intervals)(DistinctValues_SecondVariable)
        countIntervalColumn = s2.Count + 2
        countIntervalRow = s1.Count + 1

        For s As Integer = 0 To Form8.FrequencyDistributionContinousX.Count - 1

            Dim freq As Double = (Form8.FrequencyDistributionContinousX.ElementAt(s).Value.countFrequencies / count) - (20 * (Form8.FrequencyDistributionContinousX.ElementAt(s).Value.countFrequencies / count) / 100)

            Dim X As Single = Me.X_ViewPort((ViewPort2.Width), ViewPort2, MinX_windows2, RangeX2) -
                                Me.X_ViewPort(freq * (ViewPort2.Width), ViewPort2, MinX_windows2, RangeX2)

            Dim Y_Text1 As Single = Me.Y_ViewPort(MaxY_windows2 - ((ViewPort2.Height) / (countIntervalRow + 1) * (s + 1)), ViewPort2, MinY_windows2, RangeY2)

            Dim resize1 As Single = Me.X_ViewPort(freq * (ViewPort2.Width), ViewPort2, MinX_windows2, RangeX2)
            Dim rect11 = New Rectangle(X, Y_Text1, resize1, (ViewPort2.Height) / (countIntervalRow + 1))

            'g2.DrawRectangle(Pens.Black, rect11)
            g2.FillRectangle(Brushes.BlueViolet, rect11)

        Next



        For s As Integer = 0 To Form8.FrequencyDistributionContinousY.Count - 1

            Dim freq = (Form8.FrequencyDistributionContinousY.ElementAt(s).Value.countFrequencies / count)

            Dim X_Text As Single = Me.X_ViewPort((s + 1) * (ViewPort3.Width) / countIntervalColumn, ViewPort3, MinX_windows3, RangeX3)

            Dim Y_Text As Single = Me.Y_ViewPort(ViewPort3.Height, ViewPort3, MinY_windows3, RangeY3) - Me.Y_ViewPort(freq * ViewPort3.Height, ViewPort3, MinY_windows3, RangeY3)
            Dim height As Single = Me.Y_ViewPort(freq * ViewPort3.Height, ViewPort3, MinY_windows3, RangeY3)

            Dim resize As Single = Me.X_ViewPort((ViewPort3.Width) / countIntervalColumn, ViewPort3, MinX_windows3, RangeX3)
            Dim rect1 = New Rectangle(X_Text, Y_Text, resize, 60)

            'g3.DrawRectangle(Pens.Black, rect1)
            g3.FillRectangle(Brushes.BlueViolet, rect1)

        Next

        Me.PictureBox2.Image = b2
        Me.PictureBox3.Image = b3

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

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            Me.DrawHist()

        Else
            g2.Clear(Color.White)
            g3.Clear(Color.White)
            Me.PictureBox2.Image = b2
            Me.PictureBox3.Image = b3

        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.Checked Then
            RelativeF = True
            InitializeGraphics()
        Else
            RelativeF = False
            InitializeGraphics()
        End If

    End Sub
End Class