Public Class Form1

    'Statement
    Private R As New Random

    Private CurrentAvarageHeight As Double = 0
    Private CurrentAvarageWeight As Double = 0

    Private countNumber As Integer = 0

    Dim maxHeight As Double = 200
    Dim minHeight As Double = 70

    Dim maxWeight As Double = 120
    Dim minWeight As Double = 30


    Dim intervalSizeHeight As Double = 10
    Dim startingEndPointHeight As Double = 150


    Dim intervalSizeWeight As Double = 15
    Dim startingEndPointWeight As Double = 70

    Dim listIntervalsH As New List(Of sizeIntervals)
    Dim listIntervalsW As New List(Of sizeIntervals)





    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        'Increment the counter value and generate a value
        countNumber += 1
        Dim RandomHeight As Double = Math.Round(minHeight + (maxHeight - minHeight) * R.NextDouble, 2)
        Dim RandomWeight As Double = Math.Round(minWeight + (maxWeight - minWeight) * R.NextDouble, 1)



        'Calculate the arithmetic mean
        calculateMeanKnuth(RandomHeight, RandomWeight, countNumber)



        'Continuous distribution (]


        'Check range
        Dim rangeH As String = findRange(RandomHeight, listIntervalsH)
        Dim rangew As String = findRange(RandomWeight, listIntervalsW)

        calculateContinuousDistribution(intervalSizeHeight, RandomHeight, listIntervalsH, rangeH)
        calculateContinuousDistribution(intervalSizeWeight, RandomWeight, listIntervalsW, rangew)

        printContinuousDistribution(listIntervalsH, listIntervalsW)

    End Sub


    Function findRange(randomNumber As Double, listIntv As List(Of sizeIntervals))


        'Value between first interval and center 
        If ((listIntv(0).lowerPoint < randomNumber) AndAlso (randomNumber <= listIntv(listIntv.Count / 2).lowerPoint)) Then

            Return "L"

            'Value between center and last interval
        ElseIf ((listIntv(listIntv.Count / 2).lowerPoint < randomNumber) AndAlso (randomNumber <= listIntv(listIntv.Count - 1).upperPoint)) Then

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



    'Print the distribution
    Private Sub printContinuousDistribution(listIntv1 As List(Of sizeIntervals), listIntv2 As List(Of sizeIntervals))

        Me.RichTextBox2.Clear()
        Me.RichTextBox3.Clear()
        Me.Chart1.Series("Height Bar Chart").Points.Clear()
        Me.Chart2.Series("Weight Bar Chart").Points.Clear()

        Me.RichTextBox2.AppendText("Height Interval".PadRight(18) & "Count" & Environment.NewLine)
        Me.RichTextBox3.AppendText("Weight Interval".PadRight(18) & "Count" & Environment.NewLine)

        For Each interVal1 As sizeIntervals In listIntv1


            Dim stringInterval1 = "( " & interVal1.lowerPoint & " - " & interVal1.upperPoint & " )"

            Me.RichTextBox2.AppendText(stringInterval1.PadRight(18) & interVal1.countInt.ToString &
                                           Environment.NewLine)

            Me.Chart1.Series("Height Bar Chart").Points.AddXY(stringInterval1, interVal1.countInt.ToString)

        Next


        For Each interVal2 As sizeIntervals In listIntv2

            Dim stringInterval2 = "( " & interVal2.lowerPoint & " - " & interVal2.upperPoint & " )"

            Me.RichTextBox3.AppendText(stringInterval2.PadRight(18) & interVal2.countInt.ToString &
                                           Environment.NewLine)


            Me.Chart2.Series("Weight Bar Chart").Points.AddXY(stringInterval2, interVal2.countInt.ToString)


        Next

    End Sub



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


    'Calculate the distribution
    Private Sub calculateContinuousDistribution(sizeInterval As Double,
                                        valuesC As Double, listInt As List(Of sizeIntervals), range As String)


        Select Case range
            'Left from the center (the value exists) 
            Case "L"
                For index As Integer = 0 To ((listInt.Count / 2) - 1)
                    If ((valuesC > listInt(index).lowerPoint) AndAlso (valuesC <= listInt(index).upperPoint)) Then
                        listInt(index).countInt += 1
                        Exit Sub
                    End If
                Next

            'Right from the center (the value exists) 
            Case "R"
                For index As Integer = (listInt.Count / 2) To (listInt.Count - 1)
                    If ((valuesC > listInt(index).lowerPoint) AndAlso (valuesC <= listInt(index).upperPoint)) Then
                        listInt(index).countInt += 1
                        Exit Sub
                    End If
                Next

            'To the left of the first interval (Value does not exist)
            Case "OL"
                Do
                    Dim NewLeftInterval As New sizeIntervals
                    NewLeftInterval.upperPoint = listInt(0).lowerPoint
                    NewLeftInterval.lowerPoint = NewLeftInterval.upperPoint - sizeInterval

                    listInt.Insert(0, NewLeftInterval)

                    If (valuesC <= NewLeftInterval.upperPoint) AndAlso (valuesC > NewLeftInterval.lowerPoint) Then
                        NewLeftInterval.countInt += 1
                        Exit Do
                    End If
                Loop

            'To the right of the last interval (Value does not exist)
            Case "OR"
                Do
                    Dim NewRightInterval As New sizeIntervals
                    NewRightInterval.lowerPoint = listInt(listInt.Count - 1).upperPoint
                    NewRightInterval.upperPoint = NewRightInterval.lowerPoint + sizeInterval

                    listInt.Add(NewRightInterval)

                    If ((valuesC > NewRightInterval.lowerPoint) AndAlso (valuesC <= NewRightInterval.upperPoint)) Then
                        NewRightInterval.countInt += 1
                        Exit Do
                    End If
                Loop
            Case Else
                Throw New Exception("Error")
        End Select


    End Sub


    'Calculate the arithmetic mean
    Private Sub calculateMeanKnuth(height As Double, weight As Double, count As Integer)

        'Online arithmetic mean (The Knuth formula)
        '----------------------------------------------------
        CurrentAvarageHeight = CurrentAvarageHeight + (height - CurrentAvarageHeight) / count
        CurrentAvarageWeight = CurrentAvarageWeight + (weight - CurrentAvarageWeight) / count

        Dim nameOfTheStudent = "Stud " & countNumber

        Me.RichTextBox1.AppendText(nameOfTheStudent.PadRight(10) & height.ToString.PadRight(10) &
            weight.ToString.PadRight(10) & CurrentAvarageHeight.ToString.PadRight(25) &
             CurrentAvarageWeight.ToString.PadRight(25) & Environment.NewLine)

    End Sub






    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        'Start the timer
        Me.Timer1.Start()

        initialize(listIntervalsH, startingEndPointHeight, intervalSizeHeight)
        initialize(listIntervalsW, startingEndPointWeight, intervalSizeWeight)


    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        'Stop the timer
        Me.Timer1.Stop()
    End Sub




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RichTextBox1.AppendText("Student".PadRight(10) & "Height".PadRight(10) &
                                   "Weight".PadRight(10) & "Average Height".PadRight(25) &
                                   "Average Weight".PadRight(25) & Environment.NewLine)


        Me.Chart1.ChartAreas("ChartArea1").AxisX.MinorTickMark.Enabled = True
        Me.Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
        Me.Chart1.ChartAreas("ChartArea1").AxisX.IsLabelAutoFit = True

        Me.Chart2.ChartAreas("ChartArea1").AxisX.MinorTickMark.Enabled = True
        Me.Chart2.ChartAreas("ChartArea1").AxisX.Interval = 1
        Me.Chart2.ChartAreas("ChartArea1").AxisX.IsLabelAutoFit = True

    End Sub




End Class





