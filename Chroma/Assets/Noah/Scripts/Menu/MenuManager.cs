using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvasGO;
    [SerializeField] private GameObject _settingsMenuCanvasGO;

    private bool _isPaused;

    private void Start()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.instance.MenuOpenCloseInput)
        {
            if (!_isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }
    
    #region Pause/Unpause Functions
    private void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        OpenMainMenu();
    }

    private void Unpause()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        CloseAllMenus();

    }
    #endregion
    
    #region Canvas Activations

    private void OpenMainMenu()
    {
        _mainMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);
    }

    private void CloseAllMenus()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
    }
    
    #endregion

}
