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

    public void SilentLogin()
    {
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    public void GoogleLogin()
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

    public bool IsClientLoggedIn()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.Log("Client is logged in");
            DebugClass.Instance.Log("Client is logged in");
            return true;
        }
        else
        {
            Debug.Log("Client is not logged in");
            DebugClass.Instance.Log("Client is not logged in");
            return false;
        }
    }

    private void PlayFabLogin_OnLoginSuccess(LoginResult result)
    {
        loginResultSubject.OnNext("Login Success!");
        loginResultSubject.OnCompleted();
    }

    private void PlayFabLogin_OnLoginError(PlayFabError error)
    {
        loginResultSubject.OnNext("Login Failed: " + error.ErrorMessage);
        loginResultSubject.OnCompleted();
    }
}