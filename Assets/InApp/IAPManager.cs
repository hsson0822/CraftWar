#if UNITY_ANDROID || UNITY_IPHONE
//#define RECEIPT_VALIDATION
#endif

using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine.Analytics;

#if RECEIPT_VALIDATION
using UnityEngine.Purchasing.Security;
#endif

public class IAPManager : MonoBehaviour, IStoreListener
{

    public static IAPManager instance;

	private static IStoreController storeController;
	private static IExtensionProvider extensionProvider;
	public string[] productIds;

    void PushReward(int index)
    {
        switch (index)
        {
            case 0:
				PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 10000);
				StoreManager.GetInstance().UpdateGold();
                break;

            default:
                Debug.Log("Index Error");
			break;
		}
	}


	void Success(string productId) {
		for (int i = 0; i < productIds.Length; i++) {
			if (string.Compare (productId, productIds [i],false) == 0) {
				PushReward (i);
			}
		}
	}

	void Fail() {
		// Fail
	}

	void Awake() {
		instance = this;
		DontDestroyOnLoad (gameObject);
	}
	void Start()
	{
		InitializePurchasing();
	}

	private bool IsInitialized()
	{
		return (storeController != null && extensionProvider != null);
	}

	public void InitializePurchasing()
	{
		if (IsInitialized())
			return;

		var module = StandardPurchasingModule.Instance();

		ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
		foreach (string id in productIds) {
			builder.AddProduct(id, ProductType.Consumable, new IDs
				{
					{ id, AppleAppStore.Name },
					{ id, GooglePlay.Name },
				});
		}
		UnityPurchasing.Initialize(this, builder);
	}
	public void BuyProductInt(int type) {
		BuyProductID (productIds [type]);
	}
	public void BuyProductID(string productId)
	{
		try
		{
			if (IsInitialized())
			{
				Product p = storeController.products.WithID(productId);
				if (p != null && p.availableToPurchase)
				{
					storeController.InitiatePurchase(p);
				}
				else
				{
					Debug.Log("구매 실패 #1");
				}
			}
			else
			{
				Debug.Log("구매 실패 #2");
			}
		}
		catch (Exception e)
		{
			Debug.Log("구매 실패 #" + e);
		}
	}

	public void RestorePurchase()
	{
		if (!IsInitialized())
		{
			Debug.Log ("초기화 실패");
			return;
		}

		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			var apple = extensionProvider.GetExtension<IAppleExtensions>();

			apple.RestoreTransactions
			(
				(result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
			);
		}
		else
		{
			Debug.Log ("현재 플랫폼은 지원하지 않습니다.");
		}
	}

	public void OnInitialized(IStoreController sc, IExtensionProvider ep)
	{
		storeController = sc;
		extensionProvider = ep;

		StoreManager.GetInstance().SetGoldText(storeController.products.WithID("Gold").metadata.localizedPriceString);
    }

    public void OnInitializeFailed(InitializationFailureReason reason)
	{
		Debug.Log ("초기화 실패 #" + reason);
	}
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		bool validPurchase = true;
		#if RECEIPT_VALIDATION
		var validator = new CrossPlatformValidator (GooglePlayTangle.Data (), AppleTangle.Data (), 
			                UnityChannelTangle.Data (), Application.identifier);
		try {
			var result = validator.Validate (args.purchasedProduct.receipt);
			Debug.Log ("Receipt is valid. Contents:");
			foreach (IPurchaseReceipt productReceipt in result) {
				Debug.Log (productReceipt.productID);
				Debug.Log (productReceipt.purchaseDate);
				Debug.Log (productReceipt.transactionID);
			}
		} catch (IAPSecurityException) {
			Debug.Log ("Invalid receipt, not unlocking content");
			validPurchase = false;
		}
		#endif
		if (validPurchase) {
			foreach (string id in productIds) {
				if (args.purchasedProduct.definition.id.Equals (id)) {
					Success (id);
				}
			}
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

    
}