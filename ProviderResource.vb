Public Class ProviderResource
	Inherits Databasic.ProviderResource

	Public Overrides Function GetTableColumns(table As String, connection As Databasic.Connection) As Dictionary(Of String, Boolean)
		Dim result As New Dictionary(Of String, Boolean)
		Dim schema = "public"
		Dim dotPos = table.IndexOf("."c) ' ignore (screw) here all tables with dot contained in name, only realy dummy developers can use that form...:-(
		If dotPos > -1 Then
			schema = table.Substring(0, dotPos)
			table = table.Substring(dotPos + 1)
		End If
		Dim rawData As Dictionary(Of String, String) = Databasic.Statement.Prepare("
			SELECT 
				c.is_nullable, 
				c.column_name
			FROM 
				information_schema.columns c
			WHERE
				c.table_schema = @schema AND
				c.table_catalog = @database AND
				c.table_name = @table
			ORDER BY
				c.ordinal_position ASC",
			connection
		).FetchAll(New With {
			.schema = schema,
			.database = connection.Provider.Database,
			.table = table
		}).ToDictionary(Of String, String)("column_name")
		Dim columnCouldBenull As Boolean
		For Each item In rawData
			columnCouldBenull = item.Value.ToUpper().IndexOf("NO") = -1
			result.Add(item.Key, columnCouldBenull)
		Next
		Return result
	End Function

	Public Overrides Function GetLastInsertedId(ByRef transaction As Databasic.Transaction, Optional ByRef classMetaDescription As MetaDescription = Nothing) As Object
		' https://stackoverflow.com/questions/2944297/postgresql-function-for-last-inserted-id
		Return Databasic.Statement.Prepare("SELECT LAST_INSERT_ID()", transaction).FetchOne().ToInstance(Of Object)()
	End Function

	'Public Overrides Function GetAll(
	'		connection As Databasic.Connection,
	'		columns As String,
	'		table As String,
	'		Optional offset As Int64? = Nothing,
	'		Optional limit As Int64? = Nothing,
	'		Optional orderByStatement As String = ""
	'	) As Databasic.Statement
	'	Dim sql = $"SELECT {columns} FROM {table}"
	'	offset = If(offset, 0)
	'	limit = If(limit, 0)
	'	If limit > 0 Then
	'		sql += If(orderByStatement.Length > 0, " ORDER BY " + orderByStatement, "") +
	'				$" LIMIT {If(limit = 0, "18446744073709551615", limit.ToString())} OFFSET {offset}"
	'	End If
	'	Return Databasic.Statement.Prepare(sql, connection).FetchAll()
	'End Function

End Class