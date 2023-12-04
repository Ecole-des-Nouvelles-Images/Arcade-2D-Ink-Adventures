using System;
using UnityEngine;

namespace Noah.Scripts.Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {

        [SerializeField] private Transform respawnPoint;
        [SerializeField] private Sprite passive, active;
        private GameController _gameController;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _coll;

        private void Awake()
        {
            _gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _coll = GetComponent<Collider2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _gameController.UpdateCheckpoint(respawnPoint.position);
                _coll.enabled = false;
            }
        }
    }
    
}
