using Elias.Scripts.Managers;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Elias.Scripts.Helper; // Import the helper namespace

namespace Elias.Scripts.Data
{
    public class BaseObject : MonoBehaviour
    {
        private Light2D _objectLight;
        private Rigidbody2D _objectRigidbody;
        private string _objectLayer; // Change to string to store layer name
        private LayerMask _playerLayer;
        private float _colorTolerance = 4f;

        private Color _currentColor;
        
        

        void Start()
        {
            _objectRigidbody = GetComponent<Rigidbody2D>();
            _objectLight = GetComponent<Light2D>();
            _playerLayer = LayerMask.NameToLayer("Player");

            _objectLayer = LayerMask.LayerToName(gameObject.layer); // Store the layer name instead of the index

            _currentColor = _objectLight.color;

            FindObjectOfType<PlayerController>().OnColorChange += HandleColorChange;
        }

        private void HandleColorChange(Color newColor)
        {
            // Check if the player and object colors are close
            bool areColorsClose = GameManager.Instance.AreColorsClose(newColor, _objectLight.color, _colorTolerance);

            // Check if the layers should ignore each other based on color
            bool shouldIgnoreCollision = ColorLayerHelper.ShouldIgnoreCollision(_objectLayer, "Player");

            if (areColorsClose && !shouldIgnoreCollision)
            {
                if (_objectRigidbody != null)
                {
                    Physics2D.IgnoreLayerCollision(gameObject.layer, _playerLayer, false);
                    Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, false);
                    Debug.Log("Je collide !");
                }
            }
            else
            {
                if (_objectRigidbody != null)
                {
                    Physics2D.IgnoreLayerCollision(gameObject.layer, _playerLayer, true);
                    Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
                    Debug.Log("Et non !");
                }
            }

            _currentColor = newColor;
        }
    }
}
