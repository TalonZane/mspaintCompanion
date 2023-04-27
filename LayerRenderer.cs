using mspaintCompanion.Native;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace mspaintCompanion
{
    /// <summary>
    /// Overlays layers over window Windows USER device contexts.
    /// </summary>
    public partial class LayerRenderer : Form
    {
        static LayerRenderer inst;

        /// <summary>
        /// The main global instance of the <see cref="LayerRenderer"/> class.
        /// </summary>
        public static LayerRenderer Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new LayerRenderer();
                    Console.WriteLine("A new singleton LayerRenderer instance has been created.");
                }

                return inst;
            }
        }

        public LayerRenderer()
        {
            InitializeComponent();

            BackColor = MainForm.TransparentColor;

            Rectangle canvas = MainForm.Monitor.Bounds;
            Bounds = canvas;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(5, 150); // ORIGINALLY 5, 150

            AllowTransparency = true;
            TransparencyKey = MainForm.TransparentColor;
            BackgroundImageLayout = ImageLayout.None;

            inst = this;
        }

        public void UpdateRenderer()
        {
            if (MainForm.Layers.Count == 0) return; // also a failsafe
            Size = MainForm.GetCanvasImage().Size;
            BackgroundImage = GenerateImage(MainForm.Layers.ToArray(), Size);
        }

        static Image GenerateImage(Layer[] layers, Size size)
        {
            var o = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(o))
            {
                for (int i = layers.Length - 1; i >= 0; i--)
                {
                    if (layers[i].IsLayerVisible && !layers[i].IsLayerActive)
                    {
                        var toDraw = new Bitmap(layers[i].FullResolution);
                        toDraw.MakeTransparent(MainForm.TransparentColor);
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
                createParams.ExStyle |= User32.WS_EX_TRANSPARENT;
                return createParams;
            }
        }
    }
}
