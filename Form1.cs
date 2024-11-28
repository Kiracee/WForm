using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml;
using WForms;
using ServerApi; 



namespace WForms
{
    public partial class Form1 : Form
    {
       
        
        
        
        
        public Form1()
        {
            InitializeComponent();
            
            
            PropertiesForm pf = new PropertiesForm();
            pf.Show(this);
            method("RS25-1111n", 2025);
            //pf.paramList.Add();
        }

        

        private void method(string hostServ, int ver)
        {

            RevitServer rs = new RevitServer(hostServ, Convert.ToInt16(ver));
            ServerApi.DirectoryInfo di = new ServerApi.DirectoryInfo();
            di = rs.GetDirectoryInfo("/1111_ARH");
            ServerApi.FolderContents fc = new ServerApi.FolderContents();
            fc = rs.GetFolderContents("/1111_ARH");

            MessageBox.Show(fc.DriveFreeSpace.ToString());
            //rs.Copy(, , true)
        }
      



        

        private List<object> getCheckedNodes(TreeView tv)
        {
            List<object> checkedNodesList = new List<object>();

            foreach (TreeNode node in tv.Nodes)
            {
                if (!node.Checked)
                {
                    checkedNodesList.Add(node);
                }
            }

            return checkedNodesList;
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        private void Node_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
            List<object> checkedNodes = new List<object>();

            checkedNodes = getCheckedNodes(CopyFromTree);
            MessageBox.Show(checkedNodes.ToString());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }






        private void CopyFromTree_AfterCheck(object sender, TreeViewEventArgs e)
        {

            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }

            getCheckedNodes(CopyFromTree);

        }

        private void CopyToTree_AfterCheck(object sender, TreeViewEventArgs e)
        {

            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
            getCheckedNodes(CopyToTree);
        }

        private void FromButton_Click(object sender, EventArgs e)
        {
            try
            {

                // If we succeeded then let's clear the tree items

                CopyFromTree.Nodes.Clear();

                // Add the root folder

                TreeNode root =
                  CopyFromTree.Nodes.Add("revitserver");

                // Get the contents of the root folder


                

                // Show the root contents

                root.Expand();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed to retrieve data");
            }
        }

        private void ToButton_Click(object sender, EventArgs e)
        {

            try
            {

                // If we succeeded then let's clear the tree items

                CopyToTree.Nodes.Clear();

                // Add the root folder

                TreeNode root =
                  CopyToTree.Nodes.Add("revitserver");

                // Get the contents of the root folder


                

                // Show the root contents

                root.Expand();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed to retrieve data");
            }

        }

        private void FromServer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ToServer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Version_SelectedIndexChanged(object sender, EventArgs e)
        {
            toServer.Items.Clear();
            fromServer.Items.Clear();
            using (StreamReader sr = new StreamReader($"C:/ProgramData/Autodesk/Revit Server " + version.Text + "/Config/RSN.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    toServer.Items.Add(line);
                    fromServer.Items.Add(line);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
