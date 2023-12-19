using Elias.Scripts.Helper;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Elias.Scripts.Components
{
    public class PropColorCollider : MonoBehaviour
    {
        private readonly float _colorTolerance = 4f;
        private Color _currentColor;
        private string _objectLayer;
        private Light2D _objectLight;
        private Rigidbody2D _objectRigidbody;
        private GameObject _playerGameObject;
        private LayerMask _playerLayer;
        private Light2D _playerLight;

        private void Start()
        {
            _objectRigidbody = GetComponent<Rigidbody2D>();
            _objectLight = GetComponent<Light2D>();
            _playerGameObject = GameObject.FindGameObjectWithTag("Player");
            _playerLight = _playerGameObject.GetComponent<Light2D>();
            _playerLayer = LayerMask.NameToLayer("Player");

            _objectLayer = LayerMask.LayerToName(gameObject.layer);
            _currentColor = _objectLight.color;
        }

        private void Update()
        {
            bool isPropCollide = ColorHelpers.Collide(_playerLight.color, _objectLight.color);
            Physics2D.IgnoreLayerCollision(_playerGameObject.layer, gameObject.layer, !isPropCollide);
        }
    }
}