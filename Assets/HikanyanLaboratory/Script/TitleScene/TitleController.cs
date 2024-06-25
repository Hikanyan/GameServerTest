using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Network;
using UnityEditor;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitleController
    {
        readonly TitlePresenter presenter;
        readonly ServiseLogin serviceLogin;

        public TitleController(TitlePresenter presenter, ServiseLogin serviceLogin)
        {
            this.presenter = presenter;
            this.serviceLogin = serviceLogin;
        }

        public async UniTask Initialize()
        {
            await presenter.Initialize();
            await serviceLogin.Login();
            // その他の初期化コード
        }

        public void OnStartButtonPressed()
        {
            // スタートボタンが押されたときの処理
        }
    }
}