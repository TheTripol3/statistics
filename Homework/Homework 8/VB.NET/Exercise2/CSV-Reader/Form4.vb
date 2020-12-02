'Form4: Change Type
Public Class Form4

    Dim index As Integer = Form3.index
    Dim types As String
    Dim names As String


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim indexVariable As String = Form3.listCp.ElementAt(0).Lists.ElementAt(index)
        Dim indexRadio As String = Form3.listColumn.ElementAt(index).typeT.ToString

        initial(indexRadio, indexVariable)



    End Sub

    Private Sub CheckedListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseClick

        'Only one choice
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


    'Sub Only one choice
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
                'MessageBox.Show("Error#6")

        End Select

        Me.TextBox1.Text = stringS

    End Sub

    'Save Info after the Test
    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick

        Form3.listCp.ElementAt(0).Lists(index) = names

        Select Case types
            Case "Boolean"

                Dim booleanText As Boolean = True

                Form3.listColumn.ElementAt(index).typeT = booleanText.GetType()

            Case "Integer"

                Dim integerText As Integer = 3
                Form3.listColumn.ElementAt(index).typeT = integerText.GetType()

            Case "Long"

                Dim longText As Long = 3131313131
                Form3.listColumn.ElementAt(index).typeT = longText.GetType()


            Case "Double"

                Dim doubleText As Double = 2121.21
                Form3.listColumn.ElementAt(index).typeT = doubleText.GetType()


            Case "Date"

                Dim dateText As Date = "2020 / 10 / 10"
                Form3.listColumn.ElementAt(index).typeT = dateText.GetType()

            Case "String"

                Dim stringText As String = "file"
                Form3.listColumn.ElementAt(index).typeT = stringText.GetType()

            Case Else
                'MessageBox.Show("Error#7")

        End Select

        Form3.createTreeView()
        Form3.createGridView()
        Me.Close()
    End Sub

    'Sub Test: check if the data type satisfies
    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick


        Try
            Me.CheckedListBox1.Enabled = False
            Me.TextBox1.Enabled = False

            If (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Boolean"))) Then
                Dim k As Integer = 0
                For Each obj In Form3.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        If obj = "" OrElse obj = "NA" Then
                            Dim flagBoolean = Boolean.Parse("")
                        Else
                            Dim flagBoolean = Boolean.Parse(obj)

                        End If

                    End If
                    k += 1
                Next

                types = "Boolean"


            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Integer"))) Then
                Dim k As Integer = 0
                For Each obj In Form3.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        If obj = "" OrElse obj = "NA" Then
                            Dim flagInt = Integer.Parse("0")
                        Else
                            Dim flagInt = Integer.Parse(obj)

                        End If

                    End If
                    k += 1
                Next

                types = "Integer"

            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Long"))) Then
                Dim k As Integer = 0
                For Each obj In Form3.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        If obj = "" OrElse obj = "NA" Then
                            Dim flagLong = Long.Parse("0")
                        Else
                            Dim flagLong = Long.Parse(obj)

                        End If

                    End If
                    k += 1
                Next
                types = "Long"

            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Double"))) Then
                Dim k As Integer = 0
                For Each obj In Form3.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        If obj = "" OrElse obj = "NA" Then
                            Dim flagDouble = Double.Parse("0.0")
                        Else
                            Dim flagDouble = Double.Parse(obj)

                        End If

                    End If
                    k += 1
                Next

                types = "Double"


            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Date"))) Then
                Dim k As Integer = 0
                For Each obj In Form3.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        If obj = "" OrElse obj = "NA" Then
                            Dim flagDate = Date.Parse("")
                        Else
                            Dim flagDate = Date.Parse(obj)

                        End If

                    End If
                    k += 1
                Next

                types = "Date"


            ElseIf (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("String"))) Then
                Dim k As Integer = 0
                For Each obj In Form3.listColumn.ElementAt(index).ListColumnAll
                    If k <> 0 Then
                        If obj = "" OrElse obj = "NA" Then
                            Dim flagString = CStr("")
                        Else
                            Dim flagString = CStr(obj)

                        End If

                    End If
                    k += 1
                Next

                types = "String"

            Else
                'MessageBox.Show("Error#5")
                Me.CheckedListBox1.Enabled = True
                Me.Button2.Enabled = True
                Me.Button1.Enabled = False
                Exit Sub
            End If

            Me.Label3.Text = "Variables can be transformed into (" & types & ")"

            If String.IsNullOrWhiteSpace(Me.TextBox1.Text) Then
                Me.Label3.Text = "The variable name is null. Change it!"
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
            Me.Label3.Text = "Conversion cannot be performed"
            Me.CheckedListBox1.Enabled = True
            Me.TextBox1.Enabled = True
        End Try
    End Sub

    Private Sub Button3_MouseClick(sender As Object, e As MouseEventArgs) Handles Button3.MouseClick
        Dim indexVariable As String = Form3.listCp.ElementAt(0).Lists.ElementAt(index)
        Dim indexRadio As String = Form3.listColumn.ElementAt(index).typeT.ToString

        initial(indexRadio, indexVariable)
        Me.CheckedListBox1.Enabled = True
        Me.Button2.Enabled = True
        Me.Button1.Enabled = False
        Me.TextBox1.Enabled = True
        Me.Label3.Text = ""
    End Sub
End Class