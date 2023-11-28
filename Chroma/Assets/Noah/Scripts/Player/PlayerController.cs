using Noah.Scripts.Input;
using UnityEngine;

namespace Noah.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 7.5f;
        
        [Header("Jump")]
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _jumpTime = 0.5f;
        
        [Header("Turn Check")]
        [SerializeField] private GameObject _leftLeg;
        [SerializeField] private GameObject _rightLeg;
        
        private bool _isFacingRight;
        private bool _isFalling;
        private bool _isJumping;
        private float _jumpTimeCounter;
        
        private Rigidbody2D _rb;
        private float _moveInput;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            StartDirectionCheck();
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            _moveInput = UserInput.Instance.MoveInput.x;

            if (_moveInput > 0 || _moveInput < 0)
            {
                TurnCheck();
            }
            
            _rb.velocity = new Vector2(_moveInput * _moveSpeed, _rb.velocity.y);
        }

        private void StartDirectionCheck()
        {
            if (_rightLeg.transform.position.x > _leftLeg.transform.position.x)
            {
                _isFacingRight = true;
            }
        
            else
            {
                _isFacingRight = false;
            }
        }

        private void TurnCheck()
        {
            if (UserInput.Instance.MoveInput.x > 0 && !_isFacingRight)
            {
                Turn();
            }
            
            else if (UserInput.Instance.MoveInput.x < 0 && _isFacingRight)
            {
                Turn();
            }
        }

        private void Turn()
        {
            if (_isFacingRight)
            {
                Vector3 rotator = new Vector3(transform.position.x, 180f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
                _isFacingRight = !_isFacingRight;
            }

            else
            {
                Vector3 rotator = new Vector3(transform.position.x, 0f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
                _isFacingRight = !_isFacingRight;
            }
        }
    }
}
