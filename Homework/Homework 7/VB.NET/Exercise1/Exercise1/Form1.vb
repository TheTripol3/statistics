Public Class Form1

    Private b As Bitmap
    Private g As Graphics

    Private b2 As Bitmap
    Private g2 As Graphics

    Private SmallFont As New Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Pixel)
    Dim semiBlackPen As New Pen(Color.FromArgb(20, Color.Black), 0.5)
    Private semiBrushBlue As New SolidBrush(Color.FromArgb(80, Color.Orange))

    Private ViewPort As New Rectangle
    Private MinX_windows As Double = Nothing
    Private MaxX_windows As Double = Nothing
    Private MinY_windows As Double = Nothing
    Private MaxY_windows As Double = Nothing
    Private RangeX As Double = Nothing
    Private RangeY As Double = Nothing


    Private ViewPort2 As New Rectangle
    Private MinX_windows2 As Double = Nothing
    Private MaxX_windows2 As Double = Nothing
    Private MinY_windows2 As Double = Nothing
    Private MaxY_windows2 As Double = Nothing
    Private RangeX2 As Double = Nothing
    Private RangeY2 As Double = Nothing

    Dim R As New Random

    Private nSelectSample As Integer = Nothing
    Private nSample As Integer = Nothing
    Public nRandom As Integer = Nothing
    Private minX As Integer = 0
    Private maxX As Integer = 1

    Private listMean As New List(Of Double)
    Private avg As Double = Nothing
    Private sum As Double = Nothing



    Dim intervalSize As Double = Nothing
    Dim startingEndPoint As Double = 0.5
    Dim listIntervals As New List(Of sizeIntervals)

    Private minR As Double = 0
    Private maxR As Double = 1
    Private maxRow As Integer = 1


    Private X_Previous As Single
    Private Y_Previous As Single


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub



    Private Sub generation()
        listMean.Clear()
        'Me.RichTextBox1.Clear()
        For i As Integer = 1 To nSample
            sum = 0
            avg = 0

            For j As Integer = 1 To nRandom
                Dim number As Double = minX + (maxX - minX) * R.NextDouble

                sum += number
                'Me.RichTextBox1.AppendText(number.ToString.PadRight(5))
            Next
            avg = (sum / nRandom)
            listMean.Add(avg)
            'Me.RichTextBox1.AppendText(avg.ToString.PadRight(8) & Environment.NewLine)
        Next

    End Sub



    'Print the distribution
    'Private Sub printContinuousDistribution(listIntv As List(Of sizeIntervals))


    '    Me.RichTextBox1.AppendText(Environment.NewLine & Environment.NewLine)
    '    Me.RichTextBox1.AppendText("Interval".PadRight(18) & "Count" & Environment.NewLine)

    '    For Each interVal1 As sizeIntervals In listIntv

    '        Dim stringInterval1 = "( " & interVal1.lowerPoint & " - " & interVal1.upperPoint & " ]"

    '        Me.RichTextBox1.AppendText(stringInterval1.PadRight(18) & interVal1.countInt.ToString &
    '                                       Environment.NewLine)
    '    Next

    'End Sub





    Private Sub InitializeGraphics()
        Me.b = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
        Me.g = Graphics.FromImage(b)
        Me.g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        MinX_windows = 0
        MaxX_windows = PictureBox1.Width - 20
        MinY_windows = 0
        MaxY_windows = PictureBox1.Height - 20

        RangeX = MaxX_windows - MinX_windows
        RangeY = MaxY_windows - MinY_windows

        ViewPort.Width = Math.Abs(MinX_windows) + Math.Abs(MaxX_windows)
        ViewPort.Height = Math.Abs(MinY_windows) + Math.Abs(MaxY_windows)
        ViewPort.X = 5
        ViewPort.Y = 5


        Me.b2 = New Bitmap(Me.PictureBox2.Width, Me.PictureBox2.Height)
        Me.g2 = Graphics.FromImage(b2)
        Me.g2.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g2.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        MinX_windows2 = 0
        MaxX_windows2 = PictureBox2.Width - 20
        MinY_windows2 = 0
        MaxY_windows2 = PictureBox2.Height - 20

        RangeX2 = MaxX_windows2 - MinX_windows2
        RangeY2 = MaxY_windows2 - MinY_windows2

        ViewPort2.Width = Math.Abs(MinX_windows2) + Math.Abs(MaxX_windows2)
        ViewPort2.Height = Math.Abs(MinY_windows2) + Math.Abs(MaxY_windows2)
        ViewPort2.X = 5
        ViewPort2.Y = 5




    End Sub




    Private Sub DrawSceneGenerate()

        g.Clear(Color.White)
        g2.Clear(Color.White)
        ''ViewPort
        Me.g.DrawRectangle(Pens.Transparent, ViewPort)
        Me.g2.DrawRectangle(Pens.Transparent, ViewPort2)


        'X TOP
        Dim X1_Line As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_Line As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim Y_Line As Single = Me.Y_ViewPort(maxRow * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Green, X1_Line, Y_Line, X2_Line, Y_Line)

        'X Bottom
        Dim X1_Line0 As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_Line0 As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim Y_Line0 As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Black, X1_Line0, Y_Line0, X2_Line0, Y_Line0)

        'Y 
        g.DrawLine(Pens.Transparent, X1_Line, Y_Line0, X1_Line, Y_Line)


        Dim rectString1 = New Rectangle(X1_Line0, Y_Line + 2, 10, 11)
        Dim textC1 As New StringFormat
        textC1.Alignment = StringAlignment.Center
        textC1.LineAlignment = StringAlignment.Center

        g.DrawRectangle(Pens.Transparent, rectString1)
        g.FillRectangle(Brushes.Transparent, X1_Line0, Y_Line + 2, 10, 11)
        g.DrawString(1.ToString, SmallFont, Brushes.Black, rectString1, textC1)







        'X Bottom
        Dim X1_LineZ As Single = Me.X_ViewPort(0, ViewPort2, MinX_windows2, RangeX2)
        Dim X2_LineZ As Single = Me.X_ViewPort(ViewPort2.Width, ViewPort2, MinX_windows2, RangeX2)
        Dim Y_LineZ As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort2.Height / maxRow), ViewPort2, MinY_windows2, RangeY2)
        g2.DrawLine(Pens.Black, X1_LineZ, Y_LineZ, X2_LineZ, Y_LineZ)


        'Histogram

        For Each kvp As sizeIntervals In listIntervals

            Dim X_1H As Single = Me.X_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort2.Width / maxR), ViewPort2, MinX_windows2, RangeX2)
            Dim X_2H As Single = Me.X_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort2.Width / maxR), ViewPort2, MinX_windows2, RangeX2)

            Dim freq As Double = (kvp.countInt / listMean.Count) + ((kvp.countInt / listMean.Count) * 150 / 100)
            Dim Y_H As Single = Me.Y_ViewPort(freq * Math.Truncate(ViewPort2.Height / maxRow), ViewPort2, MinY_windows2, RangeY2)

            Dim heightH As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort2.Height / maxRow), ViewPort2, MinY_windows2, RangeY2) -
                Me.Y_ViewPort(freq * Math.Truncate(ViewPort2.Height / maxRow), ViewPort2, MinY_windows2, RangeY2)

            Dim sizeH As Single = Me.X_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort2.Width / maxR), ViewPort2, MinX_windows2, RangeX2) -
                Me.X_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort2.Width / maxR), ViewPort2, MinX_windows2, RangeX2)


            Dim position As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort2.Height / maxRow), ViewPort2, MinY_windows2, RangeY2) -
                heightH

            Dim rectH = New Rectangle(X_1H, position, sizeH, heightH)
            g2.DrawRectangle(Pens.Black, rectH)
            g2.FillRectangle(Brushes.DarkViolet, X_1H, position, sizeH, heightH)


        Next



        listMean.Sort()

        For Each k As Double In listMean
            Dim countMean As Double = 0

            For Each p As Double In listMean
                If p <= k Then
                    countMean += 1
                End If
            Next

            Dim XEllipse As Single = Me.X_ViewPort(k * Math.Truncate(ViewPort.Width / maxR), ViewPort, MinX_windows, RangeX)
            Dim freq As Double = countMean / listMean.Count
            Dim YEllipse As Single = Me.Y_ViewPort(freq * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
            'g.FillEllipse(Brushes.Black, New Rectangle(New Point(XEllipse - 3, YEllipse - 3), New Size(4, 4)))



            If (k = listMean.First) Then
                X_Previous = XEllipse
                Y_Previous = YEllipse
                Continue For
            End If


            'Corner
            'X
            g.DrawLine(Pens.Black, X_Previous, Y_Previous, XEllipse, Y_Previous)
            'Y
            g.DrawLine(Pens.Black, XEllipse, Y_Previous, XEllipse, YEllipse)


            X_Previous = XEllipse
            Y_Previous = YEllipse

        Next

        Me.PictureBox1.Image = b
        Me.PictureBox2.Image = b2

    End Sub


    Function Y_ViewPort(Y_World As Double, ViewPort As Rectangle, MinY As Double, RangeY As Double) As Integer
        Return CInt(ViewPort.Top + ViewPort.Height - ViewPort.Height * (Y_World - MinY) / RangeY)
    End Function

    Function X_ViewPort(X_World As Double, ViewPort As Rectangle, MinX As Double, RangeX As Double) As Integer
        Return CInt(ViewPort.Left + ViewPort.Width * (X_World - MinX) / RangeX)
    End Function




    Private Function findRange(randomNumber As Double, listIntv As List(Of sizeIntervals))
        'Value between first interval and center 
        If ((listIntv(0).lowerPoint <= randomNumber) AndAlso (randomNumber <= listIntv(Math.Ceiling(listIntv.Count / 2)).lowerPoint)) Then
            Return "L"
            'Value between center and last interval
        ElseIf ((listIntv(Math.Ceiling(listIntv.Count / 2)).lowerPoint < randomNumber) AndAlso (randomNumber <= listIntv(listIntv.Count - 1).upperPoint)) Then
            Return "R"
            'Smaller value than the first interval
        ElseIf (randomNumber <= listIntv(0).lowerPoint) Then
            Return "OL"

            'Bigger value than the last interval
        ElseIf (randomNumber > listIntv(listIntv.Count - 1).upperPoint) Then
            Return "OR"
        Else
            Throw New Exception("Error")
        End If
    End Function


    'Calculate the distribution
    Private Sub calculateContinuousDistribution(sizeInterval As Double,
                                        valuesC As Double, listInt As List(Of sizeIntervals), range As String)
        Select Case range
            'Left from the center (the value exists) 
            Case "L"
                For index As Integer = 0 To (Math.Ceiling((listInt.Count / 2)) - 1)
                    If ((Double.Parse(valuesC) > Double.Parse(listInt(index).lowerPoint)) AndAlso (Double.Parse(valuesC) <= Double.Parse(listInt(index).upperPoint))) Then
                        listInt(index).countInt += 1
                        Exit Sub
                    End If
                Next

            'Right from the center (the value exists) 
            Case "R"
                For index As Integer = Math.Ceiling((listInt.Count / 2)) To (listInt.Count - 1)
                    If ((Double.Parse(valuesC) > Double.Parse(listInt(index).lowerPoint)) AndAlso (Double.Parse(valuesC) <= Double.Parse(listInt(index).upperPoint))) Then
                        listInt(index).countInt += 1
                        Exit Sub
                    End If
                Next
            'To the left of the first interval (Value does not exist)
            Case "OL"
                Do
                    Dim NewLeftInterval As New sizeIntervals
                    NewLeftInterval.upperPoint = Double.Parse(listInt(0).lowerPoint)
                    NewLeftInterval.lowerPoint = Double.Parse(NewLeftInterval.upperPoint - sizeInterval)

                    listInt.Insert(0, NewLeftInterval)

                    If (Double.Parse(valuesC) < Double.Parse(NewLeftInterval.upperPoint)) AndAlso (Double.Parse(valuesC) >= Double.Parse(NewLeftInterval.lowerPoint)) Then
                        NewLeftInterval.countInt += 1
                        Exit Do
                    End If
                Loop

            'To the right of the last interval (Value does not exist)
            Case "OR"
                Do
                    Dim NewRightInterval As New sizeIntervals
                    NewRightInterval.lowerPoint = Double.Parse(listInt(listInt.Count - 1).upperPoint)
                    NewRightInterval.upperPoint = Double.Parse(NewRightInterval.lowerPoint + sizeInterval)

                    listInt.Add(NewRightInterval)

                    If ((Double.Parse(valuesC) > Double.Parse(NewRightInterval.lowerPoint)) And (Double.Parse(valuesC) <= Double.Parse(NewRightInterval.upperPoint))) Then
                        NewRightInterval.countInt += 1
                        Exit Do
                    Else

                    End If
                Loop
            Case Else
                Throw New Exception("Error")
        End Select
    End Sub


    'Initialize the intervals
    Private Sub initialize(list As List(Of sizeIntervals), startPoint As Double, intervalSize As Double)
        listIntervals.Clear()

        If (list.Count = 0) Then

            'Second Interval
            Dim firstInterval As New sizeIntervals

            'Set the first Interval
            firstInterval.upperPoint = startPoint
            firstInterval.lowerPoint = firstInterval.upperPoint - intervalSize

            'Now, we use a list to store the intervals
            list.Add(firstInterval)

            'First Interval
            Dim secondIntervel As New sizeIntervals

            'Set the first Interval
            secondIntervel.lowerPoint = startPoint
            secondIntervel.upperPoint = secondIntervel.lowerPoint + intervalSize

            'Now, we use a list to store the intervals
            list.Add(secondIntervel)



        End If



    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        InitializeGraphics()

        initialize(listIntervals, startingEndPoint, intervalSize)
        generation()


        For Each kvp As Double In listMean
            Dim meanReange As String = findRange(kvp, listIntervals)
            calculateContinuousDistribution(intervalSize, kvp, listIntervals, meanReange)
        Next

        'printContinuousDistribution(listIntervals)

        DrawSceneGenerate()

        nRandom += 1
        nSample = nSelectSample * nRandom

        intervalSize = Math.Round((maxX - minX) / Math.Ceiling(nSample / 20), 8)

        'Me.RichTextBox1.Clear()
        'Me.RichTextBox1.AppendText("N  " & nRandom.ToString & "     nSample  " & nSample.ToString & "      Interval   " & intervalSize)

    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        If Timer1.Enabled Then
            Button1.Text = "START"
            Timer1.Stop()

        Else
            TextBox3.ForeColor = Color.Black
            TextBox4.ForeColor = Color.Black

            If IsNumeric(TextBox3.Text) AndAlso (Convert.ToInt32(TextBox3.Text) > 0) Then
                nRandom = TextBox3.Text
                If IsNumeric(TextBox4.Text) AndAlso (Convert.ToInt32(TextBox4.Text) > 0) Then
                    nSelectSample = TextBox4.Text
                    nSample = TextBox4.Text * nRandom
                    startingEndPoint = 0.5


                    intervalSize = Math.Round((maxX - minX) / Math.Ceiling(nSample / 20), 8)

                    Button1.Text = "STOP"
                    Timer1.Start()

                Else
                    TextBox4.ForeColor = Color.Red
                End If
            Else
                TextBox3.ForeColor = Color.Red
            End If

        End If
    End Sub
End Class




