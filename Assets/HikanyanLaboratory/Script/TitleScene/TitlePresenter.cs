using HikanyanLaboratory.System;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitlePresenter
    {
        private readonly ManagerSceneController _sceneController;
        private readonly TitleUIManager _uiManager;
        
        public TitlePresenter(ManagerSceneController sceneController, TitleUIManager uiManager)
        {
            _sceneController = sceneController;
            _uiManager = uiManager;
        }
        
    }
}