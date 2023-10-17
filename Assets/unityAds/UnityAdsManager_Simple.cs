using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityAdsManager_Simple : MonoBehaviour
{
    static UnityAdsManager_Simple instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public static UnityAdsManager_Simple Instance
    {
        get
        {
            return instance;
        }
    }

    public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
}
