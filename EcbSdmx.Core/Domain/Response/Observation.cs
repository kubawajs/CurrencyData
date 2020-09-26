using System;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Domain.Response
{
    [Serializable]
    [XmlRoot(ElementName = "Obs", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public class Observation
    {
        [XmlElement(ElementName = "ObsDimension", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
        public ObservationDimension Dimension { get; set; }

        [XmlElement(ElementName = "ObsValue", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
        public ObservationValue Value { get; set; }
    }
}
