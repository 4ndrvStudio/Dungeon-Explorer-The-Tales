using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DE;
using DG.Tweening;
using Unity.Services.Authentication;

namespace DE
{
    public class View_Login : UIView
    {

        [SerializeField] private Button _startBtn;
        [SerializeField] private Slider _loadingBar;

        void Start()
        {
            _startBtn.onClick.AddListener(() => StartGame());
        }


        private async void StartGame()
        {
            _loadingBar.gameObject.SetActive(true);
            _startBtn.gameObject.SetActive(false);
            DOTween.To(() => _loadingBar.value, x => _loadingBar.value = x, 0.75f, 0.7f);

            await GameManager.Instance.LoginAnonymous();

            if (AuthenticationService.Instance.IsSignedIn)
            {
                DOTween.To(() => _loadingBar.value, x => _loadingBar.value = x, 1f, 0.5f).OnComplete( () =>
                {
                    UIManager.Instance.LoadScene(SceneName.Scene_Home);
                });
            }
            else
            {
                _loadingBar.gameObject.SetActive(false);
                _startBtn.gameObject.SetActive(true);
                _loadingBar.value = 0;
            }

        }
    }

}

