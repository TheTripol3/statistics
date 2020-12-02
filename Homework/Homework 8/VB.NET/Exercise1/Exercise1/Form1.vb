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


    'PictureBox 2
    Private b2 As Bitmap
    Private g2 As Graphics

    Private ViewPort2 As New Rectangle
    Private MinX_windows2 As Double = Nothing
    Private MaxX_windows2 As Double = Nothing
    Private MinY_windows2 As Double = Nothing
    Private MaxY_windows2 As Double = Nothing
    Private RangeX2 As Double = Nothing
    Private RangeY2 As Double = Nothing



    'PictureBox 3
    Private b3 As Bitmap
    Private g3 As Graphics

    Private ViewPort3 As New Rectangle
    Private MinX_windows3 As Double = Nothing
    Private MaxX_windows3 As Double = Nothing
    Private MinY_windows3 As Double = Nothing
    Private MaxY_windows3 As Double = Nothing
    Private RangeX3 As Double = Nothing
    Private RangeY3 As Double = Nothing


    'PictureBox 4
    Private b4 As Bitmap
    Private g4 As Graphics

    Private ViewPort4 As New Rectangle
    Private MinX_windows4 As Double = Nothing
    Private MaxX_windows4 As Double = Nothing
    Private MinY_windows4 As Double = Nothing
    Private MaxY_windows4 As Double = Nothing
    Private RangeX4 As Double = Nothing
    Private RangeY4 As Double = Nothing




    'PictureBox 5
    Private b5 As Bitmap
    Private g5 As Graphics

    Private ViewPort5 As New Rectangle
    Private MinX_windows5 As Double = Nothing
    Private MaxX_windows5 As Double = Nothing
    Private MinY_windows5 As Double = Nothing
    Private MaxY_windows5 As Double = Nothing
    Private RangeX5 As Double = Nothing
    Private RangeY5 As Double = Nothing


    Private SmallFont As New Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Pixel)
    Dim semiBlackPen As New Pen(Color.FromArgb(20, Color.Black), 0.5)
    Private semiBrushBlue As New SolidBrush(Color.FromArgb(80, Color.Orange))

    Private m As Integer = Nothing
    Private n As Integer = Nothing
    Private lambda As Integer = Nothing
    Private p As Double = Nothing

    Private sum As Integer = Nothing


    Dim R As New Random
    Private min As Double = 0
    Private max As Double = 1
    Private X_Previous As Single
    Private Y_Previous As Single


    Private startPoint As Double = 0


    Dim intervalSize As Double = 25
    Dim startingEndPoint As Double = 100

    Dim intervalSizeB As Double = 2
    Dim startingEndPointB As Double = 10

    Dim intervalSizeN As Double = Nothing
    Dim startingEndPointN As Double = Nothing

    Dim intervalSizeN2 As Double = Nothing
    Dim startingEndPointN2 As Double = Nothing

    Dim listIntervals As New List(Of sizeIntervals)
    Dim listIntervalsB2 As New List(Of sizeIntervals)
    Dim listIntervalsN As New List(Of sizeIntervals)
    Dim listIntervalsN2 As New List(Of sizeIntervals)

    'Step - Jump from origin - Jump from previous
    Private listDict As New List(Of Dictionary(Of Integer, Integer))
    Private listJump As New List(Of Dictionary(Of Integer, Tuple(Of Integer, Integer)))


    Private listDictN2 As New List(Of Dictionary(Of Integer, Tuple(Of Integer, Integer)))
    Private listDictN As New List(Of Dictionary(Of Integer, Tuple(Of Integer, Integer)))


    Dim countJump As Integer = 0

    Dim maxWidthA As Integer = 0
    Dim maxHeight As Double = 200

    Private maxTN As Double = Nothing
    Private minTN As Double = Nothing

    Private maxTN2 As Double = Nothing
    Private minTN2 As Double = Nothing

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TextBox1.Text = "200"
        Me.TextBox2.Text = "300"
        Me.TextBox3.Text = "150"
    End Sub


    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        listIntervals.Clear()
        listIntervalsB2.Clear()
        listIntervalsN.Clear()
        listIntervalsN2.Clear()
        Me.TextBox1.ForeColor = Color.Black
        Me.TextBox2.ForeColor = Color.Black
        Me.TextBox3.ForeColor = Color.Black

        If IsNumeric(TextBox1.Text) AndAlso (Convert.ToInt32(TextBox1.Text) > 0) Then
            If IsNumeric(TextBox2.Text) AndAlso (Convert.ToInt32(TextBox2.Text) > 0) Then
                If IsNumeric(TextBox3.Text) AndAlso (Convert.ToInt32(TextBox3.Text) > 0) Then
                    m = Convert.ToInt32(TextBox1.Text)
                    n = Convert.ToInt32(TextBox2.Text)
                    lambda = Convert.ToInt32(TextBox3.Text)
                    p = lambda / n

                    generationBernoulli()
                    checkMaxMinumum()
                    initialize(listIntervals, startingEndPoint, intervalSize)
                    initialize(listIntervalsB2, startingEndPointB, intervalSizeB)
                    initialize(listIntervalsN, startingEndPointN, intervalSizeN)
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

    Private Sub checkMaxMinumum()
        maxTN = 0
        minTN = 0

        maxTN2 = 0
        minTN2 = 0
        For Each s In listDictN
            For Each l In s
                If ((l.Value.Item1 / l.Value.Item2) >= maxTN) Then
                    maxTN = (l.Value.Item1 / l.Value.Item2)
                End If

                If ((l.Value.Item1 / l.Value.Item2) <= minTN) Then
                    minTN = (l.Value.Item1 / l.Value.Item2)
                End If
            Next
        Next

        For Each s In listDictN2
            For Each l In s
                If ((l.Value.Item1 / l.Value.Item2) >= maxTN2) Then
                    maxTN2 = (l.Value.Item1 / l.Value.Item2)
                End If

                If ((l.Value.Item1 / l.Value.Item2) <= minTN2) Then
                    minTN2 = (l.Value.Item1 / l.Value.Item2)
                End If
            Next
        Next

        intervalSizeN = Math.Round((maxTN - minTN) / listDictN.Count, 3)
        intervalSizeN2 = Math.Round((maxTN2 - minTN2) / (listDictN2.Count - 20), 3)

        startingEndPointN = Math.Round((maxTN - minTN) / 2, 3)
        startingEndPointN2 = Math.Round((maxTN2 - minTN2) / 2, 3)

    End Sub

    Private Sub generationBernoulli()
        listDict.Clear()
        listJump.Clear()
        listDictN.Clear()
        listDictN2.Clear()

        Dim w As Integer = 0
        For i As Integer = 1 To m
            Dim dict As New Dictionary(Of Integer, Integer)

            sum = 0
            w += 1
            Dim dicN2 As New Dictionary(Of Integer, Tuple(Of Integer, Integer))
            Dim dicN As New Dictionary(Of Integer, Tuple(Of Integer, Integer))

            For j As Integer = 1 To n
                Dim number As Double = Math.Round(min + (max - min) * R.NextDouble, 4)

                If (number >= min) AndAlso (number < p) Then

                Else
                    sum += 1
                End If

                dict.Add(j, sum)

                If (j = (n / 2)) Then
                    Dim tup As Tuple(Of Integer, Integer) = New Tuple(Of Integer, Integer)(j, sum)
                    dicN2.Add(w, tup)
                End If


                If (j = (n)) Then
                    Dim tup As Tuple(Of Integer, Integer) = New Tuple(Of Integer, Integer)(j, sum)
                    dicN.Add(w, tup)
                End If
                'Me.RichTextBox1.AppendText(j.ToString.PadRight(4) & "   " & sum.ToString.PadRight(4) & Environment.NewLine)

            Next

            listDict.Add(dict)
            listDictN2.Add(dicN2)
            listDictN.Add(dicN)
            'Me.RichTextBox1.AppendText(Environment.NewLine)

        Next

        For Each lis In listDict
            Dim jumpFromOrigin As Integer = 0
            Dim jumpPrevious As Integer = 0

            Dim previous As Integer = 0

            Dim dic As New Dictionary(Of Integer, Tuple(Of Integer, Integer))
            For Each k As KeyValuePair(Of Integer, Integer) In lis
                If (k.Value = previous) Then
                    jumpFromOrigin += 1
                    jumpPrevious += 1
                Else
                    jumpFromOrigin += 1
                    jumpPrevious += 1

                    If (jumpFromOrigin > maxWidthA) Then
                        maxWidthA = jumpFromOrigin
                    End If

                    Dim Tup As Tuple(Of Integer, Integer) = New Tuple(Of Integer, Integer)(jumpFromOrigin, jumpPrevious)
                        dic.Add(k.Key, Tup)
                        previous = k.Value
                        jumpPrevious = 0
                    End If

            Next
            listJump.Add(dic)
        Next

        'For Each c In listJump
        '    For Each l In c
        '        Me.RichTextBox3.AppendText(l.Key.ToString.PadRight(4) & l.Value.ToString.PadRight(4) & Environment.NewLine)
        '    Next
        '    Me.RichTextBox3.AppendText(Environment.NewLine)
        'Next

    End Sub


    Private Sub PhaseDistribution()
        countJump = 0
        For Each kv In listJump
            For Each kvp In kv
                Dim numb As String = findRange(kvp.Value.Item1, listIntervals)
                calculateContinuousDistribution(intervalSize, kvp.Value.Item1, listIntervals, numb)

                Dim numb2 As String = findRange(kvp.Value.Item2, listIntervalsB2)
                calculateContinuousDistribution(intervalSizeB, kvp.Value.Item2, listIntervalsB2, numb2)
                countJump += 1
            Next
        Next


        For Each kv In listDictN2
            For Each kvp In kv
                Dim numb As String = findRange((kvp.Value.Item1 / kvp.Value.Item2), listIntervalsN2)
                calculateContinuousDistribution(intervalSizeN2, (kvp.Value.Item1 / kvp.Value.Item2), listIntervalsN2, numb)
            Next
        Next

        For Each kv In listDictN
            For Each kvp In kv
                Dim numb As String = findRange((kvp.Value.Item1 / kvp.Value.Item2), listIntervalsN)
                calculateContinuousDistribution(intervalSizeN, (kvp.Value.Item1 / kvp.Value.Item2), listIntervalsN, numb)
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
        MaxX_windows = PictureBox1.Width - 20
        MinY_windows = 0
        MaxY_windows = PictureBox1.Height - 20

        RangeX = MaxX_windows - MinX_windows
        RangeY = MaxY_windows - MinY_windows

        ViewPort.Width = Math.Abs(MinX_windows) + Math.Abs(MaxX_windows)
        ViewPort.Height = Math.Abs(MinY_windows) + Math.Abs(MaxY_windows)
        ViewPort.X = 10
        ViewPort.Y = 10

        'PictureBox2
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
        ViewPort2.X = 10
        ViewPort2.Y = 10



        'PictureBox3
        Me.b3 = New Bitmap(Me.PictureBox3.Width, Me.PictureBox3.Height)
        Me.g3 = Graphics.FromImage(b3)
        Me.g3.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g3.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        MinX_windows3 = 0
        MaxX_windows3 = PictureBox3.Width - 20
        MinY_windows3 = 0
        MaxY_windows3 = PictureBox3.Height - 20

        RangeX3 = MaxX_windows3 - MinX_windows3
        RangeY3 = MaxY_windows3 - MinY_windows3

        ViewPort3.Width = Math.Abs(MinX_windows3) + Math.Abs(MaxX_windows3)
        ViewPort3.Height = Math.Abs(MinY_windows3) + Math.Abs(MaxY_windows3)
        ViewPort3.X = 10
        ViewPort3.Y = 10




        'PictureBox4
        Me.b4 = New Bitmap(Me.PictureBox4.Width, Me.PictureBox4.Height)
        Me.g4 = Graphics.FromImage(b4)
        Me.g4.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g4.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        MinX_windows4 = 0
        MaxX_windows4 = PictureBox4.Width - 20
        MinY_windows4 = 0
        MaxY_windows4 = PictureBox4.Height - 20

        RangeX4 = MaxX_windows4 - MinX_windows4
        RangeY4 = MaxY_windows4 - MinY_windows4

        ViewPort4.Width = Math.Abs(MinX_windows4) + Math.Abs(MaxX_windows4)
        ViewPort4.Height = Math.Abs(MinY_windows4) + Math.Abs(MaxY_windows4)
        ViewPort4.X = 10
        ViewPort4.Y = 10




        'PictureBox5
        Me.b5 = New Bitmap(Me.PictureBox5.Width, Me.PictureBox5.Height)
        Me.g5 = Graphics.FromImage(b5)
        Me.g5.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g5.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        MinX_windows5 = 0
        MaxX_windows5 = PictureBox5.Width - 20
        MinY_windows5 = 0
        MaxY_windows5 = PictureBox5.Height - 20

        RangeX5 = MaxX_windows5 - MinX_windows5
        RangeY5 = MaxY_windows5 - MinY_windows5

        ViewPort5.Width = Math.Abs(MinX_windows5) + Math.Abs(MaxX_windows5)
        ViewPort5.Height = Math.Abs(MinY_windows5) + Math.Abs(MaxY_windows5)
        ViewPort5.X = 10
        ViewPort5.Y = 10
        Me.DrawSceneGenerate()
        DrawSceneH()

    End Sub




    Private Sub DrawSceneH()

        g2.Clear(Color.White)
        g3.Clear(Color.White)
        g4.Clear(Color.White)
        g5.Clear(Color.White)
        ''ViewPort
        Me.g2.DrawRectangle(Pens.Transparent, ViewPort2)
        Me.g3.DrawRectangle(Pens.Transparent, ViewPort3)
        Me.g4.DrawRectangle(Pens.Transparent, ViewPort4)
        Me.g5.DrawRectangle(Pens.Transparent, ViewPort5)




        Dim X1_Line As Single = Me.X_ViewPort(0, ViewPort2, MinX_windows2, RangeX2)
        Dim X2_Line As Single = Me.X_ViewPort(ViewPort2.Width, ViewPort2, MinX_windows2, RangeX2)
        Dim Y_Line As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort2.Height / max), ViewPort2, MinY_windows2, RangeY2)
        g2.DrawLine(Pens.Black, X1_Line, Y_Line, X2_Line, Y_Line)

        Dim X1_Line2 As Single = Me.X_ViewPort(0, ViewPort3, MinX_windows3, RangeX3)
        Dim X2_Line2 As Single = Me.X_ViewPort(ViewPort3.Width, ViewPort3, MinX_windows3, RangeX3)
        Dim Y_Line2 As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort3.Height / max), ViewPort3, MinY_windows3, RangeY3)
        g3.DrawLine(Pens.Black, X1_Line2, Y_Line2, X2_Line2, Y_Line2)


        Dim X1_Line3 As Single = Me.X_ViewPort(0, ViewPort4, MinX_windows4, RangeX4)
        Dim X2_Line3 As Single = Me.X_ViewPort(ViewPort4.Width, ViewPort4, MinX_windows4, RangeX4)
        Dim Y_Line3 As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort4.Height / max), ViewPort4, MinY_windows4, RangeY4)
        g4.DrawLine(Pens.Black, X1_Line3, Y_Line3, X2_Line3, Y_Line3)

        For Each kvp As sizeIntervals In listIntervals

            Dim freq As Double = kvp.countInt / (countJump)

            Dim X_1H As Single = Me.X_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort2.Width / (maxWidthA + 1)), ViewPort2, MinX_windows2, RangeX2)
            Dim X_2H As Single = Me.X_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort2.Width / (maxWidthA + 1)), ViewPort2, MinX_windows2, RangeX2)

            Dim Y_H As Single = Me.Y_ViewPort(freq * Math.Truncate(ViewPort2.Height / max), ViewPort2, MinY_windows2, RangeY2)

            Dim heightH As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort2.Height / max), ViewPort2, MinY_windows2, RangeY2) -
                Me.Y_ViewPort(freq * Math.Truncate(ViewPort2.Height / max), ViewPort2, MinY_windows2, RangeY2)

            Dim sizeH As Single = Me.X_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort2.Width / (maxWidthA+1)), ViewPort2, MinX_windows2, RangeX2) -
                Me.X_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort2.Width / (maxWidthA+1)), ViewPort2, MinX_windows2, RangeX2)


            Dim position As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort2.Height / max), ViewPort2, MinY_windows2, RangeY2) -
                heightH

            Dim rectH = New Rectangle(X_1H, position, sizeH, heightH)
            g2.DrawRectangle(Pens.Black, rectH)
            g2.FillRectangle(semiBrushBlue, X_1H, position, sizeH, heightH)


        Next




        For Each kvp As sizeIntervals In listIntervalsB2

            Dim freq As Double = kvp.countInt / (countJump)

            Dim X_1H As Single = Me.X_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort3.Width / (maxWidthA + 1)), ViewPort3, MinX_windows3, RangeX3)
            Dim X_2H As Single = Me.X_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort3.Width / (maxWidthA + 1)), ViewPort3, MinX_windows3, RangeX3)

            Dim Y_H As Single = Me.Y_ViewPort(freq * Math.Truncate(ViewPort3.Height / max), ViewPort3, MinY_windows3, RangeY3)

            Dim heightH As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort3.Height / max), ViewPort3, MinY_windows3, RangeY3) -
                Me.Y_ViewPort(freq * Math.Truncate(ViewPort3.Height / max), ViewPort3, MinY_windows3, RangeY3)

            Dim sizeH As Single = Me.X_ViewPort(kvp.upperPoint * Math.Truncate(ViewPort3.Width / (maxWidthA + 1)), ViewPort3, MinX_windows3, RangeX3) -
                Me.X_ViewPort(kvp.lowerPoint * Math.Truncate(ViewPort3.Width / (maxWidthA + 1)), ViewPort3, MinX_windows3, RangeX3)


            Dim position As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort3.Height / max), ViewPort3, MinY_windows3, RangeY3) -
                heightH

            Dim rectH = New Rectangle(X_1H, position, sizeH, heightH)
            g3.DrawRectangle(Pens.Black, rectH)
            g3.FillRectangle(semiBrushBlue, X_1H, position, sizeH, heightH)


        Next





        'Distribution

        Dim i As Integer = 0
        For Each kvp As sizeIntervals In listIntervalsN
            Dim freq As Double = (kvp.countInt / (listDictN.Count)) * 8

            Dim X_1H As Single = Me.X_ViewPort(i * Math.Truncate(ViewPort4.Width / (listIntervalsN.Count)), ViewPort4, MinX_windows4, RangeX4)
            Dim X_2H As Single = Me.X_ViewPort((i + 1) * Math.Truncate(ViewPort4.Width / (listIntervalsN.Count)), ViewPort4, MinX_windows4, RangeX4)

            Dim Y_H As Single = Me.Y_ViewPort(freq * Math.Truncate(ViewPort4.Height / max), ViewPort4, MinY_windows4, RangeY4)

            Dim heightH As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort4.Height / max), ViewPort4, MinY_windows4, RangeY4) -
                Me.Y_ViewPort(freq * Math.Truncate(ViewPort4.Height / max), ViewPort4, MinY_windows4, RangeY4)

            Dim sizeH As Single = Me.X_ViewPort((i + 1) * Math.Truncate(ViewPort4.Width / (listIntervalsN.Count)), ViewPort4, MinX_windows4, RangeX4) -
                Me.X_ViewPort(i * Math.Truncate(ViewPort4.Width / (listIntervalsN.Count)), ViewPort4, MinX_windows4, RangeX4)


            Dim position As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort4.Height / max), ViewPort4, MinY_windows4, RangeY4) -
                heightH

            Dim rectH = New Rectangle(X_1H, position, sizeH, heightH)
            g4.DrawRectangle(Pens.Black, rectH)
            g4.FillRectangle(semiBrushBlue, X_1H, position, sizeH, heightH)

            i += 1
        Next

        i = 0
        For Each kvp As sizeIntervals In listIntervalsN2
            Dim freq As Double = (kvp.countInt / (listDictN2.Count)) * 8

            Dim X_1H As Single = Me.X_ViewPort(i * Math.Truncate(ViewPort5.Width / (listIntervalsN2.Count)), ViewPort5, MinX_windows5, RangeX5)
            Dim X_2H As Single = Me.X_ViewPort((i + 1) * Math.Truncate(ViewPort5.Width / (listIntervalsN2.Count)), ViewPort5, MinX_windows5, RangeX5)

            Dim Y_H As Single = Me.Y_ViewPort(freq * Math.Truncate(ViewPort5.Height / max), ViewPort5, MinY_windows5, RangeY5)

            Dim heightH As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort5.Height / max), ViewPort5, MinY_windows5, RangeY5) -
                Me.Y_ViewPort(freq * Math.Truncate(ViewPort5.Height / max), ViewPort5, MinY_windows5, RangeY5)

            Dim sizeH As Single = Me.X_ViewPort((i + 1) * Math.Truncate(ViewPort5.Width / (listIntervalsN2.Count)), ViewPort5, MinX_windows5, RangeX5) -
                Me.X_ViewPort(i * Math.Truncate(ViewPort5.Width / (listIntervalsN2.Count)), ViewPort5, MinX_windows5, RangeX5)


            Dim position As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort5.Height / max), ViewPort5, MinY_windows5, RangeY5) -
                heightH

            Dim rectH = New Rectangle(X_1H, position, sizeH, heightH)
            g5.DrawRectangle(Pens.Black, rectH)
            g5.FillRectangle(semiBrushBlue, X_1H, position, sizeH, heightH)

            i += 1
        Next
        Me.PictureBox2.Image = b2
        Me.PictureBox3.Image = b3
        Me.PictureBox4.Image = b4
        Me.PictureBox5.Image = b5

    End Sub




    Private Sub DrawSceneGenerate()

        g.Clear(Color.White)
        ''ViewPort
        Me.g.DrawRectangle(Pens.Transparent, ViewPort)




        Dim X1_Line As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_Line As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim Y_Line As Single = Me.Y_ViewPort(20 * Math.Truncate(ViewPort.Height / 20), ViewPort, MinY_windows, RangeY)
        g.DrawLine(semiBlackPen, X1_Line, Y_Line, X2_Line, Y_Line)

        Dim X1_Line0 As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_Line0 As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim Y_Line0 As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / 20), ViewPort, MinY_windows, RangeY)
        g.DrawLine(semiBlackPen, X1_Line0, Y_Line0, X2_Line0, Y_Line0)



        Dim countPath As Integer = 0
        For Each objectList In listDict
            Dim newColor As Color = Color.FromArgb(R.Next(0, 255), R.Next(0, 255), R.Next(0, 255), R.Next(0, 255))
            Dim nPen As Pen = New Pen(newColor, 0.8)
            Dim nBrush As SolidBrush = New SolidBrush(newColor)


            startPoint = 0
            X_Previous = Me.X_ViewPort(0 * (ViewPort.Width / (n + 1)), ViewPort, MinX_windows, RangeX)
            Y_Previous = Me.Y_ViewPort((0) * (ViewPort.Height / (20)), ViewPort, MinY_windows, RangeY)

            For Each kvp As KeyValuePair(Of Integer, Integer) In objectList

                startPoint += (kvp.Value / kvp.Key)

                Dim X_Device As Single = Me.X_ViewPort(kvp.Key * (ViewPort.Width / (n + 1)), ViewPort, MinX_windows, RangeX)
                Dim Y_Device As Single = Me.Y_ViewPort((startPoint) * (ViewPort.Height / (maxHeight)), ViewPort, MinY_windows, RangeY)

                'g.FillEllipse(nBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(3, 3)))


                g.DrawLine(nPen, X_Previous, Y_Previous, X_Device, Y_Device)

                X_Previous = X_Device
                Y_Previous = Y_Device

            Next

            countPath += 1
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

End Class
