Imports Microsoft.AspNet.SignalR
Imports System
Imports System.Web
Public Class ChatHub
    Inherits Hub

    Public Sub Send(name As String, message As String)
        Clients.All.broadcastMessage(name, message)
    End Sub
End Class
