Imports System.Drawing.Drawing2D
Public Class Form11

    Public b As Bitmap
    Public g As Graphics
    Public SmallFont As New Font("Calibri", 15, FontStyle.Regular, GraphicsUnit.Pixel)

    Public ViewPort As New Rectangle
    Public Datasets As New DataSet

    Public MinX_windows As Double = Nothing
    Public MaxX_windows As Double = Nothing
    Public MinY_windows As Double = Nothing
    Public MaxY_windows As Double = Nothing
    Public RangeX As Double = Nothing
    Public RangeY As Double = Nothing

    Dim semiBlackPen As New Pen(Color.FromArgb(20, Color.Black), 0.5)
    Dim semiBlackBorder As New Pen(Color.FromArgb(70, Color.Black), 0.7)
    Dim BlackPen As New Pen(Color.Black, 1)

    Dim semiBrush As New SolidBrush(Color.FromArgb(180, Color.Green))


    Dim semiBrushYellow As New SolidBrush(Color.FromArgb(90, Color.Yellow))
    Dim semiBrushOrange As New SolidBrush(Color.FromArgb(90, Color.Orange))


    Dim countIntervalColumn As Integer
    Dim countIntervalRow As Integer

    Dim size As Integer = 30


    Public dictXF As SortedDictionary(Of intervals, frequency)
    Public dictYF As SortedDictionary(Of intervals, frequency)
    Public listPointF As List(Of DataPoint_Numeric)



    Public MinX_Windows_At_Mouse_Down As Double
    Public MaxX_Windows_At_Mouse_Down As Double
    Public MinY_Windows_At_Mouse_Down As Double
    Public MaxY_Windows_At_Mouse_Down As Double
    Public RangeX_Windows_At_Mouse_Down As Double
    Public RangeY_Windows_At_Mouse_Down As Double

    Public sizeIntervalX As Double = Form8.sizeIntervalX
    Public sizeIntervalY As Double = Form8.sizeIntervalY

    Public minY As Double = Form8.lisIntervalY.ElementAt(0).lowerPoint
    Public minX As Double = Form8.lisIntervalX.ElementAt(0).lowerPoint

    Public averageX As Double = Form8.averageX
    Public averageY As Double = Form8.averageY

    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.PictureBox1.Image = Nothing
        InitializeGraphics(Form8.FrequencyDistributionContinousX, Form8.FrequencyDistributionContinousY, Form8.listInt)


    End Sub


    Sub InitializeGraphics(dictY As SortedDictionary(Of intervals, frequency),
                           dictX As SortedDictionary(Of intervals, frequency), listPoint As List(Of DataPoint_Numeric))
        Me.b = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
        Me.g = Graphics.FromImage(b)
        Me.g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias


        MinX_windows = 0
        MaxX_windows = 700
        MinY_windows = 0
        MaxY_windows = 600

        RangeX = MaxX_windows - MinX_windows
        RangeY = MaxY_windows - MinY_windows

        ViewPort.Width = Math.Abs(MinX_windows) + Math.Abs(MaxX_windows)
        ViewPort.Height = Math.Abs(MinY_windows) + Math.Abs(MaxY_windows)
        ViewPort.X = 280
        ViewPort.Y = 20


        countIntervalColumn = Form8.lisIntervalY.Count - 1
        countIntervalRow = Form8.lisIntervalX.Count - 1

        dictXF = dictX
        dictYF = dictY
        listPointF = listPoint



        'ViewPort

        Me.DrawScene(dictXF, dictYF, listPointF)

    End Sub




    Function Y_ViewPort(Y_World As Double, ViewPort As Rectangle, MinY As Double, RangeY As Double) As Integer

        Return CInt(ViewPort.Top + ViewPort.Height - ViewPort.Height * (Y_World - MinY) / RangeY)

    End Function

    'Sub to transformm X and Y

    Function X_ViewPort(X_World As Double, ViewPort As Rectangle, MinX As Double, RangeX As Double) As Integer

        Return CInt(ViewPort.Left + ViewPort.Width * (X_World - MinX) / RangeX)

    End Function




    Sub DrawScene(dictX As SortedDictionary(Of intervals, frequency),
                           dictY As SortedDictionary(Of intervals, frequency), listPoint As List(Of DataPoint_Numeric))

        g.Clear(Color.White)


        'ViewPort
        Me.g.DrawRectangle(Pens.Transparent, ViewPort)

        Dim drawColumn As Integer = -4
        Dim drawRow As Integer = -4



        For s As Integer = -4 To countIntervalRow


            If (s < 0) Then

                If s = -4 Then

                    'x1 y1 top
                    Dim Y1_Line As Single = Me.Y_ViewPort(MaxY_windows + (size / 2), ViewPort, MinY_windows, RangeY)

                    Dim X_Line As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                    Dim Y2_Line As Single = Me.Y_ViewPort(drawRow * size - (size / 2), ViewPort, MinY_windows, RangeY)


                    g.DrawLine(BlackPen, X_Line, Y1_Line, X_Line, Y2_Line)


                Else
                    'Dim X1_Line As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                    'Dim Y1_Line As Single = Me.Y_ViewPort(drawRow * size, ViewPort, MinY_windows, RangeY)

                    'Dim X2_Line As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                    'Dim Y2_Line As Single = Me.Y_ViewPort(MaxY_windows + (size / 2), ViewPort, MinY_windows, RangeY)
                    'g.DrawLine(semiBlackPen, X1_Line, Y1_Line, X2_Line, Y2_Line)


                End If

                drawColumn += 1

            ElseIf (s <= countIntervalRow) And (s >= 0) Then

                Dim Interval As intervals = Form8.lisIntervalX.ElementAt(s)
                Dim frequency As Integer = Interval.countInt
                Dim percentFreq As Double = Math.Round((90 * (frequency / Form8.ListVariables(0).Count) * 100) / 100, 2)


                Dim X1_Line As Single = Me.X_ViewPort(drawColumn * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
                Dim Y1_Line As Single = Me.Y_ViewPort(drawRow * size, ViewPort, MinY_windows, RangeY)

                Dim X2_Line As Single = Me.X_ViewPort(drawColumn * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
                Dim Y2_Line As Single = Me.Y_ViewPort(MaxY_windows + (size / 2), ViewPort, MinY_windows, RangeY)
                g.DrawLine(semiBlackPen, X1_Line, Y1_Line, X2_Line, Y2_Line)





                Dim positionX As Single = Me.X_ViewPort(drawColumn * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
                Dim positionY As Single = Me.Y_ViewPort(percentFreq - 115, ViewPort, MinY_windows, RangeY)
                Dim heightRect As Single = Me.Y_ViewPort(percentFreq - 115, ViewPort, MinY_windows, RangeY) - Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
                Dim sizeRect As Single = Me.X_ViewPort((drawColumn + 1) * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX) -
                                            Me.X_ViewPort((drawColumn) * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)


                g.DrawRectangle(semiBlackBorder, positionX, positionY, sizeRect, Math.Abs(heightRect))
                g.FillRectangle(semiBrushYellow, positionX, positionY, sizeRect, Math.Abs(heightRect))




                If countIntervalRow < 8 Then

                    Dim text As New StringFormat
                    text.Alignment = StringAlignment.Center
                    text.LineAlignment = StringAlignment.Center
                    Dim X_Text As Single = Me.X_ViewPort(drawColumn * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
                    Dim Y_Text As Single = Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
                    Dim rect1 = New Rectangle(X_Text, Y_Text, sizeRect, 60)
                    g.DrawString(("(" & Interval.lowerPoint & "  -  " & Interval.upperPoint & " ]") & Environment.NewLine & frequency.ToString, SmallFont, Brushes.Black, rect1, text)
                End If


                drawColumn += 1
                Else


                End If



        Next

        drawColumn = -4
        drawRow = -4

        'Column
        For s As Integer = -4 To countIntervalColumn
            If (s < 0) Then

                If s = -4 Then
                    'x1 left
                    Dim X1_Line As Single = Me.X_ViewPort(drawColumn * size - (size / 2), ViewPort, MinX_windows, RangeX)
                    Dim Y1_Line As Single = Me.Y_ViewPort(drawRow * size, ViewPort, MinY_windows, RangeY)
                    Dim X2_Line As Single = Me.X_ViewPort(MaxX_windows + (size / 2), ViewPort, MinX_windows, RangeX)

                    g.DrawLine(BlackPen, X1_Line, Y1_Line, X2_Line, Y1_Line)

                Else
                    'Dim X1_Line As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                    'Dim Y1_Line As Single = Me.Y_ViewPort(drawRow * size, ViewPort, MinY_windows, RangeY)
                    'Dim X2_Line As Single = Me.X_ViewPort(MaxX_windows + (size / 2), ViewPort, MinX_windows, RangeX)

                    'g.DrawLine(semiBlackPen, X1_Line, Y1_Line, X2_Line, Y1_Line)
                End If


                drawRow += 1

            ElseIf (s <= countIntervalColumn) And (s >= 0) Then

                Dim Interval As intervals = Form8.lisIntervalY.ElementAt(s)
                Dim frequency As Integer = Interval.countInt
                Dim percentFreq As Double = Math.Round((80 * (frequency / Form8.ListVariables(0).Count) * 100) / 100, 2)



                Dim X1_Line As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                Dim Y1_Line As Single = Me.Y_ViewPort(drawRow * Math.Truncate(ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)
                Dim X2_Line As Single = Me.X_ViewPort(MaxX_windows + (size / 2), ViewPort, MinX_windows, RangeX)

                g.DrawLine(semiBlackPen, X1_Line, Y1_Line, X2_Line, Y1_Line)


                Dim positionX As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                Dim positionY As Single = Me.Y_ViewPort((drawRow + 1) * Math.Truncate(ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

                Dim heightRect As Single = Me.Y_ViewPort(drawRow * Math.Truncate(ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY) -
                    Me.Y_ViewPort((drawRow - 1) * Math.Truncate(ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

                Dim sizeRect As Single = percentFreq + 5

                g.DrawRectangle(semiBlackBorder, positionX, positionY, sizeRect, Math.Abs(heightRect))
                g.FillRectangle(semiBrushOrange, positionX, positionY, sizeRect, Math.Abs(heightRect))


                If countIntervalColumn < 8 Then


                    Dim X_Text As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                    Dim Y_Text As Single = Me.Y_ViewPort((drawRow + 1) * Math.Truncate(ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)
                    Dim text As New StringFormat
                    text.Alignment = StringAlignment.Center
                    text.LineAlignment = StringAlignment.Center
                    Dim rect1 = New Rectangle(X_Text - 140, Y_Text, 140, Math.Abs(heightRect))
                    g.DrawString(("(" & Interval.lowerPoint & "  -  " & Interval.upperPoint & " ]") & Environment.NewLine & frequency.ToString, SmallFont, Brushes.Black, rect1, text)
                End If


                drawRow += 1
            Else

            End If

        Next



        Dim Y1_LineMeanX As Single = Me.Y_ViewPort(MaxY_windows + (size / 2), ViewPort, MinY_windows, RangeY)
        Dim X1_LineMeanX As Single = Me.X_ViewPort((averageX - minX) / sizeIntervalX * ViewPort.Width / (countIntervalRow + 1), ViewPort, MinX_windows, RangeX)
        Dim Y2_LineMeanX As Single = Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Red, X1_LineMeanX, Y1_LineMeanX, X1_LineMeanX, Y2_LineMeanX)


        Dim X1_Line2 As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
        Dim Y1_Line2 As Single = Me.Y_ViewPort((averageY - minY) / sizeIntervalY * ViewPort.Height / (countIntervalColumn + 1), ViewPort, MinY_windows, RangeY)
        Dim X2_Line2 As Single = Me.X_ViewPort(MaxX_windows + (size / 2), ViewPort, MinX_windows, RangeX)
        g.DrawLine(Pens.Red, X1_Line2, Y1_Line2, X2_Line2, Y1_Line2)



        'Me.RichTextBox1.AppendText("minX  " & minX & "  " & "minY  " & minY & Environment.NewLine)
        'Me.RichTextBox1.AppendText("intX  " & sizeIntervalX & "  " & "intY  " & sizeIntervalY & Environment.NewLine & Environment.NewLine)

        For Each d In listPoint

            Dim X_Device As Single = Me.X_ViewPort((d.X - minX) / sizeIntervalX * (ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
            Dim Y_Device As Single = Me.Y_ViewPort((d.Y - minY) / sizeIntervalY * (ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

            g.FillEllipse(semiBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(4, 4)))

            'Me.RichTextBox1.AppendText("X  " & d.X & "  " & "Y  " & d.Y & Environment.NewLine)
            'Me.RichTextBox1.AppendText("X  " & X_Device & "  " & "Y  " & Y_Device & Environment.NewLine & Environment.NewLine)


        Next


        Me.PictureBox1.Image = b

    End Sub


End Class
