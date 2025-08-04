using System;
using UnityEngine;

public class GameEventBus : MonoBehaviour
{
    public static event Action OnRestartButtonPressed;
    public static event Action OnSettingsButtonPressed;
    public static event Action OnExitButtonPressed;
    public static event Action OnPlayerDead;
    public static event Action OnCoinPickup;

    public static void RestartPressed()
    {
        OnRestartButtonPressed?.Invoke();
    }
    public static void SettingsPressed()
    {
        OnSettingsButtonPressed?.Invoke();
    }
    public static void ExitPressed()
    {
        OnExitButtonPressed?.Invoke();
    }
    public static void PlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
    public static void CoinPickup()
    {
        OnCoinPickup?.Invoke();
    }
}
