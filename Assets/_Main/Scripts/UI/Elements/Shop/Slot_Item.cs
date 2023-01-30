using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DE.Models;

namespace DE
{
    public class Slot_Item : MonoBehaviour
    {

        [SerializeField] private EquipmentItemConfig _equipmentItemConfig;
        [SerializeField] private ViewName _viewName;

        [Header("UI")]
        [SerializeField] private Image _rateBackground;
        [SerializeField] private Image _equipmentIcon;
        [SerializeField] private List<Sprite> _rateUIPrefabs = new List<Sprite>();
    
        public void SetItemData(EquipmentItemConfig equipmentItemConfig, ViewName viewName) {
            _equipmentItemConfig = equipmentItemConfig;
            _equipmentIcon.sprite = equipmentItemConfig.Icon;
            _rateBackground.sprite = _rateUIPrefabs[ (int) equipmentItemConfig.EquipmentRate -1];
            _viewName = viewName;

            //set onclick function


        }

    }

}
