namespace TheNornProject
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.display = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(608, 608);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            this.display.Paint += new System.Windows.Forms.PaintEventHandler(this.display_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(608, 608);
            this.Controls.Add(this.display);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "The Norn Project";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox display;
    }
}

