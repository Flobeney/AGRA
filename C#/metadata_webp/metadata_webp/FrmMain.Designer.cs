namespace metadata_webp {
    partial class FrmMain {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent() {
            this.ms = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.gbMetadata = new System.Windows.Forms.GroupBox();
            this.lsbMetadata = new System.Windows.Forms.ListBox();
            this.pibImg = new System.Windows.Forms.PictureBox();
            this.ms.SuspendLayout();
            this.gbMetadata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibImg)).BeginInit();
            this.SuspendLayout();
            // 
            // ms
            // 
            this.ms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem});
            this.ms.Location = new System.Drawing.Point(0, 0);
            this.ms.Name = "ms";
            this.ms.Size = new System.Drawing.Size(800, 24);
            this.ms.TabIndex = 0;
            this.ms.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ouvrirToolStripMenuItem.Text = "Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
            // 
            // ofdFile
            // 
            this.ofdFile.FileName = "img.webp";
            this.ofdFile.Filter = "Image Files (*.BMP;*.JPG;*.PNG;*.WEBP)|*.BMP;*.JPG;*.PNG;*.WEBP";
            // 
            // gbMetadata
            // 
            this.gbMetadata.Controls.Add(this.lsbMetadata);
            this.gbMetadata.Location = new System.Drawing.Point(12, 27);
            this.gbMetadata.Name = "gbMetadata";
            this.gbMetadata.Size = new System.Drawing.Size(325, 411);
            this.gbMetadata.TabIndex = 1;
            this.gbMetadata.TabStop = false;
            this.gbMetadata.Text = "Métadonnées";
            // 
            // lsbMetadata
            // 
            this.lsbMetadata.FormattingEnabled = true;
            this.lsbMetadata.HorizontalScrollbar = true;
            this.lsbMetadata.Location = new System.Drawing.Point(6, 19);
            this.lsbMetadata.Name = "lsbMetadata";
            this.lsbMetadata.Size = new System.Drawing.Size(313, 381);
            this.lsbMetadata.TabIndex = 0;
            // 
            // pibImg
            // 
            this.pibImg.Location = new System.Drawing.Point(343, 46);
            this.pibImg.Name = "pibImg";
            this.pibImg.Size = new System.Drawing.Size(445, 381);
            this.pibImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibImg.TabIndex = 2;
            this.pibImg.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pibImg);
            this.Controls.Add(this.gbMetadata);
            this.Controls.Add(this.ms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.ms;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Métadonnées et WebP";
            this.ms.ResumeLayout(false);
            this.ms.PerformLayout();
            this.gbMetadata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pibImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ms;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdFile;
        private System.Windows.Forms.GroupBox gbMetadata;
        private System.Windows.Forms.ListBox lsbMetadata;
        private System.Windows.Forms.PictureBox pibImg;
    }
}

