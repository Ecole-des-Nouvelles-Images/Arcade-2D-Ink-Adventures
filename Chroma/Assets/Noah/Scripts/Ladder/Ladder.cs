using Noah.Scripts.Player;
using UnityEngine;
using PlayerController = Noah.Scripts.Player.PlayerController;

namespace Noah.Scripts.Ladder
{
    public class Ladder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                 PlayerController.instance.IsClimbing = true;
            }        
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController.instance.IsClimbing = false;
            }  
        }
    }
}
