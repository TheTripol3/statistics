Public Class intervals
    Public countInt As Integer
    Public relFrequenc As Double
    Public Percent As Double
    Public lowerPoint As Double
    Public upperPoint As Double


    Function findRange(randomNumber As Double, listIntv As List(Of intervals))

        'Value between first interval and center 
        If ((listIntv(0).lowerPoint < randomNumber) AndAlso (randomNumber <= listIntv(Math.Ceiling(listIntv.Count / 2)).lowerPoint)) Then
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


    Sub initialize(list As List(Of intervals), startPoint As Double, intervalSize As Double)

        If (list.Count = 0) Then

            'Second Interval
            Dim firstInterval As New intervals

            'Set the first Interval
            firstInterval.upperPoint = startPoint
            firstInterval.lowerPoint = firstInterval.upperPoint - intervalSize

            'Now, we use a list to store the intervals
            list.Add(firstInterval)

            'First Interval
            Dim secondIntervel As New intervals

            'Set the first Interval
            secondIntervel.lowerPoint = startPoint
            secondIntervel.upperPoint = secondIntervel.lowerPoint + intervalSize

            'Now, we use a list to store the intervals
            list.Add(secondIntervel)


        End If
    End Sub





    'Calculate the distribution
    Sub calculateContinuousDistribution(sizeInterval As Double,
                                        valuesC As Double, listInt As List(Of intervals), range As String)


        Select Case range
            'Left from the center (the value exists) 
            Case "L"

                For index As Integer = 0 To (Math.Ceiling((listInt.Count / 2)) - 1)
                    If ((valuesC > listInt(index).lowerPoint) AndAlso (valuesC <= listInt(index).upperPoint)) Then
                        listInt(index).countInt += 1
                        Exit Sub
                    End If
                Next

            'Right from the center (the value exists) 
            Case "R"

                For index As Integer = Math.Ceiling((listInt.Count / 2)) To (listInt.Count - 1)
                    If ((valuesC > listInt(index).lowerPoint) AndAlso (valuesC <= listInt(index).upperPoint)) Then
                        listInt(index).countInt += 1
                        Exit Sub
                    End If
                Next

            'To the left of the first interval (Value does not exist)
            Case "OL"

                Do
                    Dim NewLeftInterval As New intervals
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
                    Dim NewRightInterval As New intervals
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

    'Print the distribution
    Sub printContinuousDistribution(listIntv1 As List(Of intervals))

        Form6.RichTextBox1.AppendText(Environment.NewLine & Environment.NewLine & "Distribution of a continuous variable . . ." & Environment.NewLine & Environment.NewLine)

        Form6.RichTextBox1.AppendText("Interval".PadRight(18) & "Count" & Environment.NewLine)

        For Each interVal1 As intervals In listIntv1


            Dim stringInterval1 = "( " & interVal1.lowerPoint & " - " & interVal1.upperPoint & " )"

            Form6.RichTextBox1.AppendText(stringInterval1.PadRight(18) & interVal1.countInt.ToString &
                                           Environment.NewLine)


        Next

    End Sub
End Class
