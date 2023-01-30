using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class UIPopup : MonoBehaviour
    {

        [SerializeField] private PopupName _popupName = PopupName.None;
        public PopupName PopupName => _popupName;

        private Dictionary<string, string> _customProperties;

        public virtual void Show(Dictionary<string, string> customProperties)
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
