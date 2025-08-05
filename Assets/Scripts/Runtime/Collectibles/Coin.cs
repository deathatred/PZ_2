using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameEventBus.CoinPickup();
        print("pickup");
    }
}
