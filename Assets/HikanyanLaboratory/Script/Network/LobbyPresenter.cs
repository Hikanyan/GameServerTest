using Cysharp.Threading.Tasks;

namespace HikanyanLaboratory.Network
{
    public class LobbyPresenter
    {
        readonly LobbyView view;
        readonly LobbyUIManager uiManager;

        public LobbyPresenter(LobbyView view, LobbyUIManager uiManager)
        {
            this.view = view;
            this.uiManager = uiManager;
        }

        public async UniTask Initialize()
        {
            await view.Initialize();
            await uiManager.Initialize();
            // その他の初期化コード
        }

        public void UpdateLobby(string lobbyName)
        {
            view.UpdateLobby(lobbyName);
        }
    }
}