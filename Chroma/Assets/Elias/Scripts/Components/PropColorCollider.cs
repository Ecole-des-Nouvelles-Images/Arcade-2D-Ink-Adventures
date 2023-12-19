using System;
using Elias.Scripts.Helper;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Elias.Scripts.Components
{
    public class PropColorCollider : MonoBehaviour
    {
        private Light2D _objectLight;
        private GameObject _playerGameObject;
        private Light2D _playerLight;
        

        private void Start()
        {
            _objectLight = GetComponent<Light2D>();
            _playerGameObject = GameObject.FindGameObjectWithTag("Player");
            _playerLight = _playerGameObject.GetComponent<Light2D>();

        
        }

        private void Update()
        {
            bool isPropCollide = ColorHelpers.Collide(_objectLight.color, _playerLight.color);

            Physics2D.IgnoreLayerCollision(gameObject.layer, _playerGameObject.layer, !isPropCollide);


            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log(_objectLight.color);
            }
            
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the other collider is the player
            if (other.gameObject.CompareTag("Player"))
            {
                bool isColliding = ColorHelpers.Collide(_objectLight.color, _playerLight.color);
                Debug.Log($"Trigger Enter: Collision Status = {isColliding}");
            }
        }

        // OnCollisionEnter2D is called when the Collider2D other enters the collider
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the other collider is the player
            if (collision.gameObject.CompareTag("Player"))
            {
                bool isColliding = ColorHelpers.Collide(_objectLight.color, _playerLight.color);
                Debug.Log($"Collision Enter: Collision Status = {isColliding}");
            }
        }
    }
}