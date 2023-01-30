using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace DE
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] public Animator _anim;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerAimAssistant _playerAim;
        [SerializeField] private LayerMask _obstacleMask;

        //normal attack
        [SerializeField] private string[] _normalAttack;
        [SerializeField] private int _targetAttack;
        [SerializeField] private float _attackMoveDuration;
        [SerializeField] private float _attackMoveDis;

        private bool _avoidMultiPress = false;

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }


        private void GetInput()
        {
            if (_playerInput.PlayAttack)
            {
                if (!_avoidMultiPress)
                {
                    NormalAttack();
                    StartCoroutine(AvoidMultiPress());
                }
                _playerInput.PlayAttack = false;
            }
        }
        void NormalAttack()
        {
            if (_anim.GetBool("isAction")) return;
            int targetAttack = Random.Range(0, _normalAttack.Length);
            if (targetAttack == _targetAttack)
            {
                if (targetAttack >= _normalAttack.Length - 1) targetAttack -= 1;
                else targetAttack += 1;
                Debug.Log(targetAttack);
            };
            _targetAttack = targetAttack;
            AimSupport();
            _anim.CrossFade(_normalAttack[_targetAttack], 0.2f);

        }

        void AimSupport()
        {
            if (_playerAim.SelectedNearest != null)
            {

                //Rotate support
                Vector3 from = _playerAim.SelectedNearest.transform.position - transform.position;
                Vector2 to = transform.forward;
                from.y = 0;
                var rotation = Quaternion.LookRotation(from);
                transform.DORotateQuaternion(rotation, 0f);

                //translate support
                Vector3 targetPos = _playerAim.SelectedNearest.transform.position + ((transform.position - _playerAim.SelectedNearest.transform.position).normalized * 1.2f);
                targetPos.y = transform.position.y;
                transform.DOMove(targetPos, _attackMoveDuration);
            }
            else
            {

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _attackMoveDis, _obstacleMask))
                {
                    transform.DOMove(transform.position + transform.forward * _attackMoveDis, _attackMoveDuration);
                }
               
            }
        }

        IEnumerator AvoidMultiPress()
        {
            _avoidMultiPress = true;
            yield return new WaitForSeconds(0.2f);
            _avoidMultiPress = false;
        }
    }

}
