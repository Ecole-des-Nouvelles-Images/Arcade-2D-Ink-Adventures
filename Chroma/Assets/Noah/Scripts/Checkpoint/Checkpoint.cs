using System;
using Noah.Scripts.Player;
using UnityEngine;

namespace Noah.Scripts.Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private Sprite passive, active;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _coll;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _coll = GetComponent<Collider2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameController.instance.UpdateCheckpoint(respawnPoint.position);
                _coll.enabled = false;
            }
        }
    }
    
}
