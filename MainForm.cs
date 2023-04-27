using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using mspaintCompanion.Native;

namespace mspaintCompanion
{
    /// <summary>
    /// Represents the main layer user interface form.
    /// </summary>
    public partial class MainForm : Form
    {
        public static Layer ActiveLayer;
        public static List<Layer> Layers = new List<Layer>();
        public static Process PaintProcess;
        public static Screen Monitor => Screen.FromHandle(PaintProcess.Handle);

        /// <summary>
        /// Represents the main singleton instance of the <see cref="MainForm"/> class.
        /// </summary>
        public static MainForm Instance { get; private set; }

        public MainForm()
        {
            if(Instance != null) {
                Console.WriteLine("[WARN] The MainForm object has been created more than once. This might occur because of state corruption. The singleton instance field will be overwritten.");
            }

            Instance = this;

            InitializeComponent();

            // check to see if mspaint is open, if its not then open it
            if (Process.GetProcessesByName("mspaint").Length == 0)
                PaintProcess = Process.Start("mspaint");
            else
                PaintProcess = Process.GetProcessesByName("mspaint")[0];

            // create initial layer
            new Layer();
        }

        public static void MoveLayer(Control child, int newIndex)
        {
            Instance.LayerPanel.Controls.SetChildIndex(child, newIndex);

            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].SelectedIndex = i;
            }

            Instance.LayerPanel.Refresh();
        }

        /// <summary>
        /// Represents the color to treat as transparency (an alpha of 0.0).
        /// </summary>
        public static Color TransparentColor = Color.White;

        /// <summary>
        /// Copies the image from the Paint canvas.
        /// </summary>
        public static Image GetCanvasImage()
        {
            Clipboard.Clear();

            IntPtr hwnd = PaintProcess.MainWindowHandle;
            User32.SetForegroundWindow(hwnd);

            while (Clipboard.ContainsImage() == false)
            {
                SendKeys.SendWait("^a");
                SendKeys.SendWait("^c");
            }

            Image i = Clipboard.GetImage();
            return i;
        }

        /// <summary>
        /// Erases the Paint canvas.
        /// </summary>
        public static void EraseCanvas()
        {
            IntPtr hwnd = PaintProcess.MainWindowHandle;
            if (User32.SetForegroundWindow(hwnd))
            {
                SendKeys.SendWait("^a");
                SendKeys.SendWait("{DEL}");
            }
        }

        /// <summary>
        /// Updates the specified layer with the given image.
        /// </summary>
        /// <param name="layerToChange">The layer to update.</param>
        /// <param name="replace">The image to assign to the layer.</param>
        public static void UpdateLayer(Layer layerToChange, Image replace)
        {
            if (layerToChange == null) return;

            layerToChange.FullResolution = replace;
            layerToChange.RefreshThumbnail();
        }

        /// <summary>
        /// Marks all layers as inactive.
        /// </summary>
        public static void MarkAllAsInactive()
        {
            for (int i = 0; i < Layers.Count; i++)
                Layers[i].SetAsInactive();
        }

        private void HandleAddLayerButtonClick(object sender, EventArgs e)
        {
            new Layer();
            EraseCanvas();
            LayerRenderer.Instance.UpdateRenderer();
        }

        /// <summary>
        /// Deletes the given layer.
        /// </summary>
        /// <param name="layer">The layer to delete.</param>
        public static void DeleteLayer(Layer layer)
        {
            if (Layers.Count == 1) return;

            Layer oldLayer = layer;

            Layers[0].SetAsActive();

            Layers.Remove(oldLayer);
            oldLayer.Dispose();
            LayerRenderer.Instance.UpdateRenderer();
        }


        /// <summary>
        /// Updates all layer tags (labels).
        /// </summary>
        public static void UpdateLayerTags()
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].UpdateLayerTag();
            }
        }

        private void HandleDeleteButtonClick(object sender, EventArgs e)
        {
            DeleteLayer(ActiveLayer);
        }

        private void HandleTransparencyClick(object sender, EventArgs e)
        {
            if (TransparencyColor.ShowDialog() == DialogResult.OK)
            {
                TransparentColor = TransparencyColor.Color;
            }
        }

        // TODO: i should definitely just make an export method that takes a parameter structure but. ill do it later probs.
        private void HandleExportButtonClick(object sender, EventArgs e)
        {
            Image currentLayer = GetCanvasImage();

            UpdateLayer(ActiveLayer, currentLayer);

            Size imageSize = currentLayer.Size;

            var o = new Bitmap(imageSize.Width, imageSize.Height);
            using (var gr = Graphics.FromImage(o))
            {
                using (var brush = new SolidBrush(TransparentColor))
                {
                    gr.FillRectangle(brush, 0, 0, imageSize.Width, imageSize.Height);
                }

                for (int i = Layers.Count - 1; i >= 0; i--)
                {
                    if (Layers[i].IsLayerVisible)
                    {
                        var toDraw = new Bitmap(Layers[i].FullResolution);
                        toDraw.MakeTransparent(TransparentColor);
                        gr.DrawImage(toDraw, Point.Empty);
                    }
                }
            }

            var r = new Random();
            ExportDialog.FileName = Program.DefaultFileNames[r.Next(0, Program.DefaultFileNames.Length)];

            if (ExportDialog.ShowDialog() == DialogResult.OK) {
                o.Save(ExportDialog.FileName);
            }
        }

        private void ExportButton_Transparent_Click(object sender, EventArgs e)
        {
            Image currentLayer = GetCanvasImage();

            UpdateLayer(ActiveLayer, currentLayer);

            Size imageSize = currentLayer.Size;

            var o = new Bitmap(imageSize.Width, imageSize.Height);
            using (var gr = Graphics.FromImage(o))
            {
                for (int i = Layers.Count - 1; i >= 0; i--)
                {
                    if (Layers[i].IsLayerVisible)
                    {
                        var toDraw = new Bitmap(Layers[i].FullResolution);
                        toDraw.MakeTransparent(MainForm.TransparentColor);
                        gr.DrawImage(toDraw, Point.Empty);
                    }
                }
            }

            var r = new Random();
            ExportDialog.FileName = Program.DefaultFileNames[r.Next(0, Program.DefaultFileNames.Length)];

            if (ExportDialog.ShowDialog() == DialogResult.OK)
                o.Save(ExportDialog.FileName);
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            LayerRenderer.Instance.Close();
        }
    }
}
