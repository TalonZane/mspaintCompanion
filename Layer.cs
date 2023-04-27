using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace mspaintCompanion
{
    public partial class Layer : UserControl
    {
        public int index;
        public bool IsLayerVisible = true;
        public bool IsLayerActive = false;

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Image fullres, thumbnail;

        public Layer()
        {
            InitializeComponent();

            Main.Layers.Add(this);
            Main._LayerPanel.Controls.Add(this);
            index = Main.Layers.Count - 1;
            LayerLabel.Text = "(" + (index + 1) + ")";

            Image i = Main.GetCanvasImage();

            fullres = i;

            SetActiveLayer(this);
        }

        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        public void RefreshThumbnail()
        {
            thumbnail = new Bitmap(fullres, LayerThumbnail.Size);
            LayerThumbnail.Image = thumbnail;
        }

        public void SetInactiveLayer()
        {
            IsLayerActive = false;
            Active.Checked = false;
            ActiveLabel.Text = "";
            isVisible.Enabled = true;
        }

        public static void SetActiveLayer(Layer layer)
        {
            Layer oldActiveLayer = Main.ActiveLayer;
            Main.UpdateLayer(oldActiveLayer, Main.GetCanvasImage());

            Main.UpdateActive();

            layer.IsLayerActive = true;

            layer.Active.Checked = true;
            layer.ActiveLabel.Text = "ACTIVE";
            Main.ActiveLayer = layer;
            layer.isVisible.Checked = true;
            layer.isVisible.Enabled = false;


            IntPtr hwnd = Main.mspaint.MainWindowHandle;
            if (SetForegroundWindow(hwnd))
            {
                Clipboard.SetData(DataFormats.Bitmap, layer.fullres);
                Thread.Sleep(20);
                SendKeys.SendWait("^v");
            }

            LayerRenderer.Inst.UpdateRenderer();
        }

        private void Active_Click(object sender, EventArgs e)
        {
            SetActiveLayer(this);
        }
        public void UpdateLayerTag()
        {
            LayerLabel.Text = "(" + (index + 1) + ")";
        }

        private void MoveLayerUp_Click(object sender, EventArgs e)
        {
            if (index == 0) return;

            Main.Layers.RemoveAt(index);
            Main.Layers.Insert(index - 1, this);

            Main.MoveLayer(this, index - 1);

            Main.UpdateLayerTags();

            LayerRenderer.Inst.UpdateRenderer();

            Refresh();
        }

        private void MoveLayerDown_Click(object sender, EventArgs e)
        {
            if (index == Main.Layers.Count - 1) return;

            Main.Layers.RemoveAt(index);
            Main.Layers.Insert(index + 1, this);

            Main.MoveLayer(this, index + 1);

            Main.UpdateLayerTags();

            LayerRenderer.Inst.UpdateRenderer();

            Refresh();
        }

        private void onLayerVisiblityToggled(object sender, EventArgs e)
        {
            if (isVisible.CheckState == CheckState.Checked)
            {
                IsLayerVisible = true;
            }
            else if (isVisible.CheckState == CheckState.Unchecked)
            {
                IsLayerVisible = false;
            }

            LayerRenderer.Inst.UpdateRenderer();
        }
    }
}
