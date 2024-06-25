using Cysharp.Threading.Tasks;
using HikanyanLaboratory.System;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitlePresenter
    {
        private readonly ManagerSceneController _sceneController;
        private readonly TitleUIManager _uiManager;
        private readonly TitleController _titleController;

        public TitlePresenter(
            ManagerSceneController sceneController,
            TitleUIManager uiManager,
            TitleController titleController
        )
        {
            _sceneController = sceneController;
            _uiManager = uiManager;
            _titleController = titleController;
        }


        public async UniTask Initialize()
        {
            _uiManager.Initialize();
            
            // その他の初期化コード
        }

        public void OnStartButtonPressed()
        {
            _titleController.OnStartButtonPressed();
            _ = _sceneController.ChangeScene("LobbyScene");
        }
    }
}