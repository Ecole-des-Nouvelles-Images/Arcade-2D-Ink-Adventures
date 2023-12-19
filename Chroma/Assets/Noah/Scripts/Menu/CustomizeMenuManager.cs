using System;
using System.Collections;
using System.Collections.Generic;
using Noah.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

public class CustomizeMenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject _customizeMenuGO;
    
    [Header("First Selected Options")] 
    [SerializeField] private GameObject _customizeMenuFirst;
    
    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private PlayerController player;
    
    private bool _isPaused;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pause();
            OpenMenu();
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Unpause();
            CloseMenu();
        } 
    }

    public void OnCustomValidate()
    {
        Unpause();
        CloseMenu();
    }
    
    private void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        PlayerController.instance.enabled = false;
    }

    private void Unpause()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        PlayerController.instance.enabled = true;
    }

    private void OpenMenu()
    {
        _customizeMenuFirst.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_customizeMenuFirst);
    }

    private void CloseMenu()
    {
        _customizeMenuFirst.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
