Public Class Form1

    Dim R As New Random

    Dim min As Integer = 78525583
    Dim max As Integer = 99989649



    Dim item(9999) As Integer
    Dim itemD() As Single = {0.1, 1000, 10000, 100000, 139000, 200000, 250000, 300000}


    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick

        For index As Integer = 0 To (item.Length - 1)
            Dim numberS As Integer = R.Next(min, max)
            item(index) = numberS
        Next




        Me.RichTextBox1.AppendText("Naive algorithm" & Environment.NewLine &
                                    "-----------------------------------------" & Environment.NewLine)
        Me.RichTextBox1.AppendText("Items".PadRight(20) & item.Length & Environment.NewLine)
        Me.RichTextBox1.AppendText("Sum".PadRight(20) & "Error: Buffer Overflow" & Environment.NewLine)
        Me.RichTextBox1.AppendText("Average".PadRight(20) & "Error: Buffer Overflow" & Environment.NewLine & Environment.NewLine)



        'naiveSub(item) 'Error: Overflow


        'Alternative
        kahanSub(item)
        neumaierSub(item)
        kleinSub(item)

        naiveSub(itemD)
        kahanSub(itemD)
        neumaierSub(itemD)
        kleinSub(itemD)

    End Sub



    Sub naiveSub(item() As Integer)
        Dim sum = 0
        Dim count = 0

        For i As Integer = 0 To (item.Length - 1)
            sum += item(i)
            count += 1
        Next

        Dim average = sum / count


        Me.RichTextBox1.AppendText("Items".PadRight(20) & count & Environment.NewLine)
        Me.RichTextBox1.AppendText("Sum".PadRight(20) & sum & Environment.NewLine)
        Me.RichTextBox1.AppendText("Average".PadRight(20) & average & Environment.NewLine)

    End Sub


    Sub naiveSub(item() As Single)
        Dim sum = 0
        Dim count = 0

        For i As Integer = 0 To (item.Length - 1)
            sum += item(i)
            count += 1
        Next

        Dim average = sum / count


        Me.RichTextBox2.AppendText("Items".PadRight(20) & count & Environment.NewLine)
        Me.RichTextBox2.AppendText("Sum".PadRight(20) & sum & Environment.NewLine)
        Me.RichTextBox2.AppendText("Average".PadRight(20) & average & Environment.NewLine)

    End Sub


    Sub kahanSub(item() As Integer)
        Dim sum = 0.0 'Prepare the accumulator.
        Dim c = 0.0 ' A running compensation for lost low-order bits.
        Dim count = 0


        For i As Integer = 0 To (item.Length - 1) '// The array input has elements indexed item(0) to item(lenght-1)
            Dim y = item(i) - c 'c is zero the first time around.
            Dim t = sum + y 'Alas, sum is big, y small, so low-order digits of y are lost.
            c = (t - sum) - y '(t - sum) cancels the high-order part of y; subtracting y recovers negative (low part of y)
            sum = t 'Algebraically, c should always be zero. Beware overly-aggressive optimizing compilers!
            count += 1 'Next time around, the lost low part will be added to y in a fresh attempt.
        Next

        Dim average = sum / count

        Me.RichTextBox1.AppendText("Kahan algorithm" & Environment.NewLine &
                                    "-----------------------------------------" & Environment.NewLine)
        Me.RichTextBox1.AppendText("Items".PadRight(20) & count & Environment.NewLine)
        Me.RichTextBox1.AppendText("Sum".PadRight(20) & sum & Environment.NewLine)
        Me.RichTextBox1.AppendText("Average".PadRight(20) & average & Environment.NewLine & Environment.NewLine)

    End Sub


    Sub kahanSub(item() As Single)
        Dim sum = 0.0 'Prepare the accumulator.
        Dim c = 0.0 ' A running compensation for lost low-order bits.
        Dim count = 0


        For i As Integer = 0 To (item.Length - 1) '// The array input has elements indexed item(0) to item(lenght-1)
            Dim y = item(i) - c 'c is zero the first time around.
            Dim t = sum + y 'Alas, sum is big, y small, so low-order digits of y are lost.
            c = (t - sum) - y '(t - sum) cancels the high-order part of y; subtracting y recovers negative (low part of y)
            sum = t 'Algebraically, c should always be zero. Beware overly-aggressive optimizing compilers!
            count += 1 'Next time around, the lost low part will be added to y in a fresh attempt.
        Next

        Dim average = sum / count

        Me.RichTextBox2.AppendText("Kahan algorithm" & Environment.NewLine &
                                    "-----------------------------------------" & Environment.NewLine)
        Me.RichTextBox2.AppendText("Items".PadRight(20) & count & Environment.NewLine)
        Me.RichTextBox2.AppendText("Sum".PadRight(20) & sum & Environment.NewLine)
        Me.RichTextBox2.AppendText("Average".PadRight(20) & average & Environment.NewLine & Environment.NewLine)

    End Sub


    Sub neumaierSub(item() As Integer)

        Dim sum = 0.0
        Dim c = 0.0  'A running compensation for lost low-order bits.
        Dim count = 0
        For i As Integer = 0 To (item.Length - 1)
            Dim t = sum + item(i)
            If (Math.Abs(sum) >= Math.Abs(item(i))) Then
                c += (sum - t) + item(i)   ' If sum is bigger, low-order digits of input[i] are lost.
            Else
                c += (item(i) - t) + sum  'Else low-order digits of sum are lost.
            End If
            sum = t

            count += 1
        Next

        Dim average = sum / count

        Me.RichTextBox1.AppendText("Kahan–Babuška algorithm" & Environment.NewLine &
                                    "-----------------------------------------" & Environment.NewLine)
        Me.RichTextBox1.AppendText("Items".PadRight(20) & count & Environment.NewLine)
        Me.RichTextBox1.AppendText("Sum".PadRight(20) & (sum + c) & Environment.NewLine)
        'Correction only applied once in the very end.
        Me.RichTextBox1.AppendText("Average".PadRight(20) & average & Environment.NewLine & Environment.NewLine)

    End Sub


    Sub neumaierSub(item() As Single)

        Dim sum = 0.0
        Dim c = 0.0  'A running compensation for lost low-order bits.
        Dim count = 0
        For i As Integer = 0 To (item.Length - 1)
            Dim t = sum + item(i)
            If (Math.Abs(sum) >= Math.Abs(item(i))) Then
                c += (sum - t) + item(i)   ' If sum is bigger, low-order digits of input[i] are lost.
            Else
                c += (item(i) - t) + sum  'Else low-order digits of sum are lost.
            End If
            sum = t

            count += 1
        Next

        Dim average = sum / count

        Me.RichTextBox2.AppendText("Kahan–Babuška algorithm" & Environment.NewLine &
                                    "-----------------------------------------" & Environment.NewLine)
        Me.RichTextBox2.AppendText("Items".PadRight(20) & count & Environment.NewLine)
        Me.RichTextBox2.AppendText("Sum".PadRight(20) & (sum + c) & Environment.NewLine)
        'Correction only applied once in the very end.
        Me.RichTextBox2.AppendText("Average".PadRight(20) & average & Environment.NewLine & Environment.NewLine)

    End Sub




    Sub kleinSub(item() As Integer)

        Dim sum = 0.0
        Dim cs = 0.0
        Dim ccs = 0.0
        Dim c = 0.0
        Dim cc = 0.0

        Dim count = 0


        For i As Integer = 0 To (item.Length - 1)
            Dim t = sum + item(i)
            If (Math.Abs(sum) >= Math.Abs(item(i))) Then
                c = (sum - t) + item(i)
            Else
                c = (item(i) - t) + sum
            End If
            sum = t
            t = cs + c
            If Math.Abs(cs) >= Math.Abs(c) Then
                cc = (cs - t) + c
            Else
                cc = (c - t) + cs
            End If
            cs = t
            ccs = ccs + cc

            count += 1
        Next

        Dim average = sum / count

        Me.RichTextBox1.AppendText("Klein algorithm" & Environment.NewLine &
                                    "-----------------------------------------" & Environment.NewLine)
        Me.RichTextBox1.AppendText("Items".PadRight(20) & count & Environment.NewLine)
        Me.RichTextBox1.AppendText("Sum".PadRight(20) & (sum + cs + ccs) & Environment.NewLine)
        Me.RichTextBox1.AppendText("Average".PadRight(20) & average & Environment.NewLine & Environment.NewLine)

    End Sub



    Sub kleinSub(item() As Single)

        Dim sum = 0.0
        Dim cs = 0.0
        Dim ccs = 0.0
        Dim c = 0.0
        Dim cc = 0.0

        Dim count = 0


        For i As Integer = 0 To (item.Length - 1)
            Dim t = sum + item(i)
            If (Math.Abs(sum) >= Math.Abs(item(i))) Then
                c = (sum - t) + item(i)
            Else
                c = (item(i) - t) + sum
            End If
            sum = t
            t = cs + c
            If Math.Abs(cs) >= Math.Abs(c) Then
                cc = (cs - t) + c
            Else
                cc = (c - t) + cs
            End If
            cs = t
            ccs = ccs + cc

            count += 1
        Next

        Dim average = sum / count

        Me.RichTextBox2.AppendText("Klein algorithm" & Environment.NewLine &
                                    "-----------------------------------------" & Environment.NewLine)
        Me.RichTextBox2.AppendText("Items".PadRight(20) & count & Environment.NewLine)
        Me.RichTextBox2.AppendText("Sum".PadRight(20) & (sum + cs + ccs) & Environment.NewLine)
        Me.RichTextBox2.AppendText("Average".PadRight(20) & average & Environment.NewLine & Environment.NewLine)

    End Sub



End Class
