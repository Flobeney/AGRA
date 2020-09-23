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
            //Charger la liste des traitements disponibles
            foreach (var item in Enum.GetValues(typeof(Data.TRAITEMENTS_DISPO))) {
                cmbTreatments.Items.Add(item);
            }
        }

        /// <summary>
        /// Click sur le bouton de sélection d'une image
        /// </summary>
        private void btnChooseImg_Click(object sender, EventArgs e) {
            //Image choisie
            if (ofdImg.ShowDialog() == DialogResult.OK) {
                this.Traitement = new TraitementImg(ofdImg.FileName);
                pibImgOriginal.Image = this.Traitement.ImageBase;
                pibImgRes.Image = this.Traitement.ImageRes;
                //Activer la liste des traitements
                cmbTreatments.Enabled = true;
                btnClearImg.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        /// <summary>
        /// Changement d'item sélectionné
        /// </summary>
        private void cmbTreatments_SelectedIndexChanged(object sender, EventArgs e) {
            //Appliquer le traitement
            this.Traitement.AppliquerTraitement((Data.TRAITEMENTS_DISPO)cmbTreatments.SelectedItem);
            //Afficher la nouvelle image
            pibImgRes.Image = this.Traitement.ImageRes;
            //Afficher le temps de traitement
            lsbTimeTreatments.Items.Add($"Méthode {cmbTreatments.SelectedItem} : {this.Traitement.TimeTreatment}ms");
        }

        private void btnClearImg_Click(object sender, EventArgs e) {
            pibImgRes.Image = this.Traitement.ImageBase;
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
                this.Traitement.ImageRes.Save(sfdImg.FileName, format);
            }
        }
    }
}
