namespace Aufgabe_1
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSaalAdd = new System.Windows.Forms.Button();
            this.btnSaalEdit = new System.Windows.Forms.Button();
            this.btnSaalDelete = new System.Windows.Forms.Button();
            this.gridSaal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.gridVeranstaltungen = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnVeranstaltungAdd = new System.Windows.Forms.Button();
            this.btnVeranstaltungEdit = new System.Windows.Forms.Button();
            this.btnVeranstaltundDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSaal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVeranstaltungen)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FlexGrid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.c1FlexGrid1.Location = new System.Drawing.Point(12, 281);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 11;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1FlexGrid1.Size = new System.Drawing.Size(575, 250);
            this.c1FlexGrid1.TabIndex = 0;
            this.c1FlexGrid1.TabStop = false;
            this.c1FlexGrid1.Click += new System.EventHandler(this.c1FlexGrid1_Click);
            // 
            // btnSaalAdd
            // 
            this.btnSaalAdd.BackColor = System.Drawing.Color.White;
            this.btnSaalAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaalAdd.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaalAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaalAdd.Location = new System.Drawing.Point(12, 12);
            this.btnSaalAdd.Name = "btnSaalAdd";
            this.btnSaalAdd.Size = new System.Drawing.Size(175, 72);
            this.btnSaalAdd.TabIndex = 1;
            this.btnSaalAdd.Text = "Saal hinzufügen";
            this.btnSaalAdd.UseVisualStyleBackColor = false;
            this.btnSaalAdd.Click += new System.EventHandler(this.btnSaalAdd_Click);
            // 
            // btnSaalEdit
            // 
            this.btnSaalEdit.BackColor = System.Drawing.Color.White;
            this.btnSaalEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaalEdit.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaalEdit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaalEdit.Location = new System.Drawing.Point(12, 102);
            this.btnSaalEdit.Name = "btnSaalEdit";
            this.btnSaalEdit.Size = new System.Drawing.Size(175, 72);
            this.btnSaalEdit.TabIndex = 2;
            this.btnSaalEdit.Text = "Saal bearbeiten";
            this.btnSaalEdit.UseVisualStyleBackColor = false;
            this.btnSaalEdit.Click += new System.EventHandler(this.btnSaalEdit_Click);
            // 
            // btnSaalDelete
            // 
            this.btnSaalDelete.BackColor = System.Drawing.Color.White;
            this.btnSaalDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaalDelete.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaalDelete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaalDelete.Location = new System.Drawing.Point(12, 190);
            this.btnSaalDelete.Name = "btnSaalDelete";
            this.btnSaalDelete.Size = new System.Drawing.Size(175, 72);
            this.btnSaalDelete.TabIndex = 3;
            this.btnSaalDelete.Text = "Saal löschen";
            this.btnSaalDelete.UseVisualStyleBackColor = false;
            this.btnSaalDelete.Click += new System.EventHandler(this.btnSaalDelete_Click);
            // 
            // gridSaal
            // 
            this.gridSaal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gridSaal.AllowEditing = false;
            this.gridSaal.ColumnInfo = "3,1,0,0,0,-1,Columns:1{Width:39;Visible:False;Style:\"Font:Microsoft Sans Serif, 9" +
    ".75pt;\";}\t";
            this.gridSaal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridSaal.Location = new System.Drawing.Point(203, 12);
            this.gridSaal.Name = "gridSaal";
            this.gridSaal.Size = new System.Drawing.Size(139, 250);
            this.gridSaal.StyleInfo = resources.GetString("gridSaal.StyleInfo");
            this.gridSaal.TabIndex = 8;
            this.gridSaal.TabStop = false;
            this.gridSaal.Click += new System.EventHandler(this.gridSaal_Click);
            // 
            // gridVeranstaltungen
            // 
            this.gridVeranstaltungen.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gridVeranstaltungen.AllowEditing = false;
            this.gridVeranstaltungen.ColumnInfo = "3,1,0,0,0,-1,Columns:1{Width:39;Visible:False;Style:\"Font:Microsoft Sans Serif, 9" +
    ".75pt;\";}\t";
            this.gridVeranstaltungen.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridVeranstaltungen.Location = new System.Drawing.Point(348, 12);
            this.gridVeranstaltungen.Name = "gridVeranstaltungen";
            this.gridVeranstaltungen.Size = new System.Drawing.Size(433, 250);
            this.gridVeranstaltungen.StyleInfo = resources.GetString("gridVeranstaltungen.StyleInfo");
            this.gridVeranstaltungen.TabIndex = 8;
            this.gridVeranstaltungen.TabStop = false;
            this.gridVeranstaltungen.Click += new System.EventHandler(this.gridVeranstaltungen_Click);
            // 
            // btnVeranstaltungAdd
            // 
            this.btnVeranstaltungAdd.BackColor = System.Drawing.Color.White;
            this.btnVeranstaltungAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnVeranstaltungAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVeranstaltungAdd.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVeranstaltungAdd.Location = new System.Drawing.Point(606, 281);
            this.btnVeranstaltungAdd.Name = "btnVeranstaltungAdd";
            this.btnVeranstaltungAdd.Size = new System.Drawing.Size(175, 72);
            this.btnVeranstaltungAdd.TabIndex = 4;
            this.btnVeranstaltungAdd.Text = "Veranstaltung planen";
            this.btnVeranstaltungAdd.UseVisualStyleBackColor = false;
            this.btnVeranstaltungAdd.Click += new System.EventHandler(this.btnVeranstaltungAdd_Click);
            // 
            // btnVeranstaltungEdit
            // 
            this.btnVeranstaltungEdit.BackColor = System.Drawing.Color.White;
            this.btnVeranstaltungEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnVeranstaltungEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVeranstaltungEdit.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVeranstaltungEdit.Location = new System.Drawing.Point(606, 369);
            this.btnVeranstaltungEdit.Name = "btnVeranstaltungEdit";
            this.btnVeranstaltungEdit.Size = new System.Drawing.Size(175, 72);
            this.btnVeranstaltungEdit.TabIndex = 5;
            this.btnVeranstaltungEdit.Text = "Veranstaltung bearbeiten";
            this.btnVeranstaltungEdit.UseVisualStyleBackColor = false;
            this.btnVeranstaltungEdit.Click += new System.EventHandler(this.btnVeranstaltungEdit_Click);
            // 
            // btnVeranstaltundDelete
            // 
            this.btnVeranstaltundDelete.BackColor = System.Drawing.Color.White;
            this.btnVeranstaltundDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnVeranstaltundDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVeranstaltundDelete.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVeranstaltundDelete.Location = new System.Drawing.Point(606, 459);
            this.btnVeranstaltundDelete.Name = "btnVeranstaltundDelete";
            this.btnVeranstaltundDelete.Size = new System.Drawing.Size(175, 72);
            this.btnVeranstaltundDelete.TabIndex = 6;
            this.btnVeranstaltundDelete.Text = "Veranstaltung löschen";
            this.btnVeranstaltundDelete.UseVisualStyleBackColor = false;
            this.btnVeranstaltundDelete.Click += new System.EventHandler(this.btnVeranstaltundDelete_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 543);
            this.Controls.Add(this.btnVeranstaltundDelete);
            this.Controls.Add(this.btnVeranstaltungEdit);
            this.Controls.Add(this.btnVeranstaltungAdd);
            this.Controls.Add(this.gridVeranstaltungen);
            this.Controls.Add(this.gridSaal);
            this.Controls.Add(this.btnSaalDelete);
            this.Controls.Add(this.btnSaalEdit);
            this.Controls.Add(this.btnSaalAdd);
            this.Controls.Add(this.c1FlexGrid1);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sitzplan";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSaal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVeranstaltungen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Button btnSaalAdd;
        private System.Windows.Forms.Button btnSaalEdit;
        private System.Windows.Forms.Button btnSaalDelete;
        public C1.Win.C1FlexGrid.C1FlexGrid gridSaal;
        public C1.Win.C1FlexGrid.C1FlexGrid gridVeranstaltungen;
        private System.Windows.Forms.Button btnVeranstaltungAdd;
        private System.Windows.Forms.Button btnVeranstaltungEdit;
        private System.Windows.Forms.Button btnVeranstaltundDelete;
    }
}

