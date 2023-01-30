using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class PlayerDamageable : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private Animator _anim;
        [SerializeField] private bool _canTakeDamage = true;

        public int ApplyDamage()
        {
            return _playerStats.Damage;
        }

        public void TakeDamage()
        {
            _playerStats.ReduceHealth();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("E_Damageable") && _canTakeDamage)
            {
               // _anim.Play("TakeDamage", 1, 0);
                TakeDamage();
                StartCoroutine(CanTakeDamage());
            }
        }

        IEnumerator CanTakeDamage()
        {
            _canTakeDamage = false;
            yield return new WaitForSeconds(1.5f);
            _canTakeDamage = true;
        }


    }
}
