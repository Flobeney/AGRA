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

namespace transformMitch {
    public partial class FrmMain : Form {
        //Constantes
        const int NB_CORNERS = 4;
        const string BASE_FILE = "../../../Mitch.jpg";

        //Champs
        Bitmap imgBase;
        Bitmap imgRes;

        public FrmMain() {
            InitializeComponent();
            //Init
            LoadPicture(BASE_FILE);
        }

        private void LoadPicture(string filename) {
            imgBase = new Bitmap(filename);
            imgRes = new Bitmap(imgBase.Width, imgBase.Height);
            pibImgOriginal.Image = imgBase;
        }

        private void btnChooseImg_Click(object sender, EventArgs e) {
            //Image choisie
            if (ofdImg.ShowDialog() == DialogResult.OK) {
                LoadPicture(ofdImg.FileName);
            }
        }

        private void btnTransform_Click(object sender, EventArgs e) {
            List<Bitmap> corners = new List<Bitmap>();
            Random rnd = new Random();
            int midSizeWidth = imgBase.Width / 2;
            int midSizeHeight = imgBase.Height / 2;
            Size cornerSize = new Size(midSizeWidth, midSizeHeight);

            //Couper l'image en 4 parties
            for (int i = 0; i < NB_CORNERS / 2; i++) {
                for (int j = 0; j < NB_CORNERS / 2; j++) {
                    corners.Add(CropRect(
                        imgBase,
                        new Rectangle(
                            new Point(
                                i * midSizeWidth,
                                j * midSizeHeight
                            ),
                            cornerSize
                        )
                    ));
                }
            }

            //Mélanger les images
            corners = corners.OrderBy(x => rnd.Next()).ToList();

            //Les copier sur l'image de résultat
            for (int i = 0; i < NB_CORNERS / 2; i++) {
                for (int j = 0; j < NB_CORNERS / 2; j++) {
                    imgRes = DrawOnBitmap(
                        corners.First(),
                        imgRes,
                        new Point(
                            i * midSizeWidth,
                            j * midSizeHeight
                        )
                    );
                    corners.RemoveAt(0);
                }
            }

            pibImgRes.Image = imgRes;
        }

        /// <summary>
        /// Coupe une partie d'une image
        /// </summary>
        /// <param name="src">Image à couper</param>
        /// <param name="r">Rectangle avec les dimensions à couper</param>
        /// <returns>Une nouvelle image coupée selon les dimensions demandée</returns>
        private Bitmap CropRect(Bitmap src, Rectangle r) {
            //Créer l'image résultante
            Bitmap res = new Bitmap(r.Width, r.Height);

            //Dessiner sur l'image
            using (Graphics g = Graphics.FromImage(res)) {
                //Dessiner la partie de l'image voulue
                g.DrawImage(src, -r.X, -r.Y);
            }

            return res;
        }

        /// <summary>
        /// Dessiner la totalité de l'image source sur l'image de résultat à l'endroit donné
        /// </summary>
        /// <param name="src">Image source</param>
        /// <param name="dst">Image de destination</param>
        /// <param name="p">Endroit donné où dessiner dans l'image de résultat</param>
        /// <returns></returns>
        private Bitmap DrawOnBitmap(Bitmap src, Bitmap dst, Point p) {
            using (Graphics g = Graphics.FromImage(dst)) {
                g.DrawImage(src, p);
            }

            return dst;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            //Ouvrir fenêtre de sauvegarde
            if (sfdImg.ShowDialog() == DialogResult.OK) {
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
                imgRes.Save(sfdImg.FileName, format);
            }
        }
    }
}
