using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DE
{
    public class Popup_Death : UIPopup
    {  
        [SerializeField] private Button _goHomeBtn;

        // Start is called before the first frame update
        void Start()
        {  
            _goHomeBtn.onClick.AddListener(()=> QuitGame());
        }

        void QuitGame() {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneName.Scene_Home);
        }

     
    }

}