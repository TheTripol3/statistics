'Form3: DataView
Imports System.Globalization
Imports System.IO

Public Class Form3

    'Check
    Public countFlag As Integer



    Public listCp As New List(Of listCaption)
    Public listColumn As New List(Of listAllColumn)
    Public dict As New Dictionary(Of String, Integer)
    Public index As Integer = 0
    Public delimiter As New Char


    Public countBoolean As Integer = 0
    Public countInt32 As Integer = 0
    Public countInt64 As Integer = 0
    Public countDouble As Integer = 0
    Public countDateTime As Integer = 0
    Public countString As Integer = 0


    Public Sub initialize()
        countFlag = 10
        checkDelimeters()
        retrieveInformation()
        extractMetadata()
        getTypes()
        createTreeView()

        createGridView()
        createSidebar()
    End Sub

    'Create GridView
    Sub createGridView()

        Me.DataGridView1.Rows.Clear()
        Me.DataGridView1.Columns.Clear()
        Me.RichTextBox1.Clear()

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
                            If listColumn.ElementAt(k).ListColumnAll.ElementAt(j + 1).ToString = "" OrElse listColumn.ElementAt(k).ListColumnAll.ElementAt(j + 1).ToString = "NA" Then
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

                                If listColumn.ElementAt(k).ListColumnAll.ElementAt(j + 1).ToString = "" OrElse listColumn.ElementAt(k).ListColumnAll.ElementAt(j + 1).ToString = "NA" Then
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
                            If listColumn.ElementAt(k).ListColumnAll.ElementAt(j).ToString = "" OrElse listColumn.ElementAt(k).ListColumnAll.ElementAt(j).ToString = "NA" Then
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

                                If listColumn.ElementAt(k).ListColumnAll.ElementAt(j).ToString = "" OrElse listColumn.ElementAt(k).ListColumnAll.ElementAt(j).ToString = "" Then
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
                    Me.RichTextBox1.AppendText("Error Type (Column " & listCp.ElementAt(0).Lists(k) & "): You should change the data type from (" & types.ToString & ") to (System.String)" & Environment.NewLine)
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
        TextBox1.Text = countFlag
        TextBox2.Text = delimiter
    End Sub

    'Create TreeView
    Public Sub createTreeView()

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
                        countFlag = listColumn.ElementAt(0).ListColumnAll.Count
                    Else
                        countFlag = 0
                    End If
                Else
                    If (countRows > listColumn.ElementAt(0).ListColumnAll.Count) Then
                        If (listCp.ElementAt(0).FlagCaption) Then
                            countFlag = listColumn.ElementAt(0).ListColumnAll.Count - 1
                            Me.TextBox1.Text = countFlag
                        Else
                            countFlag = listColumn.ElementAt(0).ListColumnAll.Count
                            Me.TextBox1.Text = countFlag
                        End If
                    Else
                        countFlag = countRows
                    End If

                End If
            Catch ex As Exception
                countFlag = 0
                Me.RichTextBox1.Clear()
                Me.RichTextBox1.AppendText("There are no data. . .")
            End Try

            createGridView()

        Else
            Me.RichTextBox1.Clear()
            Me.RichTextBox1.AppendText("It must be a number.")
        End If
    End Sub


    'Extract Metadata
    Private Sub extractMetadata()
        For Each objectC1 In listCp
            transformRowToColumn(objectC1.Value, objectC1.countC)
        Next
    End Sub

    'Get Data Types
    Private Sub getTypes()

        For Each objectG1 In listCp

            Dim countNode As Integer = 0

            For Each objectG2 In listColumn

                Dim flagCount As Integer = 0
                countBoolean = 0
                countInt32 = 0
                countInt64 = 0
                countDouble = 0
                countDateTime = 0
                countString = 0

                For Each objectC3 In objectG2.ListColumnAll

                    Dim result As Integer = ParseString(objectC3)
                    updateCountParse(result)


                    'If flagCount > 20 Then
                    '    Exit For
                    'End If

                    'flagCount += 1
                Next

                updateInfoParse(objectG2)

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
        delimiter = countDelimiter

        resetData()
        retrieveInformation()
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

                sw.WriteLine(String.Join(delimiter, dvgColumnNames))

                For Each row As DataGridViewRow In DataGridView1.Rows
                    Dim rowData As New List(Of String)
                    For Each column As DataGridViewColumn In DataGridView1.Columns
                        rowData.Add(Convert.ToString(row.Cells(column.Name).Value))
                    Next
                    sw.WriteLine(String.Join(delimiter, rowData))

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
                Dim dvgColumnNames As List(Of String) = listCp(0).Lists

                sw.WriteLine(String.Join(delimiter, dvgColumnNames))

                If listCp(0).FlagCaption Then

                    For s As Integer = 1 To listColumn.ElementAt(0).ListColumnAll.Count - 1

                        Dim rowData As New List(Of String)

                        For k As Integer = 0 To listColumn.Count - 1

                            rowData.Add(listColumn.ElementAt(k).ListColumnAll.ElementAt(s).ToString())

                        Next

                        sw.WriteLine(String.Join(delimiter, rowData))
                    Next

                Else

                    For s As Integer = 0 To listColumn.ElementAt(0).ListColumnAll.Count - 1

                        Dim rowData As New List(Of String)

                        For k As Integer = 0 To listColumn.Count - 1

                            rowData.Add(listColumn.ElementAt(k).ListColumnAll.ElementAt(s).ToString())

                        Next

                        sw.WriteLine(String.Join(delimiter, rowData))
                    Next

                End If

                'Open csv file after written
                Process.Start(sfd.FileName)

            End Using
        End If
    End Sub



    'Check delimiters
    Private Sub checkDelimeters()

        Dim row0 As String = ""

        Using MyReader As New FileIO.TextFieldParser(Form1.Label4.Text)

            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters("&&&&")
            Dim currentRow As String()

            While Not MyReader.EndOfData

                Try

                    currentRow = MyReader.ReadFields

                    For Each currentField As String In currentRow
                        row0 = currentField
                        Continue For
                    Next

                    Exit While

                Catch ex As FileIO.MalformedLineException
                    Me.RichTextBox1.Clear()
                    Me.RichTextBox1.AppendText("Line " & ex.Message & "is not valid and will be skipped.")
                End Try

            End While

        End Using


        Dim character As Char, c As Integer = 0, c2 As Integer = 0

        Dim Arr() = {",", ";", "|"}

        For Each character In Arr

            c2 = row0.Split(character).Length - 1

            If (c2 >= c) Then
                delimiter = character
                c = c2
            End If
        Next

    End Sub

    'Sub: Transform Rows to Columns
    Sub transformRowToColumn(ListData As List(Of listAllRow), numberColumn As Integer)


        For k As Integer = 0 To (numberColumn - 1)
            Dim objectColumn As New listAllColumn

            For Each eachList In ListData

                objectColumn.ListColumnAll.Add(eachList.ListRow.ElementAt(k))

            Next

            objectColumn.orderColumn = k
            listColumn.Add(objectColumn)

        Next

    End Sub



    Public Sub retrieveInformation()
        Using MyReader As New FileIO.TextFieldParser(Form1.Label4.Text)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.Delimiters = New String() {delimiter}

            Dim currentRow As String()
            Dim countRow As Integer = 0
            Dim countData As Integer = 0
            Dim countColumn As Integer = 0

            Dim flag As Boolean = True

            Dim listVariable As New List(Of String)
            Dim listValue As New List(Of String)
            Dim listCap As New listCaption


            While Not MyReader.EndOfData

                Try
                    Dim listData As New listAllRow

                    currentRow = MyReader.ReadFields
                    Dim countPreview As Integer = 0

                    For Each currentField As String In currentRow

                        If (flag) Then
                            listVariable.Add(currentField)
                            countColumn = currentRow.GetUpperBound(0)
                        End If

                        listData.ListRow.Add(currentField)

                        countData += 1
                        countPreview += 1
                    Next

                    countRow += 1
                    flag = False

                    listCap.Value.Add(listData)

                Catch ex As Exception
                    Me.RichTextBox1.Clear()
                    Me.RichTextBox1.AppendText("The delimiter character(" & delimiter & ") is not present on all rows")
                    Exit Sub
                End Try

            End While

            listCap.countC = countColumn + 1
            listCap.Lists = listVariable
            listCap.countD = countData
            listCap.countR = countRow


            listCp.Add(listCap)


        End Using

    End Sub


    'Sub: Dictionary containing how many types of data it checked
    Public Sub updateInfoParse(obj As listAllColumn)

        dict.Clear()

        dict.Add("Boolean", countBoolean)
        dict.Add("Int32", countInt32)
        dict.Add("Int64", countInt64)
        dict.Add("Double", countDouble)
        dict.Add("DateTime", countDateTime)
        dict.Add("String", countString)

        dict = dict.OrderBy(Function(x) x.Value).ToDictionary(Function(x) x.Key, Function(x) x.Value)
        checkType(dict.Keys.Last, obj)

    End Sub



    'Sub: Checks what kind of data an element is
    Private Sub checkType(str As String, obj As listAllColumn)

        Dim stringText As String = "file"
        Dim booleanText As Boolean = True
        Dim integerText As Integer = 3
        Dim longText As Long = 3131313131
        Dim doubleText As Double = 2121.21
        Dim dateText As Date = "2020 / 10 / 10"

        Select Case str
            Case "Boolean"

                obj.typeT = booleanText.GetType()

            Case "Int32"

                obj.typeT = integerText.GetType()


            Case "Int64"
                obj.typeT = longText.GetType()


            Case "Double"
                obj.typeT = doubleText.GetType()


            Case "DateTime"
                obj.typeT = dateText.GetType()


            Case Else
                obj.typeT = stringText.GetType()
        End Select


    End Sub


    'Sub: Increment a variable to know which type of data is most present
    Public Sub updateCountParse(result As Integer)

        Select Case result

            Case 0
                countBoolean += 1
            Case 1
                countInt32 += 1
            Case 2
                countInt64 += 1
            Case 3
                countDouble += 1
            Case 4
                countDateTime += 1
            Case Else
                countString += 1
        End Select

    End Sub



    'Sub Data Reset
    Public Sub resetData()
        listCp.Clear()
        listColumn.Clear()
        Me.TreeView1.Nodes.Clear()
        dict.Clear()
        index = 0
    End Sub


    'Function: Data Parse to check the type
    Public Function ParseString(ByVal str As String)
        Dim boolValue As Boolean
        Dim intValue As Integer
        Dim bigintValue As Long
        Dim doubleValue As Double
        Dim dateValue As Date

        If Boolean.TryParse(str, boolValue) Then
            Return 0
        ElseIf Integer.TryParse(str, intValue) Then
            Return 1
        ElseIf Long.TryParse(str, bigintValue) Then
            Return 2
        ElseIf Double.TryParse(str, doubleValue) Then
            Return 3
        ElseIf Date.TryParse(str, dateValue) Then
            Return 4
        Else
            Return 5
        End If
    End Function

    'Enum containing the default data types
    Enum dataType
        System_Boolean = 0
        System_Integer = 1
        System_Long = 2
        System_Double = 3
        System_Date = 4
        System_String = 5
    End Enum


    Public Class listCaption
        Public Lists As New List(Of String)
        Public Value As New List(Of listAllRow)
        Public FlagCaption As Boolean = True
        Public countC As Integer = 0
        Public countR As Integer = 0
        Public countD As Integer = 0
    End Class



    Public Class listAllRow
        Public ListRow As New List(Of String)
    End Class

    Public Class listAllColumn
        Public ListColumnAll As New List(Of String)
        Public orderColumn As Integer = 0
        Public typeT As Type = Nothing
    End Class

    Private Sub Button5_MouseClick(sender As Object, e As MouseEventArgs) Handles Button5.MouseClick
        resetData()
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Button6_MouseClick(sender As Object, e As MouseEventArgs) Handles Button6.MouseClick
        Form8.Show()
        Form7.BringToFront()
    End Sub
End Class