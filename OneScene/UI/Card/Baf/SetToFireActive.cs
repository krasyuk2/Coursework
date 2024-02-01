using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToFireActive : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefabLvlUpOne;
    public GameObject prefabLvlUpTwo;
    public float timeRate;
    public int DamageOnSecond;
    public int Damage;
    private float _time;
    private CameraGame _cameraGame;
    private GameObject _player;

    private bool _isOne;
    public void OneBaf()
    {
        prefab = prefabLvlUpOne;
    }

    public void TwoBaf()
    {
        prefab = prefabLvlUpTwo;
    }

    public void TreeBaf()
    {
        DamageOnSecond += 2;
        Damage += 10;

    }
    
    private void Awake()
    {
        _cameraGame = FindObjectOfType<CameraGame>();
        _player = GameObject.FindWithTag("Player");
    }

    public (float,float)  Baf()
    {
        if (_time <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector2 dir = _cameraGame.ray.direction;
                if (_isOne)
                {
                    Instantiate(prefab, _player.transform.position, Quaternion.identity);
                }
                else
                {
                    GameObject go = Instantiate(prefab, _player.transform.position, Quaternion.identity);
                    float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    go.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                    _time = timeRate;
                    StartCoroutine(WaitDelete(go));
                }
             
           
           
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
        yield return new WaitForSeconds(5f);
        Destroy(go);
    }
}
