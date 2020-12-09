Public Class heap
    Private listUnordered As New List(Of Double)
    Private listOrdered As New List(Of Double)

    Public Function getOrdinary() As List(Of Double)
        Return listOrdered
    End Function

    Public Function getNoOrdinary() As List(Of Double)
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

    Public Function getMedian(list As List(Of Double)) As Double
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