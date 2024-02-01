using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    public AudioSource deadSound;
    public GameObject Weapon;
    private Move _move;
    private Cursor _cursor;
    private Camera _camera;
    private CameraGame _cameraGame;
    private GameObject _canvas;
    private Rigidbody2D _rigidbody2DPlayer;
    private bool isDead = true;

    public GameObject prefabMenu;
    private void Awake()
    {
        _move = FindObjectOfType<Move>();
        _cursor = FindObjectOfType<Cursor>();
        if (Camera.main != null) _camera = Camera.main;
        _rigidbody2DPlayer = _move.gameObject.GetComponent<Rigidbody2D>();
        _canvas = GameObject.FindWithTag("Canvas");
        _cameraGame = FindObjectOfType<CameraGame>();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        Dead();
    }

  

    void Dead()
    {
        if (_move.Heal <= 0)
        {
            if (isDead)
            {
                Instantiate(prefabMenu,_canvas.transform);
                deadSound.Play();
                _cameraGame.enabled = false;
                _rigidbody2DPlayer.velocity = new Vector2(0, 0);
                _move.enabled = false;
                Weapon.SetActive(false);
                isDead = false;
            }
        }
    }

    
}
