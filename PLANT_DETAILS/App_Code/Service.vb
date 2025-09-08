Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Data.DataSet
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<ScriptService()>
Public Class Service
    Inherits System.Web.Services.WebService

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Outsource_F_ITEM(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (ITEM_CODE + ' , ' + ITEM_NAME) AS ITEM_CODE ,ITEM_NAME,ITEM_AU,MAT_AVG from Outsource_F_ITEM where" &
        " ITEM_CODE like @SearchText + '%' or ITEM_name like '%' + @SearchText + '%' order by ITEM_CODE desc"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}", sdr("ITEM_CODE"), sdr("ITEM_NAME"), sdr("ITEM_AU"), sdr("MAT_AVG")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function material(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (MAT_CODE + ' , ' + MAT_NAME) AS MAT_CODE ,MAT_NAME,MAT_AU,MAT_AVG from MATERIAL where" &
        " MAT_CODE like @SearchText + '%' or mat_name like '%' + @SearchText + '%' or MAT_DRAW like '%' + @SearchText + '%' order by MAT_CODE desc"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}", sdr("MAT_CODE"), sdr("MAT_NAME"), sdr("MAT_AU"), sdr("MAT_AVG")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function r_material(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (MAT_CODE + ' , ' + MAT_NAME) AS MAT_CODE ,MAT_NAME,MAT_AU,MAT_AVG from MATERIAL where" &
        " mat_code like '100%' and (MAT_CODE like @SearchText + '%' or mat_name like '%' + @SearchText + '%')"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}", sdr("MAT_CODE"), sdr("MAT_NAME"), sdr("MAT_AU"), sdr("MAT_AVG")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function supl(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (supl_id + ' , ' + supl_name) AS supl_id ,supl_NAME from supl where" &
        " supl_id like '%' + @SearchText + '%' or supl_name like '%' + @SearchText + '%' ORDER BY supl_id"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}", sdr("supl_id"), sdr("supl_NAME")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function dater(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (d_code + ' , ' + d_name) AS d_code ,d_name from dater where" &
        " d_code like '%' + @SearchText + '%' or d_name like '%' + @SearchText + '%' ORDER BY d_code"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}", sdr("d_code"), sdr("d_name")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function supl_and_dater(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (supl_id + ' , ' + supl_name) AS supl_id from supl where" &
        " supl_id like '%' + @SearchText + '%' or supl_name like '%' + @SearchText + '%' UNION select TOP 20  (d_code + ' , ' + d_name) AS supl_id from dater where" &
        " d_code like '%' + @SearchText + '%' or d_name like '%' + @SearchText + '%' ORDER BY supl_id"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("supl_id")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ISSUE(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "select TOP 20  (MAT_CODE + ' , ' + MAT_NAME) AS MAT_CODE ,MAT_NAME,MAT_AU,MAT_STOCK,MAT_AVG,MAT_LOCATION from MATERIAL where" &
                    " MAT_CODE like @SearchText + '%' or mat_name like '%' + @SearchText + '%' AND MAT_CODE LIKE '0%'"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", sdr("MAT_CODE"), sdr("MAT_NAME"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("MAT_AVG"), sdr("MAT_LOCATION"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If

            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(0, customers(i).IndexOf(",") - 1) & "' AND LINE_NO <> 0", conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                count = count + 1
                customers(i) = customers(i) & count
            Next
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function INDENT(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "select TOP 20  (MAT_CODE + ' , ' + MAT_NAME) AS MAT_CODE ,MAT_NAME,MAT_AU,MAT_STOCK,MAT_AVG,MAT_LOCATION from MATERIAL where" &
        " MAT_CODE like @SearchText + '%' or mat_name like '%' + @SearchText + '%'"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", sdr("MAT_CODE"), sdr("MAT_NAME"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("MAT_AVG"), sdr("MAT_LOCATION"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If

            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(0, customers(i).IndexOf(",") - 1) & "' AND LINE_NO <> 0", conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                count = count + 1
                customers(i) = customers(i) & count
            Next
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function STORE_ISSUE_AUTH(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "select TOP 20 MAT_DETAILS.ISSUE_NO ,(MATERIAL .MAT_CODE + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE  FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code  WHERE" &
        " MAT_DETAILS .ISSUE_NO like '%' + @SearchText + '%' AND DEPT_CODE ='STORE' AND POST_TYPE IS NULL"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}", sdr("ISSUE_NO"), sdr("MAT_CODE"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("MAT_AVG"), sdr("MAT_LOCATION"), sdr("RQD_QTY"), sdr("ISSUE_TYPE"), sdr("COST_CENT"), sdr("PURPOSE"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If
            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(customers(i).IndexOf("^") + 1, 9) & "' AND LINE_NO <> 0", conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                count = count + 1
                customers(i) = customers(i) & count
            Next
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function R_ISSUE_AUTH(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "select TOP 20 MAT_DETAILS.ISSUE_NO ,(MATERIAL .MAT_CODE + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE,MAT_DETAILS.RQD_BY,MAT_DETAILS.RQD_DATE FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code  WHERE" &
        " MAT_DETAILS .ISSUE_NO like '%' + @SearchText + '%' AND DEPT_CODE ='RM' AND POST_TYPE IS NULL and cost.mat_type='RAW MATERIAL'"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}", sdr("ISSUE_NO"), sdr("MAT_CODE"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("MAT_AVG"), sdr("MAT_LOCATION"), sdr("RQD_QTY"), sdr("ISSUE_TYPE"), sdr("COST_CENT"), sdr("PURPOSE"), sdr("RQD_BY"), sdr("RQD_DATE"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If
            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(customers(i).IndexOf("^") + 1, 9) & "' AND LINE_NO <> 0", conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                count = count + 1
                customers(i) = customers(i) & count
            Next
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function STORE_ISSUE(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "select TOP 20 MAT_DETAILS.ISSUE_NO ,(MATERIAL .MAT_CODE + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE  FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code  WHERE" &
        " MAT_DETAILS .ISSUE_NO like '%' + @SearchText + '%' AND DEPT_CODE ='STORE' AND POST_TYPE ='AUTH' AND LINE_DATE IS NULL"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}", sdr("ISSUE_NO"), sdr("MAT_CODE"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("MAT_AVG"), sdr("MAT_LOCATION"), sdr("RQD_QTY"), sdr("ISSUE_TYPE"), sdr("COST_CENT"), sdr("PURPOSE"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If
            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                'conn.Open()
                'ds.Clear()
                'da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(customers(i).IndexOf("^") + 1, 9) & "' AND LINE_NO <> 0", conn)
                'count = da.Fill(ds, "MAT_DETAILS")
                'conn.Close()
                'count = count + 1
                'customers(i) = customers(i) & count

                ''''''''''''''''''

                Dim dr As SqlDataReader
                conn.Open()
                Dim max_line As Integer
                max_line = 0
                Dim MC5 As New SqlCommand
                MC5.CommandText = "select (CASE WHEN count(line_no) IS NULL THEN '0' ELSE count(line_no) END) AS line_no from MAT_DETAILS where MAT_CODE ='" & customers(i).Substring(customers(i).IndexOf("^") + 1, 9) & "' and FISCAL_YEAR =" & CInt(STR1) & " and LINE_NO <>0"
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    max_line = dr.Item("line_no")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                max_line = max_line + 1
                customers(i) = customers(i) & max_line
            Next
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function R_MAT_ISSUE(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "select TOP 20 MAT_DETAILS.ISSUE_NO ,(CAST(MATERIAL .MAT_CODE AS VARCHAR(30)) + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE,MAT_DETAILS.RQD_BY,MAT_DETAILS.RQD_DATE FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code  WHERE" &
                " MAT_DETAILS .ISSUE_NO like '%' + @SearchText + '%' AND DEPT_CODE ='RM' AND POST_TYPE ='AUTH' AND LINE_DATE IS NULL"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}", sdr("ISSUE_NO"), sdr("MAT_CODE"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("MAT_AVG"), sdr("MAT_LOCATION"), sdr("RQD_QTY"), sdr("ISSUE_TYPE"), sdr("COST_CENT"), sdr("PURPOSE"), sdr("RQD_BY"), sdr("RQD_DATE"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0

            ''finding RQD date
            Dim dr1 As SqlDataReader
            conn.Open()
            Dim rqd_date As Date

            Dim MC6 As New SqlCommand
            MC6.CommandText = "select RQD_DATE from MAT_DETAILS where ISSUE_NO ='+ @SearchText +'"
            MC6.Parameters.AddWithValue("@SearchText", prefix)
            MC6.Connection = conn
            dr1 = MC6.ExecuteReader
            If dr1.HasRows Then
                dr1.Read()
                rqd_date = CDate(dr1.Item("RQD_DATE"))
                dr1.Close()
                conn.Close()
            Else
                conn.Close()
            End If

            ''line no
            Dim STR1 As String = ""
            If rqd_date.Date.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf rqd_date.Date.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If

            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                Dim dr As SqlDataReader
                conn.Open()
                Dim max_line As Integer
                max_line = 0
                Dim MC5 As New SqlCommand
                MC5.CommandText = "select (CASE WHEN max(line_no) IS NULL THEN '0' ELSE max(line_no) END) AS line_no from MAT_DETAILS where MAT_CODE ='" & customers(i).Substring(customers(i).IndexOf("^") + 1, 9) & "' and FISCAL_YEAR =" & CInt(STR1) & " and LINE_NO <>0"
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    max_line = dr.Item("line_no")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                max_line = max_line + 1
                customers(i) = customers(i) & max_line

            Next
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function R_ISSUE(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()
                ''ok
                cmd.CommandText = "select TOP 20  (MAT_CODE + ' , ' + MAT_NAME) AS MAT_CODE ,MAT_NAME,MAT_AU,MAT_STOCK,MAT_AVG,MAT_LOCATION from MATERIAL where" &
        " (MAT_CODE like '%' + @SearchText + '%' or mat_name like '%' + @SearchText + '%') AND MAT_CODE LIKE '1%'"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", sdr("MAT_CODE"), sdr("MAT_NAME"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("MAT_AVG"), sdr("MAT_LOCATION"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If

            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(0, customers(i).IndexOf(",") - 1) & "' AND LINE_NO <> 0", conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                count = count + 1
                customers(i) = customers(i) & count
            Next
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function STORE_ISSUE_RET(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "SELECT  DISTINCT TOP 15 MAT_DETAILS.ISSUE_NO," &
                                " MAX(MATERIAL .MAT_CODE + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE," &
                                " MAX(MATERIAL .MAT_AU) AS MAT_AU," &
                                " MAX(MATERIAL.MAT_STOCK) AS MAT_STOCK," &
                                " MAX(MAT_DETAILS .UNIT_PRICE) AS UNIT_PRICE," &
                                " MAX(MATERIAL .MAT_LOCATION) AS MAT_LOCATION ," &
                                " MAX(MAT_DETAILS .ISSUE_TYPE) AS ISSUE_TYPE," &
                                " MAX(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT," &
                                " MAX(MAT_DETAILS .PURPOSE) AS PURPOSE," &
                                " SUM(MAT_DETAILS.ISSUE_QTY) AS ISSUE_QTY," &
                                " MAX(MAT_DETAILS .RQD_BY) AS RQD_BY, MAX(MAT_DETAILS .RQD_DATE) AS RQD_DATE," &
                                " MAX(MAT_DETAILS .AUTH_BY) AS AUTH_BY, MAX(MAT_DETAILS .AUTH_DATE) AS AUTH_DATE," &
                                " MAX(MAT_DETAILS .ISSUE_BY) AS ISSUE_BY, MAX(MAT_DETAILS .ISSUE_DATE) AS ISSUE_DATE" &
                                " FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code " &
                                " WHERE MAT_DETAILS .ISSUE_NO like '%' + @SearchText + '%' AND DEPT_CODE ='STORE' AND POST_TYPE IS NOT NULL " &
                                " GROUP BY MAT_DETAILS.ISSUE_NO ORDER BY MAT_DETAILS .ISSUE_NO "

                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}^{15}^{16}", sdr("ISSUE_NO"), sdr("MAT_CODE"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("UNIT_PRICE"), sdr("MAT_LOCATION"), sdr("ISSUE_TYPE"), sdr("COST_CENT"), sdr("PURPOSE"), sdr("ISSUE_QTY"), sdr("RQD_BY"), sdr("RQD_DATE"), sdr("AUTH_BY"), sdr("AUTH_DATE"), sdr("ISSUE_BY"), sdr("ISSUE_DATE"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If
            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(customers(i).IndexOf("^") + 1, 9) & "' AND LINE_NO <> 0", conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                count = count + 1
                customers(i) = customers(i) & count
            Next
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function RM_ISSUE_RET(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "SELECT  DISTINCT TOP 15 MAT_DETAILS.ISSUE_NO," &
                                " MAX(MATERIAL .MAT_CODE + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE," &
                                " MAX(MATERIAL .MAT_AU) AS MAT_AU," &
                                " MAX(MATERIAL.MAT_STOCK) AS MAT_STOCK," &
                                " MAX(MAT_DETAILS .UNIT_PRICE) AS UNIT_PRICE," &
                                " MAX(MATERIAL .MAT_LOCATION) AS MAT_LOCATION ," &
                                " MAX(MAT_DETAILS .ISSUE_TYPE) AS ISSUE_TYPE," &
                                " MAX(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT," &
                                " MAX(MAT_DETAILS .PURPOSE) AS PURPOSE," &
                                " SUM(MAT_DETAILS.ISSUE_QTY) AS ISSUE_QTY " &
                                " FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code " &
                                " WHERE MAT_DETAILS .ISSUE_NO like '%' + @SearchText + '%' AND DEPT_CODE ='RM' AND POST_TYPE IS NOT NULL " &
                                " GROUP BY MAT_DETAILS.ISSUE_NO ORDER BY MAT_DETAILS .ISSUE_NO "

                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}", sdr("ISSUE_NO"), sdr("MAT_CODE"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("UNIT_PRICE"), sdr("MAT_LOCATION"), sdr("ISSUE_TYPE"), sdr("COST_CENT"), sdr("PURPOSE"), sdr("ISSUE_QTY"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If
            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(customers(i).IndexOf("^") + 1, 9) & "' AND LINE_NO <> 0", conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                count = count + 1
                customers(i) = customers(i) & count
            Next
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function RM_ISSUE_AUTH(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()

                cmd.CommandText = "select TOP 20 MAT_DETAILS.ISSUE_NO ,(MATERIAL .MAT_CODE + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .ISSUE_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE  FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code  WHERE" &
        " MAT_DETAILS .ISSUE_NO like '%' + @SearchText + '%' AND DEPT_CODE ='RM' AND POST_TYPE IS NULL"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}", sdr("ISSUE_NO"), sdr("MAT_CODE"), sdr("MAT_AU"), sdr("MAT_STOCK"), sdr("MAT_AVG"), sdr("MAT_LOCATION"), sdr("ISSUE_QTY"), sdr("ISSUE_TYPE"), sdr("COST_CENT"), sdr("PURPOSE"), ""))
                    End While
                End Using
                conn.Close()
            End Using

            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            count = 0
            ''line no
            Dim STR1 As String = ""
            If Today.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If
            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & customers(i).Substring(customers(i).IndexOf("^") + 1, 9) & "' AND LINE_NO <> 0", conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                count = count + 1
                customers(i) = customers(i) & count
            Next
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function FIN_GOODS(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (ITEM_CODE + ' , ' + ITEM_NAME) AS ITEM_CODE ,ITEM_NAME,ITEM_AU,ITEM_WEIGHT from F_ITEM where ITEM_AU='Pcs' and " &
                " (ITEM_CODE like '%' + @SearchText + '%' or ITEM_NAME like '%' + @SearchText + '%') ORDER BY ITEM_CODE"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}", sdr("ITEM_CODE"), sdr("ITEM_NAME"), sdr("ITEM_AU"), sdr("ITEM_WEIGHT")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function FIN_GOODMT(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (ITEM_CODE + ' , ' + ITEM_NAME) AS ITEM_CODE ,ITEM_NAME,ITEM_AU,ITEM_WEIGHT from F_ITEM where (ITEM_AU='Mt' OR ITEM_AU='MTS') and " &
                " (ITEM_CODE like '%' + @SearchText + '%' or ITEM_NAME like '%' + @SearchText + '%') ORDER BY ITEM_CODE"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}", sdr("ITEM_CODE"), sdr("ITEM_NAME"), sdr("ITEM_AU"), sdr("ITEM_WEIGHT")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function FIN_GOODACT(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (ITEM_CODE + ' , ' + ITEM_NAME) AS ITEM_CODE ,ITEM_NAME,ITEM_AU,ITEM_WEIGHT from F_ITEM where ITEM_AU='Activity' and " &
                " (ITEM_CODE like '%' + @SearchText + '%' or ITEM_NAME like '%' + @SearchText + '%' ) ORDER BY ITEM_CODE"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}", sdr("ITEM_CODE"), sdr("ITEM_NAME"), sdr("ITEM_AU"), sdr("ITEM_WEIGHT")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function BE(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  BE_NO ,BE_DATE,BL_NO,BL_DATE,SHIP_FLIGHT from BE_DETAILS where" &
                " BE_NO like '%' + @SearchText + '%' AND BE_STATUS LIKE 'PENDING' ORDER BY BE_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}", sdr("BE_NO"), sdr("BE_DATE"), sdr("BL_NO"), sdr("BL_DATE"), sdr("SHIP_FLIGHT")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function SO(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT TOP 15 (so_mat_order.so_no + ' , ' + ORDER_DETAILS.SO_ACTUAL) AS SO_NO from so_mat_order join order_details on so_mat_order.so_no=order_details.so_no where order_details.SO_STATUS<>'DRAFT' AND so_mat_order.ITEM_STATUS='PENDING' AND (ORDER_DETAILS.SO_NO like '%' + @SearchText + '%' OR ORDER_DETAILS.SO_ACTUAL LIKE '%' + @SearchText + '%')  ORDER BY SO_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("SO_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function S_SO(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT TOP 15 (so_mat_order.so_no + ' , ' + ORDER_DETAILS.SO_ACTUAL) AS SO_NO from so_mat_order join order_details on so_mat_order.so_no=order_details.so_no where so_mat_order.ITEM_STATUS='PENDING' AND (ORDER_DETAILS.SO_NO like '%' + @SearchText + '%' OR ORDER_DETAILS.SO_ACTUAL LIKE '%' + @SearchText + '%') AND SO_STATUS  IN ('ACTIVE','DRAFT')   ORDER BY SO_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("SO_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function rcm_po(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT TOP 15 (PO_RCD_MAT.PO_NO + ' , ' + ORDER_DETAILS .SO_ACTUAL) AS RCM_PO_NO   from PO_RCD_MAT JOIN ORDER_DETAILS ON PO_RCD_MAT .PO_NO =ORDER_DETAILS .SO_NO  where (PO_RCD_MAT.RCM_SGST >0 or PO_RCD_MAT.RCM_IGST >0 OR PO_RCD_MAT.RCM_CGST >0 OR PO_RCD_MAT.RCM_CESS >0) AND (PO_RCD_MAT.RCM_P IS NULL OR PO_RCD_MAT.RCM_F IS NULL) AND (PO_RCD_MAT.PO_NO LIKE '%' + @SearchText + '%' OR PO_RCD_MAT.SUPL_ID LIKE '%' + @SearchText + '%')"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("RCM_PO_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function rcm_ser_po(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                'cmd.CommandText = "select DISTINCT TOP 15 (mb_book.PO_NO + ' , ' + ORDER_DETAILS .SO_ACTUAL) AS RCM_PO_NO from mb_book join ORDER_DETAILS on mb_book .po_no =ORDER_DETAILS .SO_NO where mb_book .rcm ='Yes' and (mb_book .rcm_cgst >0 or mb_book.rcm_sgst >0 or rcm_igst >0 or rcm_cess >0 or mb_book.sgst_liab >0 or mb_book .cgst_liab >0 or igst_liab >0 or cess_liab >0) AND ( mb_book . RCM_P IS NULL OR mb_book .RCM_F IS NULL ) AND MB_BOOK.V_IND='V' AND (mb_book.PO_NO LIKE '%' + @SearchText + '%' OR mb_book.SUPL_ID LIKE '%' + @SearchText + '%')"
                cmd.CommandText = "select DISTINCT TOP 15 (mb_book.PO_NO + ' , ' + ORDER_DETAILS .SO_ACTUAL) AS RCM_PO_NO from mb_book join ORDER_DETAILS on mb_book .po_no =ORDER_DETAILS .SO_NO where (mb_book .rcm_cgst >0 or mb_book.rcm_sgst >0 or rcm_igst >0 or rcm_cess >0 or mb_book.sgst_liab >0 or mb_book .cgst_liab >0 or igst_liab >0 or cess_liab >0) AND ( mb_book . RCM_P IS NULL OR mb_book .RCM_F IS NULL ) AND MB_BOOK.V_IND='V' AND (mb_book.PO_NO LIKE '%' + @SearchText + '%' OR mb_book.SUPL_ID LIKE '%' + @SearchText + '%')"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("RCM_PO_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetRCDVoucherList(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT TOP 15 (VOUCHER.TOKEN_NO) AS token_no from VOUCHER where VOUCHER_TYPE ='RCD' AND REFUND_STATUS IS NULL AND (TOKEN_NO LIKE '%' + @SearchText + '%')"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("token_no")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ld_pen_po(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT TOP 15 (PO_RCD_MAT.PO_NO + ' , ' + ORDER_DETAILS .SO_ACTUAL) AS LD_PEN_PO_NO from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .po_no =ORDER_DETAILS .SO_NO where PO_RCD_MAT .rcm ='No' and (PO_RCD_MAT .rcm_cgst >0 or PO_RCD_MAT.rcm_sgst >0 or rcm_igst >0 or rcm_cess >0 ) AND ( PO_RCD_MAT . RCM_P IS NULL OR PO_RCD_MAT .RCM_F IS NULL ) AND PO_RCD_MAT.V_IND='V' AND (PO_RCD_MAT.PO_NO LIKE '%' + @SearchText + '%' OR PO_RCD_MAT.SUPL_ID LIKE '%' + @SearchText + '%')"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("LD_PEN_PO_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function PEND_T_NO(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT DISTINCT TOP 15 TRUCK_NO  FROM DESPATCH WHERE TRUCK_NO like '%' + @SearchText + '%' and (INV_STATUS='Pending' OR INV_STATUS='ACTIVE') ORDER BY TRUCK_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("TRUCK_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function T_NO(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT DISTINCT TOP 15 TRUCK_NO  FROM DESPATCH WHERE TRUCK_NO like '%' + @SearchText + '%' ORDER BY TRUCK_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("TRUCK_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ac_head(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT DISTINCT TOP 20 (ac_code + ' , ' + ac_description) as ac_details,ac_code  FROM ACDIC WHERE ac_code like '%' + @SearchText + '%' OR ac_description LIKE '%' + @SearchText + '%' ORDER BY acdic.ac_code"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("ac_details")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function






    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function po_no_search(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT distinct TOP 20 (SO_NO + ' , ' + SO_ACTUAL) as po_details FROM ORDER_DETAILS  WHERE (SO_STATUS='RC' OR SO_STATUS='DRAFT' OR SO_STATUS='RCW' OR SO_STATUS='RCM') AND (SO_NO like '%' + @SearchText + '%' or SO_ACTUAL like '%' + @SearchText + '%' or PARTY_CODE like '%' + @SearchText + '%')"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("po_details")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function order_search(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT distinct TOP 20 (SO_NO + ' , ' + SO_ACTUAL) as po_details FROM ORDER_DETAILS  WHERE (SO_STATUS='RC' OR SO_STATUS='ACTIVE' OR SO_STATUS='RCW') AND (SO_NO like '%' + @SearchText + '%' or SO_ACTUAL like '%' + @SearchText + '%' or PARTY_CODE like '%' + @SearchText + '%')"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("po_details")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function wo_no_search(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT distinct TOP 20 (SO_NO + ' , ' + SO_ACTUAL) as po_details FROM ORDER_DETAILS join WO_ORDER on WO_ORDER .PO_NO =ORDER_DETAILS .SO_NO  WHERE (SO_STATUS='RCW' OR SO_STATUS='ACTIVE' OR SO_STATUS='DRAFT') AND (SO_NO like '%' + @SearchText + '%' or SO_ACTUAL like '%' + @SearchText + '%' or PARTY_CODE like '%' + @SearchText + '%') and (so_no like 'w%' or so_no like 'r%') AND WO_TYPE <>'FREIGHT INWARD' AND WO_TYPE <>'FREIGHT OUTWARD' AND W_STATUS ='PENDING' ORDER BY po_details"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("po_details")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ac_head_new(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT DISTINCT TOP 10 *  FROM ACDIC WHERE ac_code like '%' + @SearchText + '%' ORDER BY  ac_code"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}", sdr("ac_code"), sdr("ac_description"), sdr("ac_type"), sdr("Account_group")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ac_head_name(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT DISTINCT TOP 10 * FROM ACDIC WHERE ac_description like '%' + @SearchText + '%' ORDER BY  ac_description"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}", sdr("ac_code"), sdr("ac_description"), sdr("ac_type"), sdr("Account_group")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ac_head_type(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT DISTINCT TOP 10 ac_type  FROM ACDIC WHERE ac_type like '%' + @SearchText + '%' ORDER BY  ac_type"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("ac_type")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ac_head_group(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT DISTINCT TOP 10 Account_group  FROM ACDIC WHERE Account_group like '%' + @SearchText + '%' ORDER BY  Account_group"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("Account_group")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function FIN_GOODS_NEW(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20 * from F_ITEM where" &
                " ITEM_CODE like '%' + @SearchText + '%' or ITEM_NAME like '%' + @SearchText + '%' ORDER BY ITEM_CODE"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}", sdr("ITEM_CODE"), sdr("ITEM_NAME"), sdr("ITEM_AU"), sdr("ITEM_DRAW"), sdr("ITEM_TYPE"), sdr("ITEM_CHPTR"), sdr("PROD_STOP_IND"), sdr("ITEM_WEIGHT")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Q_GROUP(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20 * from qual_group where qual_code like '%' + @SearchText + '%'  ORDER BY qual_code"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}", sdr("qual_code"), sdr("qual_name"), sdr("qual_desc")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function SUPL_DETAILS(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 40 * from SUPL where" &
                " SUPL_ID like '%' + @SearchText + '%' or SUPL_NAME like '%' + @SearchText + '%' ORDER BY SUPL_ID DESC"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}^{15}^{16}^{17}^{18}^{19}^{20}^{21}^{22}^{23}^{24}^{25}^{26}^{27}^{28}^{29}", sdr("SUPL_ID"), sdr("SUPL_NAME"), sdr("SUPL_CONTACT_PERSON"), sdr("SUPL_AT"), sdr("SUPL_PO"), sdr("SUPL_DIST"), sdr("SUPL_PIN"), sdr("SUPL_STATE"), sdr("SUPL_COUNTRY"), sdr("SUPL_MOB1"), sdr("SUPL_MOB2"), sdr("SUPL_LAND"), sdr("SUPL_FAX"), sdr("SUPL_EMAIL"), sdr("SUPL_WEB"), sdr("SUPL_PAN"), sdr("SUPL_TIN"), sdr("SUPL_ST_NO"), sdr("SUPL_BANK"), sdr("SUPL_ACOUNT_NO"), sdr("SUPL_IFSC"), sdr("SUPL_TYPE"), sdr("SUPL_TAX"), sdr("SUPL_LOC"), sdr("SUPL_GST_NO"), sdr("SUPL_STATE_CODE"), sdr("PARTY_TYPE"), sdr("MSME_NO"), sdr("SUPL_STATUS"), sdr("SUPL_VALIDITY")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function DEB_DETAILS(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT d_code ,d_name ,add_1 ,add_2,d_range ,d_city ,d_coll ,ecc_no ,tin_no ,stock_ac_head ,iuca_head ,supl_loc ,JOB_WORK,DEB_LOC,gst_code,d_state,d_state_code,d_pin " &
                    " FROM dater where d_code like '%' + @SearchText + '%' or d_name like '%' + @SearchText + '%' ORDER BY d_code"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}^{15}^{16}^{17}", sdr("d_code"), sdr("d_name"), sdr("add_1"), sdr("add_2"), sdr("d_range"), sdr("d_city"), sdr("d_coll"), sdr("ecc_no"), sdr("tin_no"), sdr("stock_ac_head"), sdr("iuca_head"), sdr("supl_loc"), sdr("JOB_WORK"), sdr("deb_loc"), sdr("gst_code"), sdr("d_state"), sdr("d_state_code"), sdr("d_pin"), ""))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function inv_details(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT top 20 (d_type + inv_no) as inv_no " &
                    " FROM DESPATCH where D_TYPE + inv_no like '%' + @SearchText + '%' ORDER BY inv_no"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("inv_no")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function inv_no(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT top 20 (d_type + inv_no) as inv_no " &
                    " FROM DESPATCH where D_TYPE + inv_no like '%' + @SearchText + '%'  ORDER BY inv_no"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("inv_no")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function







    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function P_R_ISSUE(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Dim demo As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim mat_code As String = ""
            Using cmd As New SqlCommand()
                ''ok
                cmd.CommandText = "select distinct  (PO_RCD_MAT.MAT_CODE + ' , ' + MATERIAL.MAT_NAME) AS MAT_CODE ,MATERIAL .MAT_AU ,MATERIAL.MAT_NAME from PO_RCD_MAT JOIN MATERIAL ON PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where GARN_NO ='PENDING' AND " &
        " (PO_RCD_MAT.MAT_CODE like '%100%' or MATERIAL .MAT_NAME like '%100%') "
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}", sdr("MAT_CODE"), sdr("MAT_AU"), sdr("MAT_NAME")))
                    End While
                End Using
                conn.Close()
            End Using
            Dim count As Integer = 0
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter
            Dim dr As SqlDataReader
            Dim MC5 As New SqlCommand
            Dim qty As Decimal = 0.0

            Dim i As Integer = 0
            For i = 0 To customers.Count - 1
                conn.Open()
                MC5.CommandText = "SELECT ((SELECT (CASE WHEN SUM(MAT_RCD_QTY) IS NULL THEN '0.000' ELSE SUM(MAT_RCD_QTY)END)  FROM PO_RCD_MAT WHERE MAT_CODE ='" & customers(i).Substring(0, customers(i).IndexOf(",") - 1) & "' AND GARN_NO ='PENDING') - " &
                    "(SELECT (CASE WHEN SUM(issue_qty) IS NULL THEN '0.000' ELSE SUM(issue_qty) END)  FROM p_issue WHERE mat_code ='" & customers(i).Substring(0, customers(i).IndexOf(",") - 1) & "' AND p_i_status ='P')) AS QTY "
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    qty = dr.Item("QTY")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                customers(i) = customers(i) & "^" & qty
            Next

            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function BE_DETAILS(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  BE_NO ,BE_DATE ,BL_NO ,BL_DATE ,INV_NO ,INV_DATE ,PO_NO ,MAT_SLNO  ,TRANS_MODE ,SHIP_FLIGHT ,CHA_ORDER ,CHA_SLNO ,BE_QTY ,RCVD_QTY  from BE_DETAILS where" &
                " BE_NO like '%' + @SearchText + '%' AND BE_STATUS LIKE 'PENDING' ORDER BY BE_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}^{15}^{16}^{17}^{18}^{19}^{20}^{21}^{22}^{23}^{24}^{25}^{26}^{27}^{28}", sdr("BE_NO"), sdr("BE_DATE"), sdr("BL_NO"), sdr("BL_DATE"), sdr("INV_NO"), sdr("INV_DATE"), sdr("PO_NO"), sdr("MAT_SLNO"), sdr("CONV_RATE"), sdr("TRANS_MODE"), sdr("SHIP_FLIGHT"), sdr("CHA_ORDER"), sdr("CHA_SLNO"), sdr("BE_QTY"), sdr("RCVD_QTY"), sdr("OCEAN_FREIGHT"), sdr("INSURANCE"), sdr("SAT_CHARGE"), sdr("BCD"), sdr("CVD"), sdr("SAD"), sdr("ED_ON_CVD"), sdr("SHE_ON_CVD"), sdr("CUST_EDU_CESS"), sdr("CUST_SHE_CESS"), sdr("UNIT_PRICE"), sdr("UNIT_CENVAT"), sdr("BE_STATUS"), sdr("PARTY_AMT"), sdr("EMP_ID")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function rm_rej_mat(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT top 10 (CRR_NO + ' , ' + PO_NO) AS CRR_NO from PO_RCD_MAT where MAT_REJ_QTY >0 and CRR_NO like 'RCRR%' AND (CRR_NO LIKE '%' + @SearchText + '%' OR PO_NO LIKE '%' + @SearchText + '%') AND RET_STATUS IS NULL ORDER BY CRR_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("CRR_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function rej_mat(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT top 10 (CRR_NO + ' , ' + PO_NO) AS CRR_NO from PO_RCD_MAT where MAT_REJ_QTY >0 and CRR_NO like 'SCRR%' AND (CRR_NO LIKE '%' + @SearchText + '%' OR PO_NO LIKE '%' + @SearchText + '%') AND RET_STATUS IS NULL ORDER BY CRR_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("CRR_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function rm_rej_prt(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT top 10 (CRR_NO + ' , ' + PO_NO) AS CRR_NO from PO_RCD_MAT where MAT_REJ_QTY >0 and CRR_NO like 'RCRR%' AND (CRR_NO LIKE '%' + @SearchText + '%' OR PO_NO LIKE '%' + @SearchText + '%' OR RET_STATUS LIKE '%' + @SearchText + '%' ) AND RET_STATUS IS NOT NULL ORDER BY CRR_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("CRR_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function rej_prt(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select DISTINCT top 10 (CRR_NO + ' , ' + PO_NO) AS CRR_NO from PO_RCD_MAT where MAT_REJ_QTY >0 and CRR_NO like 'SCRR%' AND (CRR_NO LIKE '%' + @SearchText + '%' OR PO_NO LIKE '%' + @SearchText + '%' OR RET_STATUS LIKE '%' + @SearchText + '%' ) AND RET_STATUS IS NOT NULL ORDER BY CRR_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("CRR_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function


    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function voucher_no(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select distinct top 20 TOKEN_NO  from VOUCHER WHERE TOKEN_NO like '%' + @SearchText + '%' ORDER BY TOKEN_NO desc"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("TOKEN_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function sale_voucher_no(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20 VOUCHER_TYPE +VOUCHER_NO AS VOUCHER   FROM SALE_RCD_VOUCHAR WHERE VOUCHER_TYPE +VOUCHER_NO like '%' + @SearchText + '%' "
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("VOUCHER")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function rcm_inv_no(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT distinct top 20 (D_TYPE +INV_NO ) as inv_no FROM RCM_INV where D_TYPE +INV_NO like '%' + @SearchText + '%' "
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("inv_no")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function RAW_CRR_DATA(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT DISTINCT CRR_NO  FROM PO_RCD_MAT WHERE CRR_NO LIKE '%' + @SearchText + '%' AND GARN_NO ='PENDING' AND CRR_NO LIKE 'RCRR%' AND CRR_STATUS IS NULL ORDER BY CRR_NO "
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("CRR_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GET_BE_BL_NUMBER(ByVal prefix As String) As String()
        Dim customers As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select TOP 20  (BE_NO + ' , ' + BL_NO) AS BE_BL_NO from BE_DETAILS where BE_NO like '%' + @SearchText + '%' or BL_NO like '%' + @SearchText + '%' ORDER BY BE_NO"
                cmd.Parameters.AddWithValue("@SearchText", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(String.Format("{0}", sdr("BE_BL_NO")))
                    End While
                End Using
                conn.Close()
            End Using
            Return customers.ToArray()
        End Using
    End Function



End Class