
Public Class GSTR1DataModelClassEYB2C

    ''Public Property req As New GSTR1DataReqDetailsEYObj()
    Public Property req As List(Of GSTR1DataReqDetailsEYB2C)

End Class



Public Class GSTR1DataReqDetailsEYB2C
    Public Property returnPeriod As String
    Public Property suppGstin As String
    Public Property docType As String
    Public Property docNo As String
    Public Property docDate As String

    Public Property custOrSupName As String
    Public Property custOrSupAddr1 As String
    Public Property custOrSupAddr2 As String
    Public Property custOrSupAddr4 As String
    Public Property billToState As String
    Public Property pos As String
    Public Property reverseCharge As String
    Public Property taxScheme As String
    Public Property docCat As String
    Public Property supTradeName As String
    Public Property supLegalName As String
    Public Property supLocation As String
    Public Property supStateCode As String
    Public Property invAssessableAmt As Decimal
    Public Property invIgstAmt As Decimal
    Public Property invCgstAmt As Decimal
    Public Property invSgstAmt As Decimal
    Public Property docAmt As Decimal
    Public Property tranType As String
    Public Property companyCode As String
    Public Property profitCentre1 As String


    Public Property lineItems As List(Of GSTR1DataItemDetailsEYB2C)


End Class



Public Class GSTR1DataItemDetailsEYB2C
    Public Property itemNo As String
    Public Property supplyType As String
    Public Property hsnsacCode As String
    Public Property itemDesc As String
    Public Property itemType As String
    Public Property itemUqc As String
    Public Property itemQty As Decimal
    Public Property taxableVal As Decimal
    Public Property igstRt As Decimal
    Public Property igstAmt As Decimal
    Public Property cgstRt As Decimal
    Public Property cgstAmt As Decimal
    Public Property sgstRt As Decimal
    Public Property sgstAmt As Decimal
    Public Property plantCode As String
    Public Property isService As String
    Public Property unitPrice As Decimal
    Public Property itemAmt As Decimal
    Public Property totalItemAmt As Decimal
    'Public Property lineItemAmt As Decimal


End Class




