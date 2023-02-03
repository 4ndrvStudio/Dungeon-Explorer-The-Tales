using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class PlayerAimAssistant : MonoBehaviour
    {

        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private List<Collider> _enemyInRange;
        [SerializeField] private List<Collider> _nearestToAttack;
        [SerializeField] private float _nearestTemp = Mathf.Infinity;
        [SerializeField] private float _assistantRangeAngle = 90;

        public Collider SelectedNearest;

        // Update is called once per frame
        void Update()
        {
            NearestCheck();
        }

        void NearestCheck()
        {
            //select enemy in range
            _enemyInRange = new List<Collider>(Physics.OverlapSphere(transform.position, 50f, _enemyMask));
            _nearestToAttack = new List<Collider>(Physics.OverlapSphere(transform.position, 2f, _enemyMask));

            if (_nearestToAttack.Count > 0)
            {
                float temp = Mathf.Infinity;
                //check nearest enemy by quick sort
                _nearestToAttack.ForEach(targetAttack =>
                {
                    Transform enemy = targetAttack.transform;
                    Vector3 dirToEnemy = (enemy.position - transform.position).normalized;
                    if (Vector3.Angle(transform.forward, dirToEnemy) < _assistantRangeAngle / 2)
                    {
                        Vector3 from = targetAttack.transform.position - transform.position;
                        Vector2 to = transform.forward;
                        float angle = Vector3.Angle(from, to);
                        if (angle < temp)
                        {
                            SelectedNearest = targetAttack;
                            temp = angle;
                            _nearestTemp = temp;
                        }
                    }
                    else
                    {
                        if (SelectedNearest == targetAttack) SelectedNearest = null;
                    };
                });

            }
            else
            {
                _nearestTemp = 360;
                SelectedNearest = null;
            };



        }
    }

}
