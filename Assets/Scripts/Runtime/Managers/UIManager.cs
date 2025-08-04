using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private GameView _gameView;
    [SerializeField] private Selectable _emptySelectable;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SubscribeToEvents()
    {
        GameEventBus.OnExitButtonPressed += GameEventBusOnExitButtonPressed;
        GameEventBus.OnRestartButtonPressed += GameEventBusOnRestartButtonPressed;
        GameEventBus.OnSettingsButtonPressed += GameEventBusOnSettingsButtonPressed;
        GameEventBus.OnPlayerDead += GameEventBusOnPlayerDead;
        GameEventBus.OnCoinPickup += GameEventBusOnCoinPickup; ;
    }
    private void UnsubscribeFromEvents()
    {
        GameEventBus.OnExitButtonPressed -= GameEventBusOnExitButtonPressed;
        GameEventBus.OnRestartButtonPressed -= GameEventBusOnRestartButtonPressed;
        GameEventBus.OnSettingsButtonPressed -= GameEventBusOnSettingsButtonPressed;
        GameEventBus.OnPlayerDead -= GameEventBusOnPlayerDead;
        GameEventBus.OnCoinPickup -= GameEventBusOnCoinPickup;
    }
    private void GameEventBusOnSettingsButtonPressed()
    {
        UnselectAll();
    }
    private void GameEventBusOnRestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void GameEventBusOnExitButtonPressed()
    {
        Application.Quit();
        UnselectAll();
    }
    private void GameEventBusOnPlayerDead()
    {
        Time.timeScale = 0;
        _gameOverView.gameObject.SetActive(true);
    }
    private void GameEventBusOnCoinPickup()
    {
        _gameView.ChangeCoinAmount(1);
    }
    private void UnselectAll()
    {
        _emptySelectable.Select();
    }
}
