Imports System.Globalization
Public Class Form1

    Private p As Double = 0.5
    Private n As Integer = Nothing
    Private m As Integer = Nothing
    Private j As Integer = Nothing
    Private flagSelect As Boolean = False
    Private indexSelect As Integer = Nothing

    Private minR As Double = 0
    Private maxR As Double = 1

    Private R As New Random


    Private epsilon As Double = Nothing
    'Private sizeHystogram As Double = 0.1
    Private sizeHystogramEpsilon As Double = 0.05

    Private listDictionary As New List(Of Dictionary(Of Integer, Integer))
    Private listIntervals As New List(Of sizeIntervals)
    Private listIntervalsJ As New List(Of sizeIntervals)
    Private listIntervalsEpsilon As New List(Of sizeIntervals)
    Private listIntervalsEpsilonJ As New List(Of sizeIntervals)
    Private countTotal As Integer = 0
    Private countTotalJ As Integer = 0

    Private semiBrushBlue As New SolidBrush(Color.FromArgb(180, Color.LightSkyBlue))
    Private semiBrushOrange As New SolidBrush(Color.FromArgb(200, Color.Orange))
    Private semiRed As New SolidBrush(Color.FromArgb(170, Color.Red))
    Private semiYellow As New SolidBrush(Color.FromArgb(190, Color.Yellow))

    Private b As Bitmap
    Private g As Graphics
    Private SmallFont As New Font("Calibri", 11, FontStyle.Regular, GraphicsUnit.Pixel)
    Dim semiBlackPen As New Pen(Color.FromArgb(20, Color.Black), 0.5)
    Private ViewPort As New Rectangle

    Private MinX_windows As Double = Nothing
    Private MaxX_windows As Double = Nothing
    Private MinY_windows As Double = Nothing
    Private MaxY_windows As Double = Nothing
    Private RangeX As Double = Nothing
    Private RangeY As Double = Nothing

    Private maxRow As Integer = 1

    Private X_Previous As Single
    Private Y_Previous As Single

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture
        TextBox1.Text = 1000
        TextBox2.Text = 200
        TextBox3.Text = 500
        TextBox4.Text = 0.2
    End Sub



    Private Sub printContinuousDistributionEpsilon(listIntv As List(Of sizeIntervals), position As Integer, total As Integer)


        For Each interVal As sizeIntervals In listIntv
            Dim positionX As Single = Me.X_ViewPort(position * (ViewPort.Width / (n + 1)), ViewPort, MinX_windows, RangeX)
            Dim positionY As Single = Me.Y_ViewPort(interVal.upperPoint * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)

            Dim sizeHeight As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY) - Me.Y_ViewPort(sizeHystogramEpsilon * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)

            Dim freqAs As Double = interVal.countInt
            Dim freqRel As Single = Me.X_ViewPort((interVal.countInt / total) * (ViewPort.Width / (10)), ViewPort, MinX_windows, RangeX) -
                 Me.X_ViewPort(0 * (ViewPort.Width / (n + 1)), ViewPort, MinX_windows, RangeX)

            g.DrawRectangle(Pens.Black, positionX, positionY, freqRel, sizeHeight)
            g.FillRectangle(semiBrushBlue, positionX, positionY, freqRel, sizeHeight)


            Dim text As New StringFormat
            text.Alignment = StringAlignment.Center
            text.LineAlignment = StringAlignment.Center
            Dim rect1 = New Rectangle(positionX - 100, positionY, 100, sizeHeight)
            g.DrawRectangle(Pens.Black, rect1)
            g.FillRectangle(semiBrushOrange, positionX - 100, positionY, 100, sizeHeight - 1)
            g.DrawString(Math.Round(((interVal.countInt / total) * 100), 2) & "% " & Environment.NewLine & interVal.countInt.ToString, SmallFont, Brushes.Black, rect1, text)



            'Dim stringInterval = "( " & interVal.lowerPoint & " - " & interVal.upperPoint & " )"
            'Me.RichTextBox1.AppendText(stringInterval.PadRight(18) & interVal.countInt.ToString & Environment.NewLine)
        Next

        Dim positionXC As Single = Me.X_ViewPort(position * (ViewPort.Width / (n + 1)), ViewPort, MinX_windows, RangeX)
        Dim positionYC As Single = Me.Y_ViewPort((p - epsilon) * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)

        Dim textC As New StringFormat
        textC.Alignment = StringAlignment.Center
        textC.LineAlignment = StringAlignment.Center
        Dim sizeHeightC As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY) - Me.Y_ViewPort(0.02 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        Dim rect1C = New Rectangle(positionXC, positionYC, 100, sizeHeightC)
        g.DrawRectangle(Pens.Black, rect1C)
        g.FillRectangle(semiRed, positionXC, positionYC, 100, sizeHeightC)
        g.DrawString(total.ToString, SmallFont, Brushes.Black, rect1C, textC)


    End Sub

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
        ViewPort.X = 10
        ViewPort.Y = 10

        'ViewPort

        Me.DrawSceneGenerate()

    End Sub


    Private Sub DrawSceneGenerate()

        g.Clear(Color.White)
        ''ViewPort
        Me.g.DrawRectangle(Pens.Transparent, ViewPort)


        Dim X1_Line As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_Line As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim Y_Line As Single = Me.Y_ViewPort(maxR * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        g.DrawLine(semiBlackPen, X1_Line, Y_Line, X2_Line, Y_Line)

        Dim X1_Line0 As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_Line0 As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim Y_Line0 As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        g.DrawLine(semiBlackPen, X1_Line0, Y_Line0, X2_Line0, Y_Line0)



        Dim countPath As Integer = 0
        For Each objectList In listDictionary
            Dim newColor As New Color
            Dim nPen As Pen
            Dim nBrush As SolidBrush

            If (flagSelect = True) Then
                If countPath = indexSelect Then
                    newColor = Color.FromArgb(255, 69, 0)
                    nPen = New Pen(newColor, 0.8)
                    nBrush = New SolidBrush(newColor)
                Else
                    newColor = Color.FromArgb(5, 210, 210, 210)
                    nPen = New Pen(newColor, 0.8)
                    nBrush = New SolidBrush(newColor)
                End If

            Else
                newColor = Color.FromArgb(R.Next(0, 255), R.Next(0, 255), R.Next(0, 255), R.Next(0, 255))
                nPen = New Pen(newColor, 0.8)
                nBrush = New SolidBrush(newColor)
            End If

            For Each kvp As KeyValuePair(Of Integer, Integer) In objectList
                Dim X_Device As Single = Me.X_ViewPort(kvp.Key * (ViewPort.Width / (n + 1)), ViewPort, MinX_windows, RangeX)
                Dim Y_Device As Single = Me.Y_ViewPort((kvp.Value / kvp.Key) * (ViewPort.Height / (maxRow)), ViewPort, MinY_windows, RangeY)

                g.FillEllipse(nBrush, New Rectangle(New Point(X_Device - 3, Y_Device - 3), New Size(3, 3)))

                If (kvp.Key = 1) Then
                    X_Previous = X_Device
                    Y_Previous = Y_Device
                    Continue For
                End If

                g.DrawLine(nPen, X_Previous, Y_Previous, X_Device, Y_Device)

                X_Previous = X_Device
                Y_Previous = Y_Device

            Next

            countPath += 1
        Next
        Dim textC As New StringFormat
        textC.Alignment = StringAlignment.Center
        textC.LineAlignment = StringAlignment.Center

        Dim sizeHeightC As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY) - Me.Y_ViewPort(0.02 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)

        Dim X1_LineP As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_LineP As Single = Me.X_ViewPort(ViewPort.Width + 100, ViewPort, MinX_windows, RangeX)
        Dim Y_LineP As Single = Me.Y_ViewPort(p * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.BlueViolet, X1_LineP, Y_LineP, X2_LineP, Y_LineP)

        Dim XSP_LinePlusEp As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim YSP_LinePlusEp As Single = Me.Y_ViewPort((p) * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)

        Dim rect1P = New Rectangle(XSP_LinePlusEp - 100, YSP_LinePlusEp - sizeHeightC / 2, 100, sizeHeightC - 1)
        g.DrawRectangle(Pens.Black, rect1P)
        g.FillRectangle(semiYellow, XSP_LinePlusEp - 100, YSP_LinePlusEp - sizeHeightC / 2, 100, sizeHeightC - 1)
        g.DrawString("P", SmallFont, Brushes.Black, rect1P, textC)

        Me.PictureBox1.Image = b

    End Sub




    Private Sub DrawSceneEpsilon()

        DrawSceneGenerate()


        If epsilon = Nothing Then
            Exit Sub
        End If
        Dim textC As New StringFormat

        Dim X1_LinePlusEp As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_LinePlusEp As Single = Me.X_ViewPort(ViewPort.Width + 100, ViewPort, MinX_windows, RangeX)
        Dim Y_LinePlusEp As Single = Me.Y_ViewPort((p + epsilon) * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Green, X1_LinePlusEp, Y_LinePlusEp, X2_LinePlusEp, Y_LinePlusEp)

        textC.Alignment = StringAlignment.Center
        textC.LineAlignment = StringAlignment.Center
        Dim sizeHeightC As Single = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY) - Me.Y_ViewPort(0.02 * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        Dim XS_LinePlusEp As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim YS_LinePlusEp As Single = Me.Y_ViewPort((p + epsilon) * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        Dim rect1C = New Rectangle(XS_LinePlusEp - 100, YS_LinePlusEp - sizeHeightC, 100, sizeHeightC)
        g.DrawRectangle(Pens.Black, rect1C)
        g.FillRectangle(semiYellow, XS_LinePlusEp - 100, YS_LinePlusEp - sizeHeightC, 100, sizeHeightC - 1)
        g.DrawString("P + Epsilon", SmallFont, Brushes.Black, rect1C, textC)


        Dim X1_LineMinusEp As Single = Me.X_ViewPort(0, ViewPort, MinX_windows, RangeX)
        Dim X2_LineMinusEp As Single = Me.X_ViewPort(ViewPort.Width + 100, ViewPort, MinX_windows, RangeX)
        Dim Y_LineMinusEp As Single = Me.Y_ViewPort((p - epsilon) * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        g.DrawLine(Pens.Green, X1_LineMinusEp, Y_LineMinusEp, X2_LineMinusEp, Y_LineMinusEp)

        Dim XSM_LinePlusEp As Single = Me.X_ViewPort(ViewPort.Width, ViewPort, MinX_windows, RangeX)
        Dim YSM_LinePlusEp As Single = Me.Y_ViewPort((p - epsilon) * Math.Truncate(ViewPort.Height / maxRow), ViewPort, MinY_windows, RangeY)
        Dim rect1M = New Rectangle(XSM_LinePlusEp - 100, YSM_LinePlusEp, 100, sizeHeightC)
        g.DrawRectangle(Pens.Black, rect1M)
        g.FillRectangle(semiYellow, XSM_LinePlusEp - 100, YSM_LinePlusEp, 100, sizeHeightC - 1)
        g.DrawString("P - Epsilon", SmallFont, Brushes.Black, rect1M, textC)

        Dim X_LineJ As Single = Me.X_ViewPort((j - (j * 15 / 100)) * (ViewPort.Width / (n + 1)), ViewPort, MinX_windows, RangeX)
        Dim Y1_LineJ As Single = Me.Y_ViewPort(0, ViewPort, MinY_windows, RangeY)
        Dim Y2_LineJ As Single = Me.Y_ViewPort(ViewPort.Height, ViewPort, MinY_windows, RangeY)
        'g.DrawLine(Pens.Black, X_LineJ, Y1_LineJ, X_LineJ, Y2_LineJ)
        printContinuousDistributionEpsilon(listIntervalsEpsilonJ, (j - (j * 15 / 100)), countTotalJ)



        Dim X_LineN As Single = Me.X_ViewPort((n - (n * 30 / 100)) * (ViewPort.Width / (n + 1)), ViewPort, MinX_windows, RangeX)
        Dim Y1_LineN As Single = Me.Y_ViewPort(0, ViewPort, MinY_windows, RangeY)
        Dim Y2_LineN As Single = Me.Y_ViewPort(ViewPort.Height, ViewPort, MinY_windows, RangeY)
        'g.DrawLine(Pens.Black, X_LineN, Y1_LineN, X_LineN, Y2_LineN)
        printContinuousDistributionEpsilon(listIntervalsEpsilon, (n - (n * 30 / 100)), countTotal)


        Me.PictureBox1.Image = b

    End Sub


    Function Y_ViewPort(Y_World As Double, ViewPort As Rectangle, MinY As Double, RangeY As Double) As Integer
        Return CInt(ViewPort.Top + ViewPort.Height - ViewPort.Height * (Y_World - MinY) / RangeY)
    End Function

    Function X_ViewPort(X_World As Double, ViewPort As Rectangle, MinX As Double, RangeX As Double) As Integer
        Return CInt(ViewPort.Left + ViewPort.Width * (X_World - MinX) / RangeX)
    End Function

    Public Class sizeIntervals
        Public countInt As Integer
        Public relFrequenc As Double
        Public Percent As Double
        Public lowerPoint As Double
        Public upperPoint As Double
    End Class

    'Initialize the intervals
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

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick

        listDictionary.Clear()
        TextBox1.ForeColor = Color.Black
        TextBox2.ForeColor = Color.Black
        TextBox3.ForeColor = Color.Black

        If (IsNumeric(TextBox1.Text)) AndAlso (Integer.Parse(TextBox1.Text) > 0) Then
            If (IsNumeric(TextBox2.Text)) AndAlso (Integer.Parse(TextBox2.Text) > 0) Then
                If (IsNumeric(TextBox3.Text)) AndAlso (Integer.Parse(TextBox3.Text) > 0) Then
                    n = Integer.Parse(TextBox1.Text)
                    m = Integer.Parse(TextBox2.Text)
                    j = Integer.Parse(TextBox3.Text)

                    For k As Integer = 1 To m
                        Dim dictionaryNumber As New Dictionary(Of Integer, Integer)
                        Dim countShots As Integer = 0
                        For i As Integer = 1 To n
                            Dim numberR As Double = minR + (maxR - minR) * R.NextDouble
                            If (numberR >= minR) AndAlso (numberR < p) Then
                                countShots += 1
                                dictionaryNumber.Add(i, countShots)
                                'Me.RichTextBox1.AppendText("1  ")
                            ElseIf (numberR >= p) AndAlso (numberR < maxR) Then
                                dictionaryNumber.Add(i, countShots)
                                'Me.RichTextBox1.AppendText("0  ")
                            Else
                            End If
                        Next
                        listDictionary.Add(dictionaryNumber)
                        'Me.RichTextBox1.AppendText(Environment.NewLine)
                    Next

                    InitializeGraphics()
                    createTreeView()
                    epsilon = Nothing
                    indexSelect = Nothing

                Else
                    TextBox3.ForeColor = Color.Red
                End If
            Else
                TextBox2.ForeColor = Color.Red
            End If
        Else
            TextBox1.ForeColor = Color.Red
        End If

    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick

        listIntervalsEpsilon.Clear()
        listIntervalsEpsilonJ.Clear()
        TextBox4.ForeColor = Color.Black
        countTotal = 0
        countTotalJ = 0

        If (IsNumeric(TextBox4.Text)) AndAlso (Double.Parse(TextBox4.Text) > 0) Then

            epsilon = Double.Parse(TextBox4.Text)
            sizeHystogramEpsilon = epsilon / 4

            'initialize(listIntervals, p, sizeHystogram)
            'initialize(listIntervalsJ, p, sizeHystogram)

            initialize(listIntervalsEpsilon, p, sizeHystogramEpsilon)
            initialize(listIntervalsEpsilonJ, p, sizeHystogramEpsilon)

            For Each objectList In listDictionary
                For Each kvp As KeyValuePair(Of Integer, Integer) In objectList
                    If ((kvp.Value / kvp.Key) <= (p + epsilon)) AndAlso ((kvp.Value / kvp.Key) >= (p - epsilon)) Then
                        countTotal += 1
                        Dim rangeE As String = findRange(Math.Round((kvp.Value / kvp.Key), 2), listIntervalsEpsilon)
                        calculateContinuousDistribution(sizeHystogramEpsilon, Math.Round((kvp.Value / kvp.Key), 2), listIntervalsEpsilon, rangeE)
                    End If
                    'Me.RichTextBox1.AppendText(Math.Round((kvp.Value / kvp.Key), 2) & "  ")
                    'Dim rangeN As String = findRange(Math.Round((kvp.Value / kvp.Key), 2), listIntervals)

                    'calculateContinuousDistribution(sizeHystogram, Math.Round((kvp.Value / kvp.Key), 2), listIntervals, rangeN)

                    If (kvp.Key <= j) Then
                        If ((kvp.Value / kvp.Key) <= (p + epsilon)) AndAlso ((kvp.Value / kvp.Key) >= (p - epsilon)) Then
                            countTotalJ += 1
                            Dim rangeE As String = findRange(Math.Round((kvp.Value / kvp.Key), 2), listIntervalsEpsilonJ)
                            calculateContinuousDistribution(sizeHystogramEpsilon, Math.Round((kvp.Value / kvp.Key), 2), listIntervalsEpsilonJ, rangeE)
                        End If
                        'calculateContinuousDistribution(sizeHystogram, Math.Round((kvp.Value / kvp.Key), 2), listIntervalsJ, rangeN)
                    End If

                Next
                'Me.RichTextBox1.AppendText(Environment.NewLine)
            Next

            If (epsilon < 0.2) Then
                SmallFont = New Font("Calibri", 8, FontStyle.Regular, GraphicsUnit.Pixel)
            Else
                SmallFont = New Font("Calibri", 13, FontStyle.Regular, GraphicsUnit.Pixel)
            End If
            DrawSceneEpsilon()
        Else
            TextBox4.ForeColor = Color.Red
        End If
    End Sub



    Private Sub createTreeView()

        Me.TreeView1.Nodes.Clear()
        indexSelect = Nothing
        flagSelect = False

        Dim root = New TreeNode("All")
        Me.TreeView1.Nodes.Add(root)

        Dim countNode As Integer = 1

        For Each objectList In listDictionary

            Dim stringName As String = "Path  " & countNode.ToString

            Me.TreeView1.Nodes(0).Nodes.Add(New TreeNode(stringName))


            For Each kvp As KeyValuePair(Of Integer, Integer) In objectList
                Dim X As Double = kvp.Key
                Dim Y As Double = kvp.Value / kvp.Key

                TreeView1.Nodes(0).Nodes(countNode - 1).Nodes.Add(New TreeNode(X.ToString & " / " & Y.ToString))
            Next

            countNode += 1
        Next

        For Each tn As TreeNode In TreeView1.Nodes
            tn.Expand()
        Next
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

        If Me.TreeView1.SelectedNode.Text <> "All" Then
            If TreeView1.SelectedNode.Level = 1 Then
                indexSelect = TreeView1.SelectedNode.Index
                flagSelect = True
                DrawSceneEpsilon()
            End If
        ElseIf Me.TreeView1.SelectedNode.Text = "All" Then
            indexSelect = Nothing
            flagSelect = False
            DrawSceneEpsilon()
        Else

        End If
    End Sub
End Class
