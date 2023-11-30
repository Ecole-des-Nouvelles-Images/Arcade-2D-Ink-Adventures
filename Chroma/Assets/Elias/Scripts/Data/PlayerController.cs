using System;
using System.Collections;
using System.Collections.Generic;
using Helper;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Data
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Light2D playerLight;
         
        public event Action<Color> OnColorChange;
        private void Update()
        {
            CheckInput();
        }
        private void CheckInput()
        {
            if (!Input.anyKeyDown)
                return;
            
            foreach (KeyCode key in PlayerInputs.InputList)
            {
                if (Input.GetKeyDown(key))
                {
                    switch (key)
                    {
                        case KeyCode.R:
                            if (Input.GetKey(KeyCode.G))
                                ChangeColor(Color.yellow);
                            else if (Input.GetKey(KeyCode.B))
                                ChangeColor(Color.magenta);
                            else
                                ChangeColor(Color.red);
                            break;

                        case KeyCode.G:
                            if (Input.GetKey(KeyCode.B))
                                ChangeColor(Color.cyan);
                            else if (Input.GetKey(KeyCode.R))
                                ChangeColor(Color.yellow);
                            else
                                ChangeColor(Color.green);
                            break;

                        case KeyCode.B:
                            if (Input.GetKey(KeyCode.R))
                                ChangeColor(Color.magenta);
                            else if (Input.GetKey(KeyCode.G))
                                ChangeColor(Color.cyan);
                            else
                                ChangeColor(Color.blue);
                            break;
                    }
                }
            }
        } 
        public void ChangeColor(Color newColor)
        {
            playerLight.color = newColor;
            OnColorChange?.Invoke(newColor);
        }

    }
}
