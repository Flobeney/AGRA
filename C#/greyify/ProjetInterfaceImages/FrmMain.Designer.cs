namespace ProjetInterfaceImages {
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
            this.gbImgOriginal = new System.Windows.Forms.GroupBox();
            this.pibImgOriginal = new System.Windows.Forms.PictureBox();
            this.gbImgRes = new System.Windows.Forms.GroupBox();
            this.pibImgRes = new System.Windows.Forms.PictureBox();
            this.btnChooseImg = new System.Windows.Forms.Button();
            this.ofdImg = new System.Windows.Forms.OpenFileDialog();
            this.cmbTreatments = new System.Windows.Forms.ComboBox();
            this.lblChooseTreatment = new System.Windows.Forms.Label();
            this.lsbTimeTreatments = new System.Windows.Forms.ListBox();
            this.btnClearImg = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.sfdImg = new System.Windows.Forms.SaveFileDialog();
            this.gbImgOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibImgOriginal)).BeginInit();
            this.gbImgRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibImgRes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbImgOriginal
            // 
            this.gbImgOriginal.Controls.Add(this.pibImgOriginal);
            this.gbImgOriginal.Location = new System.Drawing.Point(12, 12);
            this.gbImgOriginal.Name = "gbImgOriginal";
            this.gbImgOriginal.Size = new System.Drawing.Size(400, 350);
            this.gbImgOriginal.TabIndex = 0;
            this.gbImgOriginal.TabStop = false;
            this.gbImgOriginal.Text = "Image original";
            // 
            // pibImgOriginal
            // 
            this.pibImgOriginal.Location = new System.Drawing.Point(6, 19);
            this.pibImgOriginal.Name = "pibImgOriginal";
            this.pibImgOriginal.Size = new System.Drawing.Size(388, 325);
            this.pibImgOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibImgOriginal.TabIndex = 0;
            this.pibImgOriginal.TabStop = false;
            // 
            // gbImgRes
            // 
            this.gbImgRes.Controls.Add(this.pibImgRes);
            this.gbImgRes.Location = new System.Drawing.Point(416, 12);
            this.gbImgRes.Name = "gbImgRes";
            this.gbImgRes.Size = new System.Drawing.Size(400, 350);
            this.gbImgRes.TabIndex = 1;
            this.gbImgRes.TabStop = false;
            this.gbImgRes.Text = "Résultat";
            // 
            // pibImgRes
            // 
            this.pibImgRes.Location = new System.Drawing.Point(6, 19);
            this.pibImgRes.Name = "pibImgRes";
            this.pibImgRes.Size = new System.Drawing.Size(388, 325);
            this.pibImgRes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibImgRes.TabIndex = 1;
            this.pibImgRes.TabStop = false;
            // 
            // btnChooseImg
            // 
            this.btnChooseImg.Location = new System.Drawing.Point(12, 368);
            this.btnChooseImg.Name = "btnChooseImg";
            this.btnChooseImg.Size = new System.Drawing.Size(100, 30);
            this.btnChooseImg.TabIndex = 2;
            this.btnChooseImg.Text = "Choisir une image";
            this.btnChooseImg.UseVisualStyleBackColor = true;
            this.btnChooseImg.Click += new System.EventHandler(this.btnChooseImg_Click);
            // 
            // ofdImg
            // 
            this.ofdImg.FileName = "img";
            this.ofdImg.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            this.ofdImg.Title = "Choisir une image";
            // 
            // cmbTreatments
            // 
            this.cmbTreatments.Enabled = false;
            this.cmbTreatments.FormattingEnabled = true;
            this.cmbTreatments.Location = new System.Drawing.Point(555, 374);
            this.cmbTreatments.Name = "cmbTreatments";
            this.cmbTreatments.Size = new System.Drawing.Size(261, 21);
            this.cmbTreatments.TabIndex = 3;
            this.cmbTreatments.SelectedIndexChanged += new System.EventHandler(this.cmbTreatments_SelectedIndexChanged);
            // 
            // lblChooseTreatment
            // 
            this.lblChooseTreatment.AutoSize = true;
            this.lblChooseTreatment.Location = new System.Drawing.Point(418, 377);
            this.lblChooseTreatment.Name = "lblChooseTreatment";
            this.lblChooseTreatment.Size = new System.Drawing.Size(136, 13);
            this.lblChooseTreatment.TabIndex = 4;
            this.lblChooseTreatment.Text = "Choisir le traitement voulu : ";
            // 
            // lsbTimeTreatments
            // 
            this.lsbTimeTreatments.FormattingEnabled = true;
            this.lsbTimeTreatments.Location = new System.Drawing.Point(118, 368);
            this.lsbTimeTreatments.Name = "lsbTimeTreatments";
            this.lsbTimeTreatments.Size = new System.Drawing.Size(294, 82);
            this.lsbTimeTreatments.TabIndex = 5;
            // 
            // btnClearImg
            // 
            this.btnClearImg.Enabled = false;
            this.btnClearImg.Location = new System.Drawing.Point(12, 415);
            this.btnClearImg.Name = "btnClearImg";
            this.btnClearImg.Size = new System.Drawing.Size(100, 30);
            this.btnClearImg.TabIndex = 6;
            this.btnClearImg.Text = "Nettoyer résultat";
            this.btnClearImg.UseVisualStyleBackColor = true;
            this.btnClearImg.Click += new System.EventHandler(this.btnClearImg_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(422, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Sauvegarder";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // sfdImg
            // 
            this.sfdImg.FileName = "image";
            this.sfdImg.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 457);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClearImg);
            this.Controls.Add(this.lsbTimeTreatments);
            this.Controls.Add(this.lblChooseTreatment);
            this.Controls.Add(this.cmbTreatments);
            this.Controls.Add(this.btnChooseImg);
            this.Controls.Add(this.gbImgRes);
            this.Controls.Add(this.gbImgOriginal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface image";
            this.gbImgOriginal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pibImgOriginal)).EndInit();
            this.gbImgRes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pibImgRes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbImgOriginal;
        private System.Windows.Forms.GroupBox gbImgRes;
        private System.Windows.Forms.Button btnChooseImg;
        private System.Windows.Forms.PictureBox pibImgOriginal;
        private System.Windows.Forms.PictureBox pibImgRes;
        private System.Windows.Forms.OpenFileDialog ofdImg;
        private System.Windows.Forms.ComboBox cmbTreatments;
        private System.Windows.Forms.Label lblChooseTreatment;
        private System.Windows.Forms.ListBox lsbTimeTreatments;
        private System.Windows.Forms.Button btnClearImg;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog sfdImg;
    }
}

