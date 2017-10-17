using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOKFMWeb.Models
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class opml
    {
        public opml()
        { }

        public opml(opmlHead headField, opmlOutline[] bodyField, decimal versionField)
        {
            this.headField = headField;
            this.bodyField = bodyField;
            this.versionField = versionField;
        }

        private opmlHead headField;

        private opmlOutline[] bodyField;

        private decimal versionField;

        /// <remarks/>
        public opmlHead head
        {
            get
            {
                return this.headField;
            }
            set
            {
                this.headField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("outline", IsNullable = false)]
        public opmlOutline[] body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class opmlHead
    {
        public opmlHead(string titleField, string dateCreatedField, string dateModifiedField)
        {
            this.titleField = titleField;
            this.dateCreatedField = dateCreatedField;
            this.dateModifiedField = dateModifiedField;
        }
        public opmlHead()
        { }

        private string titleField;

        private string dateCreatedField;

        private string dateModifiedField;

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string dateCreated
        {
            get
            {
                return this.dateCreatedField;
            }
            set
            {
                this.dateCreatedField = value;
            }
        }

        /// <remarks/>
        public string dateModified
        {
            get
            {
                return this.dateModifiedField;
            }
            set
            {
                this.dateModifiedField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class opmlOutline
    {
        public opmlOutline()
        { }

        public opmlOutline(string textField, string typeField, string xmlUrlField, string htmlUrlField)
        {
            this.textField = textField;
            this.typeField = typeField;
            this.xmlUrlField = xmlUrlField;
            this.htmlUrlField = htmlUrlField;
        }

        private string textField;

        private string typeField;

        private string xmlUrlField;

        private string htmlUrlField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string xmlUrl
        {
            get
            {
                return this.xmlUrlField;
            }
            set
            {
                this.xmlUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string htmlUrl
        {
            get
            {
                return this.htmlUrlField;
            }
            set
            {
                this.htmlUrlField = value;
            }
        }
    }
}

