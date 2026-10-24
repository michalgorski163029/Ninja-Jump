
using UnityEngine;
using Unity.Services.LevelPlay;

public class BannerAds : MonoBehaviour
{
    [Header("LevelPlay")]
    [SerializeField] private string appKey = "28e28edd5";
    [SerializeField] private string bannerAdUnitId = "4k59omcrp8phb27b";

    private LevelPlayBannerAd bannerAd;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (PlayerPrefs.GetInt("AdsRemoved", 0) == 1)
        {
            Debug.Log("Reklamy wylaczone.");
            return;
        }

        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;

        LevelPlay.SetMetaData("is_test_suite", "enable");
        LevelPlay.Init(appKey);
#endif
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("AdsRemoved", 0) == 1 &&
            bannerAd != null)
        {
            bannerAd.DestroyAd();
            bannerAd = null;
            Debug.Log("Banner usuniety po zakupie!");
        }
    }

    private void OnInitSuccess(LevelPlayConfiguration configuration)
    {
        if (PlayerPrefs.GetInt("AdsRemoved", 0) == 1)
            return;

#if UNITY_ANDROID && !UNITY_EDITOR
        LevelPlay.LaunchTestSuite();
#endif

        var config = new LevelPlayBannerAd.Config.Builder()
            .SetSize(LevelPlayAdSize.BANNER)
            .SetPosition(LevelPlayBannerPosition.BottomCenter)
            .SetRespectSafeArea(true)
            .SetDisplayOnLoad(true)
            .Build();

        bannerAd = new LevelPlayBannerAd(bannerAdUnitId, config);

        bannerAd.OnAdLoaded += OnBannerLoaded;
        bannerAd.OnAdLoadFailed += OnBannerLoadFailed;

        bannerAd.LoadAd();
    }

    private void OnInitFailed(LevelPlayInitError error)
    {
        Debug.LogError("LevelPlay Init Error: " + error);
    }

    private void OnBannerLoaded(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Banner zaladowany!");
    }

    private void OnBannerLoadFailed(LevelPlayAdError error)
    {
        Debug.LogError("Blad bannera: " + error);
    }

    private void OnDestroy()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        LevelPlay.OnInitSuccess -= OnInitSuccess;
        LevelPlay.OnInitFailed -= OnInitFailed;
#endif

        if (bannerAd != null)
        {
            bannerAd.DestroyAd();
            bannerAd = null;
        }
    }
}
