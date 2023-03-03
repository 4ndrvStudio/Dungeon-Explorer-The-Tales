using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DE
{
    public class PlayerBowController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private Animator _anim;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        [SerializeField] private float _turnSmoothVelocity;

        [SerializeField] private GameObject _arrows;
        [SerializeField] private Transform _shootingPoint;
        [SerializeField] private Transform _raycastPoint;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private LayerMask IgnoreLayer;

        private Transform m_aimCamPos;
        //private bool _isMainCam;

        void Start()
        {
            var newTrans = new GameObject().transform;
            newTrans.position = this.transform.position;
            m_aimCamPos = newTrans;

        }
        // Update is called once per frame
        void Update()
        {
            Aiming();
            Shooting();
        }
        void Aiming()
        {
            //set animtion when hold aim button
            if (_playerInput.AimTime > 0.2f) _anim.SetBool("isAim", _playerInput.AimJoy.IsPressed);
            else _anim.SetBool("isAim", false);

            //get direction to aim
            Vector2 _input = new Vector2(_playerInput.AimJoy.Horizontal, _playerInput.AimJoy.Vertical);
            Vector2 inputDir = _input.normalized;
            
            //if player aiming, calculate direction and enable laser, set camera postion;
            if (_playerInput.AimJoy.IsPressed)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _turnSmoothVelocity, _turnSmoothTime);
                ShootLaserFromTargetPosition(_raycastPoint.position, _raycastPoint.forward, 30f);
                CameraManager.Instance.SetFollowPos(m_aimCamPos);
                //_isMainCam = false;
            }
            else
            {
                m_aimCamPos.position = this.transform.position;
                CameraManager.Instance.SetFollowPos(this.transform);
            }
            _raycastPoint.gameObject.SetActive(_playerInput.isAim);
        }

        void Shooting()
        {
            if (_playerInput.PlayShooting && _playerStats.Arrow > 0)
            {
                GameObject arrow = Instantiate(_arrows, _shootingPoint.position, _shootingPoint.rotation);
                arrow.GetComponent<ECExplodingProjectile>().SetIsUserOwner();
                Rigidbody rigid = arrow.GetComponent<Rigidbody>();
                rigid.AddForce(arrow.transform.forward * 30f, ForceMode.Impulse);
                _playerStats.UseArrow();
                _playerInput.PlayShooting = false;
            }
            else
            {
                _playerInput.PlayShooting = false;
            }
        }

        void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
        {
            //calculate laser endpoint
            Ray ray = new Ray(targetPosition, direction);
            RaycastHit raycastHit;
            Vector3 endPosition = targetPosition + (Mathf.Lerp(0f, length, 2f) * direction);
            Vector3 aimPosition = targetPosition + (Mathf.Lerp(0f, 3, 2f) * direction);
            
            //change camera pos
            m_aimCamPos.position = Vector3.MoveTowards(m_aimCamPos.position, aimPosition, 35f * Time.deltaTime);
            
            if (Physics.Raycast(ray, out raycastHit, length, ~IgnoreLayer))
            {
                endPosition = raycastHit.point;
            }

            _lineRenderer.SetPosition(0, targetPosition);
            _lineRenderer.SetPosition(1, endPosition);

        }
    }

}
