using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using mspaintCompanion.Native;

namespace mspaintCompanion
{
    /// <summary>
    /// Represents a single layer in the main user interface form.
    /// </summary>
    public partial class Layer : UserControl
    {
        public int SelectedIndex;
        public bool IsLayerVisible = true;
        public bool IsLayerActive = false;

        public Image FullResolution, Thumbnail;

        public Layer()
        {
            InitializeComponent();

            MainForm.Layers.Add(this);
            MainForm.Instance.LayerPanel.Controls.Add(this);
            SelectedIndex = MainForm.Layers.Count - 1;
            LayerLabel.Text = $"({SelectedIndex + 1})";

            Image i = MainForm.GetCanvasImage();

            FullResolution = i;

            SetAsActive();
        }

        public void RefreshThumbnail()
        {
            Thumbnail = new Bitmap(FullResolution, LayerThumbnail.Size);
            LayerThumbnail.Image = Thumbnail;
        }

        /// <summary>
        /// Sets the state of the layer represented by this control as "inactive".
        /// </summary>
        public void SetAsInactive()
        {
            IsLayerActive = false;
            Active.Checked = false;
            ActiveLabel.Text = "";
            isVisible.Enabled = true;
        }

        /// <summary>
        /// Sets the state of the layer represented by this control as "active".
        /// </summary>
        public void SetAsActive()
        {
            Layer oldActiveLayer = MainForm.ActiveLayer;
            MainForm.UpdateLayer(oldActiveLayer, MainForm.GetCanvasImage());

            MainForm.MarkAllAsInactive();

            this.IsLayerActive = true;

            this.Active.Checked = true;
            this.ActiveLabel.Text = "ACTIVE";
            MainForm.ActiveLayer = this;
            this.isVisible.Checked = true;
            this.isVisible.Enabled = false;

            IntPtr hwnd = MainForm.PaintProcess.MainWindowHandle;

            if (User32.SetForegroundWindow(hwnd))
            {
                Clipboard.SetData(DataFormats.Bitmap, this.FullResolution);
                Thread.Sleep(20);
                SendKeys.SendWait("^v");
            }

            LayerRenderer.Instance.UpdateRenderer();
        }

        private void HandleActiveClick(object sender, EventArgs e)
        {
            SetAsActive();
        }

        /// <summary>
        /// Updates the label (tag) associated with the layer, as displayed to
        /// the end-user.
        /// </summary>
        public void UpdateLayerTag()
        {
            LayerLabel.Text = $"({SelectedIndex + 1})";
        }

        private void MoveLayerUp_Click(object sender, EventArgs e)
        {
            if (SelectedIndex == 0) return;

            MainForm.Layers.RemoveAt(SelectedIndex);
            MainForm.Layers.Insert(SelectedIndex - 1, this);

            MainForm.MoveLayer(this, SelectedIndex - 1);

            MainForm.UpdateLayerTags();

            LayerRenderer.Instance.UpdateRenderer();

            Refresh();
        }

        private void MoveLayerDown_Click(object sender, EventArgs e)
        {
            if (SelectedIndex == MainForm.Layers.Count - 1) return;

            MainForm.Layers.RemoveAt(SelectedIndex);
            MainForm.Layers.Insert(SelectedIndex + 1, this);

            MainForm.MoveLayer(this, SelectedIndex + 1);

            MainForm.UpdateLayerTags();

            LayerRenderer.Instance.UpdateRenderer();

            Refresh();
        }

        private void OnLayerVisiblityToggled(object sender, EventArgs e)
        {
            if (isVisible.CheckState == CheckState.Checked)
            {
                IsLayerVisible = true;
            }
            else if (isVisible.CheckState == CheckState.Unchecked)
            {
                IsLayerVisible = false;
            }

            LayerRenderer.Instance.UpdateRenderer();
        }
    }
}
