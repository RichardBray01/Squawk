 $command =  
              "INSERT INTO HistSample (HostId, dtSample, dbValue, SampleTypeId) VALUES('" +
              ($_.Id -as [string]) +
               " , " +
               (Get-Date -format d -as [string]) +
               "," +
            
                " ," +
               ($idSampleType -as [string]) +
                " ) " 
          

        
