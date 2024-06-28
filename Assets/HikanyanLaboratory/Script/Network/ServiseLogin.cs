using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Script.TitleScene;
using PlayFab;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace HikanyanLaboratory.Network
{
    public class ServiseLogin : MonoBehaviour
    {
        [SerializeField] private Button _silentLoginButton;
        [SerializeField] private Button _googleLoginButton;

        [SerializeField, Tooltip("ログインしたらアクティブ化するもの")]
        private GameObject _gameStartButton;

        private PlayFabController _playFabController = new PlayFabController();

        private void Start()
        {
            _silentLoginButton.onClick.AddListener(() => { _playFabController.SilentLogin(); });

            _googleLoginButton.onClick.AddListener(() => { _playFabController.GoogleLogin(); });

            _gameStartButton.SetActive(false);

            // ログイン結果を監視
            _playFabController.OnLoginResult
                .Subscribe(result =>
                {
                    if (result == "Login Success!")
                    {
                        HideLoginButton();
                    }
                })
                .AddTo(this);

            // ログイン済みの場合はログインボタンを非表示にする
            if (_playFabController.IsClientLoggedIn())
            {
                UnityEngine.Debug.Log("ログイン済み");
                HideLoginButton();
            }
        }

        private void HideLoginButton()
        {
            _silentLoginButton.gameObject.SetActive(false);
            _googleLoginButton.gameObject.SetActive(false);
            _gameStartButton.SetActive(true);
        }
    }
}