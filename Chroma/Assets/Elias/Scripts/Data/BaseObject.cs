using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Data
{
    public class BaseObject : MonoBehaviour
    {
        //public int playerID;
        private Light2D _objectLight;
        private Rigidbody2D _objectRigidbody;
        private LayerMask playerLayer;
        void Start()
        {
            _objectRigidbody = GetComponent<Rigidbody2D>();
            _objectLight = GetComponent<Light2D>();
            
            playerLayer = LayerMask.GetMask("Player");
            
            FindObjectOfType<PlayerController>().OnColorChange += HandleColorChange;
            
            //objectColor = GetColorByID(playerID);
        }
        private void HandleColorChange(Color newColor)
        {
            if (newColor == _objectLight.color)
            {
                if (_objectRigidbody != null)
                {
                    // Exclude the player layer
                    Physics2D.IgnoreLayerCollision(gameObject.layer, playerLayer,true);

                    Debug.Log("Je collide !");
                }
            }
            else
            {
                if (_objectRigidbody != null)
                {
                    // Reset collision detection mode and layer collision mask
                    Physics2D.IgnoreLayerCollision(gameObject.layer, playerLayer,false);

                    Debug.Log("Et non !");
                }
            }
        }
        /*private static Color GetColorByID(int id)
        {
            switch (id)
            {
                case 1:
                    return Color.red;
                case 2:
                    return Color.green;
                case 3:
                    return Color.blue;
                case 4:
                    return Color.yellow;
                case 5:
                    return Color.magenta;
                case 6:
                    return Color.cyan;
                default:
                    return Color.white;
            }
        }*/
    }
}

