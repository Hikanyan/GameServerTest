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

        public void StartGame()
        {
        }

        void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false; //ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}