namespace WForms
{
    partial class PropertiesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("DisplayName");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Group");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Name");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Text");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Type");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("ValueType");
            this.Props = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // Props
            // 
            this.Props.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Props.HideSelection = false;
            this.Props.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.Props.Location = new System.Drawing.Point(0, 0);
            this.Props.Name = "Props";
            this.Props.Size = new System.Drawing.Size(335, 315);
            this.Props.TabIndex = 0;
            this.Props.UseCompatibleStateImageBehavior = false;
            this.Props.View = System.Windows.Forms.View.List;
            this.Props.SelectedIndexChanged += new System.EventHandler(this.Props_SelectedIndexChanged);
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 315);
            this.Controls.Add(this.Props);
            this.Name = "PropertiesForm";
            this.Text = "Properties";
            this.Load += new System.EventHandler(this.Properties_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView Props;
    }
}