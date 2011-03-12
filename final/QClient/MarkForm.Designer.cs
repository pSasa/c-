namespace QClient
{
    partial class MarkForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CPerson = new System.Windows.Forms.ComboBox();
            this.CSubject = new System.Windows.Forms.ComboBox();
            this.CMark = new System.Windows.Forms.ComboBox();
            this.BOk = new System.Windows.Forms.Button();
            this.BCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Студент";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Предмет";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Оценка";
            // 
            // CPerson
            // 
            this.CPerson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CPerson.FormattingEnabled = true;
            this.CPerson.Location = new System.Drawing.Point(66, 9);
            this.CPerson.Name = "CPerson";
            this.CPerson.Size = new System.Drawing.Size(150, 21);
            this.CPerson.TabIndex = 3;
            // 
            // CSubject
            // 
            this.CSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CSubject.FormattingEnabled = true;
            this.CSubject.Location = new System.Drawing.Point(66, 36);
            this.CSubject.Name = "CSubject";
            this.CSubject.Size = new System.Drawing.Size(150, 21);
            this.CSubject.TabIndex = 4;
            // 
            // CMark
            // 
            this.CMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMark.FormattingEnabled = true;
            this.CMark.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.CMark.Location = new System.Drawing.Point(66, 59);
            this.CMark.Name = "CMark";
            this.CMark.Size = new System.Drawing.Size(150, 21);
            this.CMark.TabIndex = 5;
            // 
            // BOk
            // 
            this.BOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BOk.Location = new System.Drawing.Point(246, 13);
            this.BOk.Name = "BOk";
            this.BOk.Size = new System.Drawing.Size(75, 23);
            this.BOk.TabIndex = 6;
            this.BOk.Text = "Сохранить";
            this.BOk.UseVisualStyleBackColor = true;
            this.BOk.Click += new System.EventHandler(this.BOk_Click);
            // 
            // BCancel
            // 
            this.BCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BCancel.Location = new System.Drawing.Point(246, 42);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new System.Drawing.Size(75, 23);
            this.BCancel.TabIndex = 7;
            this.BCancel.Text = "Отмена";
            this.BCancel.UseVisualStyleBackColor = true;
            // 
            // MarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 106);
            this.Controls.Add(this.BCancel);
            this.Controls.Add(this.BOk);
            this.Controls.Add(this.CMark);
            this.Controls.Add(this.CSubject);
            this.Controls.Add(this.CPerson);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MarkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор оценок";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CPerson;
        private System.Windows.Forms.ComboBox CSubject;
        private System.Windows.Forms.ComboBox CMark;
        private System.Windows.Forms.Button BOk;
        private System.Windows.Forms.Button BCancel;
    }
}