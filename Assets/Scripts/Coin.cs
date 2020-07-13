using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    private CoinHandler coinHandler;

    private void Start()
    {
        coinHandler = FindObjectOfType<CoinHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                break;
            case "Player":
                coinHandler.IncreaseCoin(coinValue);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
