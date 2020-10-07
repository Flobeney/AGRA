/*
 * Fichier : TraitementImg.cs
 * Desc. : Application de différents traitement à une image
 * Auteur : Florent Beney, CFPTI
 * Date : 02/09/2020, version initiale
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetInterfaceImages {
    /// <summary>
    /// Classe permettant l'application de différents traitement à une image
    /// </summary>
    public class TraitementImg {
        //Constantes
        private const int NB_COMPOSANTES_COULEUR = 3;
        private const int NB_IMGS = 2;

        //Propriétés
        public Bitmap[] Imgs { get; set; }
        public Bitmap ImgRes { get; set; }
        public double TimeTreatment { get; set; }
        private Stopwatch stopwatch { get; set; }

        //Méthodes

        /// <summary>
        /// Constructeur désigné
        /// </summary>
        /// <param name="pFilname">Le chemin vers l'image</param>
        public TraitementImg(string[] pFilnames) {
            //Déclarer les variables
            this.stopwatch = new Stopwatch();
            this.Imgs = new Bitmap[NB_IMGS];
            //Créer les images
            for (int i = 0; i < pFilnames.Length; i++) {
                this.Imgs[i] = new Bitmap(pFilnames[i]);
            }
            this.ImgRes = new Bitmap(this.Imgs[0].Width, this.Imgs[0].Height);
            //Vérifier que les images soient de la même taille
            for (int i = 0; i < NB_IMGS - 1; i++) {
                if((this.Imgs[i].Width != this.Imgs[i+1].Width) || (this.Imgs[i].Height != this.Imgs[i + 1].Height)) {
                    MessageBox.Show("Les images doivent être de la même taille");
                }
            }

        }

        /// <summary>
        /// Aplliquer le traitement adéquat
        /// </summary>
        /// <param name="treatment">Traitement demandé</param>
        public void ApplyTreatment(Data.TRAITEMENTS_DISPO treatment) {
            Console.WriteLine(treatment);
            switch (treatment) {
                case Data.TRAITEMENTS_DISPO.Addition:
                    this.AddImages();
                    break;
                case Data.TRAITEMENTS_DISPO.Soustraction:
                    this.SubImages();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Addition
        /// </summary>
        public void AddImages() {
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Variables
            int newValue;

            //Pour tout les pixels de l'image
            for (int x = 0; x < this.Imgs[0].Width; x++) {
                for (int y = 0; y < this.Imgs[0].Height; y++) {
                    newValue = 0;
                    //Parcourir chaques images
                    foreach (Bitmap item in this.Imgs) {
                        //Récupérer la valeur du pixel
                        newValue += item.GetPixel(x, y).ToArgb();
                    }
                    this.ImgRes.SetPixel(x, y, Color.FromArgb(newValue / NB_IMGS));
                }
            }
            

            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Soustraction
        /// </summary>
        public void SubImages() {
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Variables

            //Pour tout les pixels de l'image
            for (int x = 0; x < this.Imgs[0].Width; x++) {
                for (int y = 0; y < this.Imgs[0].Height; y++) {
                    this.ImgRes.SetPixel(x, y, Color.FromArgb(this.Imgs[1].GetPixel(x, y).ToArgb() - this.Imgs[0].GetPixel(x, y).ToArgb()));
                }
            }


            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Comparaison de 2 couleurs
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        public bool ColorEquals(Color color1, Color color2) {
            return (
                (color1.R == color2.R) &&
                (color1.G == color2.G) &&
                (color1.B == color2.B)
            );
        }

    }
}
