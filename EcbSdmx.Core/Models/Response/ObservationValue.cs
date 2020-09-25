using System.Xml.Serialization;

namespace EcbSdmx.Core.Models.Response
{
    [XmlRoot(ElementName = "ObsValue", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public class ObservationValue
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}
