/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    /// <summary>
    /// Presents an Effect that can present a user interface that allows the user
    /// to configure the properties of an effect. These properties can be saved
    /// and used to later actually apply the effect.
    /// In order to configure an affect, you must instantiate the class' concrete
    /// EffectConfigDialog class using CreateConfigDialog. You can then
    /// load in an EffectConfigToken that you had saved earlier, if you wish,
    /// otherwise it will use the default values. Once the dialog finishes, you
    /// can then retrieve the (possibly modified) EffectConfigToken and use 
    /// it in a call to the new 4-parameter Render method. 
    /// </summary>
    [Obsolete("Just use the appropriate Effect overrides")]
    public interface IConfigurableEffect
    {
        EffectConfigDialog CreateConfigDialog();
        void Render(EffectConfigToken properties, RenderArgs dstArgs, RenderArgs srcArgs, PdnRegion roi);
    }
}
