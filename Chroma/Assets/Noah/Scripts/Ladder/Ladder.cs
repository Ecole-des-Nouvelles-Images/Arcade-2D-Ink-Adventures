using Noah.Scripts.Player;
using UnityEngine;
using PlayerController = Noah.Scripts.Player.PlayerController;

namespace Noah.Scripts.Ladder
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                 playerController.IsClimbing = true;
            }        
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerController.IsClimbing = false;
            }  
        }
    }
}
