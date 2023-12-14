using System;
using System.Collections;
using System.Collections.Generic;
using Noah.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

public class Ladder : MonoBehaviour
{
    [FormerlySerializedAs("playerControllerElias")] [SerializeField] private PlayerController playerController;
    
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
