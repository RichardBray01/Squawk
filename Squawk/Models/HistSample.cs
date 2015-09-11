namespace Squawk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistSample")]
    public partial class HistSample
    {
        public int Id { get; set; }

        public int HostId { get; set; }

        public int SampleTypeId { get; set; }

        public DateTime dtSample { get; set; }

        public double dbValue { get; set; }

        public virtual Host Host { get; set; }

        public virtual SampleType SampleType { get; set; }
    }
}
