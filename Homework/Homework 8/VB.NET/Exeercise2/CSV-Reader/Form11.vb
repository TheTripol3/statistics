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



    Public sizeIntervalX As Double = Form8.sizeIntervalX
    Public sizeIntervalY As Double = Form8.sizeIntervalY

    Public minY As Double = Form8.lisIntervalY.ElementAt(0).lowerPoint
    Public minX As Double = Form8.lisIntervalX.ElementAt(0).lowerPoint

    Public averageX As Double = Form8.averageX
    Public averageY As Double = Form8.averageY

    Dim heaps As New heap
    Dim heaps2 As New heap
    Dim heaps3 As New heap


    Dim heaps4 As New heap
    Dim heaps5 As New heap
    Dim heaps6 As New heap

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
                    Dim Y1_Line As Single = Me.Y_ViewPort(ViewPort.Height + (size / 2), ViewPort, MinY_windows, RangeY)

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
                Dim Y2_Line As Single = Me.Y_ViewPort(ViewPort.Height + (size / 2), ViewPort, MinY_windows, RangeY)
                g.DrawLine(semiBlackPen, X1_Line, Y1_Line, X2_Line, Y2_Line)





                Dim positionX As Single = Me.X_ViewPort(drawColumn * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
                Dim positionY As Single = Me.Y_ViewPort(percentFreq - 120, ViewPort, MinY_windows, RangeY)
                Dim heightRect As Single = Me.Y_ViewPort(percentFreq - 120, ViewPort, MinY_windows, RangeY) - Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
                Dim sizeRect As Single = Me.X_ViewPort((drawColumn + 1) * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX) -
                                            Me.X_ViewPort((drawColumn) * Math.Truncate(ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)


                g.DrawRectangle(semiBlackBorder, positionX, positionY, sizeRect, Math.Abs(heightRect))
                g.FillRectangle(semiBrushYellow, positionX, positionY, sizeRect, Math.Abs(heightRect))


                Dim average As Double = calculateMain(Interval, Form8.listX)
                If average > 0 Then


                    Dim Y1_MeanX As Single = Me.Y_ViewPort(-4, ViewPort, MinY_windows, RangeY)

                    Dim X1_LineMean As Single = Me.X_ViewPort((average - minX) / sizeIntervalX * ViewPort.Width / (countIntervalRow + 1), ViewPort, MinX_windows, RangeX)

                    'Dim X1_Mean2 As Single = Me.X_ViewPort(X2_Line - X1_Mean1, ViewPort, MinX_windows, RangeX)

                    Dim Y2_MeanX As Single = Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
                    g.DrawLine(Pens.Green, X1_LineMean, Y1_MeanX, X1_LineMean, Y2_MeanX)
                End If

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
                    Dim X2_Line As Single = Me.X_ViewPort(ViewPort.Width + (size / 2), ViewPort, MinX_windows, RangeX)

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
                Dim X2_Line As Single = Me.X_ViewPort(ViewPort.Width + (size / 2), ViewPort, MinX_windows, RangeX)

                g.DrawLine(semiBlackPen, X1_Line, Y1_Line, X2_Line, Y1_Line)


                Dim positionX As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                Dim positionY As Single = Me.Y_ViewPort((drawRow + 1) * Math.Truncate(ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

                Dim heightRect As Single = Me.Y_ViewPort(drawRow * Math.Truncate(ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY) -
                    Me.Y_ViewPort((drawRow - 1) * Math.Truncate(ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

                Dim sizeRect As Single = percentFreq

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


                Dim average As Double = calculateMain(Interval, Form8.listY)
                If average > 0 Then

                    Dim X1_LineY As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
                    Dim Y1_Line3 As Single = Me.Y_ViewPort((average - minY) / sizeIntervalY * ViewPort.Height / (countIntervalColumn + 1), ViewPort, MinY_windows, RangeY)
                    Dim X2_LineY As Single = Me.X_ViewPort(-5, ViewPort, MinX_windows, RangeX)

                    Dim Y_Device As Single = Me.Y_ViewPort((average - Interval.lowerPoint) / sizeIntervalY * (ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

                    g.DrawLine(Pens.Violet, X1_Line, Y1_Line3, X2_LineY, Y1_Line3)



                End If



                drawRow += 1
            Else

            End If

        Next



        Dim Y1_LineMeanX As Single = Me.Y_ViewPort(ViewPort.Height + (size / 2), ViewPort, MinY_windows, RangeY)
        Dim X1_LineMeanX As Single = Me.X_ViewPort((averageX - minX) / sizeIntervalX * ViewPort.Width / (countIntervalRow + 1), ViewPort, MinX_windows, RangeX)
        Dim Y2_LineMeanX As Single = Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Red, X1_LineMeanX, Y1_LineMeanX, X1_LineMeanX, Y2_LineMeanX)


        Dim X1_Line2 As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
        Dim Y1_Line2 As Single = Me.Y_ViewPort((averageY - minY) / sizeIntervalY * ViewPort.Height / (countIntervalColumn + 1), ViewPort, MinY_windows, RangeY)
        Dim X2_Line2 As Single = Me.X_ViewPort(ViewPort.Width + (size / 2), ViewPort, MinX_windows, RangeX)
        g.DrawLine(Pens.Red, X1_Line2, Y1_Line2, X2_Line2, Y1_Line2)









        'Me.RichTextBox1.AppendText("minX  " & minX & "  " & "minY  " & minY & Environment.NewLine)
        'Me.RichTextBox1.AppendText("intX  " & sizeIntervalX & "  " & "intY  " & sizeIntervalY & Environment.NewLine & Environment.NewLine)

        For Each d In listPoint
            heaps.add(d.X)
            heaps4.add(d.Y)
            Dim X_Device As Single = Me.X_ViewPort((d.X - minX) / sizeIntervalX * (ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
            Dim Y_Device As Single = Me.Y_ViewPort((d.Y - minY) / sizeIntervalY * (ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

            g.FillEllipse(semiBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(4, 4)))

            'Me.RichTextBox1.AppendText("X  " & d.X & "  " & "Y  " & d.Y & Environment.NewLine)
            'Me.RichTextBox1.AppendText("X  " & X_Device & "  " & "Y  " & Y_Device & Environment.NewLine & Environment.NewLine)
        Next

        Dim median As Double = heaps.calculateMedian()
        Dim Y1_LineMeanXR As Single = Me.Y_ViewPort(ViewPort.Height + (size / 2), ViewPort, MinY_windows, RangeY)
        Dim X1_LineMeanXR As Single = Me.X_ViewPort((median - minX) / sizeIntervalX * ViewPort.Width / (countIntervalRow + 1), ViewPort, MinX_windows, RangeX)
        Dim Y2_LineMeanXR As Single = Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Black, X1_LineMeanXR, Y1_LineMeanXR, X1_LineMeanXR, Y2_LineMeanXR)



        Dim median4 As Double = heaps4.calculateMedian()
        Dim X1_Line24 As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
        Dim Y1_Line24 As Single = Me.Y_ViewPort((median4 - minY) / sizeIntervalY * ViewPort.Height / (countIntervalColumn + 1), ViewPort, MinY_windows, RangeY)
        Dim X2_Line24 As Single = Me.X_ViewPort(ViewPort.Width + (size / 2), ViewPort, MinX_windows, RangeX)
        g.DrawLine(Pens.Black, X1_Line24, Y1_Line24, X2_Line24, Y1_Line24)


        For Each d In listPoint

            If (d.X <= median) Then
                Continue For
            End If
            heaps2.add(d.X)
            Dim X_Device As Single = Me.X_ViewPort((d.X - minX) / sizeIntervalX * (ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
            Dim Y_Device As Single = Me.Y_ViewPort((d.Y - minY) / sizeIntervalY * (ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

            g.FillEllipse(semiBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(4, 4)))

            'Me.RichTextBox1.AppendText("X  " & d.X & "  " & "Y  " & d.Y & Environment.NewLine)
            'Me.RichTextBox1.AppendText("X  " & X_Device & "  " & "Y  " & Y_Device & Environment.NewLine & Environment.NewLine)
        Next

        Dim median2 As Double = heaps2.calculateMedian()
        Dim Y1_LineMeanXR2 As Single = Me.Y_ViewPort(ViewPort.Height + (size / 2), ViewPort, MinY_windows, RangeY)
        Dim X1_LineMeanXR2 As Single = Me.X_ViewPort((median2 - minX) / sizeIntervalX * ViewPort.Width / (countIntervalRow + 1), ViewPort, MinX_windows, RangeX)
        Dim Y2_LineMeanXR2 As Single = Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Black, X1_LineMeanXR2, Y1_LineMeanXR2, X1_LineMeanXR2, Y2_LineMeanXR2)


        For Each d In listPoint

            If (d.X >= median) Then
                Continue For
            End If
            heaps3.add(d.X)
            Dim X_Device As Single = Me.X_ViewPort((d.X - minX) / sizeIntervalX * (ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
            Dim Y_Device As Single = Me.Y_ViewPort((d.Y - minY) / sizeIntervalY * (ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

            g.FillEllipse(semiBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(4, 4)))

            'Me.RichTextBox1.AppendText("X  " & d.X & "  " & "Y  " & d.Y & Environment.NewLine)
            'Me.RichTextBox1.AppendText("X  " & X_Device & "  " & "Y  " & Y_Device & Environment.NewLine & Environment.NewLine)
        Next


        Dim median3 As Double = heaps3.calculateMedian()
        Dim Y1_LineMeanXR3 As Single = Me.Y_ViewPort(ViewPort.Height + (size / 2), ViewPort, MinY_windows, RangeY)
        Dim X1_LineMeanXR3 As Single = Me.X_ViewPort((median3 - minX) / sizeIntervalX * ViewPort.Width / (countIntervalRow + 1), ViewPort, MinX_windows, RangeX)
        Dim Y2_LineMeanXR3 As Single = Me.Y_ViewPort(-4 * size, ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Black, X1_LineMeanXR3, Y1_LineMeanXR3, X1_LineMeanXR3, Y2_LineMeanXR3)






        For Each d In listPoint

            If (d.Y >= median4) Then
                Continue For
            End If
            heaps5.add(d.Y)
            Dim X_Device As Single = Me.X_ViewPort((d.X - minX) / sizeIntervalX * (ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
            Dim Y_Device As Single = Me.Y_ViewPort((d.Y - minY) / sizeIntervalY * (ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

            g.FillEllipse(semiBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(4, 4)))

            'Me.RichTextBox1.AppendText("X  " & d.X & "  " & "Y  " & d.Y & Environment.NewLine)
            'Me.RichTextBox1.AppendText("X  " & X_Device & "  " & "Y  " & Y_Device & Environment.NewLine & Environment.NewLine)
        Next

        Dim median5 As Double = heaps5.calculateMedian()
        Dim X1_Line25 As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
        Dim Y1_Line25 As Single = Me.Y_ViewPort((median5 - minY) / sizeIntervalY * ViewPort.Height / (countIntervalColumn + 1), ViewPort, MinY_windows, RangeY)
        Dim X2_Line25 As Single = Me.X_ViewPort(ViewPort.Width + (size / 2), ViewPort, MinX_windows, RangeX)
        g.DrawLine(Pens.Black, X1_Line25, Y1_Line25, X2_Line25, Y1_Line25)





        For Each d In listPoint

            If (d.Y <= median4) Then
                Continue For
            End If
            heaps6.add(d.Y)
            Dim X_Device As Single = Me.X_ViewPort((d.X - minX) / sizeIntervalX * (ViewPort.Width / (countIntervalRow + 1)), ViewPort, MinX_windows, RangeX)
            Dim Y_Device As Single = Me.Y_ViewPort((d.Y - minY) / sizeIntervalY * (ViewPort.Height / (countIntervalColumn + 1)), ViewPort, MinY_windows, RangeY)

            g.FillEllipse(semiBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(4, 4)))

            'Me.RichTextBox1.AppendText("X  " & d.X & "  " & "Y  " & d.Y & Environment.NewLine)
            'Me.RichTextBox1.AppendText("X  " & X_Device & "  " & "Y  " & Y_Device & Environment.NewLine & Environment.NewLine)
        Next

        Dim median6 As Double = heaps6.calculateMedian()
        Dim X1_Line26 As Single = Me.X_ViewPort(drawColumn * size, ViewPort, MinX_windows, RangeX)
        Dim Y1_Line26 As Single = Me.Y_ViewPort((median6 - minY) / sizeIntervalY * ViewPort.Height / (countIntervalColumn + 1), ViewPort, MinY_windows, RangeY)
        Dim X2_Line26 As Single = Me.X_ViewPort(ViewPort.Width + (size / 2), ViewPort, MinX_windows, RangeX)
        g.DrawLine(Pens.Black, X1_Line26, Y1_Line26, X2_Line26, Y1_Line26)


        Me.PictureBox1.Image = b

    End Sub



    Private Function calculateMain(interval As intervals, listV As List(Of Double)) As Double

        Dim count = 0
        Dim arrT As ArrayList = New ArrayList()
        For Each variable In listV

            If (variable > interval.lowerPoint) AndAlso (variable <= interval.upperPoint) Then
                arrT.Add(variable)
                count += 1
            End If
        Next

        Dim av = kahanSub(arrT)

        Return av

    End Function


    Private Function kahanSub(ls As ArrayList)
        Dim sum = 0.0
        Dim c = 0.0
        Dim count = 0


        For i As Integer = 0 To (ls.Count - 1)
            Dim y = ls(i) - c
            Dim t = sum + y
            c = (t - sum) - y
            sum = t
            count += 1
        Next

        Dim average = sum / count

        Return average
    End Function


    Private Viewport_At_Mouse_Down As Rectangle
    Private MouseLocation_At_MouseDown As Point
    Private Dragging_Started As Boolean
    Private Resizing_Started As Boolean

    Public MinX_Window_At_Mouse_Down As Double
    Public MaxX_Window_At_Mouse_Down As Double
    Public MinY_Window_At_Mouse_Down As Double
    Public MaxY_Window_At_Mouse_Down As Double
    Public RangeX_At_Mouse_Down As Double
    Public RangeY_At_Mouse_Down As Double





    Private Sub PictureBox1_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseWheel

        Dim Change_X As Integer = Me.ViewPort.Width / 10

        'To maintein same aspect
        Dim Change_Y As Integer = CInt(Me.ViewPort.Height * Change_X / Me.ViewPort.Width)

        If ModifierKeys.HasFlag(Keys.Control) Then

            'Dim RealWorldChange_X As Double = Me.RangeX_At_Mouse_Down * Change_X / Me.ViewPort.Width

            'Dim RealWorldChange_Y As Double = Me.RangeY * RealWorldChange_X / Me.RangeX


            'If e.Delta > 0 Then

            '    Me.MinX_windows -= RealWorldChange_X
            '    Me.RangeX += 2 * RealWorldChange_X

            '    Me.MinY_windows -= RealWorldChange_Y
            '    Me.RangeY += 2 * RealWorldChange_Y

            '    Me.DrawScene(dictXF, dictYF, listPointF)


            'ElseIf e.Delta < 0 Then
            '    Me.MinX_windows += RealWorldChange_X
            '    Me.RangeX -= 2 * RealWorldChange_X

            '    Me.MinY_windows += RealWorldChange_Y
            '    Me.RangeY -= 2 * RealWorldChange_Y

            '    Me.DrawScene(dictXF, dictYF, listPointF)


            'End If

        Else

            If e.Delta > 0 Then
                Me.ViewPort.X -= Change_X
                Me.ViewPort.Width += 2 * Change_X

                Me.ViewPort.Y -= Change_Y
                Me.ViewPort.Height += 2 * Change_Y

                Me.DrawScene(dictXF, dictYF, listPointF)

            ElseIf e.Delta < 0 Then
                Me.ViewPort.X += Change_X
                Me.ViewPort.Width -= 2 * Change_X

                Me.ViewPort.Y += Change_Y
                Me.ViewPort.Height -= 2 * Change_Y

                Me.DrawScene(dictXF, dictYF, listPointF)

            End If


        End If


    End Sub





    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If Me.Dragging_Started Then

            Dim Delta_X As Integer = e.X - Me.MouseLocation_At_MouseDown.X
            Dim Delta_Y As Integer = e.Y - Me.MouseLocation_At_MouseDown.Y

            If ModifierKeys.HasFlag(Keys.Control) Then
                'Dim RealWorldDelta_X As Double = Me.RangeX_At_Mouse_Down * Delta_X / Me.ViewPort.Width

                'Me.MinX_windows = Me.MinX_Window_At_Mouse_Down - RealWorldDelta_X
                'Me.MaxX_windows = Me.MaxX_Window_At_Mouse_Down - RealWorldDelta_X


                'Dim RealWorldDelta_Y As Double = Me.RangeY_At_Mouse_Down * Delta_Y / Me.ViewPort.Height

                'Me.MinY_windows = Me.MinY_Window_At_Mouse_Down - RealWorldDelta_X
                'Me.MaxY_windows = Me.MaxY_Window_At_Mouse_Down - RealWorldDelta_Y

            Else

                Me.ViewPort.X = Me.Viewport_At_Mouse_Down.X + Delta_X
                Me.ViewPort.Y = Me.Viewport_At_Mouse_Down.Y + Delta_Y

            End If


            'Update of the drawing
            Me.DrawScene(dictXF, dictYF, listPointF)



        ElseIf (Me.Resizing_Started) Then

            Dim Delta_X As Integer = e.X - Me.MouseLocation_At_MouseDown.X

            Dim Delta_Y As Integer = e.Y - Me.MouseLocation_At_MouseDown.Y

            If ModifierKeys.HasFlag(Keys.Control) Then

                'Dim RealWorldDelta_X As Double = Me.RangeX_At_Mouse_Down * Delta_X / Me.ViewPort.Width

                'Me.MaxX_windows = Me.MaxX_Window_At_Mouse_Down - RealWorldDelta_X

                'Me.RangeX = Me.RangeX_At_Mouse_Down - RealWorldDelta_X



                'Dim RealWorldDelta_Y As Double = Me.RangeY_At_Mouse_Down * Delta_Y / Me.ViewPort.Height

                'Me.MinY_windows = Me.MinY_Window_At_Mouse_Down + RealWorldDelta_Y

                'Me.RangeY = Me.RangeY_At_Mouse_Down - RealWorldDelta_Y


            Else

                Me.ViewPort.Width = Me.Viewport_At_Mouse_Down.Width + Delta_X
                Me.ViewPort.Height = Me.Viewport_At_Mouse_Down.Height + Delta_Y


            End If
            Me.DrawScene(dictXF, dictYF, listPointF)

        End If
        'Update of the drawing

    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If Me.ViewPort.Contains(e.X, e.Y) Then

            Me.Viewport_At_Mouse_Down = Me.ViewPort
            Me.MouseLocation_At_MouseDown = New Point(e.X, e.Y)

            If e.Button = Windows.Forms.MouseButtons.Left Then

                Me.Dragging_Started = True

            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then

                Me.Resizing_Started = True

            End If

        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        Me.Dragging_Started = False
        Me.Resizing_Started = False
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        Me.PictureBox1.Focus()
    End Sub
End Class
