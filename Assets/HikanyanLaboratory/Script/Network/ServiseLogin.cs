using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Script.TitleScene;
using PlayFab;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace HikanyanLaboratory.Network
{
    public class ServiseLogin : MonoBehaviour
    {
        [SerializeField] private Button _slientLoginButton;
        [SerializeField] private Button _goggleLoginButton;

        [SerializeField, Tooltip("ログインしたらアクティブ化するもの")]
        private GameObject _gameStartButton;

        //TitleController _titleController;
        //
        // [Inject]
        // public void Construct(TitleController titleController)
        // {
        //     _titleController = titleController;
        // }

        private readonly PlayFabController _titleController = new PlayFabController();
        
        private void Start()
        {
            _slientLoginButton.onClick.AddListener(() =>
            {
                UnityEngine.Debug.Log("slientLoginButton");
                _titleController.SlientLogin();
            });

            _goggleLoginButton.onClick.AddListener(() =>
            {
                UnityEngine.Debug.Log("goggleLoginButton");
                _titleController.SlientLogin();
            });
            _gameStartButton.SetActive(false);

            // ログイン済みの場合はログインボタンを非表示にする
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                UnityEngine.Debug.Log("ログイン済み");
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