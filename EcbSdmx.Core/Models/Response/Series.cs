using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Models.Response
{
    [Serializable]
    [XmlRoot(ElementName = "Series", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public class Series
    {
        [XmlElement(ElementName = "Obs", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
        public List<Observation> Observations { get; set; }

        [XmlElement(ElementName = "SeriesKey", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
        public SeriesKey SeriesKey { get; set; }
    }
}
