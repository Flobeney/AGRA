namespace transformMitch {
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
            this.ofdImg = new System.Windows.Forms.OpenFileDialog();
            this.btnChooseImg = new System.Windows.Forms.Button();
            this.btnTransform = new System.Windows.Forms.Button();
            this.sfdImg = new System.Windows.Forms.SaveFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbImgOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibImgOriginal)).BeginInit();
            this.gbImgRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibImgRes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbImgOriginal
            // 
            this.gbImgOriginal.Controls.Add(this.pibImgOriginal);
            this.gbImgOriginal.Location = new System.Drawing.Point(13, 13);
            this.gbImgOriginal.Margin = new System.Windows.Forms.Padding(4);
            this.gbImgOriginal.Name = "gbImgOriginal";
            this.gbImgOriginal.Padding = new System.Windows.Forms.Padding(4);
            this.gbImgOriginal.Size = new System.Drawing.Size(533, 431);
            this.gbImgOriginal.TabIndex = 1;
            this.gbImgOriginal.TabStop = false;
            this.gbImgOriginal.Text = "Image original";
            // 
            // pibImgOriginal
            // 
            this.pibImgOriginal.Location = new System.Drawing.Point(8, 23);
            this.pibImgOriginal.Margin = new System.Windows.Forms.Padding(4);
            this.pibImgOriginal.Name = "pibImgOriginal";
            this.pibImgOriginal.Size = new System.Drawing.Size(517, 400);
            this.pibImgOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibImgOriginal.TabIndex = 0;
            this.pibImgOriginal.TabStop = false;
            // 
            // gbImgRes
            // 
            this.gbImgRes.Controls.Add(this.pibImgRes);
            this.gbImgRes.Location = new System.Drawing.Point(554, 13);
            this.gbImgRes.Margin = new System.Windows.Forms.Padding(4);
            this.gbImgRes.Name = "gbImgRes";
            this.gbImgRes.Padding = new System.Windows.Forms.Padding(4);
            this.gbImgRes.Size = new System.Drawing.Size(533, 431);
            this.gbImgRes.TabIndex = 2;
            this.gbImgRes.TabStop = false;
            this.gbImgRes.Text = "Résultat";
            // 
            // pibImgRes
            // 
            this.pibImgRes.Location = new System.Drawing.Point(8, 23);
            this.pibImgRes.Margin = new System.Windows.Forms.Padding(4);
            this.pibImgRes.Name = "pibImgRes";
            this.pibImgRes.Size = new System.Drawing.Size(517, 400);
            this.pibImgRes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibImgRes.TabIndex = 0;
            this.pibImgRes.TabStop = false;
            // 
            // ofdImg
            // 
            this.ofdImg.FileName = "img";
            this.ofdImg.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            this.ofdImg.Title = "Choisir une image";
            // 
            // btnChooseImg
            // 
            this.btnChooseImg.Location = new System.Drawing.Point(13, 452);
            this.btnChooseImg.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseImg.Name = "btnChooseImg";
            this.btnChooseImg.Size = new System.Drawing.Size(133, 37);
            this.btnChooseImg.TabIndex = 3;
            this.btnChooseImg.Text = "Choisir une image";
            this.btnChooseImg.UseVisualStyleBackColor = true;
            this.btnChooseImg.Click += new System.EventHandler(this.btnChooseImg_Click);
            // 
            // btnTransform
            // 
            this.btnTransform.Location = new System.Drawing.Point(405, 452);
            this.btnTransform.Margin = new System.Windows.Forms.Padding(4);
            this.btnTransform.Name = "btnTransform";
            this.btnTransform.Size = new System.Drawing.Size(133, 37);
            this.btnTransform.TabIndex = 4;
            this.btnTransform.Text = "Transformation";
            this.btnTransform.UseVisualStyleBackColor = true;
            this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(554, 452);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 37);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Sauver";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 499);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnTransform);
            this.Controls.Add(this.btnChooseImg);
            this.Controls.Add(this.gbImgRes);
            this.Controls.Add(this.gbImgOriginal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transformation";
            this.gbImgOriginal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pibImgOriginal)).EndInit();
            this.gbImgRes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pibImgRes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbImgOriginal;
        private System.Windows.Forms.PictureBox pibImgOriginal;
        private System.Windows.Forms.GroupBox gbImgRes;
        private System.Windows.Forms.PictureBox pibImgRes;
        private System.Windows.Forms.OpenFileDialog ofdImg;
        private System.Windows.Forms.Button btnChooseImg;
        private System.Windows.Forms.Button btnTransform;
        private System.Windows.Forms.SaveFileDialog sfdImg;
        private System.Windows.Forms.Button btnSave;
    }
}

