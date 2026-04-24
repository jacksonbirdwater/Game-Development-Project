using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int coinCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;

            UIManager.Instance.UpdateCoins(coinCount);

            Destroy(other.gameObject);
        }
    }
}