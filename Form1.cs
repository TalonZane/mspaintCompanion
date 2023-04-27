using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;

namespace mspaintCompanion
{
    public partial class Main : Form
    {
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static Layer ActiveLayer;
        public static List<Layer> Layers = new List<Layer>();
        public static Process mspaint;
        public static FlowLayoutPanel _LayerPanel;
        public static Screen Monitor => Screen.FromHandle(Main.mspaint.Handle);
        
        public Main()
        {
            InitializeComponent();

            //check to see if mspaint is open, if its not then open it
            if (Process.GetProcessesByName("mspaint").Length == 0)
                mspaint = Process.Start("mspaint");
            else
                mspaint = Process.GetProcessesByName("mspaint")[0];

            _LayerPanel = LayerPanel;

            //create initial layer
            new Layer();
        }

        public static void MoveLayer(Control child, int newIndex)
        {
            _LayerPanel.Controls.SetChildIndex(child, newIndex);

            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].index = i;
            }

            _LayerPanel.Refresh();
        }

        public static Color TransparentColor = Color.White;

        public static Image GetCanvasImage()
        {
            Clipboard.Clear();

            IntPtr hwnd = Main.mspaint.MainWindowHandle;
            SetForegroundWindow(hwnd);

            while (Clipboard.ContainsImage() == false)
            {
                SendKeys.SendWait("^a");
                SendKeys.SendWait("^c");
            }
            Image i = Clipboard.GetImage();
            return i;
        }

        public static void EraseCanvas()
        {
            IntPtr hwnd = Main.mspaint.MainWindowHandle;
            if (SetForegroundWindow(hwnd))
            {
                SendKeys.SendWait("^a");
                SendKeys.SendWait("{DEL}");
            }
        }

        public static void UpdateLayer(Layer layerToChange, Image replace)
        {
            if (layerToChange == null) return;

            layerToChange.fullres = replace;
            layerToChange.RefreshThumbnail();
        }

        public static void UpdateActive()
        {
            for (int i = 0; i < Layers.Count; i++)
                Layers[i].SetInactiveLayer();

            //LayerRenderer.Inst.UpdateRenderer();
        }

        private void AddLayer_Click(object sender, EventArgs e)
        {
            new Layer();
            EraseCanvas();
            LayerRenderer.Inst.UpdateRenderer();
        }

        public static void DeleteLayer(Layer layer)
        {
            if (Layers.Count == 1) return;

            Layer oldLayer = layer;

            Layer.SetActiveLayer(Layers[0]);

            Layers.Remove(oldLayer);
            oldLayer.Dispose();
            LayerRenderer.Inst.UpdateRenderer();
        }

        public static void UpdateLayerTags()
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].UpdateLayerTag();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteLayer(ActiveLayer);
        }

        private void transparency_Click(object sender, EventArgs e)
        {
            if (TransparencyColor.ShowDialog() == DialogResult.OK)
            {
                TransparentColor = TransparencyColor.Color;
            }
        }

        //i should definitely just make an export method that takes a parameter structure but. ill do it later probs.

        private void ExportButton_Click(object sender, EventArgs e)
        {
            Image CurrentLayer = GetCanvasImage();

            UpdateLayer(ActiveLayer, CurrentLayer);

            Size imageSize = CurrentLayer.Size;

            Bitmap o = new Bitmap(imageSize.Width, imageSize.Height);
            using (Graphics gr = Graphics.FromImage(o))
            {
                using (SolidBrush brush = new SolidBrush(Main.TransparentColor))
                {
                    gr.FillRectangle(brush, 0, 0, imageSize.Width, imageSize.Height);
                }

                for (int i = Layers.Count - 1; i >= 0; i--)
                {
                    if (Layers[i].IsLayerVisible)
                    {
                        Bitmap toDraw = new Bitmap(Layers[i].fullres);
                        toDraw.MakeTransparent(Main.TransparentColor);
                        gr.DrawImage(toDraw, Point.Empty);
                    }
                }
            }

            Random r = new Random();
            string[] randomFileNames =
            {
                "awesome drawing",
                "nice work",
                "wonderful art",
                "beautiful",
                "looks great",
                "lovely",
                "stunning"
            };

            ExportDialog.FileName = randomFileNames[r.Next(0, randomFileNames.Length)];

            if (ExportDialog.ShowDialog() == DialogResult.OK)
                o.Save(ExportDialog.FileName);

        }

        private void ExportButton_Transparent_Click(object sender, EventArgs e)
        {
            Image CurrentLayer = GetCanvasImage();

            UpdateLayer(ActiveLayer, CurrentLayer);

            Size imageSize = CurrentLayer.Size;

            Bitmap o = new Bitmap(imageSize.Width, imageSize.Height);
            using (Graphics gr = Graphics.FromImage(o))
            {
                for (int i = Layers.Count - 1; i >= 0; i--)
                {
                    if (Layers[i].IsLayerVisible)
                    {
                        Bitmap toDraw = new Bitmap(Layers[i].fullres);
                        toDraw.MakeTransparent(Main.TransparentColor);
                        gr.DrawImage(toDraw, Point.Empty);
                    }
                }
            }

            Random r = new Random();
            string[] randomFileNames =
            {
                "awesome drawing",
                "nice work",
                "wonderful art",
                "beautiful",
                "looks great",
                "lovely",
                "stunning"
            };

            ExportDialog.FileName = randomFileNames[r.Next(0, randomFileNames.Length)];

            if (ExportDialog.ShowDialog() == DialogResult.OK)
                o.Save(ExportDialog.FileName);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            LayerRenderer.Inst.Close();
        }
    }
}
