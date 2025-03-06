using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidGameId = "4058828";
    [SerializeField] private string iosGameId = "4058829";

    [SerializeField] private bool testMode = true;

    [SerializeField] private string androidAdUnitId = "4058828";
    [SerializeField] private string iosAdUnitId = "4058829";

    public static AdManager Instance { get; private set; }
    private string gameId;
    private string adUnitId;

    private GameOverHandler gameOverHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAds()
    {
        gameId = androidGameId;
        adUnitId = androidAdUnitId;
        gameId = Application.platform == RuntimePlatform.Android ? androidGameId : iosGameId;
        adUnitId = Application.platform == RuntimePlatform.Android ? androidAdUnitId : iosAdUnitId;

        if (Advertisement.isInitialized) { return; }
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Unity Ads failed to load: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Unity Ads show failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adUnitId))
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                Debug.Log("Ad completed successfully.");
                gameOverHandler.ContinueAfterAd();
            }
            else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
            {
                Debug.Log("Ad was skipped.");
                gameOverHandler.Replay();
            }
            else if (showCompletionState == UnityAdsShowCompletionState.UNKNOWN)
            {
                Debug.Log("Ad failed to show.");
                gameOverHandler.LoadMainMenu();
            }
        }
    }

    internal void ShowAd(GameOverHandler gameOverHandler)
    {
        this.gameOverHandler = gameOverHandler;
        Advertisement.Load(adUnitId, this);
    }
}
