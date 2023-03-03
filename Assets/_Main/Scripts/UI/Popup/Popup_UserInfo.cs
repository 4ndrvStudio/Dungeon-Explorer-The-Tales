using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DE
{
    public class Popup_UserInfo : UIPopup
    {

        [SerializeField] private Button _editNameBtn;

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            _editNameBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupName.SetName));
        }

    }

}
