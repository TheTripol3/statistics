Public Class Form4

    Dim index As Integer = Form3.index
    Dim types As String
    Dim names As String


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim indexVariable As String = Form2.listCp.ElementAt(0).Lists.ElementAt(index)
            Dim indexRadio As String = Form2.listColumn.ElementAt(index).typeT.ToString

            initial(indexRadio, indexVariable)



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

    Private Sub initial(index As String, stringS As String)

        Select Case index
            Case "System.Boolean"
                CheckedListBox1.SetItemChecked(0, True)
                CheckedListBox1.SetItemChecked(1, False)
                CheckedListBox1.SetItemChecked(2, False)
                CheckedListBox1.SetItemChecked(3, False)
                CheckedListBox1.SetItemChecked(4, False)
                CheckedListBox1.SetItemChecked(5, False)
            Case "System.Int32"
                CheckedListBox1.SetItemChecked(0, False)
                CheckedListBox1.SetItemChecked(1, True)
                CheckedListBox1.SetItemChecked(2, False)
                CheckedListBox1.SetItemChecked(3, False)
                CheckedListBox1.SetItemChecked(4, False)
                CheckedListBox1.SetItemChecked(5, False)
            Case "System.Int64"
                CheckedListBox1.SetItemChecked(0, False)
                CheckedListBox1.SetItemChecked(1, False)
                CheckedListBox1.SetItemChecked(2, True)
                CheckedListBox1.SetItemChecked(3, False)
                CheckedListBox1.SetItemChecked(4, False)
                CheckedListBox1.SetItemChecked(5, False)
            Case "System.Double"
                CheckedListBox1.SetItemChecked(0, False)
                CheckedListBox1.SetItemChecked(1, False)
                CheckedListBox1.SetItemChecked(2, False)
                CheckedListBox1.SetItemChecked(3, True)
                CheckedListBox1.SetItemChecked(4, False)
                CheckedListBox1.SetItemChecked(5, False)
            Case "System.DateTime"
                CheckedListBox1.SetItemChecked(0, False)
                CheckedListBox1.SetItemChecked(1, False)
                CheckedListBox1.SetItemChecked(2, False)
                CheckedListBox1.SetItemChecked(3, False)
                CheckedListBox1.SetItemChecked(4, True)
                CheckedListBox1.SetItemChecked(5, False)
            Case "System.String"
                CheckedListBox1.SetItemChecked(0, False)
                CheckedListBox1.SetItemChecked(1, False)
                CheckedListBox1.SetItemChecked(2, False)
                CheckedListBox1.SetItemChecked(3, False)
                CheckedListBox1.SetItemChecked(4, False)
                CheckedListBox1.SetItemChecked(5, True)
            Case Else
                MessageBox.Show("Error#6")

        End Select

        Me.TextBox1.Text = stringS

    End Sub


    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick

        Form2.listCp.ElementAt(0).Lists(index) = names

        Select Case types
            Case "Boolean"

                Dim booleanText As Boolean = True

                Form2.listColumn.ElementAt(index).typeT = booleanText.GetType()

            Case "Integer"

                Dim integerText As Integer = 3
                Form2.listColumn.ElementAt(index).typeT = integerText.GetType()

            Case "Long"

                Dim longText As Long = 3131313131
                Form2.listColumn.ElementAt(index).typeT = longText.GetType()


            Case "Double"

                Dim doubleText As Double = 2121.21
                Form2.listColumn.ElementAt(index).typeT = doubleText.GetType()


            Case "Date"

                Dim dateText As Date = "2020 / 10 / 10"
                Form2.listColumn.ElementAt(index).typeT = dateText.GetType()

            Case "String"

                Dim stringText As String = "file"
                Form2.listColumn.ElementAt(index).typeT = stringText.GetType()

            Case Else
                MessageBox.Show("Error#7")

        End Select

        Form3.createTreeView()
        Form3.createGridView()
        Me.Close()
    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick


        Try
            Me.CheckedListBox1.Enabled = False
            Me.TextBox1.Enabled = False

            If (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Boolean"))) Then
                Dim k As Integer = 0
                For Each obj In Form2.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        Dim flagBoolean = Boolean.Parse(obj)
                    End If
                    k += 1
                Next

                types = "Boolean"


            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Integer"))) Then
                Dim k As Integer = 0
                For Each obj In Form2.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        Dim flagInt = Integer.Parse(obj)
                    End If
                    k += 1
                Next

                types = "Integer"

            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Long"))) Then
                Dim k As Integer = 0
                For Each obj In Form2.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        Dim flagLong = Long.Parse(obj)
                    End If
                    k += 1
                Next
                types = "Long"

            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Double"))) Then
                Dim k As Integer = 0
                For Each obj In Form2.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        Dim flagDouble = Double.Parse(obj)
                    End If
                    k += 1
                Next

                types = "Double"


            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Date"))) Then
                Dim k As Integer = 0
                For Each obj In Form2.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        Dim flagDate = Date.Parse(obj)
                    End If
                    k += 1
                Next

                types = "Date"


            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("String"))) Then
                Dim k As Integer = 0
                For Each obj In Form2.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        Dim flagString = CStr(obj)
                    End If
                    k += 1
                Next

                types = "String"

            Else
                MessageBox.Show("Error#5")
                Me.CheckedListBox1.Enabled = True
                Me.Button2.Enabled = True
                Me.Button1.Enabled = False
                Exit Sub
            End If


            MessageBox.Show("Variables can be transformed into (" & types & ")", "Test")

            If String.IsNullOrWhiteSpace(Me.TextBox1.Text) Then
                MessageBox.Show("The variable name is null. Change it!", "Warning")
                Me.CheckedListBox1.Enabled = True
                Me.Button2.Enabled = True
                Me.Button1.Enabled = False
                Me.TextBox1.Enabled = True
            Else
                names = Me.TextBox1.Text
                Me.Button2.Enabled = False
                Me.Button1.Enabled = True
            End If


        Catch ex As Exception
            MessageBox.Show("Conversion cannot be performed")
            Me.CheckedListBox1.Enabled = True
            Me.TextBox1.Enabled = True
        End Try
    End Sub

    Private Sub Button3_MouseClick(sender As Object, e As MouseEventArgs) Handles Button3.MouseClick
        Dim indexVariable As String = Form2.listCp.ElementAt(0).Lists.ElementAt(index)
        Dim indexRadio As String = Form2.listColumn.ElementAt(index).typeT.ToString

        initial(indexRadio, indexVariable)
        Me.CheckedListBox1.Enabled = True
        Me.Button2.Enabled = True
        Me.Button1.Enabled = False
        Me.TextBox1.Enabled = True
    End Sub
End Class