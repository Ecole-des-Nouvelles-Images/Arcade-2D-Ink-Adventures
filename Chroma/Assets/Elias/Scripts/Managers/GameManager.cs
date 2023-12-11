using Core;
using System.Collections;
using UnityEngine;

namespace Elias.Scripts.Managers
{

    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public bool hasColorUpgradeG = false;
        public bool hasColorUpgradeB = false;

        public bool AreColorsClose(Color color1, Color color2, float toleranceMultiplier)
        {
            // Calculate the brightness of each color
            float brightness1 = (color1.r + color1.g + color1.b) / 3f;
            float brightness2 = (color2.r + color2.g + color2.b) / 3f;

            // Calculate the maximum allowable difference based on brightness
            float maxDifference = Mathf.Lerp(0.1f, 0.5f, Mathf.Max(brightness1, brightness2));

            // Calculate the actual color difference
            float colorDifference = Mathf.Abs(color1.r - color2.r) +
                                    Mathf.Abs(color1.g - color2.g) +
                                    Mathf.Abs(color1.b - color2.b);

            // Check if the color difference is within the allowable range
            return colorDifference <= maxDifference * toleranceMultiplier;
        }

    }
}

