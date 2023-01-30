using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class CustomJoy : MonoBehaviour
    {
        public Joystick _joy;
        public RectTransform _joyRange;

        void Update()
        {
            if (_joy.IsPressed) _joyRange.sizeDelta = new Vector2(256, 256);
            else _joyRange.sizeDelta = new Vector2(128, 128);

        }
    }

}
