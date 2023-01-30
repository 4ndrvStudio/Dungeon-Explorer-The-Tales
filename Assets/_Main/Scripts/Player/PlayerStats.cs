using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DE
{
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats Instance;
        

        public int Health;
        private int _maxHealth;
        public int Arrow = 3;
        private int _maxArrow;

        public bool isDeath;
        public int Damage;
        
        [SerializeField] private Animator _anim;
        [SerializeField] private PlayerInput _playerInput;

        

        public static UnityAction PlayerDeathAction;
        
        private void Start() {

            if(Instance == null) Instance = this;

            if(Instance != this) Destroy(this);


            _maxArrow = Arrow;
            _maxHealth = Health;

            View_Gameplay.Instance.SetupArrowUI(_maxArrow);
        }

 

        void Update() {
            if(isDeath) {
                _playerInput.enabled = false;
             
                if(!_anim.GetBool("isDeath")) {
                    _anim.Play("Death",1,0);
                    _anim.SetBool("isDeath", true);
                    StartCoroutine(LoserPoup());
                    gameObject.GetComponent<Collider>().enabled = false;
                    PlayerDeathAction.Invoke();
                    
                }
            }
        }

        public void UseArrow()
        {
            if (Arrow != 0)  Arrow -= 1;
    
            View_Gameplay.Instance.UpdateArrowUI(Arrow);
           
        }
        public void RefillArrow()
        {
            if(Arrow < _maxArrow) Arrow+=1;

            View_Gameplay.Instance.UpdateArrowUI(Arrow);
        }

 

        public void ReduceHealth() {
        
            if(Health > 0) Health-=1;
          
            View_Gameplay.Instance.UpdateHealthUI(Health,_maxHealth);

            if(Health<=0) isDeath = true;

        }

        IEnumerator LoserPoup() {
            yield return new WaitForSeconds(5f);
            Time.timeScale = 0;
            UIManager.Instance.ShowPopup(PopupName.GameplayDeath);
            
        }





    }

}

