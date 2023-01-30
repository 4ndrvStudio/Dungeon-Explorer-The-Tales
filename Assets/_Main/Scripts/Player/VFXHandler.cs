using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace DE
{
    public class VFXHandler : MonoBehaviour
    {
        [SerializeField] private VisualEffect[] _vfxList;
        // Start is called before the first frame update
        public void OnPlayVfx(int index)
        {
            _vfxList[index].Play();
        }
        //  public void OnStopVfx(int index) {
        //   _vfxList[index].SetActive(false);
        //  }
    }

}
