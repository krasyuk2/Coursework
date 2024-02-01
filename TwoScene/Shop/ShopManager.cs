using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPrefab;
    public TMP_Text textCoin;
    private SpawnEnemyLvlTwo _spawnEnemy;
    public bool _isShop;
    private StatisticsLvlTwo _statisticsLvlTwo;

    private void Awake()
    {
        _spawnEnemy = FindObjectOfType<SpawnEnemyLvlTwo>();
        _statisticsLvlTwo = FindObjectOfType<StatisticsLvlTwo>();
    }

    private bool blockOpenShop = true;
    private void Update()
    {
        if (blockOpenShop)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (!_isShop) EnterShop();
                else ExitShop();
            }
        }

        textCoin.text = _statisticsLvlTwo.coin + "";

    }

    public void EnterShop()
    {
        if (_spawnEnemy.isPause)
        {
            shopPrefab.SetActive(true);
            _isShop = true;
        }
    }

    public void ExitShop()
    {
        shopPrefab.SetActive(false);
        _isShop = false;
    }

    public void BlockOpenShop(bool trueOrFalse)
    {
        blockOpenShop = trueOrFalse;
    }
}
