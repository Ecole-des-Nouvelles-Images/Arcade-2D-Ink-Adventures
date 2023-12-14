using Elias.Scripts.Managers;
using UnityEngine;

namespace Elias.Scripts.Data
{
    public class ColorUnlock : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag("Player")) return;
            if (CompareTag("BlueUpgrade"))
            {
                GameManager.Instance.hasColorUpgradeB = true;
            }
            else
            {
                GameManager.Instance.hasColorUpgradeG = true;
            }
            Destroy(gameObject);
        }
    }
}
