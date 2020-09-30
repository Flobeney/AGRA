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
        public Bitmap ImageTmp { get; set; }
        public double TimeTreatment { get; set; }
        private Stopwatch stopwatch { get; set; }
        private int Offset { get; set; }
        private BitmapData x_Data { get; set; }
        private byte[] dataImg { get; set; }
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
                case Data.TRAITEMENTS_DISPO.Zoom05:
                    this.Resize(0.5);
                    break;
                case Data.TRAITEMENTS_DISPO.Zoom2:
                    this.Resize(2);
                    break;
                case Data.TRAITEMENTS_DISPO.Zoom05Bi:
                    this.ResizeBi(0.5);
                    break;
                case Data.TRAITEMENTS_DISPO.Zoom2Bi:
                    this.ResizeBi(2);
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
            //Variables
            int currentPx;
            int[] res;

            //Avant le traitement
            PreTraitement();

            //Traitement
            for (int i = 0; i < this.ImageRes.Height; i++) {
                for (int j = 0; j < this.ImageRes.Width; j++) {
                    //Récupérer le pixel actuel
                    currentPx = GetPixel(i, j);

                    //Recherche des voisins, trouver la médiane
                    res = GetMedianValue(GetNeighbour(i, j));
                    //Mettre les nouvelles valeurs dans l'image
                    this.dataImg[currentPx] = Convert.ToByte(res[0]);
                    this.dataImg[currentPx + 1] = Convert.ToByte(res[1]);
                    this.dataImg[currentPx + 2] = Convert.ToByte(res[2]);
                }
            }

            //Après le traitement
            PostTraitement();
        }

        /// <summary>
        /// Traitement à faire avant la transformation
        /// </summary>
        /// <param name="scale">Taille de transformation de l'image de résultat</param>
        public void PreTraitement(double scale) {
            int size;

            //Copier l'image de base dans celle de résultat
            this.ImageRes = new Bitmap(this.ImageBase, (int)(this.ImageBase.Width * scale), (int)(this.ImageBase.Height * scale));
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Locker les bits de l'image
            this.x_Data = this.ImageRes.LockBits(
                new Rectangle(0, 0, this.ImageRes.Width, this.ImageRes.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb
            );

            //Offset => zone mémoire inutilisée
            this.Offset = this.x_Data.Stride - this.ImageRes.Width * NB_COMPOSANTES_COULEUR;
            //Calcul de la taille de l'image (incluant l'offset)
            size = this.x_Data.Stride * this.x_Data.Height;
            //Création du tableau de la taille adéquate
            this.dataImg = new byte[size];

            //Copier les données de l'image dans le tableau de bytes
            Marshal.Copy(this.x_Data.Scan0, this.dataImg, 0, size);
        }

        /// <summary>
        /// Traitement à faire avant la transformation
        /// </summary>
        public void PreTraitement() {
            this.PreTraitement(1);
        }

        /// <summary>
        /// Traitement à faire après la transformation
        /// </summary>
        public void PostTraitement() {
            //Copier les données modifiées dans l'image
            Marshal.Copy(this.dataImg, 0, this.x_Data.Scan0, this.dataImg.Length);

            //Libérer les bits
            this.ImageRes.UnlockBits(this.x_Data);

            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Récupérer l'index du pixel à l'endroit donné
        /// </summary>
        /// <param name="i">Pos i</param>
        /// <param name="j">Pos j</param>
        /// <param name="offset">Offset</param>
        /// <returns>Index du pixel</returns>
        public int GetPixel(int i, int j, int offset) {
            return i * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + offset) + j * NB_COMPOSANTES_COULEUR;
        }

        /// <summary>
        /// Récupérer l'index du pixel à l'endroit donné
        /// </summary>
        /// <param name="i">Pos i</param>
        /// <param name="j">Pos j</param>
        /// <returns>Index du pixel</returns>
        public int GetPixel(int i, int j) {
            return this.GetPixel(i, j, this.Offset);
        }

        /// <summary>
        /// Trouver la valeur médiane d'une liste d'index de pixel
        /// </summary>
        public int[] GetMedianValue(List<int> listIndex) {
            List<int> listPx;
            int[] res = new int[NB_COMPOSANTES_COULEUR];

            //Pour les différentes couleurs qui composent un pixel
            for (int color = 0; color < NB_COMPOSANTES_COULEUR; color++) {
                //Nouvelle liste
                listPx = new List<int>();
                //Parcourir les index récupérés
                foreach (int index in listIndex) {
                    listPx.Add(this.dataImg[index + color]);
                }
                //Trier la liste
                listPx.Sort();

                //Récupérer la valeur médiane
                res[color] = listPx[
                    (int)Math.Round((double)(listPx.Count / 2), 0, MidpointRounding.AwayFromZero)
                ];
            }

            return res;
        }

        /// <summary>
        /// Retourne la liste des pixels voisins d'un pixel
        /// </summary>
        public List<int> GetNeighbour(int i, int j) {
            List<int> listIndex = new List<int>();

            //Récupérer les index
            //Ligne du haut
            //Pixel haut à gauche
            if (i > 0 && j > 0) {
                listIndex.Add(
                    (i - 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + (j - 1) * NB_COMPOSANTES_COULEUR
                );
            }
            //Pixel haut milieu
            if (i > 0) {
                listIndex.Add(
                    (i - 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + j * NB_COMPOSANTES_COULEUR
                );
            }
            //Pixel haut à droite
            if (i > 0 && j < (this.ImageRes.Width - 1)) {
                listIndex.Add(
                    (i - 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + (j + 1) * NB_COMPOSANTES_COULEUR
                );
            }

            //Ligne du centre
            //Pixel à gauche
            if (j > 0) {
                listIndex.Add(
                    i * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + (j - 1) * NB_COMPOSANTES_COULEUR
                );
            }
            //Pixel actuel (centre)
            listIndex.Add(
                i * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + j * NB_COMPOSANTES_COULEUR
            );
            //Pixel à droite
            if (j < (this.ImageRes.Width - 1)) {
                listIndex.Add(
                    i * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + (j + 1) * NB_COMPOSANTES_COULEUR
                );
            }

            //Ligne du bas
            //Pixel bas à gauche
            if (i < (this.ImageRes.Height - 1) && j > 0) {
                listIndex.Add(
                    (i + 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + (j - 1) * NB_COMPOSANTES_COULEUR
                );
            }
            //Pixel bas milieu
            if (i < (this.ImageRes.Height - 1)) {
                listIndex.Add(
                    (i + 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + j * NB_COMPOSANTES_COULEUR
                );
            }
            //Pixel bas à droite
            if (i < (this.ImageRes.Height - 1) && j < (this.ImageRes.Width - 1)) {
                listIndex.Add(
                  (i + 1) * (this.ImageRes.Width * NB_COMPOSANTES_COULEUR + this.Offset) + (j + 1) * NB_COMPOSANTES_COULEUR
                );
            }

            return listIndex;
        }

        public void ZoomLockBits(double scale) {
            //Variables
            int currentPx, currentPxRes;

            //Avant le traitement
            PreTraitement(scale);

            //Image de base 
            int size, offset, x, y;
            byte[] data;

            //Copier l'image de base dans celle de traitement
            this.ImageTmp = this.ImageBase.Clone(
                new Rectangle(0, 0, this.ImageBase.Width, this.ImageBase.Height),
                this.ImageBase.PixelFormat
            );

            //Locker les bits de l'image
            BitmapData xData = this.ImageTmp.LockBits(
                new Rectangle(0, 0, this.ImageTmp.Width, this.ImageTmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb
            );

            //Offset => zone mémoire inutilisée
            offset = xData.Stride - this.ImageTmp.Width * NB_COMPOSANTES_COULEUR;
            //Calcul de la taille de l'image (incluant l'offset)
            size = xData.Stride * xData.Height;
            //Création du tableau de la taille adéquate
            data = new byte[size];

            //Copier les données de l'image dans le tableau de bytes
            Marshal.Copy(xData.Scan0, data, 0, size);

            //Traitement
            for (int i = 0; i < this.ImageRes.Height; i++) {
                for (int j = 0; j < this.ImageRes.Width; j++) {
                    x = (int)Math.Floor(j * (1 / scale));
                    y = (int)Math.Floor(i * (1 / scale));

                    //Récupérer le pixel actuel
                    currentPx = GetPixel(y, x);
                    currentPxRes = GetPixel(i, j, this.Offset);

                    //Mettre les nouvelles valeurs dans l'image
                    this.dataImg[currentPxRes] = data[currentPx];
                    this.dataImg[currentPxRes + 1] = data[currentPx + 1];
                    this.dataImg[currentPxRes + 2] = data[currentPx + 2];
                }
            }

            //Après le traitement
            PostTraitement();

            //Libérer les bits
            this.ImageTmp.UnlockBits(xData);
        }

        public void ResizeBi(double scale) {
            //Copier l'image de base dans celle de résultat
            this.ImageRes = new Bitmap(this.ImageBase, (int)(this.ImageBase.Width * scale), (int)(this.ImageBase.Height * scale));
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Variables
            double xDiff, yDiff, newColor, tScale;
            int _x, _y;

            tScale = (1 / scale);

            //Parcourir l'image
            for (int x = 0; x < this.ImageRes.Width; x++) {
                for (int y = 0; y < this.ImageRes.Height; y++) {
                    _x = (int)Math.Floor(x * tScale);
                    _y = (int)Math.Floor(y * tScale);

                    xDiff = (tScale * x) - _x;
                    yDiff = (tScale * y) - _y;

                    newColor = (
                        this.ImageBase.GetPixel(_x, _y).ToArgb() * (1 - xDiff) * (1 - yDiff)
                        +
                        this.ImageBase.GetPixel(_x + 1, _y).ToArgb() * (xDiff) * (1 - yDiff)
                        +
                        this.ImageBase.GetPixel(_x, _y + 1).ToArgb() * (1 - xDiff) * (yDiff)
                        +
                        this.ImageBase.GetPixel(_x + 1, _y + 1).ToArgb() * (xDiff) * (yDiff)
                    );

                    this.ImageRes.SetPixel(x, y, Color.FromArgb((int)newColor));
                }
            }
            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

        public void Resize(double scale) {
            //Copier l'image de base dans celle de résultat
            this.ImageRes = new Bitmap(this.ImageBase, (int)(this.ImageBase.Width * scale), (int)(this.ImageBase.Height * scale));
            //Début du traitement
            this.TimeTreatment = 0;
            this.stopwatch.Restart();

            //Variables
            Color currentColor;
            double tScale = (1 / scale);

            //Parcourir l'image
            for (int x = 0; x < this.ImageRes.Width; x++) {
                for (int y = 0; y < this.ImageRes.Height; y++) {
                    currentColor = this.ImageBase.GetPixel(
                        (int)Math.Floor(x * tScale),
                        (int)Math.Floor(y * tScale)
                    );
                    this.ImageRes.SetPixel(x, y, currentColor);
                }
            }
            //Fin du traitement
            this.stopwatch.Stop();
            this.TimeTreatment = this.stopwatch.ElapsedMilliseconds;
        }

    }
}
