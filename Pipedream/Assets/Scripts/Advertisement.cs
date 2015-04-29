using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class Advertisement : MonoBehaviour
{
    public static Advertisement Instance;

    private BannerView bannerView;
    private InterstitialAd interstitial;

    public enum AD_TYPE
    {
        Banner,
        Interstitial
    }
    public AD_TYPE adType = AD_TYPE.Banner;

    //banner position
    public AdPosition bannerPosition = AdPosition.Bottom;

    //ad unit IDs
    public string adUnitId_Android = "";
    public string adUnitId_iPhone = "";
    public string adUnitId_Others = "";

    //targeting
    public string[] keywords;
    public int year = 1990, month = 1, day = 1;
    public Gender gender = Gender.Unknown; //0 = unknown, 1 = male, 2 = female
    public bool tagForChildDirectedTreatment = false;

    //testing
    public bool showWhenReady = false;

    void Awake()
    {
        DontDestroyOnLoad(this);

        switch(adType)
        {
            case AD_TYPE.Banner:
            RequestBanner();
            bannerView.Show();
                break;
            case AD_TYPE.Interstitial:
            RequestInterstitial();
                break;
        }
    }

    private void RequestBanner()
    {
        Debug.Log("Requesting for banner");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
         string adUnitId = adUnitId_Android;
#elif UNITY_IPHONE
         string adUnitId = adUnitId_iPhone;
#else
         string adUnitId = adUnitId_Others;
#endif
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, bannerPosition);
        // Register for ad events.
        bannerView.AdLoaded += HandleAdLoaded;
        bannerView.AdFailedToLoad += HandleAdFailedToLoad;
        bannerView.AdOpened += HandleAdOpened;
        bannerView.AdClosing += HandleAdClosing;
        bannerView.AdClosed += HandleAdClosed;
        bannerView.AdLeftApplication += HandleAdLeftApplication;
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());
    }

    private void RequestInterstitial()
    {
        Debug.Log("Requesting for interstitial");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
         string adUnitId = adUnitId_Android;
#elif UNITY_IPHONE
         string adUnitId = adUnitId_iPhone;
#else
         string adUnitId = adUnitId_Others;
#endif

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Register for ad events.
        interstitial.AdLoaded += HandleInterstitialLoaded;
        interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.AdOpened += HandleInterstitialOpened;
        interstitial.AdClosing += HandleInterstitialClosing;
        interstitial.AdClosed += HandleInterstitialClosed;
        interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
        Debug.Log("Loading interstitial");
        if (showWhenReady)
        {
            ShowInterstitial();
        }
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest createAdRequest()
    {
        Debug.Log("Creating ad request");
        //AdRequest.Builder builder = new AdRequest.Builder();
/*
        //add keywords
        foreach (String keyword in keywords) {
            builder.AddKeyword(keyword);
        }
        //birthday
        builder.SetBirthday(new DateTime(year, month, day));
        //gender
        builder.SetGender(gender);
        //tag for child directed treatment
        builder.TagForChildDirectedTreatment(tagForChildDirectedTreatment);*/

        AdRequest request = new AdRequest.Builder().Build();
        Debug.Log("Created ad request");
        return request;

    }

    public void ShowInterstitial()
    {
        Debug.Log("Trying to show interstitial");
        if (interstitial.IsLoaded())
        {
            Debug.Log("Showing interstitial");
            interstitial.Show();
            showWhenReady = false;
        }
        else
        {
            print("Interstitial is not ready yet.");
            if (showWhenReady)
            {
                Invoke("ShowInterstitial", 1.0f);
            }
        }
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received.");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    void HandleAdClosing(object sender, EventArgs args)
    {
        print("HandleAdClosing event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("HandleInterstitialLoaded event received.");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {
        print("HandleInterstitialClosing event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("HandleInterstitialClosed event received");
        RequestInterstitial();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion
}