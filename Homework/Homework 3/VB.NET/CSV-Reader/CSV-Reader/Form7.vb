Public Class Form7
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Discrete
        If Not Form6.flagDistribution Then

            Me.Chart2.Visible = False
            Me.Chart1.Visible = True

            Me.Chart1.ChartAreas("ChartArea1").AxisX.MinorTickMark.Enabled = True
            Me.Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
            Me.Chart1.ChartAreas("ChartArea1").AxisX.IsLabelAutoFit = True

            Me.Chart1.Series("Relative Frequency").Points.Clear()

            For Each freq As KeyValuePair(Of Integer, frequency) In Form6.FrequencyDistribution

                Form6.FrequencyDistribution(freq.Key).RelativeFrequencies = Form6.FrequencyDistribution(freq.Key).countFrequencies / Form6.count
                Form6.FrequencyDistribution(freq.Key).PercentageFrequencies = Form6.FrequencyDistribution(freq.Key).RelativeFrequencies * 100

                Dim fr = "0." & freq.Value.RelativeFrequencies.ToString("0.##")

                Me.Chart1.Series("Relative Frequency").Points.AddXY(freq.Key.ToString, fr)

            Next

            'Continuous
        ElseIf Form6.flagDistribution Then

            Me.Chart1.Visible = False
            Me.Chart2.Visible = True

            Me.Chart2.ChartAreas("ChartArea1").AxisX.MinorTickMark.Enabled = True
            Me.Chart2.ChartAreas("ChartArea1").AxisX.Interval = 1
            Me.Chart2.ChartAreas("ChartArea1").AxisX.IsLabelAutoFit = True

            Me.Chart2.Series("Bar Chart of Intervals").Points.Clear()

            For Each interVal1 As intervals In Form6.lisInterval

                Dim stringInterval1 = "( " & interVal1.lowerPoint & " - " & interVal1.upperPoint & " )"
                Me.Chart2.Series("Bar Chart of Intervals").Points.AddXY(stringInterval1, interVal1.countInt.ToString)

            Next

        Else

        End If


    End Sub
End Class