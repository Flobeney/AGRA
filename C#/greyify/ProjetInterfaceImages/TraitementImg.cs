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
        public double TimeTreatment { get; set; }
        private Stopwatch stopwatch { get; set; }
        private readonly object balanceLock;

        //Méthodes

        /// <summary>
        /// Constructeur désigné
        /// </summary>
        /// <param name="pImg">L'image à laquelle on va appliquer des traitements</param>
        public TraitementImg(Bitmap pImg) {
            this.ImageBase = pImg;
            this.ImageRes = pImg;
            this.stopwatch = new Stopwatch();
            this.balanceLock = new object();
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
                case Data.TRAITEMENTS_DISPO.GetSet:
                    this.GetSet();
                    break;
                case Data.TRAITEMENTS_DISPO.LockBits:
                    this.ByteArray();
                    break;
                case Data.TRAITEMENTS_DISPO.LockBitsUnsafe:
                    this.ByteArrayPtr();
                    break;
                case Data.TRAITEMENTS_DISPO.LockBitsUnsafeParallel:
                    this.ByteArrayPtrParallel();
                    break;
                case Data.TRAITEMENTS_DISPO.Median:
                    this.Median();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Traitement monochrome
        /// </summary>
        public void GetSet() {
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Variables
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
            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

        public void ByteArray() {
            //Copier l'image de base dans celle de résultat
            this.ImageRes = this.ImageBase.Clone(
                new Rectangle(0, 0, this.ImageBase.Width, this.ImageBase.Height),
                this.ImageBase.PixelFormat
            );
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Variables
            int size, bitsPerPx;
            byte newColor;
            byte[] data;

            //Locker les bits de l'image
            BitmapData xData = this.ImageRes.LockBits(
                new Rectangle(0, 0, this.ImageRes.Width, this.ImageRes.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb
            );

            //Nombre de bits par px
            bitsPerPx = Bitmap.GetPixelFormatSize(xData.PixelFormat) / 8;
            //Calcul de la taille de l'image (incluant l'offset)
            size = xData.Stride * xData.Height;
            //Création du tableau de la taille adéquate
            data = new byte[size];

            //Copier les données de l'image dans le tableau de bytes
            Marshal.Copy(xData.Scan0, data, 0, size);

            //Transformation
            for (int i = 0; i < size; i += bitsPerPx) {
                //Calculer la nouvelle valeur avec les 3 composantes
                newColor = Convert.ToByte((data[i] + data[i + 1] + data[i + 2]) / NB_COMPOSANTES_COULEUR);
                //Assigner la nouvelle couleur au pixel
                data[i] = newColor;
                data[i + 1] = newColor;
                data[i + 2] = newColor;
            }

            //Copier les données modifiées dans l'image
            Marshal.Copy(data, 0, xData.Scan0, data.Length);

            //Libérer les bits
            this.ImageRes.UnlockBits(xData);

            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

        public unsafe void ByteArrayPtr() {
            //Copier l'image de base dans celle de résultat
            this.ImageRes = this.ImageBase.Clone(
                new Rectangle(0, 0, this.ImageBase.Width, this.ImageBase.Height),
                this.ImageBase.PixelFormat
            );
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Empécher multithreading
            lock (balanceLock) {
                //Variables
                int offset;
                byte* startPx, currentPx;
                byte newColor;

                //Locker les bits de l'image
                BitmapData xData = this.ImageRes.LockBits(
                    new Rectangle(0, 0, this.ImageRes.Width, this.ImageRes.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb
                );

                //Offset => zone mémoire inutilisée
                offset = xData.Stride - this.ImageRes.Width * 3;
                //Pointeur sur le 1er pixel
                startPx = (byte*)xData.Scan0.ToPointer();

                //Transformation
                for (int i = 0; i < this.ImageRes.Height; i++) {
                    for (int j = 0; j < this.ImageRes.Width; j++) {
                        //Récupérer le pointeur sur le pixel actuel
                        currentPx = startPx + i * (this.ImageRes.Width * 3 + offset) + j * 3;

                        //Calculer la nouvelle valeur avec les 3 composantes
                        newColor = Convert.ToByte((currentPx[0] + currentPx[1] + currentPx[2]) / NB_COMPOSANTES_COULEUR);

                        //ASsigner la nouvelle couleur au pixel
                        currentPx[0] = newColor;
                        currentPx[1] = newColor;
                        currentPx[2] = newColor;
                    }
                }

                //Libérer les bits
                this.ImageRes.UnlockBits(xData);
            }

            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

        public unsafe void ByteArrayPtrParallel() {
            //Copier l'image de base dans celle de résultat
            this.ImageRes = this.ImageBase.Clone(
                new Rectangle(0, 0, this.ImageBase.Width, this.ImageBase.Height),
                this.ImageBase.PixelFormat
            );
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            lock (balanceLock) {
                //Variables
                int offset, height, width;
                byte newColor;
                byte* startPx;

                //Locker les bits de l'image
                BitmapData xData = this.ImageRes.LockBits(
                    new Rectangle(0, 0, this.ImageRes.Width, this.ImageRes.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb
                );

                //Offset => zone mémoire inutilisée
                offset = xData.Stride - this.ImageRes.Width * 3;
                //Pointeur sur le 1er pixel
                startPx = (byte*)xData.Scan0.ToPointer();

                //Dimensions image
                height = this.ImageRes.Height;
                width = this.ImageRes.Width;

                //Transformation
                Parallel.For(0, height, i => {
                    byte* currentPx = startPx;
                    Parallel.For(0, width, j => {
                        //Récupérer le pointeur sur le pixel actuel
                        currentPx = startPx + i * (width * 3 + offset) + j * 3;

                        //Calculer la nouvelle valeur avec les 3 composantes
                        newColor = Convert.ToByte((currentPx[0] + currentPx[1] + currentPx[2]) / NB_COMPOSANTES_COULEUR);

                        //Assigner la nouvelle couleur au pixel
                        currentPx[0] = newColor;
                        currentPx[1] = newColor;
                        currentPx[2] = newColor;
                    });
                });

                //Libérer les bits
                this.ImageRes.UnlockBits(xData);

                //Fin du traitement
                this.stopwatch.Stop();
                this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
            }

        }

        public void Median() {
            //Copier l'image de base dans celle de résultat
            this.ImageRes = this.ImageBase.Clone(
                new Rectangle(0, 0, this.ImageBase.Width, this.ImageBase.Height),
                this.ImageBase.PixelFormat
            );
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Variables
            int size, offset, currentPx;
            byte[] data;
            int[] res;

            //Locker les bits de l'image
            BitmapData xData = this.ImageRes.LockBits(
                new Rectangle(0, 0, this.ImageRes.Width, this.ImageRes.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb
            );

            //Offset => zone mémoire inutilisée
            offset = xData.Stride - this.ImageRes.Width * 3;
            //Calcul de la taille de l'image (incluant l'offset)
            size = xData.Stride * xData.Height;
            //Création du tableau de la taille adéquate
            data = new byte[size];

            //Copier les données de l'image dans le tableau de bytes
            Marshal.Copy(xData.Scan0, data, 0, size);

            //Transformation
            for (int i = 0; i < this.ImageRes.Height; i++) {
                for (int j = 0; j < this.ImageRes.Width; j++) {
                    //Récupérer le pixel actuel
                    currentPx = i * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + j * NB_COMPOSANTES_COULEUR;

                    //Recherche des voisins, trouver la médiane
                    res = GetNeighbour(data, i, j, offset);
                    //Mettre les nouvelles valeurs dans l'image
                    data[currentPx] = Convert.ToByte(res[0]);
                    data[currentPx + 1] = Convert.ToByte(res[1]);
                    data[currentPx + 2] = Convert.ToByte(res[2]);
                }
            }

            //Copier les données modifiées dans l'image
            Marshal.Copy(data, 0, xData.Scan0, data.Length);

            //Libérer les bits
            this.ImageRes.UnlockBits(xData);

            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

        public int[] GetNeighbour(byte[] data, int i, int j, int offset) {
            List<int> listPx;
            int[] res = new int[NB_COMPOSANTES_COULEUR];

            for (int k = 0; k < NB_COMPOSANTES_COULEUR; k++) {
                //Nouvelle liste
                listPx = new List<int>();

                //Ligne du haut
                //Pixel haut à gauche
                if (i > 0 && j > 0) {
                    listPx.Add(
                        data[
                          ((i - 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + (j - 1) * NB_COMPOSANTES_COULEUR) + k
                        ]
                    );
                }
                //Pixel haut milieu
                if (i > 0) {
                    listPx.Add(
                        data[
                            ((i - 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + j * NB_COMPOSANTES_COULEUR) + k
                        ]
                    );
                }
                //Pixel haut à droite
                if (i > 0 && j < (this.ImageRes.Width - 1)) {
                    listPx.Add(
                        data[
                            ((i - 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + (j + 1) * NB_COMPOSANTES_COULEUR) + k
                        ]
                    );
                }

                //Ligne du centre
                //Pixel à gauche
                if (j > 0) {
                    listPx.Add(
                        data[
                            (i * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + (j - 1) * NB_COMPOSANTES_COULEUR) + k
                        ]
                    );
                }
                //Pixel actuel (centre)
                listPx.Add(
                    data[
                        (i * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + j * NB_COMPOSANTES_COULEUR) + k
                    ]
                );
                //Pixel à droite
                if (j < (this.ImageRes.Width - 1)) {
                    listPx.Add(
                        data[
                            (i * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + (j + 1) * NB_COMPOSANTES_COULEUR) + k
                        ]
                    );
                }

                //Ligne du bas
                //Pixel bas à gauche
                if (i < (this.ImageRes.Height - 1) && j > 0) {
                    listPx.Add(
                        data[
                            ((i + 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + (j - 1) * NB_COMPOSANTES_COULEUR) + k
                        ]
                    );
                }
                //Pixel bas milieu
                if (i < (this.ImageRes.Height - 1)) {
                    listPx.Add(
                        data[
                            ((i + 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + j * NB_COMPOSANTES_COULEUR) + k
                        ]
                    );
                }
                //Pixel bas à droite
                if (i < (this.ImageRes.Height - 1) && j < (this.ImageRes.Width - 1)) {
                    listPx.Add(
                        data[
                            ((i + 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + (j + 1) * NB_COMPOSANTES_COULEUR) + k
                        ]
                    );
                }

                //Trier la liste
                listPx.Sort();

                //Récupérer la valeur médiane
                res[k] = listPx[
                    (int)Math.Round((double)(listPx.Count / 2), 0, MidpointRounding.AwayFromZero)
                ];
            }

            return res;
        }

    }
}
