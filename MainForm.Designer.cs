
namespace mspaintCompanion
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AddLayer = new System.Windows.Forms.Button();
            this.LayerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.TransparencyColor = new System.Windows.Forms.ColorDialog();
            this.transparency = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ExportDialog = new System.Windows.Forms.SaveFileDialog();
            this.ExportButton_Transparent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddLayer
            // 
            this.AddLayer.Location = new System.Drawing.Point(14, 18);
            this.AddLayer.Name = "AddLayer";
            this.AddLayer.Size = new System.Drawing.Size(25, 25);
            this.AddLayer.TabIndex = 0;
            this.AddLayer.Text = "+";
            this.AddLayer.UseVisualStyleBackColor = true;
            this.AddLayer.Click += new System.EventHandler(this.HandleAddLayerButtonClick);
            // 
            // LayerPanel
            // 
            this.LayerPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LayerPanel.AutoScroll = true;
            this.LayerPanel.BackColor = System.Drawing.Color.DarkGray;
            this.LayerPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.LayerPanel.Location = new System.Drawing.Point(15, 48);
            this.LayerPanel.Name = "LayerPanel";
            this.LayerPanel.Size = new System.Drawing.Size(512, 275);
            this.LayerPanel.TabIndex = 1;
            this.LayerPanel.WrapContents = false;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(45, 17);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(25, 25);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "-";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.HandleDeleteButtonClick);
            // 
            // TransparencyColor
            // 
            this.TransparencyColor.AnyColor = true;
            this.TransparencyColor.Color = System.Drawing.Color.White;
            this.TransparencyColor.FullOpen = true;
            // 
            // transparency
            // 
            this.transparency.Location = new System.Drawing.Point(15, 334);
            this.transparency.Name = "transparency";
            this.transparency.Size = new System.Drawing.Size(119, 25);
            this.transparency.TabIndex = 4;
            this.transparency.Text = "Transparent Color";
            this.transparency.UseVisualStyleBackColor = true;
            this.transparency.Click += new System.EventHandler(this.HandleTransparencyClick);
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(342, 334);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(54, 25);
            this.ExportButton.TabIndex = 5;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.HandleExportButtonClick);
            // 
            // ExportDialog
            // 
            this.ExportDialog.DefaultExt = "png";
            // 
            // ExportButton_Transparent
            // 
            this.ExportButton_Transparent.Location = new System.Drawing.Point(402, 334);
            this.ExportButton_Transparent.Name = "ExportButton_Transparent";
            this.ExportButton_Transparent.Size = new System.Drawing.Size(125, 25);
            this.ExportButton_Transparent.TabIndex = 6;
            this.ExportButton_Transparent.Text = "Export (Transparent)";
            this.ExportButton_Transparent.UseVisualStyleBackColor = true;
            this.ExportButton_Transparent.Click += new System.EventHandler(this.ExportButton_Transparent_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(541, 370);
            this.Controls.Add(this.ExportButton_Transparent);
            this.Controls.Add(this.transparency);
            this.Controls.Add(this.LayerPanel);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddLayer);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "mspaintCompanion";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddLayer;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.ColorDialog TransparencyColor;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.SaveFileDialog ExportDialog;
        private System.Windows.Forms.Button ExportButton_Transparent;
        private System.Windows.Forms.Button transparency;
        public System.Windows.Forms.FlowLayoutPanel LayerPanel;
    }
}

