using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using InstantGamesBridge.Common;

public class VKScript : MonoBehaviour
{
    private InterstitialState _state;
    public GameObject gameplay;
    private void Start()
    {
        
        
        
    }

    private void Update()
    {


    }
    #region Social
    public void Invite()
    {
        Bridge.social.InviteFriends();
    }
    #endregion

    #region Interstitial
    public void Inter()
    {
        Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
        Bridge.advertisement.ShowInterstitial();
    }

    private void OnInterstitialStateChanged(InterstitialState stateInter)
    {
        switch (stateInter)
        {
            case InterstitialState.Opened:
                AudioListener.pause = true;
                break;
            case InterstitialState.Failed:
                AudioListener.pause = false;
                Bridge.advertisement.interstitialStateChanged -= OnInterstitialStateChanged;
                break;
            case InterstitialState.Closed:
                AudioListener.pause = false;
                Bridge.advertisement.interstitialStateChanged -= OnInterstitialStateChanged;
                break;
            case InterstitialState.Loading:
                AudioListener.pause = false;
                break;

        }

    }
    #endregion

    #region Reward
    public void Reward()
    {
        Bridge.advertisement.rewardedStateChanged += RewardedResult;
        Bridge.advertisement.ShowRewarded();
    }
    private void RewardedResult(RewardedState state)
    {
        switch (state)
        {
            case RewardedState.Opened:
                AudioListener.pause = true;
                break;
            case RewardedState.Failed:
                AudioListener.pause = false;
                Bridge.advertisement.rewardedStateChanged -= RewardedResult;
                break;
            case RewardedState.Closed:
                AudioListener.pause = false;
                Bridge.advertisement.rewardedStateChanged -= RewardedResult;
                break;
            case RewardedState.Loading:
                AudioListener.pause = false;
                break;
            case RewardedState.Rewarded:
                if (PlayerPrefs.GetInt("Rewarded") == 1)
                {
                    gameplay.GetComponent<Gameplay>().Hint();
                    //Bridge.advertisement.rewardedStateChanged -= RewardedResult;
                }
                if (PlayerPrefs.GetInt("Rewarded") == 2)
                {
                    gameplay.GetComponent<Gameplay>().Next();
                    //Bridge.advertisement.rewardedStateChanged -= RewardedResult;
                }
                break;
        }

        /*
        if (state.Equals("Opened") || state.Equals("Rewarded"))
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
            
        }
        //Состояние Rewarded, выдача награды 1 и 2
        if (state.Equals("Rewarded"))
        {
            if (PlayerPrefs.GetInt("Rewarded") == 1)
            {
                gameplay.GetComponent<Gameplay>().Hint();
                Bridge.advertisement.rewardedStateChanged -= RewardedResult;
            }
            if (PlayerPrefs.GetInt("Rewarded") == 2)
            {
                gameplay.GetComponent<Gameplay>().Next();
                Bridge.advertisement.rewardedStateChanged -= RewardedResult;
            }
        }
        */
    }
    #endregion

    #region Game Logic
    public void RewardHint()
    {
        PlayerPrefs.SetInt("Rewarded", 1);
    }

    public void RewardNext()
    {
        PlayerPrefs.SetInt("Rewarded", 2);
    }
    #endregion
}
