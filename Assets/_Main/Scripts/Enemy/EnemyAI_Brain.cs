using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DE
{
    public class EnemyAI_Brain : MonoBehaviour
    {
        //EnemyState
        public E_AI_STATE E_AI_STATE;
        public E_TYPE E_TYPE;
        //Initial
        [SerializeField] protected EnemyAI_Stats _enemyStats;

        public List<Collider> PlayersInRange = new List<Collider>();
        public Collider SelectedPlayer;
        public float DisToPlayer = Mathf.Infinity;
        [HideInInspector] public float DisToCombat;
        public float DisToShortCombat;
        public float DisToLongCombat;
        public LayerMask PlayerMask;
        public LayerMask ObstacleMask;
        public bool IsStatic;

        private void OnEnable() {
            PlayerStats.PlayerDeathAction += PlayerDeathListener;
        }

        private void OnDisable() {
            PlayerStats.PlayerDeathAction -= PlayerDeathListener;
        }


        void Update()
        {

            if (SelectedPlayer != null)
            {
                DisToPlayer = Vector3.Distance(transform.position, SelectedPlayer.transform.position);
            }
            //Check State
            if (_enemyStats.IsDeath) E_AI_STATE = E_AI_STATE.Dead;
            

            RangeCombatCheck();
        }

        void PlayerDeathListener() {
            SelectedPlayer = null ;
            DisToPlayer = Mathf.Infinity;
        }

        void RangeCombatCheck() {
            if(DisToLongCombat == DisToShortCombat) {
                DisToCombat = DisToShortCombat;
                E_TYPE = E_TYPE.ShortRange;
            } else  {
                DisToCombat = DisToPlayer < DisToShortCombat ? DisToShortCombat : DisToLongCombat;
                E_TYPE = DisToCombat == DisToLongCombat ? E_TYPE.LongRange : E_TYPE.ShortRange;
            } 
        }

        public void RotateToPlayer(Transform target = null)
        {
            Transform targetRot = target != null && SelectedPlayer == null ? target.transform : SelectedPlayer.transform;
            var lookPos = targetRot.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
        }

        //
        public void AimSupport()
        {
            if (SelectedPlayer != null && DisToPlayer != 0)
            {
                transform.DOMove(CalPosBeforeTarget(SelectedPlayer.transform.position, transform.position, DisToCombat), 1f);
            }
        }

        public Vector3 CalPosBeforeTarget(Vector3 target, Vector3 point, float distance)
        {

            Vector3 targetPos = target + ((point - target).normalized * distance);
            targetPos.y = point.y;
            return targetPos;
        }

        public void ReSelectNearestPlayer()
        {
            float nearestTemp = Mathf.Infinity;
            PlayersInRange.ForEach(player =>
            {
                float disToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (disToPlayer < nearestTemp)
                {
                    SelectedPlayer = player;
                    nearestTemp = disToPlayer;
                }
            });
        }

    }
    //State
    public enum E_AI_STATE
    {
        Idle,
        Chasing,
        Patrolling,
        Dead,
        Combat,
        Spawn,
        GetHit,
    }
    public enum E_TYPE {
        ShortRange,
        LongRange,
    }





}
