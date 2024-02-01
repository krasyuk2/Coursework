using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGame : MonoBehaviour
{
    [Range(1,10)]
    public int scaleValue = 5;
    
   
    public Ray ray;
    
    private GameObject _player;
    private GameObject _cursor;
    private float _startPosZ;

    public GameObject posLeftDown;
    public GameObject posRightTop;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _cursor = GameObject.FindWithTag("Cursor");
    }
    private void Start()
    {
        _startPosZ = gameObject.transform.position.z;
    }
    private void Update()
    {
        Move();
            


    }

    void Move()
    {
        Vector2 posPlayer = _player.transform.position;
        //transform.position = new Vector3(posPlayer.x, posPlayer.y, _startPosZ); // если убрать то модельку дергает
        Vector2 posCursor = _cursor.transform.position;

        Vector2 dir2 =  posCursor - posPlayer;
        
        ray = new Ray(posPlayer, dir2);
        Vector2 pointCenter = ray.GetPoint( Vector2.Distance(posCursor, posPlayer) / scaleValue);
        if (posLeftDown != null)
        {
            // transform.position = new Vector3(pointCenter.x, pointCenter.y, _startPosZ);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(
                Mathf.Clamp(pointCenter.x, posLeftDown.transform.position.x, posRightTop.transform.position.x),
                Mathf.Clamp(pointCenter.y, posLeftDown.transform.position.y, posRightTop.transform.position.y),
                _startPosZ), Time.deltaTime * 60);
        }

        Debug.DrawRay(ray.origin,ray.direction,Color.cyan); //Отрисовка
    }
}
