/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    /// <summary>
    /// Provided for compatibility with v1.1.
    /// Normally you should just fill in the properties for the TwoAmountsConfigDialog.
    /// </summary>
    public sealed class BrightnessAndContrastAdjustmentConfigDialog 
        : TwoAmountsConfigDialogBase
    {
        public BrightnessAndContrastAdjustmentConfigDialog()
        {
            this.Amount1Label = PdnResources.GetString("BrightnessAndContrastAdjustment.Brightness");
            this.Amount1Default = 0;
            this.Amount1Minimum = -100;
            this.Amount1Maximum = +100;

            this.Amount2Label = PdnResources.GetString("BrightnessAndContrastAdjustmnet.Contrast");
            this.Amount2Default = 0;
            this.Amount2Minimum = -100;
            this.Amount2Maximum = +100;

            this.Text = BrightnessAndContrastAdjustment.StaticName;
        }

        protected override void InitialInitToken()
        {
            theEffectToken = new BrightnessAndContrastAdjustmentConfigToken(0, 0);
        }
    }
}

