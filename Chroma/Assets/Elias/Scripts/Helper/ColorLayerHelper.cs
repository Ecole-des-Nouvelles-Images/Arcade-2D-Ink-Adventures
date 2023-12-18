using UnityEngine;

namespace Elias.Scripts.Helper
{
    public static class ColorLayerHelper
    {
        public static bool ShouldIgnoreCollision(string layer1, string layer2)
        {
            if (layer1 == "Player" || layer2 == "Player")
            {
                return false;
            }
            
            Color color1 = GetColorByLayer(layer1);
            Color color2 = GetColorByLayer(layer2);

            if ((color1 == Color.red && (color2 == Color.blue || color2 == Color.green || color2 == Color.cyan)) ||
                (color1 == Color.blue && (color2 == Color.red || color2 == Color.green || color2 == Color.yellow)) ||
                (color1 == Color.green && (color2 == Color.blue || color2 == Color.red || color2 == Color.magenta)) ||
                (color1 == Color.yellow && (color2 == Color.blue || color2 == Color.magenta || color2 == Color.cyan)) ||
                (color1 == Color.magenta && (color2 == Color.green || color2 == Color.yellow || color2 == Color.cyan)) ||
                (color1 == Color.cyan && (color2 == Color.red || color2 == Color.yellow || color2 == Color.magenta)))
            {
                return true;
            }

            return false;
        }

        private static Color GetColorByLayer(string layer)
        {
            switch (layer)
            {
                case "ObjectRed":
                    return Color.red;
                case "ObjectBlue":
                    return Color.blue;
                case "ObjectGreen":
                    return Color.green;
                case "ObjectYellow":
                    return Color.yellow;
                case "ObjectMagenta":
                    return Color.magenta;
                case "ObjectCyan":
                    return Color.cyan;
                default:
                        Debug.LogWarning("Unsupported layer: " + layer);
                    return Color.white;
            }
        }
    }
}
