using System;

namespace EcbSdmx.Core.Domain
{
    public class EcbSdmxQueryParameters
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public string UpdatedAfter { get; set; }
        public int FirstNObservations { get; set; }
        public int LastNObservations { get; set; }
        public string Detail { get; set; }
        public bool IncludeHistory { get; set; }
    }
}
