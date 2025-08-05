using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private TextMeshProUGUI _bestScoreAmount;
    private int _amountOfCoins;
    private void Awake()
    {
        InitBestScore();
        ChangeCoinAmount(0);
    }
    public void ChangeCoinAmount(int amount)
    {
        _amountOfCoins += amount;
        _coinAmountText.text = _amountOfCoins.ToString();
        if (_amountOfCoins > GameData.HighScore)
        {
            ChangeBestScore(_amountOfCoins);
        }
    }
    private void InitBestScore()
    {
        _bestScoreAmount.text = GameData.HighScore.ToString();
    }
    public void ChangeBestScore(int newBest)
    {
        _bestScoreAmount.text = newBest.ToString();
        GameData.HighScore = newBest;
    }

}
