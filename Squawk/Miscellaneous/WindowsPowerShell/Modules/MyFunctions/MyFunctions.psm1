
function MyInvoke-SQL {
    param(
        [string] $sqlCommand = $(throw "Please specify a query.")
      )

$connectionString = “Server=OX-V-MGMT;Database=squawk;Integrated Security=True;”

    $connection = new-object system.data.SqlClient.SQLConnection($connectionString)
    $command = new-object system.data.sqlclient.sqlcommand($sqlCommand,$connection)
    $connection.Open()

    $adapter = New-Object System.Data.sqlclient.sqlDataAdapter $command
    $dataset = New-Object System.Data.DataSet
    $adapter.Fill($dataSet) | Out-Null

    $connection.Close()
    $dataSet.Tables

}

function MyGet-CpuUsage {

(get-counter -Counter "\Processor(_Total)\% Processor Time" -SampleInterval 1 -MaxSamples 5 |
    select -ExpandProperty countersamples | select -ExpandProperty cookedvalue | Measure-Object -Average).average
}
