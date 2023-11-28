using System;
using UnityEngine;

namespace Noah.Scripts.Input
{
    public class UserInput : MonoBehaviour
    {
        public static UserInput Instance;
        
        [HideInInspector] public Controls Controls;
        [HideInInspector] public Vector3 MoveInput;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            else
            {
                Destroy(gameObject);
            }

            Controls = new Controls();

            Controls.Movement.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        }
        private void OnEnable()
        {
            Controls.Enable();
        }
        private void OnDisable()
        {
            Controls.Disable();
        }
    }
}
