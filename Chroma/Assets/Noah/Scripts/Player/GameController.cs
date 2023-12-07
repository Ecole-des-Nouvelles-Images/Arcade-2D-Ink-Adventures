using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float respawnTime = 0.1f;
    private Vector2 _checkpointPos;
    private Rigidbody2D _playerRb;
    
    private void Start()
    {
        _checkpointPos = transform.position;
    }
    
    public void UpdateCheckpoint(Vector2 pos)
    {
        _checkpointPos = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(Respawn(respawnTime));
    }

    IEnumerator Respawn(float duration)
    {
        transform.localScale = new Vector3(0, 0, 0);
        transform.position = _checkpointPos; 
        yield return new WaitForSeconds(duration);
        transform.localScale = new Vector3(1, 1, 1);
    }
    
}
