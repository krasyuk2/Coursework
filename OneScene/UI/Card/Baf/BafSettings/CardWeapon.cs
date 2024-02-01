

using System;
using System.Collections;
using UnityEngine;


public class CardWeapon : MonoBehaviour
{
    public GameObject WeaponPrefab;
    private CardManager _cardManager;
    private SpawnUI _spawnUI;
    private CardBafManager _cardBafManager;
    private HoldWeapon _holdWeapon;
    
    private void Awake()
    {
        _cardManager = FindObjectOfType<CardManager>();
        _spawnUI = FindObjectOfType<SpawnUI>();
        _cardBafManager = FindObjectOfType<CardBafManager>();
        _holdWeapon = FindObjectOfType<HoldWeapon>();
    }

 

    public void Click() // Нажатие передает в Manager ссылку на оружие
    {
        _cardManager.Create(WeaponPrefab);
    }

    public void RegList(int index)
    {
        _cardBafManager.RegList(index);
    }

    public void RegHold(int index = 0)
    {
        _holdWeapon.indexHold = index;
    }

    public void SpawnUi()
    {
        _spawnUI.Spawn();
    }

   
}
