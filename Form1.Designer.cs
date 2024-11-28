namespace WForms
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.CopyFromTree = new System.Windows.Forms.TreeView();
            this.CopyToTree = new System.Windows.Forms.TreeView();
            this.version = new System.Windows.Forms.ComboBox();
            this.CopyButton = new System.Windows.Forms.Button();
            this.fromServer = new System.Windows.Forms.ComboBox();
            this.toServer = new System.Windows.Forms.ComboBox();
            this.fromButton = new System.Windows.Forms.Button();
            this.toButton = new System.Windows.Forms.Button();
            this.ServerApiTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CopyFromTree
            // 
            this.CopyFromTree.CheckBoxes = true;
            this.CopyFromTree.Location = new System.Drawing.Point(12, 68);
            this.CopyFromTree.Name = "CopyFromTree";
            this.CopyFromTree.Size = new System.Drawing.Size(250, 275);
            this.CopyFromTree.TabIndex = 0;
            this.CopyFromTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.CopyFromTree_AfterCheck);

            // 
            // CopyToTree
            // 
            this.CopyToTree.CheckBoxes = true;
            this.CopyToTree.Location = new System.Drawing.Point(538, 68);
            this.CopyToTree.Name = "CopyToTree";
            this.CopyToTree.Size = new System.Drawing.Size(250, 275);
            this.CopyToTree.TabIndex = 1;
            this.CopyToTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.CopyToTree_AfterCheck);
            // 
            // version
            // 
            this.version.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.version.FormattingEnabled = true;
            this.version.Location = new System.Drawing.Point(12, 417);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(53, 21);
            this.version.TabIndex = 4;
            this.version.SelectedIndexChanged += new System.EventHandler(this.Version_SelectedIndexChanged);
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(713, 415);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 23);
            this.CopyButton.TabIndex = 5;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            // 
            // fromServer
            // 
            this.fromServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromServer.FormattingEnabled = true;
            this.fromServer.Location = new System.Drawing.Point(12, 30);
            this.fromServer.Name = "fromServer";
            this.fromServer.Size = new System.Drawing.Size(160, 21);
            this.fromServer.TabIndex = 6;
            this.fromServer.SelectedIndexChanged += new System.EventHandler(this.FromServer_SelectedIndexChanged);
            // 
            // toServer
            // 
            this.toServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toServer.FormattingEnabled = true;
            this.toServer.Location = new System.Drawing.Point(538, 30);
            this.toServer.Name = "toServer";
            this.toServer.Size = new System.Drawing.Size(160, 21);
            this.toServer.TabIndex = 7;
            this.toServer.SelectedIndexChanged += new System.EventHandler(this.ToServer_SelectedIndexChanged);
            // 
            // fromButton
            // 
            this.fromButton.Location = new System.Drawing.Point(186, 29);
            this.fromButton.Name = "fromButton";
            this.fromButton.Size = new System.Drawing.Size(75, 23);
            this.fromButton.TabIndex = 8;
            this.fromButton.Text = "fromServer";
            this.fromButton.UseVisualStyleBackColor = true;
            this.fromButton.Click += new System.EventHandler(this.FromButton_Click);
            // 
            // toButton
            // 
            this.toButton.Location = new System.Drawing.Point(712, 29);
            this.toButton.Name = "toButton";
            this.toButton.Size = new System.Drawing.Size(75, 23);
            this.toButton.TabIndex = 9;
            this.toButton.Text = "toServer";
            this.toButton.UseVisualStyleBackColor = true;
            this.toButton.Click += new System.EventHandler(this.ToButton_Click);
            // 
            // ServerApiTest
            // 
            this.ServerApiTest.Location = new System.Drawing.Point(346, 360);
            this.ServerApiTest.Name = "ServerApiTest";
            this.ServerApiTest.Size = new System.Drawing.Size(94, 23);
            this.ServerApiTest.TabIndex = 10;
            this.ServerApiTest.Text = "serverApiTest";
            this.ServerApiTest.UseVisualStyleBackColor = true;
            this.ServerApiTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ServerApiTest);
            this.Controls.Add(this.toButton);
            this.Controls.Add(this.fromButton);
            this.Controls.Add(this.toServer);
            this.Controls.Add(this.fromServer);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.version);
            this.Controls.Add(this.CopyToTree);
            this.Controls.Add(this.CopyFromTree);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView CopyFromTree;
        private System.Windows.Forms.TreeView CopyToTree;
        private System.Windows.Forms.ComboBox version;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.ComboBox fromServer;
        private System.Windows.Forms.ComboBox toServer;
        private System.Windows.Forms.Button fromButton;
        private System.Windows.Forms.Button toButton;
        private System.Windows.Forms.Button ServerApiTest;
    }
}

