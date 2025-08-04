using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    private int _amountOfCoins;
    private void Awake()
    {
        //TODO PlayerPrefs
        _amountOfCoins = 0;
    }
    public void ChangeCoinAmount(int amount)
    {
        _amountOfCoins += amount;
        _coinAmountText.text = _amountOfCoins.ToString();
    }

}
