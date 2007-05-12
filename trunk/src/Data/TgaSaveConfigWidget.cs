/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace PaintDotNet.Data
{
    public sealed class TgaSaveConfigWidget 
        : SaveConfigWidget
    {
        private System.Windows.Forms.RadioButton bpp24Radio;
        private System.Windows.Forms.RadioButton bpp32Radio;
        private System.Windows.Forms.Label bppLabel;
        private System.Windows.Forms.CheckBox rleCompressCheckBox;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public TgaSaveConfigWidget()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.bpp24Radio.Text = PdnResources.GetString("TgaSaveConfigWidget.Bpp24Radio.Text");
            this.bpp32Radio.Text = PdnResources.GetString("TgaSaveConfigWidget.Bpp32Radio.Text");
            this.bppLabel.Text = PdnResources.GetString("TgaSaveConfigWidget.BppLabel.Text");
            this.rleCompressCheckBox.Text = PdnResources.GetString("TgaSaveConfigWidget.RleCompressCheckBox.Text");
        }

        protected override void InitFileType()
        {
            this.fileType = new TgaFileType();
        }

        protected override void InitTokenFromWidget()
        {
            int bitDepth;

            if (this.bpp24Radio.Checked)
            {
                bitDepth = 24;
            }
            else
            {
                bitDepth = 32;
            }

            ((TgaSaveConfigToken)this.Token).BitDepth = bitDepth;
            ((TgaSaveConfigToken)this.token).RleCompress = this.rleCompressCheckBox.Checked;
        }

        protected override void InitWidgetFromToken(SaveConfigToken token)
        {
            TgaSaveConfigToken tgaToken = (TgaSaveConfigToken)token;

            if (tgaToken.BitDepth == 24)
            {
                this.bpp24Radio.Checked = true;
                this.bpp32Radio.Checked = false;
            }
            else
            {
                this.bpp24Radio.Checked = false;
                this.bpp32Radio.Checked = true;
            }

            this.rleCompressCheckBox.Checked = tgaToken.RleCompress;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.bpp24Radio = new System.Windows.Forms.RadioButton();
            this.bpp32Radio = new System.Windows.Forms.RadioButton();
            this.rleCompressCheckBox = new System.Windows.Forms.CheckBox();
            this.bppLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bpp24Radio
            // 
            this.bpp24Radio.Location = new System.Drawing.Point(16, 48);
            this.bpp24Radio.Name = "bpp24Radio";
            this.bpp24Radio.Size = new System.Drawing.Size(168, 24);
            this.bpp24Radio.TabIndex = 2;
            this.bpp24Radio.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
            // 
            // bpp32Radio
            // 
            this.bpp32Radio.Location = new System.Drawing.Point(16, 71);
            this.bpp32Radio.Name = "bpp32Radio";
            this.bpp32Radio.Size = new System.Drawing.Size(168, 24);
            this.bpp32Radio.TabIndex = 3;
            this.bpp32Radio.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
            // 
            // rleCompressCheckBox
            // 
            this.rleCompressCheckBox.Location = new System.Drawing.Point(0, 0);
            this.rleCompressCheckBox.Name = "rleCompressCheckBox";
            this.rleCompressCheckBox.Size = new System.Drawing.Size(184, 24);
            this.rleCompressCheckBox.TabIndex = 0;
            this.rleCompressCheckBox.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
            // 
            // bppLabel
            // 
            this.bppLabel.Location = new System.Drawing.Point(0, 32);
            this.bppLabel.Name = "bppLabel";
            this.bppLabel.Size = new System.Drawing.Size(184, 16);
            this.bppLabel.TabIndex = 1;
            // 
            // TgaSaveConfigWidget
            // 
            this.Controls.Add(this.bppLabel);
            this.Controls.Add(this.rleCompressCheckBox);
            this.Controls.Add(this.bpp32Radio);
            this.Controls.Add(this.bpp24Radio);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "TgaSaveConfigWidget";
            this.Size = new System.Drawing.Size(180, 104);
            this.ResumeLayout(false);

        }
        #endregion

        private void OnCheckedChanged(object sender, System.EventArgs e)
        {
            this.UpdateToken();
        }
    }
}
