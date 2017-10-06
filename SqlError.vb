Public Class SqlError
    Inherits Databasic.SqlError

    Public Property ColumnName As String
    Public Property ConstraintName As String
    Public Property DataTypeName As String
    Public Property Detail As String
    Public Property File As String
    Public Property Hint As String
    Public Property InternalPosition As Integer
    Public Property InternalQuery As String
    Public Property Line As String
    Public Property Position As Integer
    Public Property Routine As String
    Public Property SchemaName As String
    Public Property Severity As String
    Public Property TableName As String
    Public Property Where As String

    Public Sub New(pgSqlNotice As Global.Npgsql.PostgresNotice)
        Me.Message = pgSqlNotice.MessageText
        Me.Code = pgSqlNotice.SqlState

        Me.ColumnName = pgSqlNotice.ColumnName
        Me.ConstraintName = pgSqlNotice.ConstraintName
        Me.DataTypeName = pgSqlNotice.DataTypeName
        Me.Detail = pgSqlNotice.Detail
        Me.File = pgSqlNotice.File
        Me.Hint = pgSqlNotice.Hint
        Me.InternalPosition = pgSqlNotice.InternalPosition
        Me.InternalQuery = pgSqlNotice.InternalQuery
        Me.Line = pgSqlNotice.Line
        Me.Position = pgSqlNotice.Position
        Me.Routine = pgSqlNotice.Routine
        Me.SchemaName = pgSqlNotice.SchemaName
        Me.Severity = pgSqlNotice.Severity
        Me.TableName = pgSqlNotice.TableName
        Me.Where = pgSqlNotice.Where
    End Sub
End Class
