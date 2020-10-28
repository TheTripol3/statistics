Public Class Form2


    Public listCp As New List(Of listCaption)
    Public listColumn As New List(Of listAllColumn)
    Public dict As New Dictionary(Of String, Integer)
    Public delimiter As New Char
    Dim extn As String
    Public countFlag As Integer = 0

    Public countBoolean As Integer = 0
    Public countInt32 As Integer = 0
    Public countInt64 As Integer = 0
    Public countDouble As Integer = 0
    Public countDateTime As Integer = 0
    Public countString As Integer = 0

    Dim flagData As Boolean

    Dim ListFlag As New List(Of Boolean)



    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim fi As New IO.FileInfo(Form1.Label4.Text)
        extn = fi.Extension

        MessageBox.Show("You must follow each step of each column above (required)", "Info")

        resetListFlag()

    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        Select Case ComboBox1.SelectedItem.ToString()
            Case "Open File"
                resetListFlag()
                resetData()
                Me.Hide()
                Form1.Show()
            Case "Reset File"
                resetListFlag()
                resetData()
                MessageBox.Show("Done!", "Reset")
            Case "Exit File"
                Form1.Close()
            Case Else
                MessageBox.Show("Error #1")

        End Select


    End Sub

    Public Sub resetData()
        listCp.Clear()
        listColumn.Clear()
        Me.RichTextBox1.Clear()
        Me.TreeView1.Nodes.Clear()
        dict.Clear()
        Me.ComboBox2.Text = "Analyze Information"
        Me.ComboBox3.Text = "Flag Info"
        Me.ComboBox4.Text = "Preview"



        Me.RichTextBox1.AppendText("File type (" & extn & ")" & Environment.NewLine)
    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        Select Case ComboBox2.SelectedItem.ToString()
            Case "Check Delimiter"
                resetData()
                checkDelimeters()
                ListFlag(0) = True

            Case "Get Information"
                If checkListFlag(1) Then
                    Me.TreeView1.Nodes.Clear()
                    listCp.Clear()
                    GetInformation()

                    If flagData Then
                        ListFlag(1) = True
                    End If

                End If

            Case "Get Header"
                If checkListFlag(2) Then
                    dict.Clear()
                    getHeader()
                    ListFlag(2) = True
                End If



            Case "Get Metadata"
                If checkListFlag(3) Then
                    listColumn.Clear()
                    extractMetadata()
                    Me.TreeView1.ExpandAll()
                    ListFlag(3) = True
                End If

            Case "Get Types"
                If checkListFlag(4) Then
                    dict.Clear()
                    getTypes()
                    computeTreeView()
                    Me.TreeView1.ExpandAll()
                    ListFlag(4) = True
                End If

            Case Else
                MessageBox.Show("Error #2")
        End Select

    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

        Select Case ComboBox3.SelectedItem.ToString()
            Case "Header Flag"
                If checkListFlag(5) Then
                    flagCaptioCheck()
                    computeTreeView()
                    Me.TreeView1.ExpandAll()
                    ListFlag(5) = True
                End If

            Case "Count Flag"
                If checkListFlag(6) Then
                    flagCountDimension()
                    ListFlag(6) = True
                End If

            Case Else
                MessageBox.Show("Error #3")
        End Select

    End Sub


    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

        Select Case ComboBox4.SelectedItem.ToString()
            Case "View"
                If checkListFlag(7) Then
                    Me.Hide()
                    Form3.Show()
                End If

            Case Else
                MessageBox.Show("Error #4")

        End Select

    End Sub

    Private Sub Form2Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Form1.Close()
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        e.Handled = True
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        e.Handled = True
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        e.Handled = True
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        e.Handled = True
    End Sub

    Private Sub flagCountDimension()

        Do
            Try

                Dim res As String = InputBox("How many rows do you want to select? Write 0 to get all the data",
                                             "Write the number", 10)

                If CInt(res) = 0 Then
                    countFlag = listColumn.ElementAt(0).ListColumnAll.Count
                Else
                    countFlag = CInt(res)
                End If


            Catch ex As Exception
                MessageBox.Show("Write a number!", "Error")
            End Try


        Loop Until countFlag > 0

        Me.RichTextBox1.AppendText(Environment.NewLine & "Selected rows (" & countFlag & ")" & Environment.NewLine)


    End Sub



    Private Sub flagCaptioCheck()
        Dim result As DialogResult = MessageBox.Show("Do you delete the first row from the dataset?", "Warning", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
            For Each pList In listCp
                pList.FlagCaption = False
                Me.RichTextBox1.AppendText(Environment.NewLine & "Header (NO)" & Environment.NewLine)
            Next
        ElseIf result = DialogResult.Yes Then
            For Each pList In listCp
                pList.FlagCaption = True
                Me.RichTextBox1.AppendText(Environment.NewLine & "Header (YES)" & Environment.NewLine)
            Next
        Else
        End If

    End Sub

    Private Sub GetInformation()

        Me.RichTextBox1.Clear()
        Me.RichTextBox1.AppendText("File type (" & extn & ")" & Environment.NewLine)
        Me.RichTextBox1.AppendText("The delimiter character (" & delimiter & ")" & Environment.NewLine & Environment.NewLine)

        retrieveInformation()

        For Each objectC In listCp
            Me.RichTextBox1.AppendText("File (" & Form1.Label4.Text & ")" & Environment.NewLine)
            Me.RichTextBox1.AppendText("Total Data (" & objectC.countD.ToString & ")" & Environment.NewLine)
            Me.RichTextBox1.AppendText("Total Columns (" & objectC.countC.ToString & ")" & Environment.NewLine)
            Me.RichTextBox1.AppendText("Total Rows (" & objectC.countR.ToString & ")" & Environment.NewLine)

            If objectC.countD = 0 Then
                flagData = False
                MessageBox.Show("No data . . .")
            Else
                flagData = True
            End If
        Next

    End Sub


    Private Sub getHeader()

        Me.TreeView1.Nodes.Clear()

        Dim resString As String = "Header" & Environment.NewLine & Environment.NewLine

        For Each pList In listCp
            For Each varCap In pList.Lists
                resString = resString & varCap & Environment.NewLine
            Next

            Dim result As DialogResult = MessageBox.Show(resString, "Warning", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then


            ElseIf result = DialogResult.No Then

                For k As Integer = 0 To (pList.Lists.Count - 1)

                    Dim res As String = InputBox("What is the name of column number  " & (k + 1) & " ?",
                                                         "Write the name", pList.Lists.ElementAt(k).ToString)

                    If res = "" Then
                        Exit For
                    End If

                    pList.Lists(k) = res

                Next

            End If

        Next

        Me.RichTextBox1.AppendText(Environment.NewLine & "Header . . ." & Environment.NewLine)
        For Each pList In listCp
            For Each varCap In pList.Lists
                Me.RichTextBox1.AppendText("(" & varCap & ")" & Environment.NewLine)

            Next
        Next
    End Sub

    Private Sub extractMetadata()

        Me.RichTextBox1.AppendText(Environment.NewLine & "Get Metadata . . ." & Environment.NewLine)

        Me.TreeView1.Nodes.Clear()

        Dim root = New TreeNode(Form1.Label4.Text)
        Me.TreeView1.Nodes.Add(root)

        For Each objectC1 In listCp

            transformRowToColumn(objectC1.Value, objectC1.countC)

            Dim countNode As Integer = 0
            Dim typeV As String = "ND"



            For Each objectC2 In listColumn

                Dim flagCount As Integer = 0

                Try
                    Me.TreeView1.Nodes(0).Nodes.Add(New TreeNode(objectC1.Lists.ElementAt(countNode) & "  (" & typeV & ")"))
                Catch ex As Exception
                    Me.TreeView1.Nodes(0).Nodes.Add(New TreeNode("ND"))
                End Try


                For Each objectC3 In objectC2.ListColumnAll

                    If (objectC1.FlagCaption) Then

                        If flagCount <> 0 Then
                            If (flagCount = 4) Then
                                Exit For
                            Else
                                TreeView1.Nodes(0).Nodes(countNode).Nodes.Add(New TreeNode(objectC3))
                            End If
                        End If

                    Else
                        If (flagCount = 4) Then
                            Exit For
                        Else
                            TreeView1.Nodes(0).Nodes(countNode).Nodes.Add(New TreeNode(objectC3))
                        End If
                    End If

                    flagCount += 1

                Next

                countNode += 1

            Next

        Next

    End Sub



    Private Sub computeTreeView()

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


                For Each objectC3 In objectC2.ListColumnAll

                    If (objectC1.FlagCaption) Then

                        If flagCount <> 0 Then

                            If (flagCount = 4) Then
                                Exit For
                            Else
                                TreeView1.Nodes(0).Nodes(countNode).Nodes.Add(New TreeNode(objectC3))
                            End If

                        End If

                    Else
                        If (flagCount = 4) Then
                            Exit For
                        Else
                            TreeView1.Nodes(0).Nodes(countNode).Nodes.Add(New TreeNode(objectC3))
                        End If

                    End If

                    flagCount += 1
                Next

                countNode += 1

            Next
        Next

    End Sub

    Public Sub retrieveInformation()
        Using MyReader As New FileIO.TextFieldParser(Form1.Label4.Text)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(delimiter)

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

                Catch ex As FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try

            End While

            listCap.countC = countColumn + 1
            listCap.Lists = listVariable
            listCap.countD = countData
            listCap.countR = countRow


            listCp.Add(listCap)


        End Using

    End Sub


    Private Sub getTypes()

        Me.RichTextBox1.AppendText(Environment.NewLine & "Get Types . . ." & Environment.NewLine)

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


                    If flagCount > 20 Then
                        Exit For
                    End If

                    flagCount += 1
                Next

                updateInfoParse(objectG2)

            Next

        Next

    End Sub


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


    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        'MessageBox.Show(TreeView1.SelectedNode.Text)
    End Sub


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


    Enum dataType
        System_Boolean = 0
        System_Integer = 1
        System_Long = 2
        System_Double = 3
        System_Date = 4
        System_String = 5
    End Enum

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
                    MsgBox("Line " & ex.Message &
                    "is not valid and will be skipped.")
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

        Dim result As DialogResult = MessageBox.Show("Found the delimiter character   " & delimiter, "Warning", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            Me.RichTextBox1.AppendText("The delimiter character (" & delimiter & ")" & Environment.NewLine & Environment.NewLine)
        ElseIf result = DialogResult.No Then
            Dim res As String = InputBox("What is the character?",
                                                     "", "Write the delimiter character")

            delimiter = res

            MessageBox.Show("The delimiter character    " & delimiter, "Warning")
            Me.RichTextBox1.AppendText("The delimiter character    " & delimiter & Environment.NewLine & Environment.NewLine)
        End If


    End Sub


    Private Sub resetListFlag()
        ListFlag.Clear()
        ListFlag.Add(False)
        ListFlag.Add(False)
        ListFlag.Add(False)
        ListFlag.Add(False)
        ListFlag.Add(False)
        ListFlag.Add(False)
        ListFlag.Add(False)
    End Sub

    Function checkListFlag(ind As Integer)

        If ListFlag(ind - 1) Then
            For s As Integer = ind To ListFlag.Count - 1
                ListFlag(s) = False
            Next
            Return True
        Else

            For s As Integer = 0 To ListFlag.Count - 1
                If Not ListFlag(s) Then
                    printFlag(s)
                    Exit For
                End If
            Next


            Return False
        End If
    End Function


    Private Sub printFlag(ind As Integer)
        Select Case ind
            Case 0
                MessageBox.Show("You are missing some steps, start with Check Delimiter")

            Case 1
                MessageBox.Show("You are missing some steps, start with Get Information")

            Case 2
                MessageBox.Show("You are missing some steps, start with Get Header")
            Case 3
                MessageBox.Show("You are missing some steps, start with Get Metadata")
            Case 4
                MessageBox.Show("You are missing some steps, start with Get Types")
            Case 5
                MessageBox.Show("You are missing some steps, start with Header Flag")
            Case 6
                MessageBox.Show("You are missing some steps, start with Count Flag")
            Case Else
                MessageBox.Show("You are missing some steps, start with Check Delimiter")

        End Select
    End Sub


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


End Class