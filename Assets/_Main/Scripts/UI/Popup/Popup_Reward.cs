using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DE.Models;
using TMPro;

namespace DE
{
    public class Popup_Reward : UIPopup
    {
        public Slot_Item _slotItem;
        public TextMeshProUGUI _rewardNameText;

        public override void Show(Dictionary<string, object> customProperties)
        {
            base.Show(customProperties);
            Item itemConfig = (Item) customProperties["itemConfig"];
            _slotItem.SetItemConfig(itemConfig, false);
            _rewardNameText.text = itemConfig.Name;
        }

        
    }

}
