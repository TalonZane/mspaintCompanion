
namespace mspaintCompanion
{
    partial class Layer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.isVisible = new System.Windows.Forms.CheckBox();
            this.LayerThumbnail = new System.Windows.Forms.PictureBox();
            this.Active = new System.Windows.Forms.CheckBox();
            this.ActiveLabel = new System.Windows.Forms.Label();
            this.MoveLayerUp = new System.Windows.Forms.Button();
            this.MoveLayerDown = new System.Windows.Forms.Button();
            this.LayerLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LayerThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // isVisible
            // 
            this.isVisible.AutoSize = true;
            this.isVisible.Checked = true;
            this.isVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isVisible.Location = new System.Drawing.Point(5, 28);
            this.isVisible.Name = "isVisible";
            this.isVisible.Size = new System.Drawing.Size(15, 14);
            this.isVisible.TabIndex = 0;
            this.isVisible.UseVisualStyleBackColor = true;
            this.isVisible.CheckStateChanged += new System.EventHandler(this.onLayerVisiblityToggled);
            // 
            // LayerThumbnail
            // 
            this.LayerThumbnail.BackColor = System.Drawing.SystemColors.Window;
            this.LayerThumbnail.Location = new System.Drawing.Point(29, 7);
            this.LayerThumbnail.Name = "LayerThumbnail";
            this.LayerThumbnail.Size = new System.Drawing.Size(78, 58);
            this.LayerThumbnail.TabIndex = 1;
            this.LayerThumbnail.TabStop = false;
            // 
            // Active
            // 
            this.Active.AutoSize = true;
            this.Active.Location = new System.Drawing.Point(400, 27);
            this.Active.Name = "Active";
            this.Active.Size = new System.Drawing.Size(15, 14);
            this.Active.TabIndex = 3;
            this.Active.UseVisualStyleBackColor = true;
            this.Active.Click += new System.EventHandler(this.Active_Click);
            // 
            // ActiveLabel
            // 
            this.ActiveLabel.AutoSize = true;
            this.ActiveLabel.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActiveLabel.Location = new System.Drawing.Point(382, 25);
            this.ActiveLabel.Name = "ActiveLabel";
            this.ActiveLabel.Size = new System.Drawing.Size(51, 16);
            this.ActiveLabel.TabIndex = 4;
            this.ActiveLabel.Text = "ACTIVE";
            this.ActiveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MoveLayerUp
            // 
            this.MoveLayerUp.Location = new System.Drawing.Point(457, -1);
            this.MoveLayerUp.Name = "MoveLayerUp";
            this.MoveLayerUp.Size = new System.Drawing.Size(20, 27);
            this.MoveLayerUp.TabIndex = 5;
            this.MoveLayerUp.Text = "↑";
            this.MoveLayerUp.UseVisualStyleBackColor = true;
            this.MoveLayerUp.Click += new System.EventHandler(this.MoveLayerUp_Click);
            // 
            // MoveLayerDown
            // 
            this.MoveLayerDown.Location = new System.Drawing.Point(457, 44);
            this.MoveLayerDown.Name = "MoveLayerDown";
            this.MoveLayerDown.Size = new System.Drawing.Size(20, 27);
            this.MoveLayerDown.TabIndex = 6;
            this.MoveLayerDown.Text = "↓";
            this.MoveLayerDown.UseVisualStyleBackColor = true;
            this.MoveLayerDown.Click += new System.EventHandler(this.MoveLayerDown_Click);
            // 
            // LayerLabel
            // 
            this.LayerLabel.AutoSize = true;
            this.LayerLabel.Location = new System.Drawing.Point(114, 28);
            this.LayerLabel.Name = "LayerLabel";
            this.LayerLabel.Size = new System.Drawing.Size(36, 15);
            this.LayerLabel.TabIndex = 7;
            this.LayerLabel.Text = "label1";
            // 
            // Layer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.LayerLabel);
            this.Controls.Add(this.MoveLayerDown);
            this.Controls.Add(this.MoveLayerUp);
            this.Controls.Add(this.ActiveLabel);
            this.Controls.Add(this.Active);
            this.Controls.Add(this.LayerThumbnail);
            this.Controls.Add(this.isVisible);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Layer";
            this.Size = new System.Drawing.Size(482, 69);
            ((System.ComponentModel.ISupportInitialize)(this.LayerThumbnail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox isVisible;
        private System.Windows.Forms.PictureBox LayerThumbnail;
        private System.Windows.Forms.CheckBox Active;
        private System.Windows.Forms.Label ActiveLabel;
        private System.Windows.Forms.Button MoveLayerUp;
        private System.Windows.Forms.Button MoveLayerDown;
        private System.Windows.Forms.Label LayerLabel;
    }
}
