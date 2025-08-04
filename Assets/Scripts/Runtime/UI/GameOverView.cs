using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartButtonClick);
        _settingsButton.onClick.AddListener(SettingsButtonClick);
        _exitButton.onClick.AddListener(ExitButtonClick);
    }
    private void OnDisable()
    {
        _restartButton?.onClick.RemoveListener(RestartButtonClick);
        _settingsButton?.onClick.RemoveListener(SettingsButtonClick);
        _exitButton?.onClick.RemoveListener(ExitButtonClick);
    }
    private void RestartButtonClick()
    {
        GameEventBus.RestartPressed();
    }
    private void SettingsButtonClick()
    {
        GameEventBus.SettingsPressed();
    }
    private void ExitButtonClick()
    {
        GameEventBus.ExitPressed();
    }
}
