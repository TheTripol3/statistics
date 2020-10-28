Public Class Form3

    Dim countFlag As Integer
    Dim listColumn As List(Of Form2.listAllColumn)
    Dim listCp As List(Of Form2.listCaption)
    Public index As Integer = 0


    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        countFlag = Form2.countFlag
        listColumn = Form2.listColumn
        listCp = Form2.listCp

        createGridView()
        createTreeView()
        createSidebar()
    End Sub

    Sub createGridView()
        countFlag = Form2.countFlag
        listColumn = Form2.listColumn
        listCp = Form2.listCp


        Me.DataGridView1.Rows.Clear()
        Me.DataGridView1.Columns.Clear()

        Dim count As Integer = 0

        For Each obj In listCp
            count = 0
            Me.DataGridView1.ColumnCount = obj.countC

            For Each objList In obj.Lists
                Me.DataGridView1.Columns(count).Name = objList
                count += 1
            Next

            If countFlag >= listColumn.ElementAt(0).ListColumnAll.Count Then
                If obj.FlagCaption Then
                    For k As Integer = 1 To listColumn.ElementAt(0).ListColumnAll.Count - 1
                        Me.DataGridView1.Rows.Add()
                    Next
                Else
                    For k As Integer = 1 To listColumn.ElementAt(0).ListColumnAll.Count
                        Me.DataGridView1.Rows.Add()
                    Next
                End If

            Else
                For k As Integer = 1 To (countFlag - 1)
                    Me.DataGridView1.Rows.Add()
                Next
            End If






            For k As Integer = 0 To (obj.countC - 1)
                Dim errorF As Boolean = False
                Dim row As Integer = 1
                Dim types As Type = listColumn.ElementAt(k).typeT

                For j As Integer = 0 To (listColumn.ElementAt(k).ListColumnAll.Count - 1)

                    If j >= countFlag Then
                        Exit For
                    End If

                    If obj.FlagCaption Then
                        Try
                            Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                            Me.DataGridView1.Rows(row - 1).Cells(k).Value = Convert.ChangeType(listColumn.ElementAt(k).ListColumnAll.ElementAt(j + 1).ToString, types)

                        Catch ex As Exception
                            If (j <> listColumn.ElementAt(k).ListColumnAll.Count - 1) Then
                                errorF = True
                                Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = Color.Yellow
                                Me.DataGridView1.Rows(row - 1).Cells(k).Value = Nothing
                            End If
                        End Try
                        row += 1
                    Else
                        Try
                            Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                            Me.DataGridView1.Rows(j).Cells(k).Value = Convert.ChangeType(listColumn.ElementAt(k).ListColumnAll.ElementAt(j).ToString, types)
                        Catch ex As Exception
                            If (j <> listColumn.ElementAt(k).ListColumnAll.Count - 1) Then
                                errorF = True
                                Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = Color.Yellow
                                Me.DataGridView1.Rows(j).Cells(k).Value = Nothing
                            End If
                        End Try
                    End If

                Next

                If errorF Then
                    MessageBox.Show("Error Type (Column " & listCp.ElementAt(0).Lists(k) & "): You should change the data type from (" & types.ToString & ") to (System.String)", "Warning")
                End If
            Next
        Next
    End Sub

    Private Sub createSidebar()
        For Each obj In listCp
            If obj.FlagCaption Then
                CheckedListBox1.SetItemChecked(0, True)
                CheckedListBox1.SetItemChecked(1, False)
            Else
                CheckedListBox1.SetItemChecked(0, False)
                CheckedListBox1.SetItemChecked(1, True)
            End If
        Next
        TextBox1.Text = Form2.countFlag
        TextBox2.Text = Form2.delimiter
    End Sub

    Public Sub createTreeView()

        countFlag = Form2.countFlag
        listColumn = Form2.listColumn
        listCp = Form2.listCp

        Me.TreeView1.Nodes.Clear()

        Dim root = New TreeNode(Form1.Label4.Text)
        Me.TreeView1.Nodes.Add(root)

        Dim countNode As Integer = 0

        For Each objectC1 In listCp
            For Each objectC2 In listColumn

                Dim flagCount As Integer = 0
                Dim typeV As String

                If objectC2.typeT = Nothing Then
                    typeV = "ND"
                Else
                    typeV = objectC2.typeT.ToString()
                End If

                Try
                    Me.TreeView1.Nodes(0).Nodes.Add(New TreeNode(objectC1.Lists.ElementAt(countNode) & "  (" & typeV & ")"))
                Catch ex As Exception
                    Me.TreeView1.Nodes(0).Nodes.Add(New TreeNode("ND"))
                End Try

                countNode += 1

            Next
        Next
        Me.TreeView1.ExpandAll()
    End Sub




    Private Sub Form3Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Form1.Close()
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

        If (CheckedListBox1.GetItemChecked(CheckedListBox1.Items.IndexOf("Yes"))) Then
            For Each obj In listCp
                obj.FlagCaption = True
                createTreeView()
                createGridView()
            Next
        Else
            For Each obj In listCp
                obj.FlagCaption = False
                createTreeView()
                createGridView()

            Next
        End If


    End Sub

    Private Sub CheckBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles CheckBox1.MouseClick
        If IsNumeric(Me.TextBox1.Text) Then
            Dim countRows As Integer = Me.TextBox1.Text
            countFlag = countRows
            Form2.countFlag = countRows
            createGridView()
        Else
            MessageBox.Show("It must be a number", "Warning")
        End If
    End Sub



    Private Sub extractMetadata()
        For Each objectC1 In listCp
            Form2.transformRowToColumn(objectC1.Value, objectC1.countC)
        Next
    End Sub

    Private Sub getTypes()

        For Each objectG1 In listCp

            Dim countNode As Integer = 0

            For Each objectG2 In listColumn

                Dim flagCount As Integer = 0
                Form2.countBoolean = 0
                Form2.countInt32 = 0
                Form2.countInt64 = 0
                Form2.countDouble = 0
                Form2.countDateTime = 0
                Form2.countString = 0

                For Each objectC3 In objectG2.ListColumnAll

                    Dim result As Integer = Form2.ParseString(objectC3)
                    Form2.updateCountParse(result)


                    If flagCount > 20 Then
                        Exit For
                    End If

                    flagCount += 1
                Next

                Form2.updateInfoParse(objectG2)

            Next

        Next

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        CheckBox1.Checked = 0
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        CheckBox2.Checked = 0

    End Sub

    Private Sub CheckBox2_MouseClick(sender As Object, e As MouseEventArgs) Handles CheckBox2.MouseClick

        Dim countDelimiter As Char = Me.TextBox2.Text
        Form2.delimiter = countDelimiter

        Form2.resetData()
        Form2.retrieveInformation()
        extractMetadata()
        getTypes()
        createTreeView()
        createGridView()
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If Me.TreeView1.SelectedNode.Text <> Form1.Label4.Text Then
            index = Me.TreeView1.SelectedNode.Index
            Form4.Show()
            Form4.BringToFront()
        End If
    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        Form5.Show()
        Form5.BringToFront()
    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        Form6.Show()
        Form6.BringToFront()
    End Sub
End Class