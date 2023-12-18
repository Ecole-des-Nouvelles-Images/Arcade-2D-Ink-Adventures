using Elias.Scripts.Helper;
using Elias.Scripts.Managers;
using Elias.Scripts.ObjectColorState;
using Noah.Scripts.Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Elias.Scripts.Data
{

    public class BaseObject : MonoBehaviour
    {
        private Light2D _objectLight;
        private Rigidbody2D _objectRigidbody;
        private string _objectLayer;
        private LayerMask _playerLayer;
        private readonly float _colorTolerance = 4f;
        private Color _currentColor;
        private ColorContext colorContext;

        void Start()
        {
            _objectRigidbody = GetComponent<Rigidbody2D>();
            _objectLight = GetComponent<Light2D>();
            _playerLayer = LayerMask.NameToLayer("Player");

            _objectLayer = LayerMask.LayerToName(gameObject.layer);
            _currentColor = _objectLight.color;

            FindObjectOfType<PlayerController>().OnColorChange += HandleColorChange;
            
            IColorState initialState = GetInitialState(_objectLayer);
            colorContext = new ColorContext(initialState);

            colorContext.SetupCollision(this);
        }

        private void HandleColorChange(Color newColor)
        {
            bool areColorsClose = GameManager.Instance.AreColorsClose(newColor, _objectLight.color, _colorTolerance);
            
            colorContext.SetupCollision(this);

            if (areColorsClose && !ColorLayerHelper.ShouldIgnoreCollision(_objectLayer, "Player"))
            {
                if (_objectRigidbody != null)
                {
                    Physics2D.IgnoreLayerCollision(gameObject.layer, _playerLayer, false);
                    Debug.Log("Je collide !");
                }
            }
            else
            {
                if (_objectRigidbody != null)
                {
                    Physics2D.IgnoreLayerCollision(gameObject.layer, _playerLayer, true);
                    Debug.Log("Et non !");
                }
            }

            _currentColor = newColor;
        }

        private IColorState GetInitialState(string layer)
        {
            switch (layer)
            {
                case "ObjectRed":
                    return new RedState();
                case "ObjectBlue":
                    return new BlueState();
                case "ObjectGreen":
                    return new GreenState();
                case "ObjectYellow":
                    return new YellowState();
                case "ObjectMagenta":
                    return new MagentaState();
                case "ObjectCyan":
                    return new CyanState();
                default:
                    Debug.LogWarning("Unsupported layer: " + layer);
                    return null;
            }
        }
        
        
        
    }
}
