using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DE
{
    public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public bool IsPressed = false;
        public GameObject _joyOb;
        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
            _joyOb.SetActive(true);
            this.gameObject.SetActive(false);
        }
        public void OnPointerUp(PointerEventData eventData)
        {

            IsPressed = false;
        }
    }

}

