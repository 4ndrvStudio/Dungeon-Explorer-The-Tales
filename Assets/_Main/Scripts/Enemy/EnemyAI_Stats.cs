using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class EnemyAI_Stats : MonoBehaviour
    {
        [SerializeField] private int _health = 5;
        [SerializeField] private bool _isDeath = false;

        public int Health => _health;
        public bool IsDeath => _isDeath;

        public void SetIsDeath(bool isDeath)
        {
            _isDeath = isDeath;
            Destroy(this.gameObject, 7f);
        }
        public void ReduceHealth(int targetReduce)
        {
            _health -= targetReduce;
        }

    
    }

}
