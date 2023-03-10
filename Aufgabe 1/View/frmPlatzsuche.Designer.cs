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
            this.gridPlatzsuche = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.nudTickets = new System.Windows.Forms.NumericUpDown();
            this.lblTickets = new System.Windows.Forms.Label();
            this.cb = new System.Windows.Forms.CheckBox();
            this.btnReservieren = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlatzsuche)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPlatzsuche
            // 
            this.gridPlatzsuche.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.gridPlatzsuche.Location = new System.Drawing.Point(15, 115);
            this.gridPlatzsuche.Name = "gridPlatzsuche";
            this.gridPlatzsuche.Size = new System.Drawing.Size(728, 345);
            this.gridPlatzsuche.TabIndex = 0;
            // 
            // nudTickets
            // 
            this.nudTickets.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTickets.Location = new System.Drawing.Point(168, 33);
            this.nudTickets.Name = "nudTickets";
            this.nudTickets.Size = new System.Drawing.Size(87, 27);
            this.nudTickets.TabIndex = 1;
            // 
            // lblTickets
            // 
            this.lblTickets.AutoSize = true;
            this.lblTickets.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTickets.Location = new System.Drawing.Point(15, 37);
            this.lblTickets.Name = "lblTickets";
            this.lblTickets.Size = new System.Drawing.Size(96, 16);
            this.lblTickets.TabIndex = 2;
            this.lblTickets.Text = "Ticketanzahl:";
            // 
            // cb
            // 
            this.cb.AutoSize = true;
            this.cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb.Location = new System.Drawing.Point(21, 78);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(234, 22);
            this.cb.TabIndex = 3;
            this.cb.Tag = "";
            this.cb.Text = "zusammenhängende Sitzplätze:";
            this.cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb.UseVisualStyleBackColor = true;
            // 
            // btnReservieren
            // 
            this.btnReservieren.BackColor = System.Drawing.Color.White;
            this.btnReservieren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservieren.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservieren.Location = new System.Drawing.Point(43, 480);
            this.btnReservieren.Name = "btnReservieren";
            this.btnReservieren.Size = new System.Drawing.Size(240, 53);
            this.btnReservieren.TabIndex = 4;
            this.btnReservieren.Text = "Reservieren";
            this.btnReservieren.UseVisualStyleBackColor = false;
            // 
            // frmPlatzsuche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 684);
            this.Controls.Add(this.btnReservieren);
            this.Controls.Add(this.cb);
            this.Controls.Add(this.lblTickets);
            this.Controls.Add(this.nudTickets);
            this.Controls.Add(this.gridPlatzsuche);
            this.Name = "frmPlatzsuche";
            this.Text = "frmPlatzsuche";
            ((System.ComponentModel.ISupportInitialize)(this.gridPlatzsuche)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid gridPlatzsuche;
        private System.Windows.Forms.NumericUpDown nudTickets;
        private System.Windows.Forms.Label lblTickets;
        private System.Windows.Forms.CheckBox cb;
        private System.Windows.Forms.Button btnReservieren;
    }
}