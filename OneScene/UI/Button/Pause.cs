using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private CameraGame _cameraGame;
    private Weapon _weapon;
    private Statistics _statistics;
    private DateSave _dateSave;
    private SettingsGame _settingsGame;

    

    private void Awake()
    {
        _cameraGame = FindObjectOfType<CameraGame>();
        _statistics = FindObjectOfType<Statistics>();
        _dateSave = FindObjectOfType<DateSave>();
        _settingsGame = FindObjectOfType<SettingsGame>();

    }
    

    private bool isTimeScaleZero;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if(!isTimeScaleZero)Time.timeScale = 1;
            _cameraGame.enabled = true;
            _settingsGame.isPause = true;
            if (_weapon != null) _weapon.enabled = true;
            Destroy(gameObject);

        }
    
     
    
    }

    private void Start()
    {
        if (Time.timeScale == 0) isTimeScaleZero = true;
        Time.timeScale = 0;
        _cameraGame.enabled = false;
        if (FindObjectOfType<Weapon>() != null)
        {
            _weapon = FindObjectOfType<Weapon>();
            _weapon.enabled = false;
        }
        
    }

    public void OnClickResume()
    {
        
        _settingsGame.isPause = true;
        if(!isTimeScaleZero)Time.timeScale = 1;
        _cameraGame.enabled = true;
        if (_weapon != null) _weapon.enabled = true;
        Destroy(gameObject);

    }

    public void OnClickRestart()
    {
        SaveKills();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void OnClickMenu()
    {
        SaveKills();
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickBackToBar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("BarScene");
        
    }

    void SaveKills()
    {
        if (PlayerPrefs.HasKey("Kills"))
        {
            float tempKills = _dateSave.GetSave("Kills");

            tempKills += _statistics.Kills;
          
            _dateSave.SetSave("Kills",tempKills);
        }
        else
        {
            _dateSave.SetSave("Kills",_statistics.Kills);
        }

    }
}
