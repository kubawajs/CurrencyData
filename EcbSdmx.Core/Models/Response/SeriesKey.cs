using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Models.Response
{
    [Serializable]
    [XmlRoot(ElementName = "SeriesKey", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public class SeriesKey
    {
        [XmlElement(ElementName = "Value", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
        public List<GenericValue> Value { get; set; }
    }
}
