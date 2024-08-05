
Public Class GSTR3BDataModelClassEY

    Public Property req As List(Of GSTR3BDataReqDetailsEY)

End Class


Public Class GSTR3BDataReqDetailsEY
    Public Property returnPeriod As String
    Public Property custGstin As String

    Public Property docType As String
    Public Property docNo As String
    Public Property docDate As String
    Public Property suppGstin As String
    Public Property pos As String
    Public Property reverseCharge As String
    'Public Property dataOriginTypeCode As String
    Public Property companyCode As String
    Public Property lineItems As List(Of GSTR3BDataItemDetailsEY)

End Class


Public Class GSTR3BDataItemDetailsEY
    Public Property itemNo As Int32
    Public Property supplyType As String
    Public Property taxableVal As Decimal
    Public Property igstRt As Decimal
    Public Property igstAmt As Decimal
    Public Property cgstRt As Decimal
    Public Property cgstAmt As Decimal
    Public Property sgstRt As Decimal
    Public Property sgstAmt As Decimal
    Public Property eligibilityIndicator As String
    Public Property availableIgst As Decimal
    Public Property availableCgst As Decimal
    Public Property availableSgst As Decimal
    Public Property availableCess As Decimal
    Public Property otherValues As Decimal
    Public Property plantCode As String

End Class




