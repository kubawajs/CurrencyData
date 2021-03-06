﻿using System;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Domain.Response
{
    [Serializable]
    [XmlRoot(ElementName = "Sender", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public class Sender
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }
}
