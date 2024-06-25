using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace HikanyanLaboratory.Network
{
    public class LobbyInitializer : IStartable
    {
        // ロビーの初期化処理
        // Playerを生成する
        // ゲームの初期化処理（待機）

        readonly LobbyPresenter presenter;

        public LobbyInitializer(LobbyPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Start()
        {
            presenter.Initialize().Forget();
        }

        public void Initialize()
        {
            // ロビーの初期化処理
        }

        public void CreatePlayer()
        {
            // Playerを生成する
        }

        // public PlayerJoinedEvent JoinPlayer()
        // {
        //     // Playerを参加させる
        //     return new PlayerJoinedEvent();
        // }

        public void InitializeGame()
        {
            // ゲームの初期化処理（待機）
        }
    }
}