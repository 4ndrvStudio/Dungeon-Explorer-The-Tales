using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class UISkillManager : MonoBehaviour
    {
        [SerializeField] private Joystick _skillJoy;
        [SerializeField] private GameObject _rollOb;
        [SerializeField] private GameObject _drawOb;
        public void ButtonPressedAction(int state)
        {
            if (state == 0) _drawOb.SetActive(false);
            if (state == 1) _rollOb.SetActive(false);
        }
    }

    public enum SkillUIState
    {
        Roll,
        Draw
    }

}

