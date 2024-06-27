using System;
using HikanyanLaboratory.Debug;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UniRx;

public class PlayFabController
{
    private Subject<string> loginResultSubject = new Subject<string>();

    public IObservable<string> OnLoginResult => loginResultSubject;

    public void SlientLogin()
    {
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    public void GoggleLogin()
    {
        PlayFabAuthService.Instance.Authenticate(Authtypes.Google);
    }

    public PlayFabController()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += PlayFabLogin_OnLoginError;
    }

    ~PlayFabController()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabLogin_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError -= PlayFabLogin_OnLoginError;
    }

    private void PlayFabLogin_OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Success!");
        DebugClass.Instance.Log("Login Success!");
        loginResultSubject.OnNext("Login Success!");
        loginResultSubject.OnCompleted();
    }

    private void PlayFabLogin_OnLoginError(PlayFabError error)
    {
        Debug.Log("Login Failed: " + error.ErrorMessage);
        DebugClass.Instance.Log("Login Failed: " + error.ErrorMessage);
        loginResultSubject.OnNext("Login Failed: " + error.ErrorMessage);
        loginResultSubject.OnCompleted();
    }
}