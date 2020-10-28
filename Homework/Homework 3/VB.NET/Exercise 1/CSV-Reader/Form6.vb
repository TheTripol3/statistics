Public Class Form6

    Dim countFlag As Integer
    Dim listColumn As List(Of Form2.listAllColumn)
    Dim listCp As List(Of Form2.listCaption)
    Dim order As Integer
    Dim flag As Boolean = False
    Public flagDistribution As Boolean = Nothing

    Dim sum = 0.0
    Dim c = 0.0
    Public count = 0
    Dim average = 0.0

    Public FrequencyDistribution As New SortedDictionary(Of Integer, frequency)

    Public lisInterval As New List(Of intervals)
    Dim startEndPoint
    Dim intervalSize As Double = 10


    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        createTreeView()
    End Sub


    Private Sub createTreeView()

        countFlag = Form2.countFlag
        listColumn = Form2.listColumn
        listCp = Form2.listCp


        Me.TreeView1.Nodes.Clear()
        Me.TreeView1.ExpandAll()
        Me.TreeView1.CheckBoxes = True

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


    Private Sub TreeView1_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterCheck

        For Each ob As TreeNode In TreeView1.Nodes
            chknode(ob)
        Next

        TreeView1.CheckBoxes = False
        TreeView1.ExpandAll()

        Try

            If flag Then

                If listCp(0).FlagCaption Then

                    Dim k As Type = Form2.listColumn.ElementAt(order).typeT

                    Dim arrT As ArrayList = New ArrayList()

                    For s As Integer = 1 To (listColumn.ElementAt(order).ListColumnAll.Count - 1)
                        Dim a = Convert.ChangeType(listColumn.ElementAt(order).ListColumnAll.ElementAt(s), k)
                        arrT.Add(a)
                    Next

                    kahanSub(arrT)

                    Me.RichTextBox1.AppendText(Environment.NewLine & "Method (Kahan) . . ." & Environment.NewLine & Environment.NewLine)



                    If k.ToString = "System.Int32" OrElse k.ToString = "System.Int64" Then
                        flagDistribution = False
                        Dim obj2 As New frequency

                        FrequencyDistribution = obj2.calculateDistribution(arrT)
                        Me.RichTextBox1.AppendText(Environment.NewLine)

                        obj2.printDistribution(FrequencyDistribution, (listColumn.ElementAt(order).ListColumnAll.Count - 1))
                    ElseIf k.ToString = "System.Double" Then
                        flagDistribution = True
                        Dim obj3 As New intervals

                        obj3.initialize(lisInterval, Math.Ceiling(average), intervalSize)

                        For i As Integer = 0 To (arrT.Count - 1)
                            Dim range As String = obj3.findRange(arrT(i), lisInterval)
                            obj3.calculateContinuousDistribution(intervalSize, arrT(i), lisInterval, range)
                        Next

                        obj3.printContinuousDistribution(lisInterval)



                    Else

                        Exit Try

                    End If

                    Me.Label6.Text = listCp(0).Lists.ElementAt(order)
                    Me.Label7.Text = (listColumn.ElementAt(order).ListColumnAll.Count - 1)
                    Me.Label9.Text = sum
                    Me.Label11.Text = average
                    Me.Button2.Enabled = True

                Else


                    Dim k As Type = Form2.listColumn.ElementAt(order).typeT

                    Dim arrT As ArrayList = New ArrayList()

                    For s As Integer = 0 To (listColumn.ElementAt(order).ListColumnAll.Count - 1)
                        Dim a = Convert.ChangeType(listColumn.ElementAt(order).ListColumnAll.ElementAt(s), k)
                        arrT.Add(a)
                    Next

                    kahanSub(arrT)

                    Me.RichTextBox1.AppendText(Environment.NewLine & "Method (Kahan) . . ." & Environment.NewLine & Environment.NewLine)

                    If k.ToString = "System.Int32" OrElse k.ToString = "System.Int64" Then
                        flagDistribution = False
                        Dim obj2 As New frequency

                        FrequencyDistribution = obj2.calculateDistribution(arrT)
                        Me.RichTextBox1.AppendText(Environment.NewLine)

                        obj2.printDistribution(FrequencyDistribution, listColumn.ElementAt(order).ListColumnAll.Count)

                    ElseIf k.ToString = "System.Double" Then
                        flagDistribution = True
                        Dim obj3 As New intervals

                        obj3.initialize(lisInterval, Math.Ceiling(average), intervalSize)

                        For i As Integer = 0 To (arrT.Count - 1)
                            Dim range As String = obj3.findRange(arrT(i), lisInterval)
                            obj3.calculateContinuousDistribution(intervalSize, arrT(i), lisInterval, range)
                        Next

                        obj3.printContinuousDistribution(lisInterval)

                    Else
                        Exit Try
                    End If



                    Me.Label6.Text = listCp(0).Lists.ElementAt(order)
                    Me.Label7.Text = (listColumn.ElementAt(order).ListColumnAll.Count)
                    Me.Label9.Text = sum
                    Me.Label11.Text = average

                    Me.Button2.Enabled = True

                End If

            End If
        Catch ex As Exception
            MessageBox.Show("Error#8")
        End Try


    End Sub


    Private Sub chknode(tree As TreeNode)

        For Each obj As TreeNode In tree.Nodes
            If obj.Checked Then
                If obj.Text <> Form1.Label4.Text Then
                    order = obj.Index
                    Dim index As String = Form2.listColumn.ElementAt(order).typeT.ToString
                    If index = "System.Int32" OrElse
                        index = "System.Int64" OrElse index = "System.Double" Then
                        If checkType() Then
                            obj.BackColor = Color.Yellow
                            flag = True
                        Else
                            MessageBox.Show("Error#8 (Type): The data is of a different types", "Warning")
                        End If


                    Else
                        MessageBox.Show("It must be a number", "Warning")
                    End If

                End If
            End If
            chknode(obj)
        Next

    End Sub



    Private Function checkType()
        Try
            Dim k As Type = Form2.listColumn.ElementAt(order).typeT
            If listCp(0).FlagCaption Then

                For s As Integer = 1 To (listColumn.ElementAt(order).ListColumnAll.Count - 1)
                    Dim a = Convert.ChangeType(listColumn.ElementAt(order).ListColumnAll.ElementAt(s), k)
                Next

            Else

                For s As Integer = 0 To (listColumn.ElementAt(order).ListColumnAll.Count - 1)
                    Dim a = Convert.ChangeType(listColumn.ElementAt(order).ListColumnAll.ElementAt(s), k)
                Next
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub uncheckNode(tree As TreeNode)
        For Each obj As TreeNode In tree.Nodes
            obj.Checked = False
            obj.BackColor = Color.Transparent
        Next

    End Sub

    Private Sub Button1_MouseClick(sender As Object, e As MouseEventArgs) Handles Button1.MouseClick
        resetCheck()
    End Sub


    Private Sub resetCheck()
        For Each ob As TreeNode In TreeView1.Nodes
            uncheckNode(ob)
        Next
        TreeView1.CheckBoxes = True
        TreeView1.ExpandAll()

        Me.Label6.Text = ""
        Me.Label7.Text = ""
        Me.Label9.Text = ""
        Me.Label11.Text = ""
        Me.RichTextBox1.Clear()
        order = Nothing
        flag = False
        FrequencyDistribution.Clear()
        lisInterval.Clear()
        Me.Button2.Enabled = False


    End Sub


    Sub kahanSub(ls As ArrayList)
        sum = 0.0
        c = 0.0
        count = 0


        For i As Integer = 0 To (ls.Count - 1)
            Dim y = ls(i) - c
            Dim t = sum + y
            c = (t - sum) - y
            sum = t
            count += 1
        Next

        average = sum / count
    End Sub

    Private Sub Button2_MouseClick(sender As Object, e As MouseEventArgs) Handles Button2.MouseClick
        Form7.Show()
        Form7.BringToFront()
    End Sub
End Class