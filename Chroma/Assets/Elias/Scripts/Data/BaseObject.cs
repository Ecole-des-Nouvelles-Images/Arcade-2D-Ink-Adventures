using Elias.Scripts.Managers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Elias.Scripts.Data
{
    public class BaseObject : MonoBehaviour
    {
        private Light2D _objectLight;
        private Rigidbody2D _objectRigidbody;
        private LayerMask _objectLayer;
        private LayerMask _playerLayer;
        //private LayerMask _collisionLayer; // New layer for object collisions
        private float _colorTolerance = 3f;

        void Start()
        {
            _objectRigidbody = GetComponent<Rigidbody2D>();
            _objectLight = GetComponent<Light2D>();
            _playerLayer = LayerMask.NameToLayer("Player");
            _objectLayer = LayerMask.NameToLayer("Object");
            //_collisionLayer = LayerMask.GetMask("Object");
            // Check if the layers are valid
            if (_playerLayer == -1 || _objectLayer == -1)
            {
                Debug.LogError("Player or Object layer not found. Make sure the layers exist in the project settings.");
            }

            FindObjectOfType<PlayerController>().OnColorChange += HandleColorChange;
        }

        private void HandleColorChange(Color newColor)
        {
            
            if (GameManager.Instance.AreColorsClose(newColor, this._objectLight.color, _colorTolerance))
            {
                if (_objectRigidbody != null)
                {
                    // Enable collisions with player and objects
                    Physics2D.IgnoreLayerCollision(_objectLayer, _playerLayer, false);
                    //Physics2D.IgnoreLayerCollision(_objectLayer, _collisionLayer, false);
                    Debug.Log("Je collide !");
                }
            }
            else
            {
                if (_objectRigidbody != null)
                {
                    // Disable collisions with player and objects
                    Physics2D.IgnoreLayerCollision(_objectLayer, _playerLayer, true);
                    //Physics2D.IgnoreLayerCollision(_objectLayer, _collisionLayer, true);
                    Debug.Log("Et non !");
                }
            }
        }
    }
}