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
    public class BrightnessAndContrastAdjustmentConfigToken
        : TwoAmountsConfigToken
    {
        public int Brightness
        {
            get
            {
                return Amount1;
            }

            set
            {
                this.Amount1 = value;
            }
        }

        public int Contrast
        {
            get
            {
                return Amount2;
            }

            set
            {
                this.Amount2 = value;
            }
        }

        private void Calculate()
        {
            lock (this)
            {
                if (calcBrightness != Brightness || calcContrast != Contrast)
                {
                    if (Contrast < 0) 
                    {
                        multiply = Contrast + 100;
                        divide = 100;
                    } 
                    else if (Contrast > 0) 
                    {
                        multiply = 100;
                        divide = 100 - Contrast;
                    } 
                    else 
                    {
                        multiply = 1;
                        divide = 1;
                    }

                    if (rgbTable == null)
                    {
                        rgbTable = new byte[65536];
                    }

                    if (divide == 0)
                    {
                        for (int intensity = 0; intensity < 256; ++intensity)
                        {
                            if (intensity + Brightness < 128)
                            {
                                rgbTable[intensity] = 0;
                            }
                            else
                            {
                                rgbTable[intensity] = 255;
                            }
                        }
                    }
                    else if (divide == 100)
                    {
                        for (int intensity = 0; intensity < 256; ++intensity)
                        {
                            int shift = (intensity - 127) * multiply / divide + 127 - intensity + Brightness;

                            for (int col = 0; col < 256; ++col)
                            {
                                int index = (intensity * 256) + col;
                                rgbTable[index] = Utility.ClampToByte(col + shift);
                            }
                        }
                    }
                    else
                    {
                        for (int intensity = 0; intensity < 256; ++intensity)
                        {
                            int shift = (intensity - 127 + Brightness) * multiply / divide + 127 - intensity;

                            for (int col = 0; col < 256; ++col)
                            {
                                int index = (intensity * 256) + col;
                                rgbTable[index] = Utility.ClampToByte(col + shift);
                            }
                        }
                    }

                    this.calcBrightness = Brightness;
                    this.calcContrast = Contrast;
                }
            }
        }

        private int multiply;
        internal int Multiply
        {
            get
            {
                Calculate();
                return this.multiply;
            }
        }

        private int divide;
        internal int Divide
        {
            get
            {
                Calculate();
                return this.divide;
            }
        }
        
        private byte[] rgbTable;
        internal byte[] RgbTable
        {
            get
            {
                Calculate();
                return this.rgbTable;
            }
        }

        private int calcBrightness;
        private int calcContrast;

        public BrightnessAndContrastAdjustmentConfigToken(int brightness, int contrast)
            : base(brightness, contrast)
        {
            // valid range for B or C is [-100,+100] so we just need an 'invalid' value to make
            // Calculate() say, 'hey I don't have the right values here, let me recalculate'
            this.calcBrightness = -10000;
            this.calcContrast = -10000;
        }

        public override object Clone()
        {
            return new BrightnessAndContrastAdjustmentConfigToken(this);
        }

        public BrightnessAndContrastAdjustmentConfigToken(BrightnessAndContrastAdjustmentConfigToken copyMe)
            : base(copyMe)
        {
            copyMe.Calculate();
            this.calcContrast = copyMe.calcContrast;
            this.calcBrightness = copyMe.calcBrightness;
            this.multiply = copyMe.multiply;
            this.divide = copyMe.divide;
            this.rgbTable = copyMe.rgbTable;
        }
    }
}
