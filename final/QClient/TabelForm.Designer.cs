namespace QClient
{
    partial class TabelForm
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
            this.personGrid = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Cours = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.delPerson = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectGrid = new System.Windows.Forms.DataGridView();
            this.SubjectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubjectTeacher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubjectHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delSubject = new System.Windows.Forms.Button();
            this.markGrid = new System.Windows.Forms.DataGridView();
            this.MarkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarkPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarkSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarkMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delMark = new System.Windows.Forms.Button();
            this.editMark = new System.Windows.Forms.Button();
            this.addMark = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.personGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subjectGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.markGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // personGrid
            // 
            this.personGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.personGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.personGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.personGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.surname,
            this.name,
            this.Group,
            this.Cours});
            this.personGrid.Location = new System.Drawing.Point(0, 27);
            this.personGrid.MultiSelect = false;
            this.personGrid.Name = "personGrid";
            this.personGrid.Size = new System.Drawing.Size(867, 206);
            this.personGrid.TabIndex = 0;
            this.personGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.personGreed_CellValueChanged);
            this.personGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.personGreed_RowValidating);
            this.personGrid.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.personGreed_DefaultValuesNeeded);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // surname
            // 
            this.surname.FillWeight = 86.6665F;
            this.surname.HeaderText = "Фамилия";
            this.surname.Name = "surname";
            // 
            // name
            // 
            this.name.FillWeight = 86.6665F;
            this.name.HeaderText = "Имя";
            this.name.Name = "name";
            // 
            // Group
            // 
            this.Group.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Group.FillWeight = 70F;
            this.Group.HeaderText = "Группа";
            this.Group.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.Group.Name = "Group";
            this.Group.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Group.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Group.Width = 70;
            // 
            // Cours
            // 
            this.Cours.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Cours.FillWeight = 70F;
            this.Cours.HeaderText = "Курс";
            this.Cours.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.Cours.Name = "Cours";
            this.Cours.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Cours.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Cours.Width = 70;
            // 
            // delPerson
            // 
            this.delPerson.Location = new System.Drawing.Point(12, 239);
            this.delPerson.Name = "delPerson";
            this.delPerson.Size = new System.Drawing.Size(121, 23);
            this.delPerson.TabIndex = 1;
            this.delPerson.Text = "Удалить студента";
            this.delPerson.UseVisualStyleBackColor = true;
            this.delPerson.Click += new System.EventHandler(this.delPerson_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(869, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateMenu,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(45, 20);
            this.toolStripMenuItem1.Text = "Файл";
            // 
            // updateMenu
            // 
            this.updateMenu.Name = "updateMenu";
            this.updateMenu.Size = new System.Drawing.Size(135, 22);
            this.updateMenu.Text = "Обновить";
            this.updateMenu.Click += new System.EventHandler(this.updateMenu_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // subjectGrid
            // 
            this.subjectGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.subjectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.subjectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SubjectId,
            this.SubjectName,
            this.SubjectTeacher,
            this.SubjectHour});
            this.subjectGrid.Location = new System.Drawing.Point(0, 268);
            this.subjectGrid.MultiSelect = false;
            this.subjectGrid.Name = "subjectGrid";
            this.subjectGrid.Size = new System.Drawing.Size(867, 150);
            this.subjectGrid.TabIndex = 3;
            this.subjectGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.personGreed_CellValueChanged);
            this.subjectGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.personGreed_RowValidating);
            this.subjectGrid.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.personGreed_DefaultValuesNeeded);
            // 
            // SubjectId
            // 
            this.SubjectId.HeaderText = "id";
            this.SubjectId.Name = "SubjectId";
            this.SubjectId.Visible = false;
            // 
            // SubjectName
            // 
            this.SubjectName.HeaderText = "Название предмета";
            this.SubjectName.Name = "SubjectName";
            // 
            // SubjectTeacher
            // 
            this.SubjectTeacher.HeaderText = "Преподаватель";
            this.SubjectTeacher.Name = "SubjectTeacher";
            // 
            // SubjectHour
            // 
            this.SubjectHour.HeaderText = "Часы";
            this.SubjectHour.Name = "SubjectHour";
            // 
            // delSubject
            // 
            this.delSubject.Location = new System.Drawing.Point(12, 425);
            this.delSubject.Name = "delSubject";
            this.delSubject.Size = new System.Drawing.Size(121, 23);
            this.delSubject.TabIndex = 4;
            this.delSubject.Text = "Удалить предмет";
            this.delSubject.UseVisualStyleBackColor = true;
            this.delSubject.Click += new System.EventHandler(this.delPerson_Click);
            // 
            // markGrid
            // 
            this.markGrid.AllowUserToAddRows = false;
            this.markGrid.AllowUserToDeleteRows = false;
            this.markGrid.AllowUserToResizeRows = false;
            this.markGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.markGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.markGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MarkId,
            this.MarkPerson,
            this.MarkSubject,
            this.MarkMark});
            this.markGrid.Location = new System.Drawing.Point(0, 454);
            this.markGrid.MultiSelect = false;
            this.markGrid.Name = "markGrid";
            this.markGrid.ReadOnly = true;
            this.markGrid.Size = new System.Drawing.Size(867, 150);
            this.markGrid.TabIndex = 5;
            this.markGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.markGrid_CellMouseDoubleClick);
            // 
            // MarkId
            // 
            this.MarkId.HeaderText = "id";
            this.MarkId.Name = "MarkId";
            this.MarkId.ReadOnly = true;
            this.MarkId.Visible = false;
            // 
            // MarkPerson
            // 
            this.MarkPerson.HeaderText = "Студент";
            this.MarkPerson.Name = "MarkPerson";
            this.MarkPerson.ReadOnly = true;
            this.MarkPerson.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MarkPerson.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MarkSubject
            // 
            this.MarkSubject.HeaderText = "Предмет";
            this.MarkSubject.Name = "MarkSubject";
            this.MarkSubject.ReadOnly = true;
            this.MarkSubject.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MarkSubject.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MarkMark
            // 
            this.MarkMark.HeaderText = "Оценка";
            this.MarkMark.Name = "MarkMark";
            this.MarkMark.ReadOnly = true;
            // 
            // delMark
            // 
            this.delMark.Location = new System.Drawing.Point(15, 610);
            this.delMark.Name = "delMark";
            this.delMark.Size = new System.Drawing.Size(121, 23);
            this.delMark.TabIndex = 6;
            this.delMark.Text = "Удалить Оценку";
            this.delMark.UseVisualStyleBackColor = true;
            this.delMark.Click += new System.EventHandler(this.delPerson_Click);
            // 
            // editMark
            // 
            this.editMark.Location = new System.Drawing.Point(142, 610);
            this.editMark.Name = "editMark";
            this.editMark.Size = new System.Drawing.Size(121, 23);
            this.editMark.TabIndex = 7;
            this.editMark.Text = "Редактировать оценку";
            this.editMark.UseVisualStyleBackColor = true;
            this.editMark.Click += new System.EventHandler(this.editMark_Click);
            // 
            // addMark
            // 
            this.addMark.Location = new System.Drawing.Point(269, 610);
            this.addMark.Name = "addMark";
            this.addMark.Size = new System.Drawing.Size(121, 23);
            this.addMark.TabIndex = 8;
            this.addMark.Text = "Добавить оценку";
            this.addMark.UseVisualStyleBackColor = true;
            this.addMark.Click += new System.EventHandler(this.addMark_Click);
            // 
            // TabelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 662);
            this.Controls.Add(this.addMark);
            this.Controls.Add(this.editMark);
            this.Controls.Add(this.delMark);
            this.Controls.Add(this.markGrid);
            this.Controls.Add(this.delSubject);
            this.Controls.Add(this.subjectGrid);
            this.Controls.Add(this.delPerson);
            this.Controls.Add(this.personGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TabelForm";
            this.Text = "Успеваимость";
            ((System.ComponentModel.ISupportInitialize)(this.personGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subjectGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.markGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView personGrid;
        private System.Windows.Forms.Button delPerson;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewComboBoxColumn Group;
        private System.Windows.Forms.DataGridViewComboBoxColumn Cours;
        private System.Windows.Forms.DataGridView subjectGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectTeacher;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectHour;
        private System.Windows.Forms.Button delSubject;
        private System.Windows.Forms.DataGridView markGrid;
        private System.Windows.Forms.Button delMark;
        private System.Windows.Forms.Button editMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarkId;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarkPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarkSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarkMark;
        private System.Windows.Forms.Button addMark;
        private System.Windows.Forms.ToolStripMenuItem updateMenu;
    }
}

