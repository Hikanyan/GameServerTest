using Cysharp.Threading.Tasks;
using PlayFab;
using UnityEngine;
using UnityEngine.UI;

namespace HikanyanLaboratory.Network
{
    public class ServiseLogin : MonoBehaviour
    {
        [SerializeField] private Button _slientLoginButton;
        [SerializeField] private Button _goggleLoginButton;

        [SerializeField, Tooltip("ログインしたらアクティブ化するもの")]
        private GameObject _gameStartButton;


        private void Start()
        {
            _slientLoginButton.onClick.AddListener(() => PlayFabAuthService.Instance.Authenticate(Authtypes.Silent));
            _goggleLoginButton.onClick.AddListener(() => PlayFabAuthService.Instance.Authenticate(Authtypes.Google));
            _gameStartButton.SetActive(false);
            Login();
        }

        public void Login()
        {
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                HideLoginButton();
            }
        }

        void HideLoginButton()
        {
            _slientLoginButton.gameObject.SetActive(false);
            _goggleLoginButton.gameObject.SetActive(false);
            _gameStartButton.SetActive(true);
        }
    }
}