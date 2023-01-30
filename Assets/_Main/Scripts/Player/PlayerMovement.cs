using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DE
{
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Init")]
        [SerializeField] public Animator _anim;
        [SerializeField] public Rigidbody _rigid;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private PlayerInput _playerInput;

        [Header("Stats")]
        [SerializeField] private float _runSpeed = 6f;
        [SerializeField] private float _turnSmoothTime = 0.2f;
        [SerializeField] private float _turnSmoothVelocity;
        [SerializeField] private float _speedSmoothTime = 0.1f;
        [SerializeField] private float _rollDistance = 1f;

        // c# out
        private float _speedSmoothVelocity;
        private float _currentSpeed;

        Vector2 _inputRoll;
        Vector2 inputDirRoll;


        private void FixedUpdate()
        {
            GroundCheck();
            
        }

        void LateUpdate()
        {
            Movement();

        }

        void Update()
        {
            // if(isUsingSkill) CanRotate = false;
            Rolling();
        }
        private void Rolling()
        {


            if (_playerInput.RollJoy.IsPressed)
            {
                _inputRoll = new Vector2(_playerInput.RollJoy.Horizontal, _playerInput.RollJoy.Vertical);
                inputDirRoll = _inputRoll.normalized;

            }
            if (_playerInput.PlayRolling && !_anim.GetBool("isAction"))
            {

                float targetRotation = Mathf.Atan2(inputDirRoll.x, inputDirRoll.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _turnSmoothVelocity, 0f);

                _anim.SetBool("isRolling", true);
                Vector3 rollPosition = transform.position + transform.forward * _rollDistance;
                float disToRollPosition = Vector3.Distance(rollPosition, transform.position);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, disToRollPosition, _obstacleMask))
                {
                    rollPosition = transform.position;
                }

                transform.DOMove(rollPosition, 0.2f).OnComplete(() =>
                {
                     _anim.SetBool("isRolling", false);
                     _playerInput.PlayRolling = false;
                });


            }
            else _playerInput.PlayRolling = false;

        }

        private void Movement()
        {

            Vector2 _input = new Vector2(_playerInput.Movement.x, _playerInput.Movement.y);

            Vector2 inputDir = _input.normalized;
            if (_playerInput.PlayRolling) return;
            // rotate player
            if (inputDir != Vector2.zero && !_playerInput.isAim)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _turnSmoothVelocity, _turnSmoothTime);
            }

            //check run or walk
            float targetSpeed = _runSpeed * inputDir.magnitude;
            _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _speedSmoothVelocity, _speedSmoothTime);

            //move Player
            bool canMoveCheck = inputDir != Vector2.zero && !_anim.GetBool("isAction") && !_playerInput.isAim;
            if (canMoveCheck)
            {
                transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
            }


            _anim.SetFloat("movement", inputDir.magnitude, _speedSmoothTime, Time.deltaTime);
        }



        public bool isGrounded = false;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_groundCheck.position, 0.15f);
        }
        private void GroundCheck()
        {
            if (Physics.CheckSphere(_groundCheck.position, 0.15f, _groundMask))
            {
                _anim.SetBool("onGround", true);
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
                _anim.SetBool("onGround", false);
            }
        }
    }


}
