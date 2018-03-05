
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace VCSWEB1.Models
{
    [XmlRoot(ElementName="response", Namespace ="")]
    public class Response
    {
        [XmlAttribute(AttributeName ="type")] public string type { get; set; }
        [XmlElement] public string status { get; set; }
        [XmlElement] public XmlElement data { get; set; }

        public void build_data(DataTable dt) {

            XmlDocument doc = new XmlDocument();
            XmlElement data = doc.CreateElement("data");
            this.data = data;

            XmlElement columns = doc.CreateElement("columns");
            XmlElement rows = doc.CreateElement("rows");
            data.AppendChild(columns);
            data.AppendChild(rows);

            foreach (DataColumn col in dt.Columns) {
                XmlElement tmp = doc.CreateElement(col.ColumnName);
                columns.AppendChild(tmp);
            }

            int rowid = 0;
            foreach (DataRow row in dt.Rows) {
                rowid++;
                foreach (DataColumn col in dt.Columns) {
                    XmlElement tmp = doc.CreateElement("row");
                    tmp.SetAttribute("rowid", rowid.ToString());
                    tmp.SetAttribute("column", col.ColumnName);
                    tmp.SetAttribute("value", row[col.ColumnName].ToString());
                    tmp.InnerText =row[col.ColumnName].ToString();
                    rows.AppendChild(tmp);
                }
            }
        }
    }
}