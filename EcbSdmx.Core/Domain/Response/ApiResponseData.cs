using System;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Domain.Response
{
    [Serializable]
    [XmlRoot(ElementName = "GenericData", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public class ApiResponseData
    {
        [XmlAttribute(AttributeName = "common", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Common { get; set; }

        [XmlElement(ElementName = "DataSet", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
        public DataSet DataSet { get; set; }

        [XmlAttribute(AttributeName = "generic", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Generic { get; set; }

        [XmlElement(ElementName = "Header", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
        public ResponseHeader Header { get; set; }

        [XmlAttribute(AttributeName = "message", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Message { get; set; }

        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
    }
}
