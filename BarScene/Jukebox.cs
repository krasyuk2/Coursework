using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jukebox : MonoBehaviour
{
    public float distance;
    public AudioClip[] _audioClips;
    public GameObject uiDisplay;
    private AudioSource _audioSource;
    public GameObject _player;
    private int _current;

    public GameObject jukeboxPrefab;
    private void Awake()
    {
        _audioSource = transform.GetChild(0).GetComponent<AudioSource>();
        _player = GameObject.FindWithTag("Player");
        _audioSource.clip = _audioClips[0];
        _current = 0;
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    private bool _isPlayMusic;
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "BarScene")
        {
            if (Vector2.Distance(_player.transform.position, jukeboxPrefab.transform.position) < distance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!_isPlayMusic)
                        OnBox();
                    else OffBox();
                }
                DisplayUi(true);

            }
            else
            {
                DisplayUi(false);
            }
        }

        if (_isPlayMusic)
        {
            if (!_audioSource.isPlaying)
            {
                print("temp");
                OnClickButton(1);
            }
        }
    }

    void DisplayUi(bool temp) => uiDisplay.SetActive(temp);

    void OnBox()
    {
       _audioSource.Play();
       _isPlayMusic = true;
    }

    void OffBox()
    {
        _audioSource.Stop();
        _isPlayMusic = false;
    }

    public void OnClickButton(int temp)
    {
        if (_current + temp + 1 <= _audioClips.Length && _current + temp >= 0)
        {
            _audioSource.clip = _audioClips[_current+temp];
            _current += temp;
        }
        else if (_audioClips.Length < _current + temp + 1)
        {
            _audioSource.clip = _audioClips[0];
            _current = 0;
        }
        else if (_current + temp < 0)
        {
            _audioSource.clip = _audioClips[_audioClips.Length-1];
            _current = _audioClips.Length - 1;
        }
        _audioSource.Play();
    }
}
