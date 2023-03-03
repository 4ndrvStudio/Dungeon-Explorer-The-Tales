using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using DE.Models;

namespace DE
{
    public class Popup_SetName : UIPopup
    {
        [SerializeField] private Button _setNameButton;
        [SerializeField] private TMP_InputField _inputName;
        [SerializeField] private GameObject _errorPopup;
        
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            _setNameButton.onClick.AddListener(() => SetName());
        }

        public override void Show(Dictionary<string, object> customProperties = null)
        {
            base.Show(customProperties);
            bool isRequire = (bool) customProperties["isRequire"];
            if(isRequire) {
                CloseBTN.gameObject.SetActive(false);
            } else {
                CloseBTN.gameObject.SetActive(true);
            }
           
        }

        private async void SetName() {
            string name = _inputName.text;
            // name validator
            if(name.Length < 3)  {
                 StartCoroutine(ShowError());
                 return;
            }

            CloudCodeResult res = await CloudCodeManager.Instance.SetUserName(name);
            

            //check state
            if(res.IsCompleted) {
                PlayerDataManager.Instance.UpdateName(name);
                Hide();
            } else {
                StartCoroutine(ShowError());
                _inputName.text = null;
            }
        }

        IEnumerator ShowError() {
            _errorPopup.SetActive(true);
            yield return new WaitForSeconds(3f);
            _errorPopup.SetActive(false);
        }
    
    }

}
