using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Input = UnityEngine.Windows.Input;

namespace Elias.Scripts.Components
{
    public class PlayerColor : MonoBehaviour {
        
        public List<Color> switchableColors = new List<Color>();

        private Light2D _playerLight;

        private void Awake() {
            _playerLight = GetComponent<Light2D>();
        }

        private void Update() {
            InputSwitchColor();
        }

        private void OnCollisionEnter2D(Collision2D other) {
             if (!other.collider.CompareTag("Upgrader")) return;
             switchableColors.Add(other.gameObject.GetComponent<Light2D>().color);
             Destroy(other.gameObject); 
        }
        
        private void InputSwitchColor()
        {
            if (InputManager.instance.RedLightJustPressed && switchableColors.Contains(Color.red)) {
                _playerLight.color = GetColor(InputManager.instance.GreenLightBeingHeld, 
                        Color.yellow, InputManager.instance.BlueLightBeingHeld, 
                        Color.magenta, Color.red);
            }

            if (InputManager.instance.GreenLightJustPressed && switchableColors.Contains(Color.green)) {
                _playerLight.color = GetColor(InputManager.instance.BlueLightBeingHeld, 
                    Color.cyan, InputManager.instance.RedLightBeingHeld, 
                    Color.yellow, Color.green);
            }
            
            if (InputManager.instance.BlueLightJustPressed && switchableColors.Contains(Color.blue)) {
                _playerLight.color = GetColor(InputManager.instance.RedLightBeingHeld, 
                    Color.magenta, InputManager.instance.GreenLightBeingHeld, 
                    Color.cyan, Color.blue);
            }
        }

        private Color GetColor(bool secondKey, Color colorIfBothPressed, bool thirdKey, Color colorIfThirdPressed, Color defaultColor) {
            if (secondKey)
                return colorIfBothPressed; 
            if (thirdKey)
                return colorIfThirdPressed;
                
            return defaultColor;
        }
    }
}