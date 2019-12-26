using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PDFMerge
{
    public class ParseXML
    {
        public static DataTable parse_htmlDt(string doc,string catg)
        {
            DataTable result_Dt = new DataTable();
            result_Dt.Columns.Add("category");
            result_Dt.Columns.Add("subject");
            result_Dt.Columns.Add("subjectURL");

            var hdc = new HtmlDocument();
            hdc.LoadHtml(doc.ToString());

            var htmltag = "/html/body/div[2]/div[1]/div[2]/div/div[1]/div/div[2]";
            var mythml = hdc.DocumentNode.SelectSingleNode(htmltag);
            var CNodes = mythml.ChildNodes;
            var i = 0;
            foreach(var item in CNodes)
            {
                if (item.Name != "#text")
                {
                    if (i == 5)
                    {
                        //List<string> mytxt = null;
                        htmltag = "/div[1]/div[1]";
                        var thishdc = new HtmlDocument();
                        thishdc.LoadHtml(item.InnerHtml);
                        var thishtml = thishdc.DocumentNode.SelectSingleNode(htmltag);
                        var subCNodes = thishtml.ChildNodes;
                        

                        foreach (var item_j in subCNodes)
                        {
                            if (item_j.Name != "#text")
                            {
                                List<string> topicLs = new List<string>();
                                var category_txt = item_j.InnerText.Trim().ToString();
                                var category_url = item_j.ChildNodes[1].ChildNodes[0].Attributes["href"].Value.Trim().ToString();
                                topicLs.Add(catg);
                                topicLs.Add(category_txt);
                                topicLs.Add(category_url);
                                Console.WriteLine(string.Format("{0} : {1}", category_txt, category_url));
                                result_Dt.Rows.Add(topicLs.ToArray());
                            }

                        }

                        //mytxt = parse_WorksheetLs(item);
                        //result_Dt.Rows.Add(mytxt.ToArray());
                    }

                }
                i++;
                //Console.WriteLine(item);
            }
            
            return result_Dt;
        }


        
        public static DataTable parse_topicDt(string doc, string catg, string subject, string subjectURL)
        {
            DataTable result_Dt = new DataTable();
            result_Dt.Columns.Add("category");
            result_Dt.Columns.Add("subject");
            result_Dt.Columns.Add("subjectURL");
            result_Dt.Columns.Add("topic");
            result_Dt.Columns.Add("topicURL");

            var hdc = new HtmlDocument();
            hdc.LoadHtml(doc.ToString());

            var htmltag = "/html/body/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[2]/div[3]/div";
            var mythml = hdc.DocumentNode.SelectSingleNode(htmltag);
            var CNodes = mythml.ChildNodes;
            var i = 0;
            foreach (var item in CNodes)
            {
                if (item.Name != "#text")
                {
                    List<string> topicLs = new List<string>();
                    var title = item.ChildNodes[1].Attributes["title"].Value.Trim().ToString();
                    var topicURL = item.ChildNodes[1].Attributes["href"].Value.Trim().ToString();
                    topicLs.Add(catg);
                    topicLs.Add(subject);
                    topicLs.Add(subjectURL);
                    topicLs.Add(title);
                    topicLs.Add(topicURL);
                    result_Dt.Rows.Add(topicLs.ToArray());
                }
                i++;
                //Console.WriteLine(item);
            }

            return result_Dt;
        }

        public static DataTable parse_topic_PDFfile_Dt(string doc, string catg, string subject, string subjectURL,string topic,string topicURL)
        {
            DataTable result_Dt = new DataTable();
            result_Dt.Columns.Add("category");
            result_Dt.Columns.Add("subject");
            result_Dt.Columns.Add("subjectURL");
            result_Dt.Columns.Add("topic");
            result_Dt.Columns.Add("topicURL");
            result_Dt.Columns.Add("subtopic");
            result_Dt.Columns.Add("subtopicURL");

            var hdc = new HtmlDocument();
            hdc.LoadHtml(doc.ToString());

            var htmltag = "/html/body/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[2]/div[3]/div";
            var mythml = hdc.DocumentNode.SelectSingleNode(htmltag);
            var CNodes = mythml.ChildNodes;
            var i = 0;
            foreach (var item in CNodes)
            {
                if (item.Name != "#text")
                {
                    List<string> topicLs = new List<string>();
                    var subtopic = item.ChildNodes[1].Attributes["title"].Value.Trim().ToString();
                    var subtopicURL = item.ChildNodes[1].ChildNodes[1].ChildNodes[0].ChildNodes[0].Attributes["src"].Value.Trim().ToString();
                    subtopicURL = subtopicURL.Replace("thumb", "question");
                    subtopicURL = "https://www.edubuzzkids.com/" + subtopicURL.Replace(".jpg",".pdf");
                    topicLs.Add(catg);
                    topicLs.Add(subject);
                    topicLs.Add(subjectURL);
                    topicLs.Add(topic);
                    topicLs.Add(topicURL);
                    topicLs.Add(subtopic);
                    topicLs.Add(subtopicURL);
                    result_Dt.Rows.Add(topicLs.ToArray());
                }
                i++;
                //Console.WriteLine(item);
            }

            return result_Dt;
        }

        public static List<string> parse_WorksheetLs(HtmlNode input_txt)
        {
            var mytxt = input_txt.InnerText.Trim().Split('\n');
            var newtxt_Ls = new List<string>();
            var i = 0;
            foreach(var eachtxt in mytxt)
            {
                if (!eachtxt.Contains("<!=="))
                {
                    string temptxt;
                    temptxt = eachtxt.Trim();
                    if (temptxt != "")
                    {
                        newtxt_Ls.Add(temptxt);
                    }
                }
                i++;
            }
            return newtxt_Ls;
        }

    }
}
