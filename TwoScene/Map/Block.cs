using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite[] spritesBlock;
    private SpriteRenderer _spriteRenderer;
    private int _heal = 40;
    private Rigidbody2D _rigidbody2D;
    private void OnEnable()
    {
        _heal = 40;
        _spriteRenderer.sprite = spritesBlock[0];
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
      
    }
    
    private void Update()
    {
        switch (_heal)
        {
            case >30:
                _spriteRenderer.sprite = spritesBlock[0];
                break;
            case >20 and < 30:
                _spriteRenderer.sprite = spritesBlock[1];
                break;
            case >10 and <=20:
                _spriteRenderer.sprite = spritesBlock[2];
                break;
            case > 0 and <= 10:
                _spriteRenderer.sprite = spritesBlock[3];
                break;
            case <= 0:
                gameObject.SetActive(false);
                _heal = 0;
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        _heal -= damage;
    }

    public void ResetBlock()
    {
        _heal = 40;
        _spriteRenderer.sprite = spritesBlock[0];

    }

    public int PriceReset() =>  (40 - _heal) / 10;



}
