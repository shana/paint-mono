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
    public sealed class ZoomBlurEffect
        : Effect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("ZoomBlurEffect.Name");
            }
        }

        public static ImageResource StaticImage
        {
            get
            {
                return ImageResource.Get("Icons.ZoomBlurEffect.png");
            }
        }

        public ZoomBlurEffect()
            : base(StaticName,
                   StaticImage.Reference,
                   PdnResources.GetString("Effects.Blurring.Submenu.Name"),
                   EffectDirectives.None,
                   true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            AmountEffectConfigDialog oacd = new AmountEffectConfigDialog();

            oacd.Text = StaticName;
            oacd.SliderLabel = PdnResources.GetString("ZoomBlurEffect.ConfigDialog.AmountLabel");
            oacd.SliderMaximum = 100;
            oacd.SliderMinimum = 0;
            oacd.SliderInitialValue = 10;
            oacd.Icon = PdnResources.GetIconFromImage("Icons.ZoomBlurEffect.png");

            return oacd;
        }

        public unsafe override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, 
            Rectangle[] rois, int startIndex, int length)
        {
            AmountEffectConfigToken token = (AmountEffectConfigToken)parameters;
            Surface dst = dstArgs.Surface;
            Surface src = srcArgs.Surface;
            long w = dst.Width;
            long h = dst.Height;
            long fcx = w << 15;
            long fcy = h << 15;
            long fz = token.Amount;
            
            for (int r = startIndex; r < startIndex + length; ++r)
            {
                Rectangle rect = rois[r];

                for (int y = rect.Top; y < rect.Bottom; ++y)
                {
                    ColorBgra *dstPtr = dst.GetPointAddressUnchecked(rect.Left, y);
                    ColorBgra *srcPtr = src.GetPointAddressUnchecked(rect.Left, y);

                    for (int x = rect.Left; x < rect.Right; ++x)
                    {
                        long fx = (x << 16) - fcx;
                        long fy = (y << 16) - fcy;
                        const int n = 64;

                        int sr = 0;
                        int sg = 0;
                        int sb = 0;
                        int sa = 0;
                        int sc = 0;

                        sr += srcPtr->R * srcPtr->A;
                        sg += srcPtr->G * srcPtr->A;
                        sb += srcPtr->B * srcPtr->A;
                        sa += srcPtr->A;
                        ++sc;

                        for (int i = 0; i < n; ++i)
                        {
                            fx -= ((fx >> 4) * fz) >> 10;
                            fy -= ((fy >> 4) * fz) >> 10;

                            int u = (int)(fx + fcx + 32768 >> 16);
                            int v = (int)(fy + fcy + 32768 >> 16);

                            ColorBgra *srcPtr2 = src.GetPointAddressUnchecked(u, v);

                            sr += srcPtr2->R * srcPtr2->A;
                            sg += srcPtr2->G * srcPtr2->A;
                            sb += srcPtr2->B * srcPtr2->A;
                            sa += srcPtr2->A;
                            ++sc;
                        }
                 
                        if (sa != 0)
                        {
                            *dstPtr = ColorBgra.FromBgra(
                                Utility.ClampToByte(sb / sa),
                                Utility.ClampToByte(sg / sa),
                                Utility.ClampToByte(sr / sa),
                                Utility.ClampToByte(sa / sc));
                        }
                        else
                        {
                            dstPtr->Bgra = 0;
                        }

                        ++srcPtr;
                        ++dstPtr;
                    }
                }
            }                       
        }
    }
}
