Public Class Form1

    Private R As New Random
    Dim heaps As New heap

    Public Class heap
        Private listUnordered As New List(Of Integer)
        Private listOrdered As New List(Of Integer)


        Public Function getOrdinary() As List(Of Integer)
            Return listOrdered
        End Function

        Public Function getNoOrdinary() As List(Of Integer)
            Return listUnordered
        End Function

        Public Sub add(num As Integer)
            listUnordered.Add(num)
            listOrdered.Add(num)
            listOrdered.Sort()
        End Sub

        Public Function calculateMedian() As Double
            Dim res As Double = getMedian(listOrdered)
            Return res
        End Function

        Public Function getMedian(list As List(Of Integer)) As Double
            Dim idx As Integer = Nothing
            Dim result As Double = Nothing
            If (list.Count) Mod 2 = 0 Then
                Dim idxUp As Integer = Math.Ceiling((list.Count - 1) / 2)
                Dim idxDown As Integer = Math.Floor((list.Count - 1) / 2)

                result = (list.ElementAt(idxUp) + list.ElementAt(idxDown)) / 2

                Return result

            Else
                idx = (list.Count - 1) / 2
                result = list.ElementAt(idx)
                Return result
            End If
        End Function


    End Class

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.RichTextBox2.Clear()
        Me.RichTextBox3.Clear()
        Me.RichTextBox2.AppendText("Ordered List" & Environment.NewLine & Environment.NewLine)

        Dim number As Integer = R.Next(0, 100)
        heaps.add(number)
        Dim median As Double = heaps.calculateMedian()
        Dim listNoOrdered As List(Of Integer) = heaps.getNoOrdinary
        Dim listOrdered As List(Of Integer) = heaps.getOrdinary

        Me.RichTextBox1.AppendText(number & Environment.NewLine)
        Me.RichTextBox3.AppendText("Median  " & median & Environment.NewLine)

        For Each kvp In listOrdered
            Me.RichTextBox2.AppendText(kvp.ToString & Environment.NewLine)
        Next

    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        If (Timer1.Enabled) Then
            Timer1.Stop()
            Button1.Text = "Start"
        Else
            Timer1.Start()
            Button1.Text = "Stop"
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RichTextBox1.AppendText("Unordered List" & Environment.NewLine & Environment.NewLine)
    End Sub
End Class
