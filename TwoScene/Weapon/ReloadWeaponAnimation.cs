using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeaponAnimation : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject targetPosition;
    public GameObject current;

    private float _timeReload;
    private float _time = 0;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void StartAnimationReload(float timeReload)
    {
        _timeReload = timeReload;
    }
    private void OnEnable()
    {
        _time = 0;
    }
    void Update()
    {
        
        current.transform.position = Vector3.Lerp(startPosition.transform.position, targetPosition.transform.position, _time/_timeReload); 
        _time += Time.deltaTime;
        LocalScale();
    }

    
    void LocalScale()
    {
        transform.localScale = new Vector3(_player.transform.localScale.x, 1, 1);
    }
}
