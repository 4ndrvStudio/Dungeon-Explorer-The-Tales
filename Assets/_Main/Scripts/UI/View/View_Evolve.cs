using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DE
{
    public class View_Evolve : UIView
    {
        [SerializeField] private Button _goBackBtn;
        [SerializeField] private Button _goHomeBtn;
        
        // Start is called before the first frame update
        void Start()
        {
            _goBackBtn.onClick.AddListener(() => UIManager.Instance.GoBackTab());
            _goHomeBtn.onClick.AddListener(() => UIManager.Instance.GoHomeTab());
        }
    }

}
