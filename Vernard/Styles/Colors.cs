using System.Drawing;
using Color = Windows.UI.Color;

namespace Vernard.Styles
{
    internal static class Colors
    {
        internal static readonly Color Slate = FromHex("#94a3b8");
        internal static readonly Color Red = FromHex("#b91c1c");
        internal static readonly Color Amber = FromHex("#b45309");
        internal static readonly Color Green = FromHex("#15803d");
        internal static readonly Color Blue = FromHex("#1d4ed8");
        internal static readonly Color Purple = FromHex("#7e22ce");

        private static Color FromHex(string hexColor)
        {
            var drawingColor = ColorTranslator.FromHtml(hexColor);
            return Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        }
    }
}
