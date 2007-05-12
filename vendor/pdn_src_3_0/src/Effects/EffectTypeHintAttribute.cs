/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet.Effects
{
    /// <summary>
    /// Tags an effect with a specific EffectTypeHint.
    /// </summary>
    public class EffectTypeHintAttribute
        : Attribute
    {
        private EffectTypeHint effectTypeHint;
        public EffectTypeHint EffectTypeHint
        {
            get
            {
                return effectTypeHint;
            }
        }

        public EffectTypeHintAttribute(EffectTypeHint effectTypeHint)
        {
            this.effectTypeHint = effectTypeHint;
        }
    }
}
