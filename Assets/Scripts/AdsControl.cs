using UnityEngine;
using System;
public class AdsControl : MonoBehaviour
{


    protected AdsControl()
    {
    }

    private static AdsControl _instance;
    public string AdmobID_Android, AdmobID_IOS, BannerID_Android, BannerID_IOS;
    public string UnityID_Android, UnityID_IOS, UnityZoneID;

    public static AdsControl Instance { get { return _instance; } }

    void Awake()
    {
        if (FindObjectsOfType(typeof(AdsControl)).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        MakeNewInterstial();
        RequestBanner();

        DontDestroyOnLoad(gameObject); //Already done by CBManager


    }


    public void HandleInterstialAdClosed(object sender, EventArgs args)
    {

     

    }

    void MakeNewInterstial()
    {
        
    }


    public void showAds()
    {
      
      
    }


    public bool GetRewardAvailable()
    {
        bool avaiable = false;
      
        return avaiable;
    }

    public void ShowRewardVideo()
    {

     
    }


    private void RequestBanner()
    {
    }

    public void ShowBanner()
    {
    }

    public void HideBanner()
    {
    }



    public void ShowFB()
    {
    }

    public void RateMyGame()
    {

    }
}

