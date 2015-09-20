
$idSampleType = MyInvoke-SQL "Select Id From SampleType where name = 'CPU Usage '"
write-host $idSampleType.Id

$hostlist = MyInvoke-SQL "select Id from host" 


$hostlist | select Id | ForEach-Object { 
    $insertcmd =  "INSERT INTO HistSample (HostId, dtSample, dbValue, SampleTypeId) VALUES(" +
              ($_.Id -as [string]) +
               " , '" +
               $(Get-Date -format 'yyyy-MMM-dd HH:mm:ss') +
               "' , 66 ," +
               ($idSampleType).Id +
                " ) " 
                write-host $insertcmd
          
    MyInvoke-SQL $insertcmd
 } 
