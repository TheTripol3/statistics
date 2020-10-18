Public Class Form1

    Public R As New Random
    Public CurrentAvarage As Double = 0
    Public countNumber As Integer = 0


    Dim FrequencyDistribution As New SortedDictionary(Of Integer, frequencies)


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        'Online arithmetical mean
        '----------------------------------------------------

        'Increment the counter value
        countNumber += 1

        'The Knuth formula
        Dim grade As Integer = Me.R.Next(18, 31)
        CurrentAvarage = CurrentAvarage + (grade - CurrentAvarage) / countNumber

        Dim nameOfTheExam = "Exam " & countNumber

        Me.RichTextBox1.AppendText(nameOfTheExam.PadRight(10) &
                                   grade.ToString.PadRight(8) & CurrentAvarage &
                                   Environment.NewLine)




        '---------------------------------------------------
        'Distribution

        If FrequencyDistribution.ContainsKey(grade) Then
            FrequencyDistribution(grade).countFrequencies += 1
        Else
            FrequencyDistribution.Add(grade, New frequencies)
        End If

        Me.RichTextBox2.Clear()
        Me.Chart1.Series("Relative Frequency").Points.Clear()
        Me.RichTextBox2.AppendText("Grade".PadRight(7) & "Count".PadRight(7) & "Freq".PadRight(7) & "Perc".PadRight(7) & Environment.NewLine)


        'Calculate the relative frequencies and percentage frequencies. Then draw the graphic
        For Each freq As KeyValuePair(Of Integer, frequencies) In FrequencyDistribution

            FrequencyDistribution(freq.Key).RelativeFrequencies = FrequencyDistribution(freq.Key).countFrequencies / countNumber
            FrequencyDistribution(freq.Key).PercentageFrequencies = FrequencyDistribution(freq.Key).RelativeFrequencies * 100

            Dim fr = "0." & freq.Value.RelativeFrequencies.ToString("0.##")
            Me.Chart1.Series("Relative Frequency").Points.AddXY(freq.Key.ToString, fr)

            Me.RichTextBox2.AppendText(freq.Key.ToString.PadRight(7) &
                                       freq.Value.countFrequencies.ToString.PadRight(7) &
                                       freq.Value.RelativeFrequencies.ToString("0.##").PadRight(7) &
                                       freq.Value.PercentageFrequencies.ToString("0.##").PadRight(7) & " % " & Environment.NewLine)



        Next

        Me.RichTextBox2.AppendText(Environment.NewLine & Environment.NewLine &
                                   "Total Count: " & countNumber & Environment.NewLine)





    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        'Start the timer
        Me.Timer1.Start()
    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        'Stop the timer
        Me.Timer1.Stop()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RichTextBox1.AppendText("Exam".PadRight(10) & "Grade".PadRight(8) & "Current Mean" & Environment.NewLine)

        Me.Chart1.ChartAreas("ChartArea1").AxisX.MinorTickMark.Enabled = True
        Me.Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
        Me.Chart1.ChartAreas("ChartArea1").AxisX.IsLabelAutoFit = True

    End Sub

End Class

'Class frequencies
Public Class frequencies

    Public countFrequencies As Integer = 1
    Public RelativeFrequencies As Double
    Public PercentageFrequencies As Double

End Class

