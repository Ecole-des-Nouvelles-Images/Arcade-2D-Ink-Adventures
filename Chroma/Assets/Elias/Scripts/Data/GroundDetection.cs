using System;
using UnityEngine;

namespace Elias.Scripts.Data
{
    public class GroundDetection : MonoBehaviour {

        public static bool IsCollided;

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Ground"))
            {
                IsCollided = false;
            }
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (other.CompareTag("Ground"))
            {
                IsCollided = true;
            }
        }
    }
}