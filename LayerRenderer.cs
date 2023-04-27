using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace mspaintCompanion
{
    public partial class LayerRenderer : Form
    {
        static LayerRenderer inst;

        public static LayerRenderer Inst
        {
            get
            {
                if (inst == null)
                {
                    inst = new LayerRenderer();
                    Console.WriteLine("fuck");
                }

                return inst;
            }
        }

        public LayerRenderer()
        {
            InitializeComponent();

            BackColor = Main.TransparentColor;

            Rectangle canvas = Main.Monitor.Bounds;
            Bounds = canvas;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(5, 150); //ORIGINALLY 5, 150

            AllowTransparency = true;
            TransparencyKey = Main.TransparentColor;
            BackgroundImageLayout = ImageLayout.None;

            inst = this;
        }


        public void UpdateRenderer()
        {
            if (Main.Layers.Count == 0) return; //also a failsafe
            Size = Main.GetCanvasImage().Size;
            BackgroundImage = GenerateImage(Main.Layers.ToArray(), Size);
        }

        static Image GenerateImage(Layer[] layers, Size size)
        {
            Bitmap o = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(o))
            {
                for (int i = layers.Length - 1; i >= 0; i--)
                {
                    if (layers[i].IsLayerVisible && !layers[i].IsLayerActive)
                    {
                        Bitmap toDraw = new Bitmap(layers[i].fullres);
                        toDraw.MakeTransparent(Main.TransparentColor);
                        g.DrawImage(toDraw, Point.Empty);
                    }
                }
            }

            return o;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT

                return createParams;
            }
        }
    }
}
