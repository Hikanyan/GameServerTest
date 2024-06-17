using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UniRx;

public class PlayFabController : MonoBehaviour
{
    private Subject<string> loginResultSubject = new Subject<string>();

    public IObservable<string> OnLoginResult => loginResultSubject;

    void Start()
    {
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    void OnEnable()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += PlayFabLogin_OnLoginError;
    }

    void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabLogin_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError -= PlayFabLogin_OnLoginError;
    }

    private void PlayFabLogin_OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Success!");
        loginResultSubject.OnNext("Login Success!");
        loginResultSubject.OnCompleted();
    }

    private void PlayFabLogin_OnLoginError(PlayFabError error)
    {
        Debug.Log("Login Failed: " + error.ErrorMessage);
        loginResultSubject.OnNext("Login Failed: " + error.ErrorMessage);
        loginResultSubject.OnCompleted();
    }
}