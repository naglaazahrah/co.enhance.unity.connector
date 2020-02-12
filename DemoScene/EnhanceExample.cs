using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnhanceExample : MonoBehaviour
{
	private bool _isBannerAdShown = false;
	private bool _hasLocalNotificationPermission = false;

    void Awake() {
		// Ask for permission to show local notifications
		// It can be done at any point in the application

		if (!_hasLocalNotificationPermission)
			Enhance.RequestLocalNotificationPermission (OnPermissionGranted, OnPermissionRefused);
    
		// Set currency callback (offerwalls)
		// It is important to do in the beginning of the application's logic

		Enhance.SetReceivedCurrencyCallback (onCurrencyReceived);
		UpdateIAPLabel ();
	}

	// Interstitial Ad

    public void OnShowInterstitialAd() {
		// Check whether an interstitial ad is ready

		if (!Enhance.IsInterstitialReady()) {
			writeLog("Interstitial ad is not ready");
			return;
		}

		// The ad is ready

		Enhance.ShowInterstitialAd();
		writeLog ("Showing interstitial ad");
    }
		
	// Rewarded Ad

    public void OnShowRewardedAd() {
		// Check whether a rewarded ad is ready 

		if (!Enhance.IsRewardedAdReady()) {
			writeLog("Rewarded ad is not ready");
			return;
		}

		// The ad is ready, show it

		Enhance.ShowRewardedAd (OnRewardGranted, OnRewardDeclined, OnRewardUnavailable);
		writeLog("Showing rewarded ad");
    }

	private void OnRewardGranted(Enhance.RewardType rewardType, int rewardValue) {
		// Reward is granted (user watched video for example)
		// You can check reward type:

		if (rewardType == Enhance.RewardType.ITEM) {
			writeLog ("Reward granted (item)");
		} else if (rewardType == Enhance.RewardType.COINS) {
			writeLog ("Reward granted (coins)");
			writeLog ("Reward value: " + rewardValue);
		}
	}

	private void OnRewardDeclined() {
		// Reward is declined (user closed the ad for example)

		writeLog ("Reward declined");
	}

	private void OnRewardUnavailable() {
		// Reward is unavailable (network error for example)

		writeLog ("Reward unavailable");
	}

	// Banner Ad
		
    public void OnToggleBannerAd() {
		// Hide a banner ad if it's already visible

		if (_isBannerAdShown) {
			_isBannerAdShown = false;
			Enhance.HideBannerAd();
			writeLog ("Hiding banner ad");
			return;
		}

		// The ad isn't visible, check whether it's available

		if (!Enhance.IsBannerAdReady()) {
			writeLog ("Banner ad is not ready");
			return;
		}

		// The ad is available, display it

		Enhance.ShowBannerAdWithPosition(Enhance.Position.BOTTOM);
		_isBannerAdShown = true; 
		writeLog ("Showing banner ad");

		// This will show our ad at the bottom of the screen
		// You can also change the position to Enhance.Position.TOP
    }

	// Analytics

    public void OnLogEvent() {
		// Send a custom event to analytics network(e.g. Google Analytics)

		// Simple event:
        Enhance.LogEvent("event_type");
	
		// Event with an additional parameter:
		Enhance.LogEvent("event_type", "event_param_key", "event_param_value");

		writeLog ("Sent analytics event");
    }

	// Offerwall

	public void OnShowOfferwall() {
		// Check whether an offerwall is available

		if (!Enhance.IsOfferwallReady()) {
			writeLog ("Offerwall is not ready");
			return;
		}

		// The offerwall is ready, show it

		Enhance.ShowOfferwall();
		writeLog ("Showing offerwall");
	}

	// Special Offer

	public void OnShowSpecialOffer() {
		// Check whether a special offer is available

		if (!Enhance.IsSpecialOfferReady()) {
			writeLog ("Special offer is not ready");
			return;
		}

		// The special offer is ready, display it

		Enhance.ShowSpecialOffer();
		writeLog ("Showing special offer");
	}

	// Local Notification

	public void OnEnableLocalNotification() {
		// Check if we have permission to schedule notification

		if (!_hasLocalNotificationPermission) {
			writeLog ("Permission is not granted");
			return;
		}

		// We have permission, enable a local notification
		// It will appear 5 seconds after the app is deactivated

		Enhance.EnableLocalNotification ("Enhance", "Local Notification!", 5);
		writeLog ("Enabled local notification");
	}

	public void OnDisableLocalNotification() {
		// Cancel previously scheduled notification

		Enhance.DisableLocalNotification ();
		writeLog ("Disabled local notification");
	}

	// In App Purchases 

	public void OnPurchaseItem() {
		writeLog ("Attempting purchase for Enhance_SKU_One");
		Enhance.Purchases.AttemptPurchase ("Enhance_SKU_One", onPurchaseSuccess, onPurchaseFailed);
	}

	public void OnConsumeItem() {
		writeLog ("Attempting consume for Enhance_SKU_One");

		Enhance.Purchases.Consume ("Enhance_SKU_One", onConsumeSuccess, onConsumeFailed);
	}

	// GDPR

	public void OnGDPROptIn() {
		Enhance.ServiceTermsOptIn();
		writeLog("Called explicit opt-in");
	}

	public void OnGDPROptOut() {
		Enhance.ServiceTermsOptOut();
		writeLog("Called explicit opt-out");
	}

	public void OnShowGDPRDialog() {
		Enhance.RequiresDataConsentOptIn(OnOptInRequired, OnOptInNotRequired);
	}

	private void OnOptInRequired() {
		Enhance.ShowServiceOptInDialogs(OnDialogComplete);
	}

	private void OnOptInNotRequired() {
		// No action needed
	}

	private void OnDialogComplete() {
		writeLog("Finished displaying opt-in dialogs");
	}

	// Permission callbacks

	private void OnPermissionGranted() {
		_hasLocalNotificationPermission = true;
	}

	private void OnPermissionRefused() {
		_hasLocalNotificationPermission = false;
	}

	// Offerwall callback

	private void onCurrencyReceived(int amount) {
		writeLog ("Currency received: " + amount);
	}

	// Purchase Callback
	private void onPurchaseSuccess() {
		writeLog ("Purchase success");
		UpdateIAPLabel ();
	}

	private void onPurchaseFailed() {
		writeLog ("Purchase failed");
	}

	// Consume Callback
	private void onConsumeSuccess() {
		writeLog ("Consume success");
		UpdateIAPLabel ();
	}

	private void onConsumeFailed() {
		writeLog ("Consume failed");
	}

	private void UpdateIAPLabel() {
		Text logText = GameObject.Find("UI Canvas/IAP Text").GetComponent<UnityEngine.UI.Text>();
		//Enhance.Purchases.GetDisplayTitle("Enhance_SKU_One", "Default Title") + " - " +
		logText.text =  Enhance.Purchases.GetDisplayPrice("Enhance_SKU_One", "Default Price") +  " - Qty: " +  Enhance.Purchases.GetOwnedItemCount("Enhance_SKU_One");
	}

    // Non-enhance logic
	// Show logs on screen

	private int _lines = 1;

    private void writeLog(string text) {
		Text logText = GameObject.Find("UI Canvas/Log Output/Log Text").GetComponent<UnityEngine.UI.Text>();

		if (_lines == 6) {
			_lines = 0;	
			logText.text = "";
		}

		logText.text += text + "\n";
		_lines++;

		Debug.Log (text);
    }
}
