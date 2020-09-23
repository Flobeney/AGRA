using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }
    }
}
