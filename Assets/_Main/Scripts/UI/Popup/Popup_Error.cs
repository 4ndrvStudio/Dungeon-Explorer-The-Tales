using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DE
{
    public class Popup_Error : UIPopup
    {
        public TextMeshProUGUI _errorTypeText;
        public TextMeshProUGUI _errorMessageText;

        public override void Show(Dictionary<string, object> customProperties)
        {
            base.Show(customProperties);
             _errorTypeText.text = customProperties["errorType"].ToString();
             _errorMessageText.text = customProperties["errorMessage"].ToString();
            
        }

    }

}
