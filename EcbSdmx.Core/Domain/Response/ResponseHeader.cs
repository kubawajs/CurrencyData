using System;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Domain.Response
{
    [Serializable]
    [XmlRoot(ElementName = "Header", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public class ResponseHeader
    {
        [XmlElement(ElementName = "ID", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
        public string ID { get; set; }

        [XmlElement(ElementName = "Prepared", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
        public string Prepared { get; set; }

        [XmlElement(ElementName = "Sender", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
        public Sender Sender { get; set; }

        [XmlElement(ElementName = "Test", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
        public string Test { get; set; }
    }
}
