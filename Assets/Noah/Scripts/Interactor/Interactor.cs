using Noah.Scripts.Player;
using UnityEngine;

namespace Noah.Scripts.Interactor
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                ActivateMechanism();
            }        
        }

        private void ActivateMechanism()
        {
            throw new System.NotImplementedException();
        }
    }
}
