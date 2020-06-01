namespace WinFormsClient
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewEmp = new System.Windows.Forms.DataGridView();
            this.AddDept = new System.Windows.Forms.Button();
            this.AddEmp = new System.Windows.Forms.Button();
            this.listBoxDept = new System.Windows.Forms.ListBox();
            this.contextMenuDept = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmp)).BeginInit();
            this.contextMenuDept.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewEmp
            // 
            this.dataGridViewEmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewEmp.Location = new System.Drawing.Point(192, 12);
            this.dataGridViewEmp.Name = "dataGridViewEmp";
            this.dataGridViewEmp.ReadOnly = true;
            this.dataGridViewEmp.Size = new System.Drawing.Size(596, 369);
            this.dataGridViewEmp.TabIndex = 1;
            this.dataGridViewEmp.DoubleClick += new System.EventHandler(this.dataGridViewEmp_DoubleClick);
            // 
            // AddDept
            // 
            this.AddDept.Location = new System.Drawing.Point(12, 398);
            this.AddDept.Name = "AddDept";
            this.AddDept.Size = new System.Drawing.Size(162, 25);
            this.AddDept.TabIndex = 2;
            this.AddDept.Text = "Добавить";
            this.AddDept.UseVisualStyleBackColor = true;
            this.AddDept.Click += new System.EventHandler(this.AddDept_Click);
            // 
            // AddEmp
            // 
            this.AddEmp.Location = new System.Drawing.Point(626, 398);
            this.AddEmp.Name = "AddEmp";
            this.AddEmp.Size = new System.Drawing.Size(162, 25);
            this.AddEmp.TabIndex = 2;
            this.AddEmp.Text = "Добавить";
            this.AddEmp.UseVisualStyleBackColor = true;
            this.AddEmp.Click += new System.EventHandler(this.AddEmp_Click);
            // 
            // listBoxDept
            // 
            this.listBoxDept.FormattingEnabled = true;
            this.listBoxDept.Location = new System.Drawing.Point(12, 12);
            this.listBoxDept.Name = "listBoxDept";
            this.listBoxDept.Size = new System.Drawing.Size(162, 368);
            this.listBoxDept.TabIndex = 3;
            // 
            // contextMenuDept
            // 
            this.contextMenuDept.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolStripMenuItem});
            this.contextMenuDept.Name = "contextMenuDept";
            this.contextMenuDept.Size = new System.Drawing.Size(178, 26);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.EditToolStripMenuItem.Text = "Изменить/Удалить";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBoxDept);
            this.Controls.Add(this.AddEmp);
            this.Controls.Add(this.AddDept);
            this.Controls.Add(this.dataGridViewEmp);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmp)).EndInit();
            this.contextMenuDept.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewEmp;
        private System.Windows.Forms.Button AddDept;
        private System.Windows.Forms.Button AddEmp;
        private System.Windows.Forms.ListBox listBoxDept;
        private System.Windows.Forms.ContextMenuStrip contextMenuDept;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
    }
}

