/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    public sealed class EmbossEffectConfigDialog 
        : AngleChooserConfigDialogBase
    {
        public EmbossEffectConfigDialog()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            this.Text = EmbossEffect.StaticName;
        }

        // create default config token with angle 45 degress
        protected override void InitialInitToken()
        {
            theEffectToken = new EmbossEffectConfigToken(45);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // EmbossEffectConfigDialog
            // 
            this.Name = "EmbossEffectConfigDialog";
            //this.Icon = PdnResources.GetIconFromImage("Icons.EmbossEffect.png");

        }
        #endregion
    }
}
