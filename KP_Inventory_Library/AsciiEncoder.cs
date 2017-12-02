using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KP_Inventory_Library
{
    public static class AsciiEncoder
    {
        public static int convertToAscii(string Text)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;
            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(Text);
            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            return int.Parse(asciiBytes[0].ToString());
        }
    }
}
