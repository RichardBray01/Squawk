
function Invoke-SQL {
    param(
        [string] $sqlCommand = $(throw "Please specify a query.")
      )

$connectionString = “Server=SAMSUNG-LAPTOP\SQLEXPRESS;Database=Sysquawk;Integrated Security=True;”

    $connection = new-object system.data.SqlClient.SQLConnection($connectionString)
    $command = new-object system.data.sqlclient.sqlcommand($sqlCommand,$connection)
    $connection.Open()

    $adapter = New-Object System.Data.sqlclient.sqlDataAdapter $command
    $dataset = New-Object System.Data.DataSet
    $adapter.Fill($dataSet) | Out-Null

    $connection.Close()
    $dataSet.Tables

}

$mydata = Invoke-SQL "Select * from Host"
$mydata | Get-Member
$mydata | select HostName 