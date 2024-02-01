using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RechargeAnimation : MonoBehaviour
{
    public GameObject current;
    public GameObject targetPosition;
    public GameObject startPosition;
    public float time = 0;


    private GameObject _player;
    private Weapon _weapon;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _weapon = GameObject.FindWithTag("Weapon").GetComponent<Weapon>();
      
    }

    void Update()
    {
        
        current.transform.position = Vector3.Lerp(startPosition.transform.position, targetPosition.transform.position, time/_weapon.rechargeRate); 
        time += Time.deltaTime;
        LocalScale();
    }

    
    void LocalScale()
    {
        transform.localScale = new Vector3(_player.transform.localScale.x, 1, 1);
    }
}
