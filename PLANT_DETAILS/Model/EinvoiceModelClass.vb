Public Class EinvoiceModelClass
    Public Property Version As String
    Public Property TranDtls As New TransanctionDtls()
    Public Property DocDtls As New DocumentsDtls()
    Public Property SellerDtls As New SellerDetails()
    Public Property BuyerDtls As New BuyerDetails()
    Public Property ItemList As List(Of ItemDetails)
    Public Property ValDtls As New ValueDetails()

End Class

Public Class TransanctionDtls
    Public Property TaxSch As String
    Public Property SupTyp As String
    Public Property RegRev As String
    'Public Property EcmGstin As String()
    Public Property IgstOnIntra As String

End Class
Public Class DocumentsDtls
    Public Property Typ As String
    Public Property No As String
    Public Property Dt As String

End Class

Public Class SellerDetails
    Public Property Gstin As String
    Public Property LglNm As String
    Public Property Addr1 As String
    Public Property Addr2 As String
    Public Property Loc As String
    Public Property Pin As Int32
    Public Property Stcd As String
    Public Property Ph As String
    Public Property Em As String


End Class

Public Class BuyerDetails
    Public Property Gstin As String
    Public Property LglNm As String
    Public Property Pos As String
    Public Property Addr1 As String
    Public Property Addr2 As String
    Public Property Loc As String
    Public Property Pin As Int32
    Public Property Stcd As String
    Public Property Ph As String
    Public Property Em As String
End Class

Public Class ItemDetails
    Public Property SlNo As String
    Public Property PrdDesc As String
    Public Property IsServc As String
    Public Property HsnCd As String
    Public Property Qty As Decimal
    Public Property Unit As String
    Public Property UnitPrice As Decimal
    Public Property TotAmt As Decimal
    Public Property Discount As Decimal
    Public Property AssAmt As Decimal
    Public Property GstRt As Decimal
    Public Property IgstAmt As Decimal
    Public Property CgstAmt As Decimal
    Public Property SgstAmt As Decimal
    Public Property OthChrg As Decimal
    Public Property TotItemVal As Decimal
    Public Property OrdLineRef As String

End Class

Public Class ValueDetails

    Public Property AssVal As Decimal
    Public Property CgstVal As Decimal
    Public Property SgstVal As Decimal
    Public Property IgstVal As Decimal
    Public Property Discount As Decimal
    Public Property OthChrg As Decimal
    Public Property RndOffAmt As Decimal
    Public Property TotInvVal As Decimal

End Class
