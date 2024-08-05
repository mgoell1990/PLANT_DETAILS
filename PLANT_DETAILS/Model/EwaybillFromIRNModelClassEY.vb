
Public Class EwaybillFromIRNModelClassEY

    Public Property req As New EwaybillFromIRNReqDetailsEY()


End Class

Public Class EwaybillFromIRNReqDetailsEY
    Public Property gstin As String
    Public Property irn As String
    Public Property distance As Int32
    Public Property transMode As String
    Public Property transId As String
    Public Property transName As String
    Public Property vehType As String
    Public Property vehNo As String
    Public Property suppPincd As Int32
    Public Property custPincd As Int32
    Public Property shipToPincd As Int32
    Public Property expShipDtls As New BuyerDetailsEwaybillFromIRN()
    Public Property dispDtls As New SellerDetailsEwaybillFromIRN()


End Class



Public Class SellerDetailsEwaybillFromIRN
    Public Property nm As String
    Public Property addr1 As String
    Public Property loc As String
    Public Property pin As Int32
    Public Property stcd As String


End Class

Public Class BuyerDetailsEwaybillFromIRN

    Public Property addr1 As String
    Public Property loc As String
    Public Property pin As Int32
    Public Property stcd As String


End Class


