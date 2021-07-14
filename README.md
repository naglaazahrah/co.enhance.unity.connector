# Enhance Drag & Drop Library by Enhance, Inc.
# https://enhance.co/documentation
# 
# UNITY

Setup
-----

For quick tetsing you can use test Unity scene from "DemoScene" folder.


Interstitial Ads
----------------

Interstitial Ads are short static or video ads, usually shown between levels or when game is over. 


Example Usage:

if (Enhance.IsInterstitialReady()) {
    // The ad is ready, show it
    Enhance.ShowInterstitialAd();
}


Methods:

bool Enhance.IsInterstitialReady(
    string placement = Enhance.INTERSTITIAL_PLACEMENT_DEFAULT
)

Check if an interstitial ad from any of the included SDK providers is ready to be shown.
Placement is optional and specifies an internal placement (from the Enhance mediation editor). 
Returns true if any ad is ready, false otherwise.


void Enhance.ShowInterstitialAd(
    string placement = Enhance.INTERSTITIAL_PLACEMENT_DEFAULT
)

Display a new interstitial ad if any is currently available. The ad provider is selected based on your app's mediation settings. 
Placement is an optional internal placement (from the Enhance mediation editor).
Returns nothing.


Members:

string Enhance.INTERSTITIAL_PLACEMENT_DEFAULT
The default placement of ads, including interstitial ads.


Rewarded Ads
------------

Rewarded Ads are usually full-screen video ads which users can view to receive a reward inside the application, like an additional in-game currency or a health bonus.


Example Usage:

// Callbacks:

private void OnRewardGranted(Enhance.RewardType rewardType, int rewardValue) {
    if (rewardType == Enhance.RewardType.ITEM) {
        writeLog ("Reward granted (item)");
    } else if (rewardType == Enhance.RewardType.COINS) {
        writeLog ("Reward granted (coins)");
        writeLog ("Reward value: " + rewardValue);
    }
}

private void OnRewardDeclined() {
    writeLog ("Reward declined");
}

private void OnRewardUnavailable() {
    writeLog ("Reward unavailable");
}

if (Enhance.IsRewardedAdReady()) {
    // The ad is ready, show it
    Enhance.ShowRewardedAd (OnRewardGranted, OnRewardDeclined, OnRewardUnavailable);
}


Methods:

bool Enhance.IsRewardedAdReady(
    string placement = Enhance.REWARDED_PLACEMENT_NEUTRAL
)

Check if a rewarded ad from any of the included SDK providers is ready to be shown.
Placement is an optional internal placement (from the Enhance mediation editor).
Returns true if any ad is ready, false otherwise.


void Enhance.ShowRewardedAd(
    string placement = Enhance.REWARDED_PLACEMENT_NEUTRAL, 
    Action<RewardType, int> onRewardGrantedCallback,
    Action onRewardDeclinedCallback,
    Action onRewardUnavailableCallback
)

Display a new rewarded ad if any is currently available. The ad provider is selected based on your app's mediation settings.
Callbacks specify functions which are invoked when reward is granted, declined or unavailable.
Placement is an optional internal placement (from the Enhance mediation editor).
Returns nothing.


Members:

enum Enhance.RewardType.ITEM
The granted reward is a game-defined item.

enum Enhance.RewardType.COINS
The granted reward is a specified number of coins.


Banner Ads
----------

Banner Ads are small sized ads displayed on the screen as a rectangle filled with content without interrupting the flow of the app.


Example Usage:

// Toggle the banner ad

if (isBannerVisible) {
    Enhance.HideBannerAd();
    isBannerVisible = false;
}

else if (Enhance.IsBannerAdReady()) {
    // The ad is ready, show it
    Enhance.ShowBannerAdWithPosition(Enhance.Position.TOP);
    isBannerVisible = true;
}


Methods:

bool Enhance.IsBannerAdReady(
    string placement = Enhance.PLACEMENT_DEFAULT
)

Check if a banner ad from any of the included SDK providers is ready to be shown.
Placement is an optional internal placement (from the Enhance mediation editor).
Returns true if any ad is ready, false otherwise.


void Enhance.ShowBannerAdWithPosition(
    string placement = Enhance.PLACEMENT_DEFAULT,
    Enhance.Position position
)

Display a new banner ad if any is currently available. The ad provider is selected based on your app's mediation settings.
Position specifies the position of the ad on the screen.
Placement is an optional internal placement (from the Enhance mediation editor).
Returns nothing.


void Enhance.HideBannerAd()

Hide the banner ad which is currently visible, if any.
Returns nothing.


Members:

string Enhance.PLACEMENT_DEFAULT
The default placement of ads, including banner ads.

enum Enhance.Position.TOP
The top of the screen, use to set banner ads position. 

enum Enhance.Position.BOTTOM
The bottom of the screen, use to set banner ads position.


Offer Walls
-----------

Offer Walls show full screen of real world offers (e.g. surveys), usually with a reward offered in return for a completion.


Example usage:

private void OnCurrencyReceived(int amount) {
    writeLog ("Currency received: " + amount);
}

Enhance.SetReceivedCurrencyCallback (OnCurrencyReceived);

if(Enhance.IsOfferwallReady()) {
    Enhance.ShowOfferwall();
}


Methods:

void Enhance.SetReceivedCurrencyCallback(
    Action<int> onCurrencyReceived
)

Specify the function which is called every time the user receives a reward from any offerwall. We recommend that you use this function at the beginning of your app’s logic to be sure the callback is ready as soon as an offerwall sends the reward. This could happen at different times, even right after your app starts! As a parameter, this action will receive an amount of the granted currency (int).
Returns nothing.


bool Enhance.IsOfferwallReady(
    string placement = Enhance.PLACEMENT_DEFAULT
)

Check if an offerwall from any of the included SDK providers is ready to be shown.
Placement is an optional internal placement (from the Enhance mediation editor).
Returns true if any offerwall is ready, false otherwise.


void Enhance.ShowOfferwall(
    string placement = Enhance.PLACEMENT_DEFAULT
)

Display a new offerwall if any is currently available. The offerwall provider is selected based on your app's mediation settings.
Placement is an optional internal placement (from the Enhance mediation editor).
Returns nothing.


Members:

string Enhance.PLACEMENT_DEFAULT
The default placement of ads, including offer walls.


Special Offers
--------------

Special offers are real world offers (e.g. surveys). They are available through Enhance ZeroCode, but you can also request them manually from code.


Example Usage:

if (Enhance.IsSpecialOfferReady()) {
    // The offer is ready, show it
    Enhance.ShowSpecialOffer();
}


Methods:

bool Enhance.IsSpecialOfferReady(
    string placement = Enhance.PLACEMENT_DEFAULT
)

Check if a special offer from any of the included SDK providers is ready to be shown.
Placement is an optional internal placement (from the Enhance mediation editor).
Returns true if any offer is ready, false otherwise.


void Enhance.ShowSpecialOffer(
    string placement = Enhance.PLACEMENT_DEFAULT
)

Display a new special offer if any is currently available. The offer provider is selected based on your app's mediation settings.
Placement is an optional internal placement (from the Enhance mediation editor).
Returns nothing.


Members:

string Enhance.PLACEMENT_DEFAULT
The default placement of ads, including special offers.


Analytics
---------

The connector library allows you to send custom events to the hooked analytics networks.


Example Usage:

Enhance.LogEvent("game_over", "level", "1");
Enhance.LogEvent("user_exit");


Methods:

void Enhance.LogEvent(
    string eventType
)

Send an event without any additional parameters.
Returns nothing.


void Enhance.LogEvent(
    string eventType,
    string paramKey,
    string paramValue
)

Send an event with an additional parameter.
Returns nothing


void Enhance.LogEventWithParameters(
    string eventType,
    Dictionary<string, string> parameters
)

Send an analytics event with multiple additional parameters.


Local Notifications
-------------------

Local Notifications are reminders which show up on your screen after the app becomes inactive for a specific amount of time.


Example Usage:

private void OnPermissionGranted() {
    // Success
    Enhance.EnableLocalNotification("Game", "Play me!", 60);
}

private void OnPermissionRefused() {
    // Failure
}

Enhance.RequestLocalNotificationPermission(OnPermissionGranted, OnPermissionRefused);


Methods:

void Enhance.RequestLocalNotificationPermission(
    Action onPermissionGrantedCallback,
    Action onPermissionRefusedCallback
)

Request a permission from the user to show local notifications. This won't have any effect on Android devices as you don't need a permission to schedule local notifications there (onPermissionGrantedCallback will be still fired).
Returns nothing.


void Enhance.EnableLocalNotification(
    string title,
    string message,
    int delay
)

Schedule a new local notification, if possible. The notification will persist until you disable it manually. For example, if you set a notification for 60 seconds, it will invoke this notification 60 seconds after the app is closed every time.
Use seconds for delay.
Returns nothing.


void Enhance.DisableLocalNotification()

Disable any local notification that was previously enabled.


In-App Purchases
-------------------

The connector library provides a set of functions which help you to make use of different In-App Purchases SDKs in your application.


Example Usage:

private void OnPurchaseSuccess() {
    string price = Enhance.Purchases.GetDisplayPrice("my_product", "$5");

    string title = Enhance.Purchases.GetDisplayTitle("my_product", "My Product");

    string desc  = Enhance.Purchases.GetDisplayDescription("my_product", "Some useful item.");
}

private void OnPurchaseFailed() {
    // Failure
}

private void OnPurchasePending() {
    // purchase is pending
}

if (Enhance.Purchases.IsSupported()) {
    Enhance.Purchases.AttemptPurchase("my_product", OnPurchaseSuccess, OnPurchaseFailed);
}


Methods:

bool Enhance.Purchases.IsSupported()

Check if the In-App Purchasing is currently available in your application.
Returns true if purchasing is available, false otherwise.


void Enhance.Purchases.AttemptPurchase(
    string productName,
    Action onPurchaseSuccessCallback,
    Action onPurchaseFailedCallback,
    Action onPurchasePendingCallback
)

Start the purchase flow for the given product.
Product name is the reference name which you entered during the Enhance flow. 
Callbacks specify functions which are invoked when purchase is successful or not.
Returns nothing.


void Enhance.Purchases.Consume(
    string productName,
    Action onConsumeSuccessCallback,
    Action onConsumeFailedCallback
)

Consume the given product, if applicable (depends on the SDK provider).
Product name is the reference name which you entered during the Enhance flow.
Callbacks specify functions which are invoked when consume is successful or not.
Returns nothing.


bool Enhance.Purchases.IsItemOwned(
    string productName
)

Check if the given product is already owned. The result may be inaccurate, depending on whether the SDK provider stores the information about your products or not.
Product name is the reference name which you entered during the Enhance flow.
Returns true if the item is owned, false otherwise.


int Enhance.Purchases.GetOwnedItemCount(
    string productName
)

Check if the given product has pending status. The pending status means that user has completed the purchase transaction but it's not yet finalized - for instance, because of slow credit card or choosen cash payment method.
Product name is the reference name which you entered during the Enhance flow.
Returns true if the item has pending status, false otherwise.


int Enhance.Purchases.IsProductStatusPending(
    string productName
)

Get a number of the given product that user owns, or 0 if none. The result may be inaccurate, depending on whether the SDK provider stores the information about your products or not.
Product name is the reference name which you entered during the Enhance flow.
Returns a number of the given product copies.


void Enhance.Purchases.ManuallyRestorePurchases(
    Action onRestoreSuccessCallback,
    Action onRestoreFailedCallback
)

Manually restore purchases and update the user's inventory, if applicable (availability of this feature depends on the SDK provider).
Callbacks specify functions which are invoked when restore is successful or not.
Returns nothing.


string Enhance.Purchases.GetDisplayPrice(
    string productName,
    string defaultPrice
)

Get a localized display price of the given product, for example: "100zł", "100¥".
Product name is the reference name which you entered during the Enhance flow.
Default price will be used if a real one can't be fetched. 
Returns a string containing the localized display price.


string Enhance.Purchases.GetDisplayTitle(
    string productName,
    string defaultTitle
)

Get a localized display title of the given product.
Product name is the reference name which you entered during the Enhance flow.
Default title will be used if a real one can't be fetched.
Returns a string containing the localized display title.


string Enhance.Purchases.GetDisplayDescription(
    string productName,
    string defaultDescription
)

Get a localized display description of the given product.
Product name is the reference name which you entered during the Enhance flow.
Default description will be used if a real one can't be fetched.
Returns a string containing the localized display description.


Settings
--------

The settings class is provided for advanced users to change Enhance SDK configuration at runtime.

void Enhance.Settings.SetSdkConfiguration(
    string sdk_id,
    string config_key,
    string config_value
)

Change the value of an SDK configuration.


Demo Project
--------------

For a full implementation example, please see the demo project which can be found in the the distribution package.

