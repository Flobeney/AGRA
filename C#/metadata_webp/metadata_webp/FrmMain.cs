using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebPWrapper;

namespace metadata_webp {
    public partial class FrmMain : Form {
        public FrmMain() {
            InitializeComponent();
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e) {
            if(ofdFile.ShowDialog() == DialogResult.OK) {
                Console.WriteLine(ofdFile.FileName);
                Console.WriteLine(Path.GetExtension(ofdFile.FileName));

                Bitmap img;

                if(Path.GetExtension(ofdFile.FileName) == ".webp") {
                    WebP webp = new WebP();
                    img = webp.Load(ofdFile.FileName);
                } else {
                    img = new Bitmap(ofdFile.FileName);
                }
                pibImg.Image = img;

                lsbMetadata.Items.Clear();

                IEnumerable<MetadataExtractor. Directory> directories = ImageMetadataReader.ReadMetadata(ofdFile.FileName);
                foreach (var dir in directories) {
                    foreach (var item in dir.Tags) {
                        Console.WriteLine(item);
                        lsbMetadata.Items.Add(item);
                    }
                }
            }
        }
    }
}
