using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JukeBoxYouTube : MonoBehaviour
{
    public GameObject player;
    public GameObject box;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public float distance;
    public GameObject UI;
    
    private bool _isPlayMusic;
    private int _currentId = 0;

    private void Awake()
    {
        
        audioSource.clip = audioClips[0];
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "BarScene")
        {
            if(player == null) Destroy(gameObject);
            if (Vector2.Distance(player.transform.position, box.transform.position) < distance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!_isPlayMusic)
                        OnBox();
                    else OffBox();
                }

                DisplayUI(true);
            }
            else
            {
                DisplayUI(false);
            }
        }

        if (_isPlayMusic)
        {
            if (!audioSource.isPlaying)
            {
                OnClickButton(1);
            }
        }
        
    
    }

    void OnBox()
    {
        audioSource.Play();
        _isPlayMusic = true;
    }

    void OffBox()
    {
        audioSource.Stop();
        _isPlayMusic = false;
    }

    void DisplayUI(bool temp) => UI.SetActive(temp);

    public void OnClickButton(int temp)
    {
        if (audioClips.Length - 1 >= temp + _currentId && _currentId + temp > 0)
        {
            _currentId += temp;
        }
        else if (_currentId + temp < 0)
        {
            _currentId = audioClips.Length - 1;
        }
        else if (_currentId + temp > audioClips.Length - 1)
        {
            _currentId = 0;
        }

        audioSource.clip = audioClips[_currentId];
        audioSource.Play();
    }
}
