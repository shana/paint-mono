/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace PaintDotNet.SystemLayer
{
    /// <summary>
    /// Static methods related to font handling.
    /// </summary>
    public static class Fonts
    {
        /// <summary>
        /// Determines whether a font uses the 'symbol' character set.
        /// </summary>
        /// <remarks>
        /// Symbol fonts do not typically contain glyphs that represent letters of the alphabet.
        /// Instead they might contain pictures and symbols. As such, they are not useful for
        /// drawing text. Which means you can't use a symbol font to write out its own name for
        /// illustrative purposes (like for the font drop-down chooser).
        /// </remarks>
        public static bool IsSymbolFont(Font font)
        {
		// Could be improved, right now, we dont even bother looking up the font.
		return false;
        }

        private static IntPtr CreateFontObject(Font font, bool antiAliasing)
        {
	    // FIXME: 
	    // this currently ignores antiAliasing
	    // low-priority, as this method is never actually used
		
		return font.ToHfont ();
        }

        /// <summary>
        /// Measures text with the given graphics context, font, string, location, and anti-aliasing flag.
        /// </summary>
        /// <param name="g">The Graphics context to measure for.</param>
        /// <param name="font">The Font to measure with.</param>
        /// <param name="text">The string of text to measure.</param>
        /// <param name="antiAliasing">Whether the font should be rendered with anti-aliasing.</param>
        public static Size MeasureString(Graphics g, Font font, string text, bool antiAliasing, FontSmoothing fontSmoothing)
        {
                PixelOffsetMode oldPOM = g.PixelOffsetMode;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                TextRenderingHint oldTRH = g.TextRenderingHint;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                StringFormat format = (StringFormat)StringFormat.GenericTypographic.Clone();
                format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

                SizeF sf = g.MeasureString(text, font, new PointF(0, 0), format);
                sf.Height = font.GetHeight();

                g.PixelOffsetMode = oldPOM;
                g.TextRenderingHint = oldTRH;
                return Size.Ceiling(sf);
        }

        /// <summary>
        /// Renders text with the given graphics context, font, string, location, and anti-aliasing flag.
        /// </summary>
        /// <param name="g">The Graphics context to render to.</param>
        /// <param name="font">The Font to render with.</param>
        /// <param name="text">The string of text to draw.</param>
        /// <param name="pt">The offset of where to start drawing (upper-left of rendering rectangle).</param>
        /// <param name="antiAliasing">Whether the font should be rendered with anti-aliasing.</param>
        public static void DrawText(Graphics g, Font font, string text, Point pt, bool antiAliasing, FontSmoothing fontSmoothing)
        {
                PixelOffsetMode oldPOM = g.PixelOffsetMode;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                TextRenderingHint oldTRH = g.TextRenderingHint;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                StringFormat format = (StringFormat)StringFormat.GenericTypographic.Clone();
                format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

                g.DrawString(text, font, Brushes.Black, pt, format);

                g.PixelOffsetMode = oldPOM;
                g.TextRenderingHint = oldTRH;
        }
    }
}
