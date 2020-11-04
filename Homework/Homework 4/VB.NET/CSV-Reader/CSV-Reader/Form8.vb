Imports System.Globalization

Public Class Form8

    Dim countFlag As Integer
    Dim listColumn As List(Of Form3.listAllColumn)
    Dim listCp As List(Of Form3.listCaption)
    Dim order As Integer
    Dim flag As Boolean = False
    Public flagDistribution As Boolean = Nothing
    Dim sum = 0.0
    Dim c = 0.0
    Public count = 0
    Dim average = 0.0
    Public FrequencyDistribution As New SortedDictionary(Of Integer, frequency)
    Public lisIntervalY As New List(Of intervals)
    Public lisIntervalX As New List(Of intervals)
    Dim checkInt As Integer = 0
    Dim checkFlag As Boolean = False
    Dim listObject As New List(Of String)


    'Distribution
    Public listX As New List(Of Double)
    Public listY As New List(Of Double)
    Public listInt As New List(Of DataPoint_Numeric)
    Dim countN As Integer
    'Public scatter As New scatterPlot
    Public ListVariables As New List(Of List(Of Double))
    Public FrequencyDistributionContinousX As SortedDictionary(Of intervals, frequency)
    Public FrequencyDistributionContinousY As SortedDictionary(Of intervals, frequency)
    Public ListOfBivariateObservation As New List(Of bivariateNumericObservation)

    Public startingPointX As Double
    Public startingPointY As Double
    Public sizeIntervalX As Double
    Public sizeIntervalY As Double

    Public averageX As Double = Nothing
    Public averageY As Double = Nothing


    Public DistinctValues_FirstVariable As New HashSet(Of intervals)
    Public DistinctValues_SecondVariable As New HashSet(Of intervals)
    Public FrequencyDistributionBivariateContinous As Dictionary(Of Tuple(Of intervals, intervals), frequency)

    Public flagFirstTime As Boolean = True

    Public rows As Integer



    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        resetCheck()
        createTreeView()

    End Sub


    'Create TreeView
    Private Sub createTreeView()

        countFlag = Form3.countFlag
        listColumn = Form3.listColumn
        listCp = Form3.listCp


        Me.TreeView1.Nodes.Clear()
        Me.TreeView1.ExpandAll()
        Me.TreeView1.CheckBoxes = True

        Dim root = New TreeNode(Form1.Label4.Text)
        Me.TreeView1.Nodes.Add(root)

        Dim countNode As Integer = 0

        For Each objectC1 In listCp
            For Each objectC2 In listColumn

                Dim flagCount As Integer = 0
                Dim typeV As String

                If objectC2.typeT = Nothing Then
                    typeV = "ND"
                Else
                    typeV = objectC2.typeT.ToString()
                End If

                Try
                    Me.TreeView1.Nodes(0).Nodes.Add(New TreeNode(objectC1.Lists.ElementAt(countNode) & "  (" & typeV & ")"))
                Catch ex As Exception
                    Me.TreeView1.Nodes(0).Nodes.Add(New TreeNode("ND"))
                End Try

                countNode += 1

            Next
        Next
        Me.TreeView1.ExpandAll()
    End Sub


    Sub printData(listXY As List(Of intervals), flagType As String, flagVariable As String)
        Dim k As Type = Form3.listColumn.ElementAt(flagType).typeT


        If (listCp(0).FlagCaption) Then

            For s As Integer = 1 To (rows)
                Dim a

                If listColumn.ElementAt(flagType).ListColumnAll.ElementAt(s) = "" OrElse listColumn.ElementAt(flagType).ListColumnAll.ElementAt(s) = "NA" Then

                    Select Case k.ToString
                        Case "System.Boolean"
                            a = ""
                        Case "System.Int32"
                            a = 0
                        Case "System.Int64"
                            a = 0
                        Case "System.Double"
                            a = 0
                        Case "System.DateTime"
                            a = ""
                        Case Else
                            a = ""
                    End Select
                Else
                    a = Convert.ChangeType(listColumn.ElementAt(flagType).ListColumnAll.ElementAt(s), k, CultureInfo.InvariantCulture)

                End If


                If flagVariable = "X" Then
                    Dim dataP As New DataPoint_Numeric
                    dataP.X = a
                    dataP.Y = Nothing
                    listInt.Add(dataP)
                    listX.Add(a)
                Else
                    listInt(s - 1).Y = a
                    listY.Add(a)
                End If


            Next
        Else

            For s As Integer = 0 To (rows - 1)
                Dim a



                If listColumn.ElementAt(flagType).ListColumnAll.ElementAt(s) = "" OrElse listColumn.ElementAt(flagType).ListColumnAll.ElementAt(s) = "NA" Then

                    Select Case k.ToString
                        Case "System.Boolean"
                            a = ""
                        Case "System.Int32"
                            a = 0
                        Case "System.Int64"
                            a = 0
                        Case "System.Double"
                            a = 0
                        Case "System.DateTime"
                            a = ""
                        Case Else
                            a = ""
                    End Select
                Else
                    a = Convert.ChangeType(listColumn.ElementAt(flagType).ListColumnAll.ElementAt(s), k, CultureInfo.InvariantCulture)

                End If

                If flagVariable = "X" Then
                    Dim dataP As New DataPoint_Numeric
                    dataP.X = a
                    dataP.Y = Nothing
                    listInt.Add(dataP)
                    listX.Add(a)
                Else
                    listInt(s).Y = a
                    listY.Add(a)
                End If


            Next

        End If

    End Sub

    'Select the item to calculate the distribution and calculate
    Private Sub TreeView1_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterCheck



        For Each ob As TreeNode In TreeView1.Nodes
            chknode(ob)
        Next

        If checkFlag Then
            TreeView1.CheckBoxes = False
            TreeView1.ExpandAll()
        End If

        If (checkInt > 2) Then
            TreeView1.CheckBoxes = False
            TreeView1.ExpandAll()
            flag = True
        End If

        calculate()

    End Sub


    Sub calculate()
        Try

            If flag Then

                listX.Clear()
                listY.Clear()
                listInt.Clear()
                ListVariables.Clear()
                Form10.DataGridView1.Rows.Clear()
                Form10.DataGridView1.Columns.Clear()
                lisIntervalX.Clear()
                lisIntervalY.Clear()
                FrequencyDistributionBivariateContinous = Nothing
                FrequencyDistributionContinousX = Nothing
                FrequencyDistributionContinousY = Nothing
                ListOfBivariateObservation.Clear()
                FrequencyDistribution.Clear()
                Me.Label26.Text = ""


                Dim variableX As String = listObject.ElementAt(0)
                Dim variableY As String = listObject.ElementAt(1)


                printData(lisIntervalX, variableX, "X")
                    printData(lisIntervalY, variableY, "Y")




                kahanSub(listX)
                    Me.Label6.Text = listCp(0).Lists.ElementAt(variableX)
                    Me.Label7.Text = (rows)
                    Me.Label9.Text = sum
                    Me.Label11.Text = average
                    averageX = average

                    If flagFirstTime Then

                        startingPointX = Math.Round(averageX, 2)
                        sizeIntervalX = Math.Round(sum / (listColumn.ElementAt(variableX).ListColumnAll.Count - 1) * 10 / 100, 2)
                    End If


                    kahanSub(listY)
                    Me.Label20.Text = listCp(0).Lists.ElementAt(variableY)
                    Me.Label18.Text = (rows)
                    Me.Label16.Text = sum
                    Me.Label14.Text = average
                    Me.Button2.Enabled = True
                    ListVariables.Add(listX)
                    ListVariables.Add(listY)
                    averageY = average

                    If flagFirstTime Then
                        startingPointY = Math.Round(averageY, 2)
                        sizeIntervalY = Math.Round((sum / (listColumn.ElementAt(variableY).ListColumnAll.Count - 1)) * 10 / 100, 2)
                    End If


                    Dim obj As New intervals
                        obj.initialize(lisIntervalX, startingPointX, sizeIntervalX)
                        obj.initialize(lisIntervalY, startingPointY, sizeIntervalY)






                        For Each s In listX
                            Dim range As String = obj.findRange(s, lisIntervalX)
                            obj.calculateContinuousDistribution(sizeIntervalX, s, lisIntervalX, range)
                        Next

                        ' obj.printContinuousDistributionForm8(lisIntervalX)



                        For Each s In listY
                            Dim range As String = obj.findRange(s, lisIntervalY)
                            obj.calculateContinuousDistribution(sizeIntervalY, s, lisIntervalY, range)
                            'Me.RichTextBox3.AppendText(s & Environment.NewLine)
                        Next

                        'obj.printContinuousDistributionForm8(lisIntervalY)

                        For Each DataP_numeric As DataPoint_Numeric In listInt
                            Dim BivariateNumericObservation As New bivariateNumericObservation
                            BivariateNumericObservation.X = DataP_numeric.X
                            BivariateNumericObservation.Y = DataP_numeric.Y
                            ListOfBivariateObservation.Add(BivariateNumericObservation)
                        Next

                        

                        FrequencyDistributionBivariateContinous = Me.BivariateDistribution_ContinousVariable(ListOfBivariateObservation, sizeIntervalX, sizeIntervalY, startingPointX, startingPointY)
                    Me.PrintResults_BivariateDistributionContinuous(FrequencyDistributionBivariateContinous, ListOfBivariateObservation, ListVariables)


                    Me.Button2.Enabled = True
                    Me.Button3.Enabled = True
                    Me.TextBox1.Text = sizeIntervalX
                    Me.TextBox2.Text = startingPointX
                    Me.TextBox3.Text = startingPointY
                    Me.TextBox4.Text = sizeIntervalY
                    Me.TextBox1.Enabled = True
                    Me.TextBox2.Enabled = True
                    Me.TextBox3.Enabled = True
                    Me.TextBox4.Enabled = True
                    Me.TextBox5.Enabled = True
                'No header


            End If
        Catch ex As Exception
            'MessageBox.Show("Error#8")
        End Try
    End Sub

    'Check the selected item
    Private Sub chknode(tree As TreeNode)

        For Each obj As TreeNode In tree.Nodes
            If obj.Checked Then
                If obj.Text <> Form1.Label4.Text Then
                    order = obj.Index
                    Dim index As String = Form3.listColumn.ElementAt(order).typeT.ToString
                    If index = "System.Int32" OrElse
                        index = "System.Int64" OrElse index = "System.Double" Then
                        If checkType() Then
                            obj.BackColor = Color.Yellow
                            checkInt += 1

                            If Not listObject.Contains(obj.Index) Then
                                listObject.Add(obj.Index)
                            End If

                        Else
                            Me.Label26.Text = "Error (Type): The data is of a different types"
                            checkFlag = True
                        End If


                    Else
                        Me.Label26.Text = "It must be a number"

                        checkFlag = True
                    End If

                End If
            End If
            chknode(obj)
        Next

    End Sub


    'Function: check if the data type satisfies
    Private Function checkType()
        Try
            Dim k As Type = Form3.listColumn.ElementAt(order).typeT
            If listCp(0).FlagCaption Then

                For s As Integer = 1 To rows
                    Dim a

                    If listColumn.ElementAt(order).ListColumnAll.ElementAt(s) = "" OrElse listColumn.ElementAt(order).ListColumnAll.ElementAt(s) = "NA" Then

                        Select Case k.ToString
                            Case "System.Boolean"
                                a = ""
                            Case "System.Int32"
                                a = 0
                            Case "System.Int64"
                                a = 0
                            Case "System.Double"
                                a = 0
                            Case "System.DateTime"
                                a = ""
                            Case Else
                                a = ""
                        End Select
                    Else
                        a = Convert.ChangeType(listColumn.ElementAt(order).ListColumnAll.ElementAt(s), k, CultureInfo.InvariantCulture)

                    End If

                Next

            Else

                For s As Integer = 0 To rows
                    Dim a

                    If listColumn.ElementAt(order).ListColumnAll.ElementAt(s) = "" OrElse listColumn.ElementAt(order).ListColumnAll.ElementAt(s) = "NA" Then

                        Select Case k.ToString
                            Case "System.Boolean"
                                a = ""
                            Case "System.Int32"
                                a = 0
                            Case "System.Int64"
                                a = 0
                            Case "System.Double"
                                a = 0
                            Case "System.DateTime"
                                a = ""
                            Case Else
                                a = ""
                        End Select
                    Else
                        a = Convert.ChangeType(listColumn.ElementAt(order).ListColumnAll.ElementAt(s), k, CultureInfo.InvariantCulture)

                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub uncheckNode(tree As TreeNode)
        For Each obj As TreeNode In tree.Nodes
            obj.Checked = False
            obj.BackColor = Color.Transparent
        Next

    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        resetCheck()
    End Sub

    'Reset All
    Private Sub resetCheck()
        For Each ob As TreeNode In TreeView1.Nodes
            uncheckNode(ob)
        Next
        rows = Form3.countFlag
        Me.TextBox5.Text = rows
        Form10.DataGridView1.Rows.Clear()
        Form10.DataGridView1.Columns.Clear()
        TreeView1.CheckBoxes = True
        TreeView1.ExpandAll()
        checkInt = 0
        checkFlag = False
        Me.Label6.Text = ""
        Me.Label7.Text = ""
        Me.Label9.Text = ""
        Me.Label11.Text = ""
        Me.Label20.Text = ""
        Me.Label18.Text = ""
        Me.Label16.Text = ""
        Me.Label14.Text = ""
        Me.Label26.Text = ""
        ListOfBivariateObservation.Clear()
        Me.RichTextBox1.Clear()
        order = Nothing
        flag = False
        FrequencyDistribution.Clear()
        lisIntervalX.Clear()
        lisIntervalY.Clear()
        Me.Button2.Enabled = False
        listObject.Clear()
        Me.Button3.Enabled = False
        Me.TextBox1.Text = ""
        Me.TextBox2.Text = ""
        Me.TextBox3.Text = ""
        Me.TextBox4.Text = ""
        Me.TextBox1.Enabled = False
        Me.TextBox2.Enabled = False
        Me.TextBox3.Enabled = False
        Me.TextBox4.Enabled = False
        Me.TextBox5.Enabled = False

    End Sub

    'Sub: Method Kahan
    Sub kahanSub(ls As List(Of Double))
        sum = 0.0
        c = 0.0
        count = 0


        For i As Integer = 0 To (ls.Count - 1)
            Dim y = ls(i) - c
            Dim t = sum + y
            c = (t - sum) - y
            sum = t
            count += 1
        Next

        average = sum / count


    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        Form10.Show()
        Form10.BringToFront()

    End Sub


    Function FindIntervalForValue(v As Double, IntervalSize As Double, ByRef ListOfIntervals As List(Of intervals)) As intervals

        For Each s In ListOfIntervals
            If s.ContainsValue(v) Then
                Return s
            End If
        Next


        If v <= ListOfIntervals(0).lowerPoint Then

            Do
                Dim NewLeftInterval As New intervals
                NewLeftInterval.upperPoint = ListOfIntervals(0).lowerPoint
                NewLeftInterval.lowerPoint = NewLeftInterval.upperPoint - IntervalSize

                ListOfIntervals.Insert(0, NewLeftInterval)


                If NewLeftInterval.ContainsValue(v) Then
                    Return NewLeftInterval
                End If

            Loop

        ElseIf v > ListOfIntervals(ListOfIntervals.Count - 1).upperPoint Then
            Do
                Dim NewRightInterval As New intervals
                NewRightInterval.lowerPoint = ListOfIntervals(ListOfIntervals.Count - 1).upperPoint
                NewRightInterval.upperPoint = NewRightInterval.lowerPoint + IntervalSize

                ListOfIntervals.Add(NewRightInterval)


                If NewRightInterval.ContainsValue(v) Then
                    Return NewRightInterval
                End If

            Loop

        Else

            Throw New Exception("Not expected occurrence")
        End If


    End Function





    Function BivariateDistribution_ContinousVariable(ListOfObservations As List(Of bivariateNumericObservation),
                                                     IntervalSize_X As Double, IntervalSize_Y As Double, StartingEndPoint_X As Double,
                                                     StartingEndPoint_Y As Double) As Dictionary(Of Tuple(Of intervals, intervals), frequency)


        Dim FrequencyDistribution As New Dictionary(Of Tuple(Of intervals, intervals), frequency)


        '----------------------------------------------------------------
        Dim Interval_X_0 As New intervals
        Interval_X_0.lowerPoint = StartingEndPoint_X
        Interval_X_0.upperPoint = Interval_X_0.lowerPoint + IntervalSize_X
        Dim ListOfIntervals_X As New List(Of intervals)
        ListOfIntervals_X.Add(Interval_X_0)


        Dim Interval_Y_0 As New intervals
        Interval_Y_0.lowerPoint = StartingEndPoint_Y
        Interval_Y_0.upperPoint = Interval_Y_0.lowerPoint + IntervalSize_Y
        Dim ListOfIntervals_Y As New List(Of intervals)
        ListOfIntervals_Y.Add(Interval_Y_0)


        Dim Inteval_Found_X As intervals
        Dim Inteval_Found_Y As intervals


        For Each b As bivariateNumericObservation In ListOfObservations

            Inteval_Found_X = Nothing
            Inteval_Found_Y = Nothing


            Inteval_Found_X = Me.FindIntervalForValue(b.X, IntervalSize_X, ListOfIntervals_X)

            Inteval_Found_Y = Me.FindIntervalForValue(b.Y, IntervalSize_Y, ListOfIntervals_Y)

            Dim FoundTuple As New Tuple(Of intervals, intervals)(Inteval_Found_X, Inteval_Found_Y)

            If FrequencyDistribution.ContainsKey(FoundTuple) Then
                FrequencyDistribution(FoundTuple).countFrequencies += 1
            Else
                FrequencyDistribution.Add(FoundTuple, New frequency)
            End If
        Next



        Return FrequencyDistribution

    End Function




    Sub PrintResults_BivariateDistributionContinuous(FrequencyDistribution As Dictionary(Of Tuple(Of intervals, intervals), frequency), ListOfBivariateObservation As List(Of bivariateNumericObservation), ListVar As List(Of List(Of Double)))


        'Extraction of distinct values of the two variables

        DistinctValues_FirstVariable.Clear()
        DistinctValues_SecondVariable.Clear()

        For Each freq As KeyValuePair(Of Tuple(Of intervals, intervals), frequency) In FrequencyDistribution
            Dim t As Tuple(Of intervals, intervals) = freq.Key

            If Not DistinctValues_FirstVariable.Contains(t.Item1) Then
                DistinctValues_FirstVariable.Add(t.Item1)
            End If


            If Not DistinctValues_SecondVariable.Contains(t.Item2) Then
                DistinctValues_SecondVariable.Add(t.Item2)
            End If

        Next



        Dim s1 As New SortedSet(Of intervals)(DistinctValues_FirstVariable)
        Dim s2 As New SortedSet(Of intervals)(DistinctValues_SecondVariable)

        Form10.DataGridView1.ColumnCount = s2.Count + 2
        Form10.DataGridView1.Columns(0).Name = "X\Y"

        Dim flagCount = 1

        For Each I_Y As intervals In s2
            Form10.DataGridView1.Columns(flagCount).Name = I_Y.ToString
            flagCount += 1
        Next

        Form10.DataGridView1.Columns(s2.Count + 1).Name = "Marginal X"


        'Table
        Dim flagCountRow = 0
        For Each I_X As intervals In s1
            Form10.DataGridView1.Rows.Add()
            Form10.DataGridView1.Rows(flagCountRow).Cells(0).Value = I_X.ToString

            Dim flagColumn = 1
            For Each I_Y As intervals In s2

                Dim t As New Tuple(Of intervals, intervals)(I_X, I_Y)

                Dim c As Integer
                If FrequencyDistribution.ContainsKey(t) Then
                    c = FrequencyDistribution(t).countFrequencies     'joint frequency of X and Y
                Else
                    c = 0    'joint frequency of X and Y
                End If

                Form10.DataGridView1.Rows(flagCountRow).Cells(flagColumn).Value = c.ToString
                flagColumn += 1
            Next

            flagCountRow += 1
        Next


        Form10.DataGridView1.Rows.Add()
        Form10.DataGridView1.Rows(s1.Count).Cells(0).Value = "Marginal Y"


        'X
        FrequencyDistributionContinousX = UnivariateDistribution_ContinousVariable(listX, sizeIntervalX, startingPointX)
        Dim countRows = 0
        For Each kvp As KeyValuePair(Of intervals, frequency) In FrequencyDistributionContinousX
            'Me.RichTextBox3.AppendText("(" & kvp.Key.lowerPoint & "-" & kvp.Key.upperPoint & "]" & "    " & kvp.Value.countFrequencies & Environment.NewLine)
            Dim FrequencieForValue As frequency = kvp.Value
            Form10.DataGridView1.Rows(countRows).Cells(s2.Count + 1).Value = FrequencieForValue.countFrequencies.ToString
            countRows += 1
        Next

        Dim count = 0
        FrequencyDistributionContinousY = UnivariateDistribution_ContinousVariable(listY, sizeIntervalY, startingPointY)

        Dim countColumns = 1
        For Each kvp As KeyValuePair(Of intervals, frequency) In FrequencyDistributionContinousY
            Dim FrequencieForValue As frequency = kvp.Value
            Form10.DataGridView1.Rows(s1.Count).Cells(countColumns).Value = FrequencieForValue.countFrequencies.ToString
            countColumns += 1
            count += FrequencieForValue.countFrequencies
        Next


        Form10.DataGridView1.Rows(s1.Count).Cells(s2.Count + 1).Value = count


    End Sub






    Function UnivariateDistribution_ContinousVariable(ListOfObservations As List(Of Double), IntervalSize As Double, StartingEndPoint As Double) As SortedDictionary(Of intervals, frequency)

        Dim FrequencyDistribution As New SortedDictionary(Of intervals, frequency)

        Dim Interval_0 As New intervals
        Interval_0.lowerPoint = StartingEndPoint
        Interval_0.upperPoint = Interval_0.lowerPoint + IntervalSize


        Dim ListOfIntervals As New List(Of intervals)
        ListOfIntervals.Add(Interval_0)


        For Each v As Double In ListOfObservations
            Dim IntevalWhereTheValueFalls = Me.FindIntervalForValue(v, IntervalSize, ListOfIntervals)

            If FrequencyDistribution.ContainsKey(IntevalWhereTheValueFalls) Then
                FrequencyDistribution(IntevalWhereTheValueFalls).countFrequencies += 1
            Else
                FrequencyDistribution.Add(IntevalWhereTheValueFalls, New frequency)
            End If
        Next


        Return FrequencyDistribution

    End Function

    Public Class bivariateNumericObservation

        Public X As Double
        Public Y As Double
    End Class


    Private Sub Form8Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If flag Then
            Form10.Close()
            Form11.Close()
        End If

    End Sub

    Private Sub Button3_MouseClick(sender As Object, e As MouseEventArgs) Handles Button3.MouseClick
        flagFirstTime = False
        Form10.Close()
        Form11.Close()
        If IsNumeric(Me.TextBox1.Text) Then
            sizeIntervalX = Convert.ToDouble(Me.TextBox1.Text)
        End If
        If IsNumeric(Me.TextBox2.Text) Then
            startingPointX = Convert.ToDouble(Me.TextBox2.Text)
        End If
        If IsNumeric(Me.TextBox3.Text) Then
            startingPointY = Convert.ToDouble(Me.TextBox3.Text)
        End If
        If IsNumeric(Me.TextBox4.Text) Then
            sizeIntervalY = Convert.ToDouble(Me.TextBox4.Text)
        End If

        If IsNumeric(Me.TextBox5.Text) Then
            If Convert.ToInt32(Me.TextBox5.Text) <= Form3.countFlag Then
                rows = Convert.ToInt32(Me.TextBox5.Text)
            Else
                Me.TextBox5.Text = Form3.countFlag
            End If
        End If

        calculate()

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        Me.Button2.Enabled = False
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        Me.Button2.Enabled = False
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        Me.Button2.Enabled = False
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        Me.Button2.Enabled = False
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        Me.Button2.Enabled = False
    End Sub
End Class