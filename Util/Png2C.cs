using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace SharpSplatPrinter.Util
{
    /// <summary>
    /// This is my attempt at converting png2c.py to C#
    /// Some extra functions are not available for now (I'm so lazy!?)
    /// </summary>
    public static class Png2C
    {
        /// <summary>
        /// We can use this method to convert the binary png to the image.c file we need
        /// </summary>
        /// <param name="ImagePath">The file path to the .png file</param>
        /// <param name="Error">The error code to return</param>
        public static bool Convert(Bitmap Png, out byte Error)
        {
            if (Png == null)
            {
                Error = 1;
                return false;
            }

            if (Png.Height != 120 && Png.Width != 320)
            {
                Error = 2;
                return false;
            }

            Error = 0; // No errors! yay
            List<int> ColourData = new List<int>();

            for (int i = 0; i < 120; i++)
            {
                for (int j = 0; j < 320; j++)
                {
                    // Adds 0 if colour is white and 1 if it's not. (I think that's how it is?)
                    ColourData.Add(Png.GetPixel(j, i).ToArgb() == -1 ? 0 : 1);
                }
            }

            StringBuilder Sb = new StringBuilder("#include <stdint.h>\n#include <avr/pgmspace.h>\n\nconst uint8_t image_data[0x12c1] PROGMEM = {");

            for (int i = 0; i < (320 * 120) / 8; i++)
            {
                int Val = 0;

                for (int j = 0; j < 8; j++)
                {
                    Val |= ColourData[(i * 8) + j] << j;
                }

                Val = Val & 0xFF;

                Sb.Append(string.Format("0x{0:x}", Val));
                Sb.Append(", ");
            }

            Sb.Append("0x0};\n");
        
            //if (File.Exists("./util/image.c"))
            //{
            //    File.Delete("./util/image.c");
            //}

            File.WriteAllText("./util/image.c", Sb.ToString());
            return true;
        }
    }
}
