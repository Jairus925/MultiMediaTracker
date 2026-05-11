using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace MultiMediaTrackerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboType.Items.Add("Anime");
            cboType.Items.Add("Movie");
            cboType.Items.Add("Game");
            cboType.Items.Add("Manga");
 

            cboStatus.Items.Add("Planning");
            cboStatus.Items.Add("In Progress");
            cboStatus.Items.Add("On-Hold");
            cboStatus.Items.Add("Completed");
            cboStatus.Items.Add("Dropped");

            if (File.Exists("watchlist.xml"))
            {
                XmlDocument doc = new XmlDocument();

                doc.Load("watchlist.xml");

                XmlElement root = doc.DocumentElement;

                foreach (XmlNode node in root.ChildNodes)
                {
                    lstEntries.Items.Add(node.InnerText);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = tbxTitle.Text;
            string type = cboType.Text;
            string status = cboStatus.Text;

            string entry = title + " | " + type + " | " + status;

            lstEntries.Items.Add(entry);

            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("watchlist");
            doc.AppendChild(root);

            foreach (string item in lstEntries.Items)
            {
                XmlElement entryElement = doc.CreateElement("entry");

                XmlText entryText = doc.CreateTextNode(item);

                entryElement.AppendChild(entryText);

                root.AppendChild(entryElement);
            }

            doc.Save("watchlist.xml");

            MessageBox.Show(
                "Entry Added!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
