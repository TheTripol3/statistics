
Imports System.Globalization

Public Class Form1

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

    Dim semiBrushYellow As New SolidBrush(Color.FromArgb(90, Color.Yellow))
    Dim semiBrushOrange As New SolidBrush(Color.FromArgb(90, Color.Orange))
    Dim semiBlackPen As New Pen(Color.FromArgb(80, Color.Black), 0.5)
    Dim j As Integer = 0
    Dim l As Double = 0

    Dim sc As New MSScriptControl.ScriptControl
    Dim lastSum As Double = 0


    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick
        Dim insertText = "Sqr(number) "

        Dim insertPos As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Text = RichTextBox1.Text.Insert(insertPos, insertText)
        RichTextBox1.SelectionStart = insertPos + insertText.Length
    End Sub

    Private Sub PictureBox2_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseClick
        Dim insertText = "(number)^(1/root)"

        Dim insertPos As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Text = RichTextBox1.Text.Insert(insertPos, insertText)
        RichTextBox1.SelectionStart = insertPos + insertText.Length
    End Sub

    Private Sub PictureBox3_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox3.MouseClick
        Dim insertText = "^(number)"

        Dim insertPos As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Text = RichTextBox1.Text.Insert(insertPos, insertText)
        RichTextBox1.SelectionStart = insertPos + insertText.Length
    End Sub

    Private Sub PictureBox4_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox4.MouseClick
        Dim insertText = "(numerator)/(denominator) "

        Dim insertPos As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Text = RichTextBox1.Text.Insert(insertPos, insertText)
        RichTextBox1.SelectionStart = insertPos + insertText.Length
    End Sub

    Private Sub PictureBox5_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox5.MouseClick
        Dim insertText = "+"

        Dim insertPos As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Text = RichTextBox1.Text.Insert(insertPos, insertText)
        RichTextBox1.SelectionStart = insertPos + insertText.Length
    End Sub

    Private Sub PictureBox6_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox6.MouseClick
        Dim insertText = "-"

        Dim insertPos As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Text = RichTextBox1.Text.Insert(insertPos, insertText)
        RichTextBox1.SelectionStart = insertPos + insertText.Length
    End Sub

    Private Sub PictureBox7_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox7.MouseClick
        Dim insertText = "*"

        Dim insertPos As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Text = RichTextBox1.Text.Insert(insertPos, insertText)
        RichTextBox1.SelectionStart = insertPos + insertText.Length
    End Sub

    Private Sub PictureBox8_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox8.MouseClick
        Dim insertText = "/"

        Dim insertPos As Integer = RichTextBox1.SelectionStart
        RichTextBox1.Text = RichTextBox1.Text.Insert(insertPos, insertText)
        RichTextBox1.SelectionStart = insertPos + insertText.Length
    End Sub


    Private Sub CheckedListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseClick
        Dim idx, sidx As Integer
        sidx = CheckedListBox1.SelectedIndex
        For idx = 0 To CheckedListBox1.Items.Count - 1
            If idx <> sidx Then
                CheckedListBox1.SetItemChecked(idx, False)
            Else
                CheckedListBox1.SetItemChecked(sidx, True)
            End If
        Next
    End Sub

    Private Sub CheckedListBox2_MouseClick(sender As Object, e As MouseEventArgs) Handles CheckedListBox2.MouseClick
        Dim idx, sidx As Integer
        sidx = CheckedListBox2.SelectedIndex
        For idx = 0 To CheckedListBox2.Items.Count - 1
            If idx <> sidx Then
                CheckedListBox2.SetItemChecked(idx, False)
            Else
                CheckedListBox2.SetItemChecked(sidx, True)
            End If
        Next

        If (CheckedListBox2.GetItemChecked(CheckedListBox2.Items.IndexOf("Definite"))) Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            'Else
            '    TextBox1.Text = "- inf"
            '    TextBox2.Text = "+ inf"
            '    TextBox3.Text = "Max"
            '    TextBox1.Enabled = False
            '    TextBox2.Enabled = False
        End If
    End Sub


    Private Sub calculationRiemannDefinite(a As Double, b As Double, equation As String, numberInterval As Double, k As Integer, n As Integer)
        Me.RichTextBox2.Clear()


        Dim dX As Double = (b - a) / Math.Abs(numberInterval)
        Dim sum As Double = 0
        'point, height
        Dim dictionaryRiemann As New SortedDictionary(Of Double, Double)



        Try
            For i As Integer = k To n
                Dim stringReplace As String = equation.Replace("x", (a + dX * i))
                Dim stringFinal As String = "(" & stringReplace & ")" & " * " & dX.ToString
                Dim Result As Double = Convert.ToDouble(sc.Eval(stringFinal))
                sum += Result

                Dim stringHeight As String = "(" & stringReplace & ")"
                Dim ResultHeight As Double = Convert.ToDouble(sc.Eval(stringHeight))
                'Me.RichTextBox2.AppendText(stringFinal & "    " & ResultHeight & Environment.NewLine)

                dictionaryRiemann.Add(Math.Round((a + dX * i), 2), Math.Round(ResultHeight, 2))
            Next

            Dim flagGener As String = Nothing
            If (k = 0) Then
                Dim stringReplace As String = equation.Replace("x", (a + dX * (n + 1)))
                Dim stringHeight As String = "(" & stringReplace & ")"
                Dim ResultHeight As Double = Convert.ToDouble(sc.Eval(stringHeight))

                dictionaryRiemann.Add(Math.Round(a + dX * (n + 1), 2), Math.Round(ResultHeight, 2))

                flagGener = "Left"
            ElseIf (k = 1) Then
                Dim stringReplace As String = equation.Replace("x", (a + dX * (0)))
                Dim stringHeight As String = "(" & stringReplace & ")"
                Dim ResultHeight As Double = Convert.ToDouble(sc.Eval(stringHeight))

                dictionaryRiemann.Add(Math.Round(a + dX * (0), 2), Math.Round(ResultHeight, 2))
                flagGener = "Right"
            End If

            Me.DrawScene(dictionaryRiemann, dX, equation, flagGener)
            Me.RichTextBox2.AppendText("The summation is  " & sum.ToString)


        Catch ex As Exception
            Me.RichTextBox2.AppendText("Not a valid math formula for a double.")
        End Try

    End Sub




    Private Sub calculationLebesgueDefinite(a As Double, b As Double, equation As String, numberInterval As Double, k As Integer)
        Me.RichTextBox3.Clear()

        Dim stringFinal As String = equation.Replace("x = ", "")


        Dim dY As Double = (b - a) / (2 * numberInterval)


        Dim sum As Double = 0
        'point, height
        Dim dictionaryLebesgue As New SortedDictionary(Of Double, Tuple(Of Double, Double))

        Try
            For y As Integer = k To 10000
                Dim stringReplaceY As String = stringFinal.Replace("y", (dY * y))
                Dim ResultX As Double = Convert.ToDouble(sc.Eval(stringReplaceY))

                If ResultX < a Then
                    Continue For
                End If

                If ResultX > b Then
                    Exit For
                End If

                Dim height
                If ResultX = a Then
                    height = y * dY
                Else
                    height = dY
                End If

                Dim baseX As Double = b - ResultX
                Dim stringArea As String = "(" & height & ")" & " * " & baseX.ToString

                Dim area As Double = Convert.ToDouble(sc.Eval(stringArea))
                'Me.RichTextBox3.AppendText("X " & ResultX.ToString & "     Y " & (dY * y) & "     dX " & (baseX) & "     Area " & (area) & Environment.NewLine)


                sum += area
                lastSum = sum
                Dim widthPoint As Tuple(Of Double, Double) = New Tuple(Of Double, Double)(ResultX, baseX)
                dictionaryLebesgue.Add(Math.Round((dY * y), 2), widthPoint)
            Next

            Me.DrawSceneLebesgue(dictionaryLebesgue, stringFinal, dY)
            Me.RichTextBox3.AppendText("The summation is  " & sum.ToString)


        Catch ex As Exception
            Me.RichTextBox3.AppendText("The summation is  " & lastSum.ToString)
            Me.RichTextBox4.AppendText("Not a valid math formula for a double.")
            Timer2.Stop()
            Button4.Text = "Start Timer"
            l = 0
        End Try


    End Sub


    Private Sub checkInverese(equation As String)
        Dim stringReplace As String = equation.Replace("x", "y")
        If (stringReplace.Contains("+")) Then
            stringReplace = stringReplace.Replace("+", "-")
        ElseIf (stringReplace.Contains("-")) Then
            stringReplace = stringReplace.Replace("-", "+")
        End If


        If (stringReplace.Contains("/")) Then
            stringReplace = stringReplace.Replace("/", "*")
        ElseIf (stringReplace.Contains("*")) Then
            stringReplace = stringReplace.Replace("*", "/")
        End If

        If (stringReplace.Contains("Sqr")) Then
            stringReplace = stringReplace.Replace("Sqr", "")
            stringReplace = stringReplace.Replace(")", ")^2")
        End If

        Me.RichTextBox4.AppendText("x = " & stringReplace)
        Me.Button5.Enabled = True
        Me.Button5.Visible = True
    End Sub


    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        If (CheckedListBox2.GetItemChecked(CheckedListBox2.Items.IndexOf("Definite"))) Then

            If RichTextBox1.Text <> "" Then
                Dim equation = RichTextBox1.Text.Trim
                If IsNumeric(TextBox1.Text) Then
                    Dim a = TextBox1.Text
                    If IsNumeric(TextBox2.Text) Then
                        Dim b = TextBox2.Text
                        If IsNumeric(TextBox3.Text) Then
                            Dim N = CDbl(TextBox3.Text)
                            If (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Left Riemann sum"))) Then
                                calculationRiemannDefinite(CDbl(a), CDbl(b), equation, N, 0, N - 1)
                            Else
                                calculationRiemannDefinite(CDbl(a), CDbl(b), equation, N, 1, N)
                            End If
                            Else
                            Me.RichTextBox2.AppendText("Choose N")
                        End If
                    Else
                        Me.RichTextBox2.AppendText("Choose B")
                    End If
                Else
                    Me.RichTextBox2.AppendText("Choose A")
                End If
            Else
                Me.RichTextBox2.AppendText("Write an equation!")
            End If
        End If
    End Sub




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture
        CheckedListBox1.SetItemChecked(0, True)
        CheckedListBox2.SetItemChecked(0, True)

        Me.PictureBox11.Image = Nothing
        InitializeGraphics()

        TextBox1.Text = 2
        TextBox2.Text = 8
        TextBox3.Text = 2
        RichTextBox1.Text = "x+1"

        sc.Language = "VBSCRIPT"

    End Sub

    Private Sub CheckBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles CheckBox1.MouseClick
        Me.RichTextBox1.Clear()
        Me.RichTextBox2.Clear()
        CheckedListBox1.SetItemChecked(0, True)
        CheckedListBox1.SetItemChecked(1, False)
        CheckedListBox2.SetItemChecked(0, True)
        CheckBox1.Checked = False
        g.Clear(Color.White)
        Me.PictureBox11.Image = b
        Me.PictureBox12.Image = b
        Me.RichTextBox4.Clear()
        Me.RichTextBox3.Clear()
        Me.Button5.Enabled = False
        Me.Button5.Visible = False

    End Sub

    Private Sub PictureBox9_MouseClick(sender As Object, e As MouseEventArgs)
        If (TextBox1.Text = "") Then
            TextBox1.Text = "+infinity"
        ElseIf (TextBox2.Text = "") Then
            TextBox2.Text = "+infinity"
        End If
    End Sub

    Private Sub PictureBox10_MouseClick(sender As Object, e As MouseEventArgs)
        If (TextBox1.Text = "") Then
            TextBox1.Text = "-infinity"
        ElseIf (TextBox2.Text = "") Then
            TextBox2.Text = "-infinity"
        End If
    End Sub

    Private Sub InitializeGraphics()
        Me.b = New Bitmap(Me.PictureBox11.Width, Me.PictureBox11.Height)
        Me.g = Graphics.FromImage(b)
        Me.g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        MinX_windows = 0
        MaxX_windows = PictureBox11.Width - 10
        MinY_windows = 0
        MaxY_windows = PictureBox11.Height - 10

        RangeX = MaxX_windows - MinX_windows
        RangeY = MaxY_windows - MinY_windows

        ViewPort.Width = Math.Abs(MinX_windows) + Math.Abs(MaxX_windows)
        ViewPort.Height = Math.Abs(MinY_windows) + Math.Abs(MaxY_windows)
        ViewPort.X = 0
        ViewPort.Y = 0



        Me.b = New Bitmap(Me.PictureBox12.Width, Me.PictureBox12.Height)
        Me.g = Graphics.FromImage(b)
        Me.g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Me.g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        MinX_windows = 0
        MaxX_windows = PictureBox11.Width - 10
        MinY_windows = 0
        MaxY_windows = PictureBox11.Height - 10

        RangeX = MaxX_windows - MinX_windows
        RangeY = MaxY_windows - MinY_windows

        ViewPort.Width = Math.Abs(MinX_windows) + Math.Abs(MaxX_windows)
        ViewPort.Height = Math.Abs(MinY_windows) + Math.Abs(MaxY_windows)
        ViewPort.X = 0
        ViewPort.Y = 0

        'ViewPort

        'Me.DrawScene()

    End Sub

    Function Y_ViewPort(Y_World As Double, ViewPort As Rectangle, MinY As Double, RangeY As Double) As Integer

        Return CInt(ViewPort.Top + ViewPort.Height - ViewPort.Height * (Y_World - MinY) / RangeY)

    End Function

    'Sub to transformm X and Y

    Function X_ViewPort(X_World As Double, ViewPort As Rectangle, MinX As Double, RangeX As Double) As Integer

        Return CInt(ViewPort.Left + ViewPort.Width * (X_World - MinX) / RangeX)

    End Function




    Private Sub DrawSceneLebesgue(dictionaryLebesgue As SortedDictionary(Of Double, Tuple(Of Double, Double)), equation As String, dY As Double)

        g.Clear(Color.White)
        ''ViewPort
        Me.g.DrawRectangle(Pens.Transparent, ViewPort)

        'Me.RichTextBox4.AppendText("Count" & dictionaryLebesgue.Count & Environment.NewLine)

        Dim firstElement As Double = dictionaryLebesgue.ElementAt(0).Key
        Dim secondElement As Double = dictionaryLebesgue.ElementAt(1).Key
        Dim interval As Double = secondElement - firstElement

        Me.RichTextBox4.AppendText(Environment.NewLine & Environment.NewLine)


        For k As Integer = -(firstElement + 20) To dictionaryLebesgue.Count + 20

            Dim y As Double = firstElement + interval * k
            Dim stringReplaceY As String = equation.Replace("y", y)
            Dim x As Double = Convert.ToDouble(sc.Eval(stringReplaceY))

            Dim positionX As Single = Me.X_ViewPort(x * Math.Truncate(ViewPort.Width / (dictionaryLebesgue.Values.Max.Item1 + 0.1 + dY)), ViewPort, MinX_windows, RangeX)
            Dim positionY As Single = Me.Y_ViewPort((y) * Math.Truncate(ViewPort.Height / (dictionaryLebesgue.Keys.Max + 1)), ViewPort, MinY_windows, RangeY)



            Dim yN As Double = firstElement + interval * (k + 1)
            Dim stringReplaceYN As String = equation.Replace("y", yN)
            Dim xN As Double = Convert.ToDouble(sc.Eval(stringReplaceYN))

            Dim positionXN As Single = Me.X_ViewPort(xN * Math.Truncate(ViewPort.Width / (dictionaryLebesgue.Values.Max.Item1 + 0.1 + dY)), ViewPort, MinX_windows, RangeX)
            Dim positionYN As Single = Me.Y_ViewPort((yN) * Math.Truncate(ViewPort.Height / (dictionaryLebesgue.Keys.Max + 1)), ViewPort, MinY_windows, RangeY)

            g.DrawLine(Pens.Black, positionX, positionY, positionXN, positionYN)
            'g.FillEllipse(Brushes.Black, New Rectangle(New Point(positionXN - 3, positionYN - 3), New Size(4, 4)))
        Next



        For k As Integer = 0 To dictionaryLebesgue.Count - 1

            Dim y As Double = dictionaryLebesgue.ElementAt(k).Key

            Dim sizeWidth As Tuple(Of Double, Double) = dictionaryLebesgue.ElementAt(k).Value
            Dim x As Double = sizeWidth.Item1
            Dim baseX As Double = sizeWidth.Item2


            Dim positionX As Single = Me.X_ViewPort(x * Math.Truncate(ViewPort.Width / (dictionaryLebesgue.Values.Max.Item1 + 0.1 + dY)), ViewPort, MinX_windows, RangeX)
            Dim positionY As Single = Me.Y_ViewPort((y) * Math.Truncate(ViewPort.Height / (dictionaryLebesgue.Keys.Max + 1)), ViewPort, MinY_windows, RangeY)

            If x = 0 Then
                Continue For
            End If

            Dim heightRect
            If k = 0 Then
                heightRect = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / (dictionaryLebesgue.Keys.Max + 1)), ViewPort, MinY_windows, RangeY) -
    Me.Y_ViewPort((y) * Math.Truncate(ViewPort.Height / (dictionaryLebesgue.Keys.Max + 1)), ViewPort, MinY_windows, RangeY)
            Else
                heightRect = Me.Y_ViewPort(0 * Math.Truncate(ViewPort.Height / (dictionaryLebesgue.Keys.Max + 1)), ViewPort, MinY_windows, RangeY) -
    Me.Y_ViewPort((dY) * Math.Truncate(ViewPort.Height / (dictionaryLebesgue.Keys.Max + 1)), ViewPort, MinY_windows, RangeY)
            End If

            Dim sizeRect As Single = Me.X_ViewPort(((baseX)) * Math.Truncate(ViewPort.Width / (dictionaryLebesgue.Values.Max.Item1 + 0.1 + dY)), ViewPort, MinX_windows, RangeX)

            g.DrawRectangle(semiBlackPen, positionX, positionY, sizeRect, Math.Abs(heightRect))
            g.FillRectangle(semiBrushOrange, positionX, positionY, sizeRect, Math.Abs(heightRect))

            'Me.RichTextBox4.AppendText("x " & positionX.ToString & "     position " & (sizeRect) & Environment.NewLine)
            'Me.RichTextBox4.AppendText("X " & x.ToString & "     Y " & (y) & "     dX " & (baseX) & "     dY " & (dY) & Environment.NewLine)
        Next

        Me.PictureBox12.Image = b

    End Sub


    Private Sub DrawScene(dictionaryRiemann As SortedDictionary(Of Double, Double), width As Double, equation As String, flagGener As String)

        g.Clear(Color.White)
        ''ViewPort
        Me.g.DrawRectangle(Pens.Transparent, ViewPort)

        'Me.RichTextBox4.AppendText("Count" & dictionaryRiemann.Count & Environment.NewLine)

        Dim firstElement As Double = dictionaryRiemann.ElementAt(0).Key
        Dim secondElement As Double = dictionaryRiemann.ElementAt(1).Key

        Dim interval As Double = secondElement - firstElement
        Dim stepS As Double = 0.001
        For k As Double = 0 To (dictionaryRiemann.Count + 20) Step stepS
            Dim point As Double = firstElement + k * interval
            Dim stringEquation As String = equation.Replace("x", point)
            Dim equationY As Double = Convert.ToDouble(sc.Eval(stringEquation))
            Dim X1_LineEq As Single = Me.X_ViewPort(k * Math.Truncate(ViewPort.Width / (dictionaryRiemann.Count + 1)), ViewPort, MinX_windows, RangeX)
            Dim Y1_LineEq As Single = Me.Y_ViewPort(equationY * Math.Truncate(ViewPort.Height / (dictionaryRiemann.Values.Max)), ViewPort, MinY_windows, RangeY)


            Dim pointN As Double = firstElement + (k + stepS) * interval
            Dim stringEquationN As String = equation.Replace("x", pointN)
            Dim equationYN As Double = Convert.ToDouble(sc.Eval(stringEquationN))

            Dim X1_LineEqN As Single = Me.X_ViewPort((k + stepS) * Math.Truncate(ViewPort.Width / (dictionaryRiemann.Count + 1)), ViewPort, MinX_windows, RangeX)
            Dim Y1_LineEqN As Single = Me.Y_ViewPort(equationYN * Math.Truncate(ViewPort.Height / (dictionaryRiemann.Values.Max)), ViewPort, MinY_windows, RangeY)

            g.DrawLine(Pens.Black, X1_LineEq, Y1_LineEq, X1_LineEqN, Y1_LineEqN)

        Next




        For k As Integer = 0 To dictionaryRiemann.Count - 1

            If flagGener = "Left" Then
                If (k = dictionaryRiemann.Count - 1) Then
                    Exit For
                End If
                Dim point As Double = dictionaryRiemann.ElementAt(k).Key
                Dim height As Double = dictionaryRiemann.ElementAt(k).Value

                Dim X1_Line As Single = Me.X_ViewPort(k * Math.Truncate(ViewPort.Width / (dictionaryRiemann.Count + 1)), ViewPort, MinX_windows, RangeX)
                Dim Y1_Line As Single = Me.Y_ViewPort(0, ViewPort, MinY_windows, RangeY)
                Dim Y2_Line As Single = Me.Y_ViewPort(height * Math.Truncate(ViewPort.Height / (dictionaryRiemann.Values.Max)), ViewPort, MinY_windows, RangeY)

                'g.DrawLine(Pens.Black, X1_Line, Y1_Line, X1_Line, Y2_Line)
                'Me.RichTextBox4.AppendText(point & Environment.NewLine)

                Dim sizeRect As Single = Me.X_ViewPort(Math.Truncate(ViewPort.Width / (dictionaryRiemann.Count + 1)), ViewPort, MinX_windows, RangeX)
                Dim heightRectangle As Single = Y1_Line - Y2_Line
                Dim rect1 = New Rectangle(X1_Line, Y2_Line, sizeRect, heightRectangle)
                g.DrawRectangle(semiBlackPen, rect1)
                g.FillRectangle(semiBrushYellow, rect1)

            ElseIf flagGener = "Right" Then

                If (k = 0) Then
                    Continue For
                End If
                Dim point As Double = dictionaryRiemann.ElementAt(k).Key
                Dim height As Double = dictionaryRiemann.ElementAt(k).Value

                Dim X1_Line As Single = Me.X_ViewPort((k - 1) * Math.Truncate(ViewPort.Width / (dictionaryRiemann.Count + 1)), ViewPort, MinX_windows, RangeX)
                Dim Y1_Line As Single = Me.Y_ViewPort(0, ViewPort, MinY_windows, RangeY)
                Dim Y2_Line As Single = Me.Y_ViewPort(height * Math.Truncate(ViewPort.Height / (dictionaryRiemann.Values.Max)), ViewPort, MinY_windows, RangeY)

                Dim sizeRect As Single = Me.X_ViewPort(Math.Truncate(ViewPort.Width / (dictionaryRiemann.Count + 1)), ViewPort, MinX_windows, RangeX)
                Dim heightRectangle As Single = Y1_Line - Y2_Line
                Dim rect1 = New Rectangle(X1_Line, Y2_Line, sizeRect, heightRectangle)
                g.DrawRectangle(semiBlackPen, rect1)
                g.FillRectangle(semiBrushOrange, rect1)
            Else

            End If

        Next

        Me.PictureBox11.Image = b

    End Sub

    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        g.Dispose()
        InitializeGraphics()
    End Sub

    Private Sub Button3_MouseClick(sender As Object, e As MouseEventArgs) Handles Button3.MouseClick
        If (Timer1.Enabled) Then
            Timer1.Stop()
            Button3.Text = "Start Timer"
            j = 0
        Else
            Timer1.Start()
            Button3.Text = "Stop Timer"
        End If

    End Sub

    Private Sub Button4_MouseClick(sender As Object, e As MouseEventArgs) Handles Button4.MouseClick
        If (Timer2.Enabled) Then
            Timer2.Stop()
            Button4.Text = "Start Timer"
            l = 0
        Else
            Timer2.Start()
            Button4.Text = "Stop Timer"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If RichTextBox1.Text <> "" Then
            Dim equation = RichTextBox1.Text.Trim
            If IsNumeric(TextBox1.Text) Then
                Dim a = TextBox1.Text
                If IsNumeric(TextBox2.Text) Then
                    Dim b = TextBox2.Text
                    If IsNumeric(TextBox3.Text) Then
                        Dim N = CDbl(TextBox3.Text)
                        If (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Left Riemann sum"))) Then
                            calculationRiemannDefinite(CDbl(a), CDbl(b), equation, N + j, 0, N + j - 1)
                        Else
                            calculationRiemannDefinite(CDbl(a), CDbl(b), equation, N + j, 1, N + j)
                        End If
                    Else
                        Me.RichTextBox2.AppendText("Choose N")
                    End If
                Else
                    Me.RichTextBox2.AppendText("Choose B")
                End If
            Else
                Me.RichTextBox2.AppendText("Choose A")
            End If
        Else
            Me.RichTextBox2.AppendText("Write an equation!")
        End If

        j += 1
    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        Me.Button5.Enabled = False
        Me.Button5.Visible = False
        Me.RichTextBox4.Clear()
        g.Clear(Color.White)
        Me.PictureBox12.Image = b

        If RichTextBox1.Text <> "" Then
            Dim equation = RichTextBox1.Text.Trim
            If IsNumeric(TextBox1.Text) Then
                'Dim a = TextBox1.Text
                If IsNumeric(TextBox2.Text) Then
                    'Dim b = TextBox2.Text
                    If IsNumeric(TextBox3.Text) Then
                        'Dim N = CDbl(TextBox3.Text)
                        checkInverese(equation)
                    Else
                        Me.RichTextBox2.AppendText("Choose N")
                    End If
                Else
                    Me.RichTextBox2.AppendText("Choose B")
                End If
            Else
                Me.RichTextBox2.AppendText("Choose A")
            End If
        Else
            Me.RichTextBox2.AppendText("Write an equation!")
        End If
    End Sub

    Private Sub Button5_MouseClick(sender As Object, e As MouseEventArgs) Handles Button5.MouseClick
        Me.Button5.Enabled = False
        Me.Button5.Visible = False

        If RichTextBox4.Text <> "" Then
            Dim equation = RichTextBox4.Text.Trim
            If IsNumeric(TextBox1.Text) Then
                Dim a = TextBox1.Text
                If IsNumeric(TextBox2.Text) Then
                    Dim b = TextBox2.Text
                    If IsNumeric(TextBox3.Text) Then
                        Dim N = CDbl(TextBox3.Text)
                        calculationLebesgueDefinite(CDbl(a), CDbl(b), equation, N, 0)
                    Else
                        Me.RichTextBox3.AppendText("Choose N")
                    End If
                Else
                    Me.RichTextBox3.AppendText("Choose B")
                End If
            Else
                Me.RichTextBox3.AppendText("Choose A")
            End If
        Else
            Me.RichTextBox3.AppendText("Write an equation!")
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        If RichTextBox4.Text <> "" Then
            Dim equation = RichTextBox4.Text.Trim
            If IsNumeric(TextBox1.Text) Then
                Dim a = TextBox1.Text
                If IsNumeric(TextBox2.Text) Then
                    Dim b = TextBox2.Text
                    If IsNumeric(TextBox3.Text) Then
                        Dim N = CDbl(TextBox3.Text)
                        calculationLebesgueDefinite(CDbl(a), CDbl(b), equation, N + l, 0)
                    Else
                        Me.RichTextBox3.AppendText("Choose N")
                    End If
                Else
                    Me.RichTextBox3.AppendText("Choose B")
                End If
            Else
                Me.RichTextBox3.AppendText("Choose A")
            End If
        Else
            Me.RichTextBox3.AppendText("Write an equation!")
        End If
        l += 1
    End Sub
End Class
