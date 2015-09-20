# first how do we do the average ?



(get-counter -Counter "\Processor(_Total)\% Processor Time" -SampleInterval 1 -MaxSamples 5 |
    select -ExpandProperty countersamples | select -ExpandProperty cookedvalue | Measure-Object -Average).average


 MyGet-CpuUsage 