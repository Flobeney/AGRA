using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetInterfaceImages {
    public partial class FrmMain : Form {
        //Propriétés
        public TraitementImg Traitement { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmMain() {
            InitializeComponent();
        }

        public void ApplyTreatment(Data.TRAITEMENTS_DISPO treatment) {
            //Appliquer le traitement
            this.Traitement.ApplyTreatment(treatment);
            //Afficher la nouvelle image
            pibImgRes.Image = this.Traitement.ImgRes;
            //Afficher le temps de traitement
            lsbTimeTreatments.Items.Add($"Méthode {treatment} : {this.Traitement.TimeTreatment}ms");
        }

        /// <summary>
        /// Click sur le bouton de sélection d'une image
        /// </summary>
        private void btnChooseImg_Click(object sender, EventArgs e) {
            //Image choisie
            if (ofdImg.ShowDialog() == DialogResult.OK) {
                //Si le nb d'image n'est pas bon
                if(ofdImg.FileNames.Length != 2) {
                    MessageBox.Show("Veuillez choisir 2 images");
                } else {
                    this.Traitement = new TraitementImg(ofdImg.FileNames);
                }
                pibFirstImg.Image = this.Traitement.Imgs[0];
                pibSecondImg.Image = this.Traitement.Imgs[1];
                //Rendre les traitements disponible
                btnClearImg.Enabled = true;
                btnAdd.Enabled = true;
                btnSub.Enabled = true;
            }
        }

        private void btnClearImg_Click(object sender, EventArgs e) {
            pibImgRes.Image = null;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            //Ouvrir fenêtre de sauvegarde
            if(sfdImg.ShowDialog() == DialogResult.OK) {
                string ext = Path.GetExtension(sfdImg.FileName);
                //Format de l'image
                ImageFormat format = ImageFormat.Png;
                switch (ext) {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                //Sauver l'image
                this.Traitement.ImgRes.Save(sfdImg.FileName, format);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            ApplyTreatment(Data.TRAITEMENTS_DISPO.Addition);
        }

        private void btnSub_Click(object sender, EventArgs e) {
            ApplyTreatment(Data.TRAITEMENTS_DISPO.Soustraction);
        }
    }
}
