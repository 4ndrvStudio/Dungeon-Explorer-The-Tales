using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DE
{
    public class Popup_Pause : UIPopup
    {
        [SerializeField] private Button _settingBtn;
        [SerializeField] private Button _countinueBtn;
        [SerializeField] private Button _replayBtn;
        [SerializeField] private Button _quitBtn;

        // Start is called before the first frame update
        void Start()
        {

            _countinueBtn.onClick.AddListener(() => Hide());
            _settingBtn.onClick.AddListener(() => UIManager.Instance.ShowPopup(PopupName.Settings));
            _quitBtn.onClick.AddListener(() => {
                Hide();
                UIManager.Instance.LoadScene(SceneName.Scene_Home);
            });
            
        }


    }

}
