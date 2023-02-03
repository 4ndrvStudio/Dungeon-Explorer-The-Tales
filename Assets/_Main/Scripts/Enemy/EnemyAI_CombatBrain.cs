using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

namespace DE
{
    public abstract class EnemyAI_CombatBrain : MonoBehaviour
    {

        [SerializeField] private EnemyAI_Brain _enemyBrain;
        [SerializeField] private EnemyAI_Animation _enemyAnim;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private E_COMBAT_STATE E_COMBAT_STATE;

        //normal attack
        [SerializeField] private float _attack_ReloadTime = 2f;
        [SerializeField] private float _attack_ReloadTime_M = 2f;
        [SerializeField] private string[] _attack_List;
        [SerializeField] private string[] _shoot_List;

        [SerializeField] private GameObject _shootBullet;
        [SerializeField] private Transform _shootPosition;

        private bool _canAttack = true;
        private int _preSRIndex = 0;
        private int _preLRIndex = 0;


        protected virtual void Start()
        {
            E_COMBAT_STATE = E_COMBAT_STATE.None;
            _canAttack = true;
        }

        protected virtual void Update()
        {
            if (_enemyBrain.E_AI_STATE == E_AI_STATE.Combat)
            {
                //if player player not in combat state, return to chasing
                if (_enemyBrain.DisToPlayer > _enemyBrain.DisToCombat) _enemyBrain.E_AI_STATE = E_AI_STATE.Chasing;
                else E_COMBAT_STATE = E_COMBAT_STATE.NormalAttack;
                
                //select combat state. 
                switch (E_COMBAT_STATE)
                {
                    case E_COMBAT_STATE.NormalAttack:
                        NormalAttack();
                        break;
                    case E_COMBAT_STATE.GetHit: // do something;
                        break;
                    case E_COMBAT_STATE.Turned: // Do something;
                        break;
                    default: return;
                }
            }
        }

        private void NormalAttack()
        {
            
            _attack_ReloadTime -= Time.deltaTime;
            if (_attack_ReloadTime >= _attack_ReloadTime_M - 0.1f)
            {
                if (_enemyBrain.E_TYPE == E_TYPE.ShortRange)
                {
                    if (_canAttack) ShortRangeAttack();
                }
                else
                {
                    if (_canAttack) LongRangeAttack();
                };

            }
            if (_attack_ReloadTime <= 0)
            {
                _attack_ReloadTime = _attack_ReloadTime_M;
                _canAttack = true;
            }
        }

        private void LongRangeAttack()
        {
            int targetAttack = GetRandomAttack(); // Define logic Here.
            _enemyBrain.RotateToPlayer();
            _enemyAnim.PlayShoot(_shoot_List[targetAttack]);
            StartCoroutine(ShootBullet());
            _canAttack = false;
        }


        private void ShortRangeAttack()
        {
            int targetAttack = GetRandomAttack(); // Define logic Here.
            _enemyBrain.RotateToPlayer();
            _enemyBrain.AimSupport();
            _enemyAnim.PlayAttack(_attack_List[targetAttack]);
            _canAttack = false;
        }

        IEnumerator ShootBullet()
        {
            yield return new WaitForSeconds(0.3f);
            _enemyBrain.RotateToPlayer();
            GameObject bullet = Instantiate(_shootBullet, _shootPosition.position + _shootPosition.forward * 0.5f, transform.rotation);
            Rigidbody rigid = bullet.GetComponent<Rigidbody>();
            rigid.AddForce(bullet.transform.forward * 10f, ForceMode.Impulse);
        }

        private int GetRandomAttack()
        {
            bool isShortRange = _enemyBrain.E_TYPE == E_TYPE.ShortRange;
            string[] targetList = isShortRange ? _attack_List : _shoot_List;
            int targetIndex = targetList.Length == 1 ? 0 : Random.Range(0, targetList.Length);
            return targetIndex;
            // int targetPreIndex = isShortRange ? _preSRIndex : _preLRIndex;

            // if (targetList.Length == 1) return 0;

            // if (targetIndex == targetPreIndex)
            // {
            //     if (targetIndex == targetList.Length - 1) targetIndex = 0;
            //     else targetIndex += 1;
            // }

            // if (isShortRange)
            // {
            //     _preSRIndex = targetIndex;
            // }
            // else
            // {
            //     _preLRIndex = targetIndex;
            // }
            // return targetIndex;
        }



    }
    public enum E_COMBAT_STATE
    {
        None,
        NormalAttack,
        GetHit,
        Turned,
        PreAttack,
        DecideAttack,
        EndAttack

    }
    public enum ACTION_STATE
    {
        State1,
        State2,
        State3,
        State4,
        State5
    }



}
