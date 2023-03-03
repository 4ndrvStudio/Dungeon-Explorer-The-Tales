using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DE
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Views")]
        [SerializeField] private List<UIView> _listView = new List<UIView>();
        [Space]
        [Header("Popups")]
        [SerializeField] private List<UIPopup> _listPopup = new List<UIPopup>();

        [SerializeField] private ViewName _prevTab = ViewName.None;
        [SerializeField] private ViewName _currentTab = ViewName.Login;
        
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void ShowPopup(PopupName popupName, Dictionary<string, object> customProperties = null)
        {
            UIPopup selectedPopup = _listPopup.Find(popup => popup.PopupName == popupName);
            if (selectedPopup != null) selectedPopup.Show(customProperties);

        }
        public void HidePopup(PopupName popupName)
        {
            UIPopup selectedPopup = _listPopup.Find(popup => popup.PopupName == popupName);
            if (selectedPopup != null) selectedPopup.Hide();
        }
        public void ToggleView(ViewName panelName)
        {
            UIView selectedPanel = _listView.Find(tab => tab.ViewName == panelName);
            if (selectedPanel != null)
            {
                _prevTab = _currentTab;
                _currentTab = panelName;
                _listView.ForEach(panel => panel.Hide());
                selectedPanel.Show();
            }
        }
        public void GoBackTab() => ToggleView(_prevTab);
        public void GoHomeTab() => ToggleView(ViewName.Home);

        public void LoadScene(string sceneName) => StartCoroutine(SceneLoader(sceneName));

        IEnumerator SceneLoader(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
                switch (SceneManager.GetActiveScene().name)
                {
                    case string value when value == SceneName.Scene_Login:
                        ToggleView(ViewName.Login);
                        break;
                    case string value when value == SceneName.Scene_Home:
                        ToggleView(ViewName.Home);
                        if(PlayerDataManager.Instance.UserInfo.UserName == null) CheckNameState();
                        break;
                    case string value when value == SceneName.Prototype:
                        ToggleView(ViewName.Gameplay);
                        break;
                }
            }
        }

         void CheckNameState() {
            ShowPopup(PopupName.SetName,new Dictionary<string, object>{{"isRequire", "true"}});
        }



    }

}
