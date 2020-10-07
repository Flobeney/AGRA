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
            this.gbFirstImg = new System.Windows.Forms.GroupBox();
            this.pibFirstImg = new System.Windows.Forms.PictureBox();
            this.gbImgRes = new System.Windows.Forms.GroupBox();
            this.pibImgRes = new System.Windows.Forms.PictureBox();
            this.btnChooseImg = new System.Windows.Forms.Button();
            this.ofdImg = new System.Windows.Forms.OpenFileDialog();
            this.lsbTimeTreatments = new System.Windows.Forms.ListBox();
            this.btnClearImg = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.sfdImg = new System.Windows.Forms.SaveFileDialog();
            this.gbSecondImg = new System.Windows.Forms.GroupBox();
            this.pibSecondImg = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.gbFirstImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibFirstImg)).BeginInit();
            this.gbImgRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibImgRes)).BeginInit();
            this.gbSecondImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibSecondImg)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFirstImg
            // 
            this.gbFirstImg.Controls.Add(this.pibFirstImg);
            this.gbFirstImg.Location = new System.Drawing.Point(12, 12);
            this.gbFirstImg.Name = "gbFirstImg";
            this.gbFirstImg.Size = new System.Drawing.Size(400, 350);
            this.gbFirstImg.TabIndex = 0;
            this.gbFirstImg.TabStop = false;
            this.gbFirstImg.Text = "Première image";
            // 
            // pibFirstImg
            // 
            this.pibFirstImg.Location = new System.Drawing.Point(6, 19);
            this.pibFirstImg.Name = "pibFirstImg";
            this.pibFirstImg.Size = new System.Drawing.Size(388, 325);
            this.pibFirstImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibFirstImg.TabIndex = 0;
            this.pibFirstImg.TabStop = false;
            // 
            // gbImgRes
            // 
            this.gbImgRes.Controls.Add(this.pibImgRes);
            this.gbImgRes.Location = new System.Drawing.Point(428, 368);
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
            this.ofdImg.Multiselect = true;
            this.ofdImg.Title = "Choisir une image";
            // 
            // lsbTimeTreatments
            // 
            this.lsbTimeTreatments.FormattingEnabled = true;
            this.lsbTimeTreatments.Location = new System.Drawing.Point(118, 368);
            this.lsbTimeTreatments.Name = "lsbTimeTreatments";
            this.lsbTimeTreatments.Size = new System.Drawing.Size(294, 342);
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
            this.btnSave.Location = new System.Drawing.Point(12, 680);
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
            // gbSecondImg
            // 
            this.gbSecondImg.Controls.Add(this.pibSecondImg);
            this.gbSecondImg.Location = new System.Drawing.Point(428, 12);
            this.gbSecondImg.Name = "gbSecondImg";
            this.gbSecondImg.Size = new System.Drawing.Size(400, 350);
            this.gbSecondImg.TabIndex = 1;
            this.gbSecondImg.TabStop = false;
            this.gbSecondImg.Text = "Seconde image";
            // 
            // pibSecondImg
            // 
            this.pibSecondImg.Location = new System.Drawing.Point(6, 19);
            this.pibSecondImg.Name = "pibSecondImg";
            this.pibSecondImg.Size = new System.Drawing.Size(388, 325);
            this.pibSecondImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibSecondImg.TabIndex = 0;
            this.pibSecondImg.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(12, 462);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Additionner";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSub
            // 
            this.btnSub.Enabled = false;
            this.btnSub.Location = new System.Drawing.Point(12, 509);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(100, 30);
            this.btnSub.TabIndex = 9;
            this.btnSub.Text = "Soustraction";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 732);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gbSecondImg);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClearImg);
            this.Controls.Add(this.lsbTimeTreatments);
            this.Controls.Add(this.btnChooseImg);
            this.Controls.Add(this.gbImgRes);
            this.Controls.Add(this.gbFirstImg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface image";
            this.gbFirstImg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pibFirstImg)).EndInit();
            this.gbImgRes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pibImgRes)).EndInit();
            this.gbSecondImg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pibSecondImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFirstImg;
        private System.Windows.Forms.GroupBox gbImgRes;
        private System.Windows.Forms.Button btnChooseImg;
        private System.Windows.Forms.PictureBox pibFirstImg;
        private System.Windows.Forms.PictureBox pibImgRes;
        private System.Windows.Forms.OpenFileDialog ofdImg;
        private System.Windows.Forms.ListBox lsbTimeTreatments;
        private System.Windows.Forms.Button btnClearImg;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog sfdImg;
        private System.Windows.Forms.GroupBox gbSecondImg;
        private System.Windows.Forms.PictureBox pibSecondImg;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSub;
    }
}

