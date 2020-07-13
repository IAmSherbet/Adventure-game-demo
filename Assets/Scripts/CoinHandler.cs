using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinHandler : MonoBehaviour
{
    public int coin = 0;
    [SerializeField] Text coinCountText;

    void Start()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        coinCountText.text = coin.ToString();
    }

    public void IncreaseCoin(int amount)
    {
        coin += amount;
        UpdateScore();
    }
}
