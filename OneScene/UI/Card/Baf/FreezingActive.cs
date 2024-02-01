using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingActive : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefabLvlUpOne;
    public GameObject prefabLvlUpTwo;
    public GameObject prefabLvlUpTree;
    public float speedAngleLvlTree;
    public float timeRate;
    private float _time;
    private float _timeLive = 5f;
    private CameraGame _cameraGame;
    [HideInInspector]
    public GameObject _player;
    
    public bool isDamage;
    public bool isLvlTree;
    private void Awake()
    {
        _cameraGame = FindObjectOfType<CameraGame>();
        _player = GameObject.FindWithTag("Player");
    }

    private bool _isFreezeRotation = true;
    public void OneBaf()
    {
        prefab = prefabLvlUpOne;
        _isFreezeRotation = false;
        _timeLive = 15f;
        timeRate += 15f;
    }

    public void TwoBaf()
    {
        prefab = prefabLvlUpTwo;
        isDamage = true;
    }

    public void TreeBaf()
    {
        prefab = prefabLvlUpTree;
        isLvlTree = true;
        _timeLive = 10f;
        timeRate += 10;
    }
    public (float,float) Baf()
    {
        if (_time <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector2 dir = _cameraGame.ray.direction;
                GameObject go = Instantiate(prefab, _player.transform.position, Quaternion.identity);
                float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                if (_isFreezeRotation)
                {
                    go.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                }
                _time = timeRate;
                StartCoroutine(WaitDelete(go));
            }
        }
        else
        {
            _time -= Time.deltaTime;
        }

        return (_time, timeRate);
    }

    IEnumerator WaitDelete(GameObject go)
    {
        yield return new WaitForSeconds(_timeLive);
        Destroy(go);
    }

    
}
