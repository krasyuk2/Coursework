using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    
    private GameObject _weaponParent;
    private SettingsGame _settingsGame;

    private void Awake()
    {
        _weaponParent = FindObjectOfType<HoldWeapon>().gameObject;
        _settingsGame = FindObjectOfType<SettingsGame>();
    }
    
    private void Start()
    {
        Time.timeScale = 0f;
        _settingsGame.isPause = false;
    }
    
    public void Create(GameObject weapon) // Вызывается при нажатии на кнопку и получаем ссылку на оружие 
    {
        GameObject gun = Instantiate(weapon, _weaponParent.transform);
        _settingsGame.isPause = true;
        DestroyCards();
        Time.timeScale = 1f;
    }
    
    void DestroyCards()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("CardWeapon");
        foreach (var card in cards)
        {
            Destroy(card);
        }
       
    }
    
}
