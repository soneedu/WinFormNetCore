using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

namespace PDFMerge
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient _client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        public static async void HttpAsync()
        {
            DataTable finalDt = new DataTable();
            List<string> category_Ls = new List<string>() { "english","math", "science" };
            foreach (var catg in category_Ls)
            {
                var result = await _client.GetAsync("https://www.edubuzzkids.com/worksheets/pre-k/" + catg);
                string x = await result.Content.ReadAsStringAsync();
                //Console.WriteLine($"{i}:{x}");
                
                DataTable category_Dt = ParseXML.parse_htmlDt(x,catg);
                finalDt.Merge(category_Dt);
            }

            DataTable topic_finalDt = new DataTable();
            foreach (DataRow row in finalDt.Rows)
            {
                var catg = row.ItemArray[0].ToString();
                var subject = row.ItemArray[1].ToString();
                var subject_URL = "https://www.edubuzzkids.com/" + row.ItemArray[2].ToString();
                var result = await _client.GetAsync(subject_URL);
                string x = await result.Content.ReadAsStringAsync();
                DataTable topic_Dt = ParseXML.parse_topicDt(x, catg,subject,subject_URL);
                topic_finalDt.Merge(topic_Dt);
            }

            DataTable pdfFile_finalDt = new DataTable();
            foreach (DataRow row in topic_finalDt.Rows)
            {
                var catg = row.ItemArray[0].ToString();
                var subject = row.ItemArray[1].ToString();
                var subject_URL = row.ItemArray[2].ToString();
                var topic = row.ItemArray[3].ToString();
                var topicURL = "https://www.edubuzzkids.com/" + row.ItemArray[4].ToString();

                var result = await _client.GetAsync(topicURL);
                string x = await result.Content.ReadAsStringAsync();
                DataTable topic_PDFfile_Dt = ParseXML.parse_topic_PDFfile_Dt(x, catg, subject, subject_URL,topic,topicURL);
                pdfFile_finalDt.Merge(topic_PDFfile_Dt);
            }

            My_DataTable_Extensions.ExportToExcel(pdfFile_finalDt, AppDomain.CurrentDomain.BaseDirectory + "FinalAllPDFinfor.xlsx");

            int counter = 0;
            foreach (DataRow row in pdfFile_finalDt.Rows)
            {
                var catg = row.ItemArray[0].ToString();
                var subject = row.ItemArray[1].ToString();
                var topic = row.ItemArray[3].ToString();
                var subtopic = row.ItemArray[5].ToString();
                var subtopicURL = row.ItemArray[6].ToString();

                await DownloadPDFfile(catg, subject, topic, subtopic, subtopicURL);
                counter++;
                Console.WriteLine(string.Format("=== {0} Download Done! => {1} {2} {3} {4} {5}",counter.ToString() ,catg,subject,topic,subtopic,subtopicURL));
            }
                Console.WriteLine("=== The End ===");
        }


        private static async Task DownloadPDFfile(string catg, string subject,  string topic, string subtopic,string subtopicurl)
        {
            var client = new HttpClient();
            System.IO.FileStream fs;
            var filepath = subtopicurl;
            var m = Regex.Matches(filepath, @".*/(.*?)$");
            var filename = m[0].Groups[1].Value.ToString();

            var PDFfilePath = AppDomain.CurrentDomain.BaseDirectory + "\\KidsLearning\\" + catg + "\\" + subject + "\\" + topic +  "\\" + filename;
            var fi = new FileInfo(PDFfilePath);
            var di = fi.Directory;
            if (!di.Exists)
                di.Create();
            if (!File.Exists(PDFfilePath))
            {
                var uri = new Uri(Uri.EscapeUriString(subtopicurl));
                byte[] urlContents = await client.GetByteArrayAsync(uri);

                fs = new System.IO.FileStream(PDFfilePath, System.IO.FileMode.CreateNew);
                fs.Write(urlContents, 0, urlContents.Length);
                fs.Close();
                Console.WriteLine(" file download complete!");
            }
            Console.WriteLine(" file exist!");
        }


            private void btn_run_Click(object sender, EventArgs e)
        {
            HttpAsync();
            Console.WriteLine("开始工作!");



        }

        private void btn_openfile_Click(object sender, EventArgs e)
        {
            //DownloadPDFfile("science", "Birds and Insects", "Birds", "Identify Birds", "https://www.edubuzzkids.com/content/pre-k/worksheets/science/Birds and Insects/Birds/Identify-Birds.pdf");
            Console.WriteLine("===  Download Done!  ===");
        }

        private void btn_MergePDF_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            List<DirectoryInfo> list = new List<DirectoryInfo>();

            GetEndDirectories(dir, list);
            // 输出所有目录
            foreach (DirectoryInfo di in list)
            {
                Console.WriteLine(di.FullName);
                string[] files = Directory.GetFiles(di.FullName);
                //String[] files = new String[] { "2_Phonic-Match.pdf", "3_Phonic-Match.pdf" };
                string outputFile = di.FullName + "\\合并打印用.pdf";
                PdfDocumentBase doc = PdfDocument.MergeFiles(files);
                doc.Save(outputFile, FileFormat.PDF);
                Console.WriteLine(" ---");
            }


            ////System.Diagnostics.Process.Start(outputFile);
        }

        /// <summary>
        /// 查找指定目录下的所有末级子目录
        /// </summary>
        /// <param name="dir">要查找的目录</param>
        /// <param name="list">查找结果列表</param>
        /// <param name="system">是否包含系统目录</param>
        /// <param name="hidden">是否包含隐藏目录</param>
        static void GetEndDirectories(DirectoryInfo dir, List<DirectoryInfo> list, bool system = false, bool hidden = false)
        {
            DirectoryInfo[] sub = dir.GetDirectories();

            if (sub.Length == 0)
            {// 没有子目录了
                list.Add(dir);
                return;
            }

            foreach (DirectoryInfo subDir in sub)
            {
                // 跳过系统目录
                if (!system && (subDir.Attributes & FileAttributes.System) == FileAttributes.System)
                    continue;
                // 跳过隐藏目录
                if (!hidden && (subDir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    continue;

                GetEndDirectories(subDir, list);
            }
        }


    }
}
