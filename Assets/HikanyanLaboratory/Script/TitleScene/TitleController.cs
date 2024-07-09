using System;
using Cysharp.Threading.Tasks;
using HikanyanLaboratory.DebugMode;
using PlayFab;
using PlayFab.ClientModels;
using VContainer.Unity;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitleController : IStartable
    {
        private bool _isAuthenticated = false;

        public void Start()
        {
            // 初期化や必要なセットアップをここで行う
            PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
            PlayFabAuthService.OnPlayFabError += OnPlayFabError;
            SlientLogin().Forget();
            GoggleLogin().Forget();
        }

        ~TitleController()
        {
            PlayFabAuthService.OnLoginSuccess -= OnLoginSuccess;
            PlayFabAuthService.OnPlayFabError -= OnPlayFabError;
        }

        public async UniTaskVoid SlientLogin()
        {
            if (!_isAuthenticated) await TryAuthenticate(Authtypes.Silent);
        }

        public async UniTaskVoid GoggleLogin()
        {
            if (!_isAuthenticated) await TryAuthenticate(Authtypes.Google);
        }

        private async UniTask TryAuthenticate(Authtypes authType)
        {
            try
            {
                PlayFabAuthService.Instance.Authenticate(authType);
                await UniTask.WaitUntil(() => _isAuthenticated).Timeout(TimeSpan.FromSeconds(10));
            }
            catch (TimeoutException)
            {
                DebugClass.Instance.LogWarning($"{authType}認証がタイムアウトしました。次の認証方法を試します。");
            }
        }

        private void OnLoginSuccess(LoginResult result)
        {
            _isAuthenticated = true;
            UnityEngine.Debug.Log("ログインに成功しました: " + result.PlayFabId);
        }

        private void OnPlayFabError(PlayFabError error)
        {
            _isAuthenticated = false;
            UnityEngine.Debug.LogError("ログインに失敗しました: " + error.GenerateErrorReport());
        }

        public void OnStartButtonPressed()
        {
            // スタートボタンが押されたときの処理
        }
    }
}