using System;
using System.Collections;
using System.Collections.Generic;
using Noah.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;
using PlayerControllerElias = Elias.Scripts.Data.PlayerControllerElias;

public class Ladder : MonoBehaviour
{
    [FormerlySerializedAs("playerController")] [SerializeField] private PlayerControllerElias playerControllerElias;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerControllerElias.IsClimbing = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerControllerElias.IsClimbing = false;
        }  
    }
}
