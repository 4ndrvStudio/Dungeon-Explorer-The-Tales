using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DE
{
    public class EnemyAI_Movement : MonoBehaviour
    {
        [SerializeField] private EnemyAI_Brain _enemyBrain;
        [SerializeField] protected NavMeshAgent _navMeshAgent;
        [SerializeField] protected float _startWaitTime = 2.5f;
        [SerializeField] protected float _timeToRotate = 2;
        [SerializeField] protected float _speedWalk = 2;
        [SerializeField] protected float _speedRun = 4;
       // [SerializeField] protected float _stoppingDistance = 4;
        [SerializeField] protected float _viewRadius = 15;
        [SerializeField] protected float _viewAngle = 180;
        [SerializeField] protected LayerMask _playerMask;
        [SerializeField] protected LayerMask _obstacleMask;

        [SerializeField] private Transform[] waypoints;

        //hidden in inspector             
        int m_CurrentWaypointIndex;

        Vector3 playerLastPosition = Vector3.zero;
        Vector3 m_PlayerPosition;

        float m_DisToAttack;
        float m_WaitTime;
        float m_TimeToRotate;
        float m_stoppingDistance;
        bool m_playerInRange;
        bool m_PlayerNear;
        bool m_IsPatrol;
        bool m_CaughtPlayer;



        void Start()
        {
            m_PlayerPosition = Vector3.zero;
            m_IsPatrol = true;
            m_CaughtPlayer = false;
            m_playerInRange = false;
            m_PlayerNear = false;
            m_WaitTime = _startWaitTime;
            m_TimeToRotate = _timeToRotate;
            m_CurrentWaypointIndex = 0;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = _speedWalk;
            _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        }

        void Update()
        {
            if(_enemyBrain.IsStatic) return;
             
            m_DisToAttack = _enemyBrain.DisToCombat;
            m_stoppingDistance = _enemyBrain.DisToCombat;

            if (_enemyBrain.E_AI_STATE == E_AI_STATE.Dead)
            {
                Stop();
                return;
            };

            EnviromentView();
            if (_enemyBrain.E_AI_STATE != E_AI_STATE.Combat)
            {
                if (!m_IsPatrol)
                {
                    Chasing();
                }
                else
                {
                    Patroling();
                }
            }
        }
        private void Chasing()
        {

            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            _navMeshAgent.stoppingDistance = m_stoppingDistance;
            _enemyBrain.E_AI_STATE = E_AI_STATE.Chasing;
            if (!m_CaughtPlayer)
            {
                Move(_speedRun);
                _navMeshAgent.SetDestination(m_PlayerPosition);
            }

            // Check if player out the enemy view but still have another inside the view
            if (_enemyBrain.DisToPlayer > _viewRadius)
            {
                if (_enemyBrain.PlayersInRange.Count > 0)
                {
                    float tempDis = Mathf.Infinity;
                    _enemyBrain.PlayersInRange.ForEach(player =>
                    {
                        float distance = Vector3.Distance(player.transform.position, transform.position);
                        if (distance < tempDis)
                        {
                            tempDis = distance;
                            _enemyBrain.SelectedPlayer = player;
                        }
                    });
                }
            }


            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (_enemyBrain.DisToPlayer < m_DisToAttack)
                {
                    _enemyBrain.E_AI_STATE = E_AI_STATE.Combat;
                    _enemyBrain.RotateToPlayer();
                }
                else _enemyBrain.E_AI_STATE = E_AI_STATE.Chasing;

                if (m_WaitTime <= 0 && !m_CaughtPlayer && _enemyBrain.DisToPlayer >= 6f)
                {
                    _enemyBrain.SelectedPlayer = null;
                    m_IsPatrol = true;
                    m_PlayerNear = false;
                    Move(_speedWalk);
                    m_TimeToRotate = _timeToRotate;
                    m_WaitTime = _startWaitTime;
                    _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
                else
                {
                    if (_enemyBrain.DisToPlayer >= m_DisToAttack) Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }


        private void Patroling()
        {
                                  
              _enemyBrain.E_AI_STATE = E_AI_STATE.Patrolling;
            _navMeshAgent.stoppingDistance = 0;

            if (m_PlayerNear)
            {
                if (m_TimeToRotate <= 0)
                {
                    Move(_speedWalk);
                    LookingPlayer(playerLastPosition);
                }
                else
                {
                    Stop();
                    m_TimeToRotate -= Time.deltaTime;
                }
            }
            else
            {
                m_PlayerNear = false;
                playerLastPosition = Vector3.zero;
                _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (m_WaitTime <= 0)
                    {

                        Debug.Log("Patroling");
                        NextPoint();
                        Move(_speedWalk);
                        m_WaitTime = _startWaitTime;
                    }
                    else
                    {
                        Debug.Log("Idle");
                       _enemyBrain.E_AI_STATE = E_AI_STATE.Idle;
                        Stop();
                        m_WaitTime -= Time.deltaTime;
                    }
                }
            }
        }

        public void NextPoint()
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        }

        void Stop()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.speed = 0;
        }

        void Move(float speed)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = speed;
        }

        void CaughtPlayer()
        {
            m_CaughtPlayer = true;
            
        }

        void LookingPlayer(Vector3 player)
        {
            _navMeshAgent.SetDestination(player);
            if (Vector3.Distance(transform.position, player) <= 0.3)
            {
                if (m_WaitTime <= 0)
                {
                    m_PlayerNear = false;
                    Move(_speedWalk);
                    _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                    m_WaitTime = _startWaitTime;
                    m_TimeToRotate = _timeToRotate;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _viewRadius);
        }

        void EnviromentView()
        {
            Collider[] rangeTemp = Physics.OverlapSphere(transform.position, _viewRadius, _playerMask);
            _enemyBrain.PlayersInRange = rangeTemp.ToList();

            for (int i = 0; i < _enemyBrain.PlayersInRange.Count; i++)
            {
                Transform player = _enemyBrain.PlayersInRange[i].transform;
                Vector3 dirToPlayer = (player.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToPlayer) < _viewAngle / 2)
                {
                    if (_enemyBrain.SelectedPlayer == null) _enemyBrain.SelectedPlayer = _enemyBrain.PlayersInRange[i];
                }
            }

            if (_enemyBrain.SelectedPlayer != null)
            {
                Vector3 dirToPlayer = (_enemyBrain.SelectedPlayer.transform.position - transform.position).normalized;
                float dstToPlayer = Vector3.Distance(transform.position, _enemyBrain.SelectedPlayer.transform.position);

                // Check Player In Range
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _obstacleMask))
                {
                    m_playerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_playerInRange = false;
                }
                // Check player Exit
                if (dstToPlayer > _viewRadius)
                {
                    m_playerInRange = false;
                }
                // Check 
                if (m_playerInRange)
                {
                    m_PlayerPosition = _enemyBrain.SelectedPlayer.transform.position;
                }
            }
        }
    }

}