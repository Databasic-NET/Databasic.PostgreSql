Imports System.Data.Common
Imports Npgsql

Public Class Transaction
    Inherits Databasic.Transaction

    Public Overrides Property Instance As DbTransaction
        Get
            Return Me._instance
        End Get
        Set(value As DbTransaction)
            Me._instance = value
        End Set
    End Property
    Private _instance As NpgsqlTransaction

    Public Shadows Sub Rollback(name As String)
        Try
            Me._instance.Rollback(name)
            Me.ConnectionWrapper.OpenedTransaction = Nothing
        Catch ex As Exception
            Me.ConnectionWrapper.OpenedTransaction = Nothing
            Throw ex
        End Try
    End Sub

    Public Sub Save(name As String)
        Try
            Me._instance.Save(name)
            Me.ConnectionWrapper.OpenedTransaction = Nothing
        Catch ex As Exception
            Me.ConnectionWrapper.OpenedTransaction = Nothing
            Throw ex
        End Try
    End Sub

    Public Sub Release(name As String)
        Try
            Me._instance.Release(name)
            Me.ConnectionWrapper.OpenedTransaction = Nothing
        Catch ex As Exception
            Me.ConnectionWrapper.OpenedTransaction = Nothing
            Throw ex
        End Try
    End Sub

End Class