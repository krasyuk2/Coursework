using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowDown : MonoBehaviour
{
    private Camera _camera;
    public GameObject prefabArrow;

    public float spawnRate;
    private float _time;
    public bool isStart;
    

    private void Awake()
    {
        if (Camera.main != null) _camera = Camera.main;
    }

    public void StartBaf()
    {
        isStart = true;
    }

    public void LowRate()
    {
        spawnRate -= 0.2f;
    }


    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
                Vector2 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));
                Instantiate(prefabArrow, new Vector2(Random.Range(min.x, max.x), max.y + 0.2f),
                    Quaternion.identity);
                _time = spawnRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
       
    }
}
