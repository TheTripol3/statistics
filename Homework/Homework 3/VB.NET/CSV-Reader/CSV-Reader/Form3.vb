'Form3: DataView
Imports System.Globalization
Imports System.IO

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

    'Create GridView
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

            'Add Rows
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


            'Insert column by column
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
                            If listColumn.ElementAt(k).ListColumnAll.ElementAt(j + 1).ToString = "" Then
                                Select Case types.ToString
                                    Case "System.Boolean"
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Value = ""
                                    Case "System.Int32"
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Value = 0
                                    Case "System.Int64"
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Value = 0
                                    Case "System.Double"
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Value = 0
                                    Case "System.DateTime"
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Value = ""
                                    Case Else
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(row - 1).Cells(k).Value = ""
                                End Select
                            Else
                                Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                Me.DataGridView1.Rows(row - 1).Cells(k).Value = Convert.ChangeType(listColumn.ElementAt(k).ListColumnAll.ElementAt(j + 1).ToString, types, CultureInfo.InvariantCulture)
                            End If


                        Catch ex As Exception
                            If (j <> listColumn.ElementAt(k).ListColumnAll.Count - 1) Then
                                errorF = True

                                If listColumn.ElementAt(k).ListColumnAll.ElementAt(j + 1).ToString = "" Then
                                    Select Case types.ToString
                                        Case "System.Boolean"
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Value = ""
                                        Case "System.Int32"
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Value = 0
                                        Case "System.Int64"
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Value = 0
                                        Case "System.Double"
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Value = 0
                                        Case "System.DateTime"
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Value = ""
                                        Case Else
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(row - 1).Cells(k).Value = ""
                                    End Select
                                Else
                                    Me.DataGridView1.Rows(row - 1).Cells(k).Style.BackColor = Color.Yellow
                                    Me.DataGridView1.Rows(row - 1).Cells(k).Value = Nothing
                                End If

                            End If
                        End Try
                        row += 1
                    Else
                        Try
                            If listColumn.ElementAt(k).ListColumnAll.ElementAt(j).ToString = "" Then
                                Select Case types.ToString
                                    Case "System.Boolean"
                                        Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(j).Cells(k).Value = ""
                                    Case "System.Int32"
                                        Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(j).Cells(k).Value = 0
                                    Case "System.Int64"
                                        Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(j).Cells(k).Value = 0
                                    Case "System.Double"
                                        Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(j).Cells(k).Value = 0
                                    Case "System.DateTime"
                                        Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(j).Cells(k).Value = ""
                                    Case Else
                                        Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                        Me.DataGridView1.Rows(j).Cells(k).Value = ""
                                End Select
                            Else
                                Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                Me.DataGridView1.Rows(j).Cells(k).Value = Convert.ChangeType(listColumn.ElementAt(k).ListColumnAll.ElementAt(j).ToString, types, CultureInfo.InvariantCulture)
                            End If




                        Catch ex As Exception
                            If (j <> listColumn.ElementAt(k).ListColumnAll.Count - 1) Then
                                errorF = True

                                If listColumn.ElementAt(k).ListColumnAll.ElementAt(j).ToString = "" Then
                                    Select Case types.ToString
                                        Case "System.Boolean"
                                            Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(j).Cells(k).Value = ""
                                        Case "System.Int32"
                                            Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(j).Cells(k).Value = 0
                                        Case "System.Int64"
                                            Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(j).Cells(k).Value = 0
                                        Case "System.Double"
                                            Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(j).Cells(k).Value = 0
                                        Case "System.DateTime"
                                            Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(j).Cells(k).Value = ""
                                        Case Else
                                            Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = DefaultBackColor()
                                            Me.DataGridView1.Rows(j).Cells(k).Value = ""
                                    End Select
                                Else
                                    Me.DataGridView1.Rows(j).Cells(k).Style.BackColor = Color.Yellow
                                    Me.DataGridView1.Rows(j).Cells(k).Value = Nothing
                                End If
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

    'Create Sidebar
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

    'Create TreeView
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

    'CheckList: Header
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

    'Change the number of lines
    Private Sub CheckBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles CheckBox1.MouseClick
        If IsNumeric(Me.TextBox1.Text) Then
            Dim countRows As Integer = Me.TextBox1.Text
            Try
                If countRows = 0 Then
                    If listColumn.ElementAt(0).ListColumnAll.Count = 0 Then
                        countFlag = listColumn.ElementAt(0).ListColumnAll.Count
                        Form2.countFlag = listColumn.ElementAt(0).ListColumnAll.Count
                    Else
                        countFlag = 0
                        Form2.countFlag = 0
                    End If
                Else
                    countFlag = countRows
                    Form2.countFlag = countRows
                End If
            Catch ex As Exception
                countFlag = 0
                Form2.countFlag = 0
                MessageBox.Show("There are no data. . .", "Warning")
            End Try

            createGridView()

        Else
            MessageBox.Show("It must be a number.", "Warning")
        End If
    End Sub


    'Extract Metadata
    Private Sub extractMetadata()
        For Each objectC1 In listCp
            Form2.transformRowToColumn(objectC1.Value, objectC1.countC)
        Next
    End Sub

    'Get Data Types
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

    'Change the delimiter character
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

    'Select the item of the TreeView
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If Me.TreeView1.SelectedNode.Text <> Form1.Label4.Text Then
            index = Me.TreeView1.SelectedNode.Index
            Form4.Show()
            Form4.BringToFront()
        End If
    End Sub

    'Oper Average Form
    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        Form5.Show()
        Form5.BringToFront()
    End Sub

    'Open Distribution Form
    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        Form6.Show()
        Form6.BringToFront()
    End Sub

    'Print GridView into file
    Private Sub Button3_MouseClick(sender As Object, e As MouseEventArgs) Handles Button3.MouseClick
        Dim sfd As New SaveFileDialog()
        sfd.FileName = "Export DataGridView"
        sfd.Filter = "csv File | *.csv |txt File | *.txt"

        If sfd.ShowDialog() = DialogResult.OK Then
            Using sw As StreamWriter = File.CreateText(sfd.FileName)
                Dim dvgColumnNames As List(Of String) = DataGridView1.Columns.Cast(Of DataGridViewColumn).ToList().Select(Function(c) c.Name).ToList()

                sw.WriteLine(String.Join(Form2.delimiter, dvgColumnNames))

                For Each row As DataGridViewRow In DataGridView1.Rows
                    Dim rowData As New List(Of String)
                    For Each column As DataGridViewColumn In DataGridView1.Columns
                        rowData.Add(Convert.ToString(row.Cells(column.Name).Value))
                    Next
                    sw.WriteLine(String.Join(Form2.delimiter, rowData))

                Next

                'Open csv file after written
                Process.Start(sfd.FileName)

            End Using
        End If


    End Sub

    'Print data into file
    Private Sub Button4_MouseClick(sender As Object, e As MouseEventArgs) Handles Button4.MouseClick

        Dim sfd As New SaveFileDialog()
        sfd.FileName = "Export Data"
        sfd.Filter = "csv File | *.csv |txt File | *.txt"

        If sfd.ShowDialog() = DialogResult.OK Then
            Using sw As StreamWriter = File.CreateText(sfd.FileName)
                Dim dvgColumnNames As List(Of String) = Form2.listCp(0).Lists

                sw.WriteLine(String.Join(Form2.delimiter, dvgColumnNames))

                If Form2.listCp(0).FlagCaption Then

                    For s As Integer = 1 To listColumn.ElementAt(0).ListColumnAll.Count - 1

                        Dim rowData As New List(Of String)

                        For k As Integer = 0 To Form2.listColumn.Count - 1

                            rowData.Add(listColumn.ElementAt(k).ListColumnAll.ElementAt(s).ToString())

                        Next

                        sw.WriteLine(String.Join(Form2.delimiter, rowData))
                    Next

                Else

                    For s As Integer = 0 To listColumn.ElementAt(0).ListColumnAll.Count - 1

                        Dim rowData As New List(Of String)

                        For k As Integer = 0 To Form2.listColumn.Count - 1

                            rowData.Add(listColumn.ElementAt(k).ListColumnAll.ElementAt(s).ToString())

                        Next

                        sw.WriteLine(String.Join(Form2.delimiter, rowData))
                    Next

                End If

                'Open csv file after written
                Process.Start(sfd.FileName)

            End Using
        End If
    End Sub
End Class