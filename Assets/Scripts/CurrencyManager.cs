using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public TMP_Text coinText;
    [SerializeField]private int coins;

    public int Coins { get => coins; set => coins = value; }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
        coinText.text = coins.ToString();
    }
    public void AddCurrency(int num)
    {
        coins += num;
        PlayerPrefs.SetInt("Coins", coins);
        coinText.text = coins.ToString();
    }
}
