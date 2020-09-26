using System;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Domain.Response
{
    [Serializable]
    [XmlRoot(ElementName = "DataSet", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public class DataSet
    {
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }

        [XmlElement(ElementName = "Series", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
        public Series Series { get; set; }

        [XmlAttribute(AttributeName = "structureRef")]
        public string StructureRef { get; set; }

        [XmlAttribute(AttributeName = "validFromDate")]
        public string ValidFromDate { get; set; }
    }
}
