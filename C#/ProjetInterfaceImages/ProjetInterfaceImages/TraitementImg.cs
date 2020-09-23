/*
 * Fichier : TraitementImg.cs
 * Desc. : Application de différents traitement à une image
 * Auteur : Florent Beney, CFPTI
 * Date : 02/09/2020, version initiale
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInterfaceImages {
    /// <summary>
    /// Classe permettant l'application de différents traitement à une image
    /// </summary>
    public class TraitementImg {
        //Constantes
        private const int NB_COMPOSANTES_COULEUR = 3;

        //Propriétés
        public Bitmap ImageBase { get; set; }
        public Bitmap ImageRes { get; set; }

        //Méthodes

        /// <summary>
        /// Constructeur désigné
        /// </summary>
        /// <param name="pImg">L'image à laquelle on va appliquer des traitements</param>
        public TraitementImg(Bitmap pImg) {
            this.ImageBase = pImg;
            this.ImageRes = pImg;
        }

        /// <summary>
        /// Constructeur prenant le chemin vers l'image comme paramètre
        /// </summary>
        /// <param name="pFilname">Le chemin vers l'image</param>
        public TraitementImg(string pFilname) : this(new Bitmap(pFilname)) {
            //Code vide
        }

        /// <summary>
        /// Appliquer le traitement demandé à l'image
        /// </summary>
        /// <param name="pTraitement">Le traitement demandé</param>
        public void AppliquerTraitement(Data.TRAITEMENTS_DISPO pTraitement) {
            Console.WriteLine(pTraitement);
            switch (pTraitement) {
                case Data.TRAITEMENTS_DISPO.Rouge:
                    break;

                case Data.TRAITEMENTS_DISPO.Monochrome:
                    this.TraiterMonochrome();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Traitement monochrome
        /// </summary>
        public void TraiterMonochrome() {
            int newValue;
            Color currentColor;

            //Parcourir l'image
            for (int x = 0; x < this.ImageBase.Width; x++) {
                for (int y = 0; y < this.ImageBase.Height; y++) {
                    currentColor = this.ImageBase.GetPixel(x, y);
                    newValue = (currentColor.R + currentColor.G + currentColor.B) / NB_COMPOSANTES_COULEUR;
                    this.ImageRes.SetPixel(x, y, Color.FromArgb(newValue, newValue, newValue));
                }
            }
        }
    }
}
