using System;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Models.Response
{
    [Serializable]
    [XmlRoot(ElementName = "Value", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public class GenericValue
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}
