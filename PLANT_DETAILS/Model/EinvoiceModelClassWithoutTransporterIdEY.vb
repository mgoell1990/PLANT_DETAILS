
Public Class EinvoiceModelClassWithoutTransporterIdEY

    Public Property req As New EinvReqDetailsWithoutTransporterIdEY()




End Class

Public Class EinvReqDetailsWithoutTransporterIdEY
    Public Property suppGstin As String
    Public Property docType As String
    Public Property docNo As String
    Public Property docDate As String
    Public Property custGstin As String
    Public Property custOrSupName As String
    Public Property custOrSupAddr1 As String
    Public Property custOrSupAddr2 As String
    Public Property custOrSupAddr4 As String
    Public Property billToState As String
    Public Property pos As String
    Public Property reverseCharge As String
    Public Property supTradeName As String
    Public Property supLegalName As String

    Public Property supBuildingNo As String
    Public Property supBuildingName As String
    Public Property supLocation As String
    Public Property supPincode As Int32
    Public Property supStateCode As String
    'Public Property supPhone As String
    'Public Property supEmail As String
    Public Property custPincode As Int32
    'Public Property custPhone As String
    'Public Property custEmail As String
    Public Property invAssessableAmt As Decimal
    Public Property invIgstAmt As Decimal
    Public Property invCgstAmt As Decimal
    Public Property invSgstAmt As Decimal
    Public Property roundOff As Decimal
    Public Property docAmt As Decimal
    Public Property payloadId As String

    ''Ship to details
    Public Property shipToState As String
    Public Property shipToLegalName As String
    Public Property shipToBuildingNo As String
    Public Property shipToLocation As String
    Public Property shipToPincode As Int32

    Public Property tcsFlag As String
    Public Property returnPeriod As String
    ''E-WAY bill details
    Public Property transporterName As String
    Public Property tranType As String

    'Public Property transDocDate As String
    Public Property transportMode As String
    Public Property distance As Int32

    Public Property vehicleNo As String
    Public Property vehicleType As String

    Public Property subsupplyType As String
    Public Property docCat As String
    Public Property lineItems As List(Of ItemDetailsWithoutTransporterIdEY)


End Class



Public Class ItemDetailsWithoutTransporterIdEY
    Public Property itemNo As String
    Public Property supplyType As String
    Public Property hsnsacCode As String
    Public Property itemDesc As String
    Public Property itemUqc As String
    Public Property itemQty As Decimal
    Public Property taxableVal As Decimal
    Public Property igstRt As Decimal
    Public Property igstAmt As Decimal
    Public Property cgstRt As Decimal
    Public Property cgstAmt As Decimal
    Public Property sgstRt As Decimal
    Public Property sgstAmt As Decimal
    Public Property otherValues As Decimal
    Public Property isService As String
    Public Property unitPrice As Decimal
    Public Property itemAmt As Decimal
    Public Property totalItemAmt As Decimal
    'Public Property lineItemAmt As Decimal


End Class



