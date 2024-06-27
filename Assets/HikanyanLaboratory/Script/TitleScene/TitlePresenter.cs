using Cysharp.Threading.Tasks;
using HikanyanLaboratory.System;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitlePresenter : IStartable
    {
        private readonly ManagerSceneController _sceneController;
        private readonly TitleController _titleController;

        [Inject]
        public TitlePresenter(
            ManagerSceneController sceneController,
            TitleController titleController
        )
        {
            _sceneController = sceneController;
            _titleController = titleController;
        }

        public void Start()
        {
            //UnityEngine.Debug.Log("TitlePresenter Start");
        }

        public void OnStartButtonPressed()
        {
            _ = _sceneController.ChangeScene("LobbyScene");
        }
    }
}