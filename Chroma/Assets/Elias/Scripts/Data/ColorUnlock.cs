using Elias.Scripts.Managers;
using UnityEngine;

namespace Elias.Scripts.Data
{
    public class ColorUnlock : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag("Player")) return;

            if (CompareTag("RedUpgrade"))
            {
                GameManager.Instance.hasColorUpgradeR = true;
            }
            if (CompareTag("BlueUpgrade"))
            {
                GameManager.Instance.hasColorUpgradeB = true;
            }
            if (CompareTag("GreenUpgrade"))
            {
                GameManager.Instance.hasColorUpgradeG = true;
            }
            Destroy(gameObject);
        }
    }
}
