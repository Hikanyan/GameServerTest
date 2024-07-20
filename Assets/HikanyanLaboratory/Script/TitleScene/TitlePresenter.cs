using Cysharp.Threading.Tasks;
using HikanyanLaboratory.System;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitlePresenter 
    {
        private readonly ManagerSceneController _sceneController;
        private readonly TitleController _titleController;

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