using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DE
{
    public class View_Gameplay : UIView
    {
        public static View_Gameplay Instance;
        [Header("Popup Buttons")]
        [SerializeField] private Button _gamepauseBtn;
        
        [Header("Controllers")]
        public Joystick MovementJoy;
        public Joystick AimJoy;
        public Joystick RollJoy;
        public Image RollJoyReload;
        public Button AttackBtn;
        
        public Slider HealthUI;
        public GameObject ArrowUI;
        public List<GameObject> ArrowListUI = new List<GameObject>();
        public GameObject ArrowUIContainer;
        public Color ArrowUIUsedColor;
       
       
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

           void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        
        
         // Start is called before the first frame update
        void Start()
        {
            //set Event Popups
            _gamepauseBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupName.Pause));

        }   

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Tab)) UpdateArrowUI(2);
        }

        public void SetupArrowUI(int numArrow) {
            //clear
            ArrowListUI.ForEach(arrow => Destroy(arrow));
            ArrowListUI.Clear();
            //add new
            for (int i = 0 ; i< numArrow; i++) {
                GameObject arrow = Instantiate(ArrowUI,transform.position , Quaternion.identity,ArrowUIContainer.transform);
                ArrowListUI.Add(arrow);
            }
        }

        public void UpdateArrowUI (int numArrow) {
            //display available and hidden  arrowUI 
            for (int i = 0 ; i < ArrowListUI.Count ; i++) {
                ArrowListUI[i].GetComponent<Image>().color = i< numArrow ? Color.white : ArrowUIUsedColor;
            }
            
        }

        public void UpdateHealthUI(int health, int maxHealth) {
            //calculate value to display
            HealthUI.value = (float) health  /  (float) maxHealth ;
        }
        

        

     

    }

}
