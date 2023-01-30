using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DE
{
    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance;

        //ref button
        public Joystick MovementJoy;
        public Joystick AimJoy;
        public Joystick RollJoy;
        public Image RollJoyReload;
        public Button AttackBtn;

        public bool PlayAttack;
        public bool isAim;
        public bool PlayShooting;
        public Vector2 Movement;

        public bool isStartRoll;
        public bool PlayRolling;

        private bool _canRoll;
        private bool _canShot;

          public float AimTime = 0f;


        // Start is called before the first frame update
        void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            Movement = new Vector2();
            _canRoll = true;
            InitialSetup();


        }


        // Update is called once per frame
        void Update()
        {
            MovementInput();
            Aiming();
            Rolling();
        }

        private void InitialSetup()
        {

            //set up UI controller
            if (AimJoy == null) AimJoy = View_Gameplay.Instance.AimJoy;
            if (MovementJoy == null) MovementJoy = View_Gameplay.Instance.MovementJoy;
            if (RollJoy == null) RollJoy = View_Gameplay.Instance.RollJoy;
            if (AttackBtn == null) AttackBtn = View_Gameplay.Instance.AttackBtn;
            if (RollJoyReload == null) RollJoyReload = View_Gameplay.Instance.RollJoyReload;

            AttackBtn.onClick.AddListener(() =>
            {
                NormalAttack();
            });

        }

        void MovementInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal") + MovementJoy.Horizontal;
            float vertical = Input.GetAxisRaw("Vertical") + MovementJoy.Vertical;
            Movement.x = horizontal;
            Movement.y = vertical;
        }

        public void NormalAttack()
        {
            PlayAttack = true;
        }
        public void Aiming()
        {
        
            AimTime = AimJoy.IsPressed ?  AimTime += Time.deltaTime : 0;

            if (AimJoy.IsPressed && AimJoy.Direction.magnitude > 0.8f && AimTime> 0.4f) isAim = true;

            if (isAim)
            {
                if (AimJoy.IsPressed == false)
                {
                    PlayShooting = true;
                    isAim = false;
                }
            }
        
        }
        public void Rolling()
        {
            if(!_canRoll) return;

            if ( RollJoy.IsPressed && RollJoy.Direction.magnitude > 0.8f) isStartRoll = true;
        
            if (isStartRoll)
            {
                if (RollJoy.IsPressed == false)
                {
                    Debug.Log("rolling");
                    PlayRolling = true;
                    RollJoyReload.fillAmount = 1;
                    DOTween.To(()=> RollJoyReload.fillAmount, x =>  RollJoyReload.fillAmount = x, 0, 3f ).OnComplete(() => _canRoll = true);
                    isStartRoll = false;
                    _canRoll = false;
                }
            }

          
        }
    }



}
