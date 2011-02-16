namespace task5
{
    partial class Form1
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
            this.image = new System.Windows.Forms.PictureBox();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.fileList = new System.Windows.Forms.ListBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // image
            // 
            this.image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image.Location = new System.Drawing.Point(120, 24);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(689, 443);
            this.image.TabIndex = 0;
            this.image.TabStop = false;
            // 
            // fileList
            // 
            this.fileList.Dock = System.Windows.Forms.DockStyle.Left;
            this.fileList.FormattingEnabled = true;
            this.fileList.Location = new System.Drawing.Point(0, 24);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(120, 433);
            this.fileList.TabIndex = 3;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.fileList_SelectedIndexChanged);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFile});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mainMenu.Size = new System.Drawing.Size(809, 24);
            this.mainMenu.TabIndex = 4;
            this.mainMenu.Text = "menuStrip1";
            // 
            // mFile
            // 
            this.mFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpenFolder,
            this.mExit});
            this.mFile.Name = "mFile";
            this.mFile.Size = new System.Drawing.Size(37, 20);
            this.mFile.Text = "File";
            // 
            // mOpenFolder
            // 
            this.mOpenFolder.Name = "mOpenFolder";
            this.mOpenFolder.Size = new System.Drawing.Size(139, 22);
            this.mOpenFolder.Text = "Open Folder";
            this.mOpenFolder.Click += new System.EventHandler(this.mOpenFolder_Click);
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(139, 22);
            this.mExit.Text = "Exit";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 467);
            this.Controls.Add(this.image);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.ListBox fileList;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mFile;
        private System.Windows.Forms.ToolStripMenuItem mOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem mExit;
    }
}

