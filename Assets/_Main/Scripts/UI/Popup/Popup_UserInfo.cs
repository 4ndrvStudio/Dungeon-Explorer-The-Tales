using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DE
{
    public class Popup_UserInfo : UIPopup
    {
        [SerializeField] private Button _dimedBtn;
        [SerializeField] private Button _closeBtn;
        [SerializeField] private Button _editNameBtn;

        // Start is called before the first frame update
        void Start()
        {  
            _dimedBtn.onClick.AddListener(()=> Hide());
            _closeBtn.onClick.AddListener(()=> Hide());
            _editNameBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupName.SetName));
        }
    }

}
