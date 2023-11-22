using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;

public class VKScript : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void Inter()
    {
        Bridge.advertisement.ShowInterstitial();
    }

    public void Reward()
    {
        Bridge.advertisement.ShowRewarded();
    }
    public void Share()
    {
        Bridge.social.InviteFriends();
    }

}
