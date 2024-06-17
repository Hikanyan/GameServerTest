using TMPro;
using UnityEngine;
using UniRx;

public class UIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private PlayFabController _playFabController;

    void Start()
    {
        // PlayFabControllerのOnLoginResultを購読して、ログイン結果を表示します
        _playFabController.OnLoginResult
            .Subscribe(result =>
            {
                _textMeshPro.text = result;
            });
    }
}