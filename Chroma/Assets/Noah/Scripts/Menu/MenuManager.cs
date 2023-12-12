using Noah.Scripts.Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject _mainMenuCanvasGO;
    [SerializeField] private GameObject _settingsMenuCanvasGO;

    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private PlayerController player;

    [Header("First Selected Options")] 
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;



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
        player.enabled = false;
        OpenMainMenu();
    }

    private void Unpause()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        player.enabled = true;
        CloseAllMenus();

    }
    #endregion
    

    #region Canvas Activations

    private void OpenMainMenu()
    {
        _mainMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
    
    private void OpenSettingsMenuHandle()
    {
        _settingsMenuCanvasGO.SetActive(true);
        _mainMenuCanvasGO.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);

    }

    private void CloseAllMenus()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    #endregion

    #region Main Menu Button Actions

    public void OnSettingsPress()
    {
        OpenSettingsMenuHandle();
    }

    public void OnResumePress()
    {
        Unpause();
    }
    #endregion

    #region Settings Menu Button Actions

    public void OnSettingsBackPress()
    {
        OpenMainMenu();
    }
    #endregion

}
