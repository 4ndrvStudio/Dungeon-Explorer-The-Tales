using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DE
{
    public class View_Home : UIView
    {
        [Header("Tab Buttons")]
        [SerializeField] private Button _shopBtn;
        [SerializeField] private Button _equipmentBtn;
        [SerializeField] private Button _evolveBtn;
        [SerializeField] private Button _stageBtn;
        [SerializeField] private Button _battleBtn;

        [Header("Popup Buttons")]
        [SerializeField] private Button _settingBtn;
        [SerializeField] private Button _userInfoBtn;

        // Start is called before the first frame update
        void Start()
        {
            //set Event Popups
            _settingBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupName.Settings));
            _userInfoBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupName.UserInfo));

            //set event Tabs;
            _equipmentBtn.onClick.AddListener(() => UIManager.Instance.ToggleView(ViewName.Equipment));
            _shopBtn.onClick.AddListener(() => UIManager.Instance.ToggleView(ViewName.Shop));
            _evolveBtn.onClick.AddListener(()=> UIManager.Instance.ToggleView(ViewName.Evolve));
            _stageBtn.onClick.AddListener(() => UIManager.Instance.ToggleView(ViewName.Stage));
            _battleBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.LoadScene(SceneName.Prototype);

            });
    
        }

       
    }

}
