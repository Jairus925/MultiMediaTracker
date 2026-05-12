using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            if (!File.Exists("watchlist.xml"))
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("watchlist");
                doc.AppendChild(root);
                doc.Save("watchlist.xml");
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("watchlist.xml");
                XmlNode root = doc.SelectSingleNode("watchlist");
                foreach (XmlNode node in root.ChildNodes)
                {
                    lstEntries.Items.Add("Title: " + node.Attributes["title"].Value);
                    lstEntries.Items.Add("Type: " + node.Attributes["type"].Value);
                    lstEntries.Items.Add("Status: " + node.Attributes["status"].Value);
                    lstEntries.Items.Add("Rating: " + node.Attributes["rating"].Value);
                    lstEntries.Items.Add("---------------------------------------------");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string title = tbxTitle.Text;
            string type = cboType.Text;
            string status = cboStatus.Text;
            string rating = cboRating.Text;


            if (title == "" || type == "" || status == "" || rating == "")
            {
                MessageBox.Show("Please fill out all fields.");
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("watchlist.xml");
            XmlNode root = doc.SelectSingleNode("watchlist");
            int entryNum = root.ChildNodes.Count + 1;

            doc.AppendChild(root);

            XmlElement entryElement = doc.CreateElement("entry");

            entryElement.SetAttribute("title", title);
            entryElement.SetAttribute("number", entryNum.ToString());
            entryElement.SetAttribute("type", type);
            entryElement.SetAttribute("status", status);
            entryElement.SetAttribute("rating", rating);

            root.AppendChild(entryElement);

            lstEntries.Items.Add("Title: " + entryElement.Attributes["title"].Value);
            lstEntries.Items.Add("Type: " + entryElement.Attributes["type"].Value);
            lstEntries.Items.Add("Status: " + entryElement.Attributes["status"].Value);
            lstEntries.Items.Add("Rating: " + entryElement.Attributes["rating"].Value);
            lstEntries.Items.Add("---------------------------------------------");

            doc.Save("watchlist.xml");

            MessageBox.Show(
                "Entry Added!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string title = tbxTitle.Text;
            string type = cboType.Text;
            string status = cboStatus.Text;
            string rating = cboRating.Text;

            XmlDocument doc = new XmlDocument();
            doc.Load("watchlist.xml");

            XmlNode entry = doc.SelectSingleNode("//entry[@number='" + entrySelect.Text + "']");

            if (entry == null)
            {
                MessageBox.Show("Cannot edit entry. Entry not found.");
                return;
            }
            lstEntries.Items.Clear();
            XmlNode parent = entry.ParentNode;
            XmlElement editedEntry = doc.CreateElement("entry");

            editedEntry.SetAttribute("title", title);
            editedEntry.SetAttribute("number", entry.Attributes["number"].Value);
            editedEntry.SetAttribute("type", type);
            editedEntry.SetAttribute("status", status);
            editedEntry.SetAttribute("rating", rating);

            parent.ReplaceChild(editedEntry, entry);

            foreach(XmlNode node in parent.ChildNodes)
            {
                lstEntries.Items.Add("Title: " + node.Attributes["title"].Value);
                lstEntries.Items.Add("Type: " + node.Attributes["type"].Value);
                lstEntries.Items.Add("Status: " + node.Attributes["status"].Value);
                lstEntries.Items.Add("Rating: " + node.Attributes["rating"].Value);
                lstEntries.Items.Add("---------------------------------------------");
            }

            doc.Save("watchlist.xml");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string title = tbxTitle.Text;
            string type = cboType.Text;
            string status = cboStatus.Text;
            string rating = cboRating.Text;

            XmlDocument doc = new XmlDocument();
            doc.Load("watchlist.xml");

            XmlNode entry = doc.SelectSingleNode("//entry[@number='" + entrySelect.Text + "']");

            if (entry == null)
            {
                MessageBox.Show("Cannot delete entry. Entry not found.");
                return;
            }
            lstEntries.Items.Clear();
            int nodeNum = int.Parse(entry.Attributes["number"].Value);
            XmlNode root = doc.SelectSingleNode("watchlist");
            root.RemoveChild(entry);

            foreach (XmlNode node in root.ChildNodes)
            {
                if (int.Parse(node.Attributes["number"].Value) > nodeNum)
                    node.Attributes["number"].Value = (int.Parse(node.Attributes["number"].Value) - 1).ToString();

                lstEntries.Items.Add("Title: " + node.Attributes["title"].Value);
                lstEntries.Items.Add("Type: " + node.Attributes["type"].Value);
                lstEntries.Items.Add("Status: " + node.Attributes["status"].Value);
                lstEntries.Items.Add("Rating: " + node.Attributes["rating"].Value);
                lstEntries.Items.Add("---------------------------------------------");
            }
            doc.Save("watchlist.xml");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
