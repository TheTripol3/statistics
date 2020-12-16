Public Class Form1


    'PictureBox 1
    Private b As Bitmap
    Private g As Graphics

    Private ViewPort As New Rectangle
    Private MinX_windows As Double = Nothing
    Private MaxX_windows As Double = Nothing
    Private MinY_windows As Double = Nothing
    Private MaxY_windows As Double = Nothing
    Private RangeX As Double = Nothing
    Private RangeY As Double = Nothing

    Private max As Double = Nothing


    'PictureBox 2
    'Private b2 As Bitmap
    'Private g2 As Graphics

    'Private ViewPort2 As New Rectangle
    'Private MinX_windows2 As Double = Nothing
    'Private MaxX_windows2 As Double = Nothing
    'Private MinY_windows2 As Double = Nothing
    'Private MaxY_windows2 As Double = Nothing
    'Private RangeX2 As Double = Nothing
    'Private RangeY2 As Double = Nothing



    Private SmallFont As New Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Pixel)
    Dim semiBlackPen As New Pen(Color.FromArgb(20, Color.Black), 0.5)
    Private semiBrushBlue As New SolidBrush(Color.FromArgb(80, Color.Orange))

    Private m As Integer = Nothing
    Private n As Integer = Nothing
    Private sigma As Double = Nothing
    Private mu As Double = Nothing
    Private initial As Double = Nothing
    Private totalTime As Double = Nothing
    Private dt As Double = Nothing








    Private p As Double = Nothing

    Private sum As Integer = Nothing


    Dim R As New Random
    Private X_Previous As Single
    Private Y_Previous As Single


    Dim intervalSizeN As Double = 5
    Dim startingEndPointN As Double = 50

    Dim intervalSizeN2 As Double = 5
    Dim startingEndPointN2 As Double = 50

    Dim listIntervalsNFinal As New List(Of sizeIntervals)
    Dim listIntervalsN As New List(Of sizeIntervals)
    Dim listIntervalsN2 As New List(Of sizeIntervals)


    Private listDictN2 As New List(Of Dictionary(Of Integer, Double))
    Private listDictN As New List(Of Dictionary(Of Integer, Double))
    Private listDictNFinal As New List(Of Dictionary(Of Integer, Double))


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TextBox1.Text = "1000"
        Me.TextBox2.Text = "800"
        Me.TextBox3.Text = "1"
        Me.TextBox4.Text = "0"
        Me.TextBox5.Text = "100"
        Me.TextBox6.Text = "1"
    End Sub


    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        listIntervalsN.Clear()
        listIntervalsN2.Clear()
        listIntervalsNFinal.Clear()
        Me.TextBox1.ForeColor = Color.Black
        Me.TextBox2.ForeColor = Color.Black
        Me.TextBox3.ForeColor = Color.Black



        If IsNumeric(TextBox1.Text) AndAlso (Convert.ToInt32(TextBox1.Text) > 0) Then
            If IsNumeric(TextBox2.Text) AndAlso (Convert.ToInt32(TextBox2.Text) > 0) Then
                If IsNumeric(TextBox3.Text) AndAlso (Convert.ToDouble(TextBox3.Text) > 0) Then
                    m = Convert.ToInt32(TextBox1.Text)
                    n = Convert.ToInt32(TextBox2.Text)
                    sigma = Convert.ToDouble(TextBox3.Text)
                    mu = Convert.ToDouble(TextBox4.Text)
                    initial = Convert.ToDouble(TextBox5.Text)
                    totalTime = Convert.ToDouble(TextBox6.Text)
                    dt = totalTime / n

                    generation()
                    initialize(listIntervalsNFinal, startingEndPointN, intervalSizeN)
                    initialize(listIntervalsN2, startingEndPointN2, intervalSizeN2)
                    PhaseDistribution()
                    InitializeGraphics()

                Else
                    Me.TextBox3.ForeColor = Color.Red
                End If
            Else
                Me.TextBox2.ForeColor = Color.Red
            End If
        Else
            Me.TextBox1.ForeColor = Color.Red
        End If
    End Sub


    Private Sub generation()
        listDictN.Clear()
        listDictN2.Clear()
        listDictNFinal.Clear()


        Dim maxValue As Double = 0.0

        For i As Integer = 1 To m

            Dim dicN As New Dictionary(Of Integer, Double)
            Dim dicN2 As New Dictionary(Of Integer, Double)
            Dim dicNFinal As New Dictionary(Of Integer, Double)

            Dim precedent As Double = 0.0
            For j As Integer = 1 To n
                Dim numberZi As Double = SurroundingSub(0, 1)

                If (j = 1) Then
                    precedent = initial
                End If

                precedent = precedent * Math.Exp((mu - ((sigma ^ 2) / 2)) * dt + sigma * Math.Sqrt(dt) * numberZi)

                If (precedent > maxValue) Then
                    maxValue = precedent
                End If


                'dicN.Add(j - 1, precedent)

                If (j = n / 2) Then
                    dicN2.Add(j - 1, precedent)
                End If

                If (j = n) Then
                    dicN.Add(j - 1, precedent)
                End If

                If (j <= n) Then
                    dicNFinal.Add(j - 1, precedent)
                End If

            Next

            listDictN.Add(dicN)
            listDictN2.Add(dicN2)
            listDictNFinal.Add(dicNFinal)

        Next

        max = maxValue


    End Sub


    Private Sub PhaseDistribution()


        For Each kv In listDictN2
            For Each kvp In kv
                Dim numb As String = findRange((kvp.Value), listIntervalsN2)
                calculateContinuousDistribution(intervalSizeN2, (kvp.Value), listIntervalsN2, numb)
            Next
        Next

        For Each kv In listDictN
            For Each kvp In kv
                Dim numb As String = findRange((kvp.Value), listIntervalsNFinal)
                calculateContinuousDistribution(intervalSizeN, (kvp.Value), listIntervalsNFinal, numb)
            Next
        Next
    End Sub


    Private Sub InitializeGraphics()

        'PictureBox1
        Me.b = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
        Me.g = Graphics.FromImage(b)
        Me.g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        MinX_windows = 0
        MaxX_windows = PictureBox1.Width
        MinY_windows = -PictureBox1.Height
        MaxY_windows = PictureBox1.Height

        RangeX = MaxX_windows - MinX_windows
        RangeY = MaxY_windows - MinY_windows

        ViewPort.Width = Math.Abs(MinX_windows) + Math.Abs(MaxX_windows)
        ViewPort.Height = Math.Abs(MinY_windows) + Math.Abs(MaxY_windows)
        ViewPort.X = 0
        ViewPort.Y = 0


        'PictureBox2
        'Me.b2 = New Bitmap(Me.PictureBox2.Width, Me.PictureBox2.Height)
        'Me.g2 = Graphics.FromImage(b2)
        'Me.g2.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        'Me.g2.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        'MinX_windows2 = 0
        'MaxX_windows2 = PictureBox2.Width - 20
        'MinY_windows2 = 0
        'MaxY_windows2 = PictureBox2.Height - 20

        'RangeX2 = MaxX_windows2 - MinX_windows2
        'RangeY2 = MaxY_windows2 - MinY_windows2

        'ViewPort2.Width = Math.Abs(MinX_windows2) + Math.Abs(MaxX_windows2)
        'ViewPort2.Height = Math.Abs(MinY_windows2) + Math.Abs(MaxY_windows2)
        'ViewPort2.X = 10
        'ViewPort2.Y = 10


        Me.DrawSceneGenerate()

    End Sub








    Private Sub DrawSceneGenerate()

        g.Clear(Color.White)
        ''ViewPort
        Me.g.DrawRectangle(Pens.Transparent, ViewPort)



        Dim X1_Line As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_Line As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim Y_Line As Single = Me.Y_ViewPort(MaxY_windows * Math.Truncate(ViewPort.Height / MaxY_windows), ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Black, X1_Line, Y_Line, X2_Line, Y_Line)



        Dim countPath As Integer = 0
        For Each objectList In listDictNFinal
            Dim newColor As Color = Color.FromArgb(R.Next(0, 255), R.Next(0, 255), R.Next(0, 255), R.Next(0, 255))
            Dim nPen As Pen = New Pen(newColor, 0.8)
            Dim nBrush As SolidBrush = New SolidBrush(newColor)


            X_Previous = Me.X_ViewPort(0 * (ViewPort.Width / (n + Convert.ToInt32(TextBox1.Text / 4))), ViewPort, MinX_windows, RangeX)
            Y_Previous = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY)

            For Each kvp As KeyValuePair(Of Integer, Double) In objectList

                Dim X_Device As Single = Me.X_ViewPort(kvp.Key * (ViewPort.Width / (n + Convert.ToInt32(TextBox1.Text / 4))), ViewPort, MinX_windows, RangeX)
                Dim Y_Device As Single = Me.Y_ViewPort(kvp.Value * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY)

                'g.FillEllipse(nBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(3, 3)))


                g.DrawLine(nPen, X_Previous, Y_Previous, X_Device, Y_Device)

                X_Previous = X_Device
                Y_Previous = Y_Device

            Next

            countPath += 1
        Next

        'g.DrawLine(Pens.Black, X1_Line, Y_Line0, X1_Line, Y_Line)




        ''Histograms

        For Each kvp As sizeIntervals In listIntervalsN2
            Dim freq As Double = (kvp.countInt / ((n / 2) * m)) * 300

            'Me.RichTextBox1.AppendText(kvp.lowerPoint & " " & kvp.upperPoint & "  " & kvp.countInt & "   " & freq & Environment.NewLine)

            Dim XL As Single = Me.X_ViewPort((n / 2) * (ViewPort.Width / (n + Convert.ToInt32(TextBox1.Text / 4))), ViewPort, MinX_windows, RangeX)
            Dim YL2 As Single = Me.Y_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY)
            Dim YL1 As Single = Me.Y_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY)

            Dim X2L As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)

            Dim height As Single = Me.Y_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY) - Me.Y_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY)


            Dim width As Single = Me.X_ViewPort(freq * (ViewPort.Width), ViewPort, MinX_windows, RangeX)
            Dim rect1 = New Rectangle(XL, YL2, width, height)


            g.DrawRectangle(Pens.Transparent, rect1)
            g.FillRectangle(Brushes.BlueViolet, rect1)

            'Me.RichTextBox1.AppendText(kvp.lowerPoint & "-" & kvp.upperPoint & "    " & kvp.countInt & "    " & freq & Environment.NewLine)
        Next

        For Each kvp As sizeIntervals In listIntervalsNFinal
            Dim freq As Double = (kvp.countInt / (n * m)) * 500

            Dim XL As Single = Me.X_ViewPort(n * (ViewPort.Width / (n + Convert.ToInt32(TextBox1.Text / 4))), ViewPort, MinX_windows, RangeX)
            Dim YL2 As Single = Me.Y_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY)
            Dim YL1 As Single = Me.Y_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY)

            Dim X2L As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)

            Dim height As Single = Me.Y_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY) - Me.Y_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort.Height / max), ViewPort, MinY_windows, RangeY)


            Dim width As Single = Me.X_ViewPort(freq * (ViewPort.Width), ViewPort, MinX_windows, RangeX)
            Dim rect1 = New Rectangle(XL, YL2, width, height)


            g.DrawRectangle(Pens.Transparent, rect1)
            g.FillRectangle(Brushes.BlueViolet, rect1)

            'Me.RichTextBox1.AppendText(kvp.lowerPoint & "-" & kvp.upperPoint & "    " & kvp.countInt & "    " & freq & Environment.NewLine)
        Next


        Me.PictureBox1.Image = b

    End Sub

    Private Sub initialize(list As List(Of sizeIntervals), startPoint As Double, intervalSize As Double)

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

    Function Y_ViewPort(Y_World As Double, ViewPort As Rectangle, MinY As Double, RangeY As Double) As Integer
        Return CInt(ViewPort.Top + ViewPort.Height - ViewPort.Height * (Y_World - MinY) / RangeY)
    End Function

    Function X_ViewPort(X_World As Double, ViewPort As Rectangle, MinX As Double, RangeX As Double) As Integer
        Return CInt(ViewPort.Left + ViewPort.Width * (X_World - MinX) / RangeX)
    End Function

    Private Function SurroundingSub(mean As Double, stdDev As Double) As Double
        'Box-Muller transform
        Dim rand As Random = New Random()
        Dim u1 As Double = 1.0 - rand.NextDouble()  'uniform(0,1] random doubles
        Dim u2 As Double = 1.0 - rand.NextDouble()
        Dim randStdNormal As Double = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2)   'random normal(0,1)
        Dim randNormal As Double = mean + stdDev * randStdNormal  'random normal(mean,stdDev^2)

        Return randNormal
    End Function

End Class
