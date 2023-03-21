namespace Aufgabe_1.View
{
    partial class frmPlatzsuche
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
            this.nudTickets = new System.Windows.Forms.NumericUpDown();
            this.lblTickets = new System.Windows.Forms.Label();
            this.cbZusammen = new System.Windows.Forms.CheckBox();
            this.btnReservieren = new System.Windows.Forms.Button();
            this.btnAnzeigen = new System.Windows.Forms.Button();
            this.btnAbbrechen = new System.Windows.Forms.Button();
            this.gridPlatzsuche2 = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlatzsuche2)).BeginInit();
            this.SuspendLayout();
            // 
            // nudTickets
            // 
            this.nudTickets.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTickets.Location = new System.Drawing.Point(137, 17);
            this.nudTickets.Name = "nudTickets";
            this.nudTickets.Size = new System.Drawing.Size(87, 26);
            this.nudTickets.TabIndex = 1;
            // 
            // lblTickets
            // 
            this.lblTickets.AutoSize = true;
            this.lblTickets.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTickets.Location = new System.Drawing.Point(9, 19);
            this.lblTickets.Name = "lblTickets";
            this.lblTickets.Size = new System.Drawing.Size(107, 18);
            this.lblTickets.TabIndex = 2;
            this.lblTickets.Text = "Ticketanzahl:";
            // 
            // cbZusammen
            // 
            this.cbZusammen.AutoSize = true;
            this.cbZusammen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbZusammen.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbZusammen.Location = new System.Drawing.Point(18, 57);
            this.cbZusammen.Name = "cbZusammen";
            this.cbZusammen.Size = new System.Drawing.Size(264, 22);
            this.cbZusammen.TabIndex = 3;
            this.cbZusammen.Tag = "";
            this.cbZusammen.Text = "zusammenhängende Sitzplätze:";
            this.cbZusammen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbZusammen.UseVisualStyleBackColor = true;
            // 
            // btnReservieren
            // 
            this.btnReservieren.BackColor = System.Drawing.Color.White;
            this.btnReservieren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservieren.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservieren.Location = new System.Drawing.Point(323, 466);
            this.btnReservieren.Name = "btnReservieren";
            this.btnReservieren.Size = new System.Drawing.Size(175, 53);
            this.btnReservieren.TabIndex = 4;
            this.btnReservieren.Text = "Reservieren";
            this.btnReservieren.UseVisualStyleBackColor = false;
            this.btnReservieren.Click += new System.EventHandler(this.btnReservieren_Click);
            // 
            // btnAnzeigen
            // 
            this.btnAnzeigen.BackColor = System.Drawing.Color.White;
            this.btnAnzeigen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnzeigen.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnzeigen.Location = new System.Drawing.Point(323, 15);
            this.btnAnzeigen.Name = "btnAnzeigen";
            this.btnAnzeigen.Size = new System.Drawing.Size(175, 53);
            this.btnAnzeigen.TabIndex = 5;
            this.btnAnzeigen.Text = "Plätze anzeigen";
            this.btnAnzeigen.UseVisualStyleBackColor = false;
            this.btnAnzeigen.Click += new System.EventHandler(this.btnAnzeigen_Click);
            // 
            // btnAbbrechen
            // 
            this.btnAbbrechen.BackColor = System.Drawing.Color.White;
            this.btnAbbrechen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbbrechen.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbbrechen.Location = new System.Drawing.Point(12, 466);
            this.btnAbbrechen.Name = "btnAbbrechen";
            this.btnAbbrechen.Size = new System.Drawing.Size(175, 53);
            this.btnAbbrechen.TabIndex = 6;
            this.btnAbbrechen.Text = "Abbrechen";
            this.btnAbbrechen.UseVisualStyleBackColor = false;
            this.btnAbbrechen.Click += new System.EventHandler(this.btnAbbrechen_Click);
            // 
            // gridPlatzsuche2
            // 
            this.gridPlatzsuche2.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gridPlatzsuche2.AllowEditing = false;
            this.gridPlatzsuche2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.gridPlatzsuche2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.gridPlatzsuche2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.gridPlatzsuche2.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.gridPlatzsuche2.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.gridPlatzsuche2.Location = new System.Drawing.Point(12, 98);
            this.gridPlatzsuche2.Name = "gridPlatzsuche2";
            this.gridPlatzsuche2.Size = new System.Drawing.Size(486, 346);
            this.gridPlatzsuche2.TabIndex = 7;
            this.gridPlatzsuche2.Click += new System.EventHandler(this.gridPlatzsuche2_Click);
            // 
            // frmPlatzsuche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 536);
            this.Controls.Add(this.gridPlatzsuche2);
            this.Controls.Add(this.btnAbbrechen);
            this.Controls.Add(this.btnAnzeigen);
            this.Controls.Add(this.btnReservieren);
            this.Controls.Add(this.cbZusammen);
            this.Controls.Add(this.lblTickets);
            this.Controls.Add(this.nudTickets);
            this.Name = "frmPlatzsuche";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPlatzsuche";
            this.Load += new System.EventHandler(this.frmPlatzsuche_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTickets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlatzsuche2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudTickets;
        private System.Windows.Forms.Label lblTickets;
        private System.Windows.Forms.CheckBox cbZusammen;
        private System.Windows.Forms.Button btnReservieren;
        private System.Windows.Forms.Button btnAnzeigen;
        private System.Windows.Forms.Button btnAbbrechen;
        private C1.Win.C1FlexGrid.C1FlexGrid gridPlatzsuche2;
    }
}