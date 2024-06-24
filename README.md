# GameServerTest

## 環境

- Unity
  - URP
- UniTask
- UniRx
- VContainer
- Photon
- PlayFab

```
classDiagram
    class LootLifeTimeScope {
    }

    class TileScope {
    }

    class LobbyScope {
    }

    class InGameScope {
    }

    class ResultScope {
    }

    class DebugScope {
    }

    class LobbyInitializer {
        +initialize()
    }

    class PlayFabAuthService {
        +login()
        +logout()
    }

    class PlayFabController {
        +saveData()
        +loadData()
        +fetchFriends()
        +startGacha()
    }

    LootLifeTimeScope --> TileScope
    TileScope --> LobbyScope
    LobbyScope --> InGameScope
    InGameScope --> ResultScope
    ResultScope --> DebugScope

    LobbyScope --> LobbyInitializer
    LobbyInitializer --> PlayFabAuthService
    LobbyInitializer --> PlayFabController
    PlayFabController --> PlayFabAuthService

```
