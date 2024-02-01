using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SaoBaf : MonoBehaviour
{
    public GameObject[] prefabs;
    private SwordWeapon _swordWeapon;
    private CameraGame _cameraGame;
    private GameObject _player;
    public bool isStart;
    public int damage;
    private void Awake()
    {
        _cameraGame = FindObjectOfType<CameraGame>();
        _player = GameObject.FindWithTag("Player");
    }
    private bool right;
    private bool up;
    private bool left;
    private bool down;

    public void StartBaf()
    {
        _swordWeapon = FindObjectOfType<SwordWeapon>();
        isStart = true;
    }
    (float x, float y) pos;
    void Spawn()
    {
        if (Input.GetButtonDown("Fire1") && _swordWeapon.isFire1)
        {
            if (_swordWeapon.FireTime <= 0)
            {

                pos.x = _cameraGame.ray.GetPoint(1).x - _player.transform.position.x;
                pos.y = _cameraGame.ray.GetPoint(1).y - _player.transform.position.y;
                switch (pos)
                {
                    case (>=0.8f and <=1.5f, <=0.3f and >=-0.3f ):
                        if (right == false)
                        {
                            Instantiate(prefabs[0], _swordWeapon.gameObject.transform.position, Quaternion.identity);
                            print("Right");
                            right = true;
                            
                        }else Reset();
                        break;
                    case (<=0.3f and >=-0.3f , <=-0.8f and >=-1.5f):
                        if (down == false)
                        {
                            Instantiate(prefabs[1], _swordWeapon.gameObject.transform.position, Quaternion.identity);
                            print("Down");
                            down = true;
                        }else Reset();
                        break;
                    case (<=-0.8f and >=-1.5f, <=0.3f and >=-0.3f):
                        if (left == false)
                        {
                            Instantiate(prefabs[2], _swordWeapon.gameObject.transform.position, Quaternion.identity);
                            print("Left");
                            left = true;
                        }
                        else Reset();
                        break;
                    case ( <=0.3f and >=-0.3f,>=0.8f and <=1.5f):
                        if (up == false)
                        {
                            Instantiate(prefabs[3], _swordWeapon.gameObject.transform.position, Quaternion.identity);
                            print("Up");
                            up = true;
                        }else Reset();
                        break;
                    default:
                        Reset();
                        break;
                }
            }
        }    
    }

    void Reset()
    {
        print("Reset");
        right = false;
        left = false;
        down = false;
        up = false;
    }
    private void Update()
    {
        if(isStart) Spawn();
        if (up && down && left && right)
        {
            StartCoroutine(WaitForSecond());
           Reset();
        }
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(prefabs[4], _player.gameObject.transform.position, Quaternion.identity);
        
    }
}
