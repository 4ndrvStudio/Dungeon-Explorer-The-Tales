using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DE
{
    public abstract class UIPopup : MonoBehaviour
    {

        [SerializeField] private PopupName _popupName = PopupName.None;
        public PopupName PopupName => _popupName;

        [Header("Options Close")]
        public Button CloseBTN = null;
        public Button Dimmed = null;
        [Space]

        protected Dictionary<string, object> _customProperties;

        public virtual void Start() {
           
            Initialize();

        }

        public virtual void Initialize() {
            if(CloseBTN != null) CloseBTN.onClick.AddListener(()=> Hide());
            if(Dimmed != null) Dimmed.onClick.AddListener(()=> Hide());
        }


        public virtual void Show(Dictionary<string, object> customProperties)
        {
            this._customProperties = customProperties;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            _customProperties = null;
            gameObject.SetActive(false);
        }
    }

}
