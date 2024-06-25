using Cysharp.Threading.Tasks;
using UnityEditor;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitleController
    {
        private readonly TitlePresenter _presenter;

        public TitleController(TitlePresenter presenter)
        {
            _presenter = presenter;
        }
        
        public async UniTask Initialize()
        {
            await _presenter.Initialize();
            // その他の初期化コード
        }

        public void OnStartButtonPressed()
        {
            // スタートボタンが押されたときの処理
        }

        public void OnOptionButtonPressed()
        {
            // オプションボタンが押されたときの処理
        }
        
        public void OnExitButtonPressed()
        {
            // 終了ボタンが押されたときの処理
        }
    }
}