using BOBCheats;
using PlayerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameCheats : CheatBase
{
    private const string WALLET_CATEGORY = "WALLET";

    [Cheat]
    public static void ResetLvlCheat()
    {
        GamePlayManager.Instance.ResetLvl();
    }

    [Cheat]
    public static void WinLvlCheat()
    {
        GamePlayManager.Instance.LvlSuccess(10);
    }

    [Cheat]
    public static void TestVibrationsCheat()
    {
        Handheld.Vibrate();
    }

    [Cheat]
    public static void TestVibrationsNativeCheat(int milliseconds)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibratorService = unityActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

        long amplitude = milliseconds;
        vibratorService.Call("vibrate", amplitude);
    }

    [Cheat]
    public static void ToggleShowDebugConsole()
    {
        Reporter rep = GameObject.FindObjectOfType<Reporter>();
        rep.showGuiOnGesture = !rep.showGuiOnGesture;
    }

    [Cheat(WALLET_CATEGORY)]
    public static void ResetPlayerWallet()
    {
        PlayerManager manager = PlayerManager.Instance;
        var prop = manager.GetType().GetField("wallet", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        prop.SetValue(manager, new PlayerWallet());
    }

    [Cheat(WALLET_CATEGORY)]
    public static void AddCoinsCheat(int value)
    {
        PlayerManager manager = PlayerManager.Instance;
        manager.Wallet.AddCoins(value);
    }

    [Cheat(WALLET_CATEGORY)]
    public static void AddPointsCheat(int value)
    {
        PlayerManager manager = PlayerManager.Instance;
        manager.Wallet.CompletedLvls.Last().Score = value;
    }
}
