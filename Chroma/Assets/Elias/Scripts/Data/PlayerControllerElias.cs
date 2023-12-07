using System;
using System.Collections;
using Helper;
using Noah.Scripts.Camera;
using Noah.Scripts.Input;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Elias.Scripts.Data
{
    
    public class PlayerControllerElias : MonoBehaviour
    {
        [SerializeField] private Light2D playerLight;
        [HideInInspector] public bool IsClimbing;

        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 7.5f;
        
        [Header("Jump")]
        [SerializeField] private float _jumpForce = 12f;
        [SerializeField] private float _jumpTime = 0.35f;
        
        [Header("Turn Check")]
        [SerializeField] private GameObject _leftLeg;
        [SerializeField] private GameObject _rightLeg;

        [Header("Ground Check")] 
        [SerializeField] private float extraHeight = 0.25f;
        [SerializeField] private LayerMask _whatIsGround;
        
        [Header("Camera")] 
        [SerializeField] private GameObject _cameraFollowGO;
        
        [HideInInspector] public bool IsFacingRight;
        
        private bool _isFalling;
        private bool _isJumping;
        private float _jumpTimeCounter;
        
        private Rigidbody2D _rb;
        private Animator _anim;
        private Collider2D _coll;
        private RaycastHit2D _groundHit;
        
        private float _moveInputx;
        private float _moveInputy;

        public GroundDetection GroundDetection;


        private Coroutine _resetTriggerCoroutine;

        private CameraFollowObject _cameraFollowObject;
        private float _fallSpeedYDampingChangeThreshold;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>(); 
            //     _anim = GetComponent<Animator>();
            _coll = GetComponent<Collider2D>();
            _cameraFollowObject = _cameraFollowGO.GetComponent<CameraFollowObject>();
            StartDirectionCheck();
            _fallSpeedYDampingChangeThreshold = CameraManager.Instance._fallSpeedYDampingChangeThreshold;
        }

        private void FixedUpdate()
        {
            Climb();
            Move();
        }

        private void Update()
        {
            Jump();
            
            CheckInput();
            
            if (_rb.velocity.y < _fallSpeedYDampingChangeThreshold && !CameraManager.Instance.IsLerpingYDamping && !CameraManager.Instance.LerpedFromPlayerFalling)
            {
                CameraManager.Instance.LerpYDamping(true);
            }

            if (_rb.velocity.y >= 0f && !CameraManager.Instance.IsLerpingYDamping && CameraManager.Instance.LerpedFromPlayerFalling)
            {
                CameraManager.Instance.LerpedFromPlayerFalling = false;
                CameraManager.Instance.LerpYDamping(false);
            }
        }
        
        #region Jump Function
        private void Jump()
        {
            if (UserInput.Instance.Controls.InGame.Jump.WasPressedThisFrame() &&  (IsClimbing)) 
            {
                _isJumping = true;
                _jumpTimeCounter = _jumpTime;
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                
    //            _anim.SetTrigger("jump");
            }

            if (UserInput.Instance.Controls.InGame.Jump.IsPressed())
            {
                if (_jumpTimeCounter > 0 && _isJumping)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                    _jumpTimeCounter -= Time.deltaTime;
                }

                else if (_jumpTimeCounter == 0)
                {
                    _isFalling = true;
                    _isJumping = false;
                }

                else
                {
                    _isJumping = false;
                }
                
            }
            
            if (UserInput.Instance.Controls.InGame.Jump.WasReleasedThisFrame())
            {
                _isJumping = false;
                _isFalling = true;
            }

            if (!_isJumping && CheckForLand())
            {
    //            _anim.SetTrigger("land");
                _resetTriggerCoroutine = StartCoroutine(Reset());
            }
            DrawGroundCheck();
        }
        #endregion
        
        #region Movement Functions
        private void Move()
        {
            _moveInputx = UserInput.Instance.MoveInput.x;

            if (_moveInputx > 0 || _moveInputx < 0)
            {
                TurnCheck();
            }
            
            _rb.velocity = new Vector2(_moveInputx * _moveSpeed, _rb.velocity.y);
        }
        #endregion

        #region Ground/Landed Check

        private bool CheckForLand()
        {
            if (_isFalling)
            {
                if (Data.GroundDetection.IsCollided)
                {
                    _isFalling = false;
                    return true;
                }

                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        private IEnumerator Reset()
        {
            yield return null;
            
//            _anim.ResetTrigger("land");
        }
        #endregion

        #region Turn Checks
        private void StartDirectionCheck()
        {
            if (_rightLeg.transform.position.x > _leftLeg.transform.position.x)
            {
                IsFacingRight = true;
            }
        
            else
            {
                IsFacingRight = false;
            }
        }

        private void TurnCheck()
        {
            if (UserInput.Instance.MoveInput.x > 0 && !IsFacingRight)
            {
                Turn();
            }
            
            else if (UserInput.Instance.MoveInput.x < 0 && IsFacingRight)
            {
                Turn();
            }
        }

        private void Turn()
        {
            if (IsFacingRight)
            {
                Vector3 rotator = new Vector3(0f, 180f, 0f);
                transform.rotation = Quaternion.Euler(rotator);
                IsFacingRight = !IsFacingRight;
                _cameraFollowObject.CallTurn();
            }

            else
            {
                Vector3 rotator = new Vector3(0f, 0f, 0f);
                transform.rotation = Quaternion.Euler(rotator);
                IsFacingRight = !IsFacingRight;
                _cameraFollowObject.CallTurn();
            }
        }
        #endregion
        
        #region Climb Function
        private void Climb()
        {
            _moveInputx = UserInput.Instance.MoveInput.x;
            _moveInputy = UserInput.Instance.MoveInput.y;

            if (IsClimbing)
            { 
                _rb.velocity = new Vector2(_moveInputx * _moveSpeed, _moveInputy * _moveSpeed); 
            }
        }
        #endregion


        private void DrawGroundCheck()
        {
            Color rayColor;

            if (Data.GroundDetection.IsCollided)
            {
                rayColor = Color.green;
            }

            else
            {
                rayColor = Color.red;
            }
            
            Debug.DrawRay(_coll.bounds.center + new Vector3(_coll.bounds.extents.x, 0), Vector2.down * (_coll.bounds.extents.y + extraHeight), rayColor);
            Debug.DrawRay(_coll.bounds.center - new Vector3(_coll.bounds.extents.x, 0), Vector2.down * (_coll.bounds.extents.y + extraHeight), rayColor);
            Debug.DrawRay(
                _coll.bounds.center - new Vector3(_coll.bounds.extents.x, _coll.bounds.extents.y + extraHeight),
                Vector2.right * (_coll.bounds.extents.x * 2), rayColor);
        }
         
        public event Action<Color> OnColorChange;
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
                            ChangeColor(GetColor(KeyCode.G, Color.yellow, KeyCode.B, Color.magenta, Color.red));
                            break;

                        case KeyCode.G:
                            ChangeColor(GetColor(KeyCode.B, Color.cyan, KeyCode.R, Color.yellow, Color.green));
                            break;

                        case KeyCode.B:
                            ChangeColor(GetColor(KeyCode.R, Color.magenta, KeyCode.G, Color.cyan, Color.blue));
                            break;
                    }
                }
            }

            Color GetColor(KeyCode secondKey, Color colorIfBothPressed, KeyCode thirdKey, Color colorIfThirdPressed, Color defaultColor)
            {
                if (Input.GetKey(secondKey))
                    return colorIfBothPressed;
                else if (Input.GetKey(thirdKey))
                    return colorIfThirdPressed;
                else
                    return defaultColor;
            }
        } 
        public void ChangeColor(Color newColor)
        {
            playerLight.color = newColor;
            OnColorChange?.Invoke(newColor);
        }

    }
}
