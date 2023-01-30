using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DE
{
    public class EnemyAI_Damageable : MonoBehaviour
    {
        [SerializeField] private EnemyAI_Brain _enemyBrain;
        [SerializeField] private EnemyAI_Stats _enemyStats;
        [SerializeField] private EnemyAI_Animation _enemyAnim;

        private void OnTriggerEnter(Collider other)
        {   
            bool damageTag = other.CompareTag("P_Damageable") || other.CompareTag("PA_Damageable");
            if (damageTag && !_enemyStats.IsDeath) {
                GetHit(other.transform);
                if(other.CompareTag("P_Damageable")) PlayerStats.Instance.RefillArrow();
            } 
        }

        private void GetHit(Transform hitFromPos)
        {
            _enemyAnim.GetDame();
            bool isLastHit = _enemyStats.Health == 1;
            CameraManager.Instance.ShakeCam(5, 0.1f);
            
            // CameraManager.Instance.ShakeCam(2,0.1f);
            PushEnemyBack(hitFromPos,0.5f);
            _enemyBrain.RotateToPlayer(hitFromPos);
            _enemyBrain.ReSelectNearestPlayer();
            _enemyStats.ReduceHealth(1);
            if (_enemyStats.Health <= 0)
            {
                 PushEnemyBack(hitFromPos,5f);
                _enemyStats.SetIsDeath(true);
                gameObject.GetComponent<Collider>().enabled = false;
               
            }

        }
        public void PushEnemyBack(Transform hitFromPos, float factor)
        {
            Vector3 direction = transform.position - hitFromPos.transform.position;
            direction = direction.normalized * factor;
            direction.y = transform.position.y;
            Vector3 targetPos = transform.position + direction;
            targetPos.y = transform.position.y;
            transform.DOMove(targetPos, 1f);
        }

    }
}
