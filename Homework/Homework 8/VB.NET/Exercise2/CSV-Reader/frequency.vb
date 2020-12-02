Public Class frequency

    Public countFrequencies As Integer = 1
    Public RelativeFrequencies As Double
    Public PercentageFrequencies As Double


    Function calculateDistribution(arr As ArrayList) As SortedDictionary(Of Integer, frequency)
        Dim FrequencyDistribution As New SortedDictionary(Of Integer, frequency)

        For i As Integer = 0 To (arr.Count - 1)

            If FrequencyDistribution.ContainsKey(arr(i)) Then
                FrequencyDistribution(arr(i)).countFrequencies += 1
            Else
                FrequencyDistribution.Add(arr(i), New frequency)
            End If
        Next


        Return FrequencyDistribution
    End Function








    Sub printDistribution(frequencyDistribution As SortedDictionary(Of Integer, frequency), countNumber As Integer)

        Form6.RichTextBox1.AppendText(Environment.NewLine & "Distribution of a discrete variable . . ." & Environment.NewLine & Environment.NewLine)

        Form6.RichTextBox1.AppendText("#".PadRight(7) & "Count".PadRight(7) & "Freq".PadRight(7) & "Perc".PadRight(7) & Environment.NewLine)

        For Each freq As KeyValuePair(Of Integer, frequency) In frequencyDistribution

            frequencyDistribution(freq.Key).RelativeFrequencies = frequencyDistribution(freq.Key).countFrequencies / countNumber
            frequencyDistribution(freq.Key).PercentageFrequencies = frequencyDistribution(freq.Key).RelativeFrequencies * 100

            Dim fr = "0." & freq.Value.RelativeFrequencies.ToString("0.##")


            Form6.RichTextBox1.AppendText(freq.Key.ToString.PadRight(7) &
                                       freq.Value.countFrequencies.ToString.PadRight(7) &
                                       freq.Value.RelativeFrequencies.ToString("0.##").PadRight(7) &
                                       freq.Value.PercentageFrequencies.ToString("0.##").PadRight(7) & " % " & Environment.NewLine)


        Next
    End Sub

End Class