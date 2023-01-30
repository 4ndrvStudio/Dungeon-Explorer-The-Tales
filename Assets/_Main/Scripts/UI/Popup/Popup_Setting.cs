using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DE
{
    public class Popup_Setting : UIPopup
    {  
        [SerializeField] private Button _dimedBtn;
        [SerializeField] private Button _closeBtn;

        // Start is called before the first frame update
        void Start()
        {  
            _dimedBtn.onClick.AddListener(()=> Hide());
            _closeBtn.onClick.AddListener(()=> Hide());
        }

     
    }

}
