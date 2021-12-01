using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace US_Future
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            US_future();
        }

        private void US_future()
        {
            while (true)
            {
                string url = "https://www.investing.com/indices/indices-futures";

                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = null;

                try
                {
                    doc = web.Load(url); // FileNotFoundExceptions are handled here.
                }
                catch (Exception e)
                {
                    continue;
                }

                List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table")
                    .Descendants("tr")
                    .Skip(1)
                    .Where(tr => tr.Elements("td").Count() > 1)
                    .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                    .ToList();
                if (table.Count == 0)
                    continue;

                string sp_string = table[1][7].Trim(new Char[] { '%' });
                string sp_time = table[1][8].Trim(new Char[] { '%' });
                string nasdaq_string = table[2][7].Trim(new Char[] { '%' });
                string nasdaq_time = table[2][8].Trim(new Char[] { '%' });

                textBox1.Text = sp_string;
                textBox2.Text = nasdaq_string;
            }
        }
    }
}
