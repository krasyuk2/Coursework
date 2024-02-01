using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class SwordBackObject : MonoBehaviour
{
    private Camera _camera;
    public GameObject currentEnemy;
    public GameObject _moveToward;
    public float speedAngle;
    private GameObject _player;
    private SwordBack _swordBack;
    public float TimeRate;
    private float _time;
    private GameObject _center;
    public GameObject prefabSword;
    private GameObject _sword;
    public bool isDamage;

    private void Awake()
    {
        if (Camera.main != null) _camera = Camera.main;
        _swordBack = FindObjectOfType<SwordBack>();
        _player = GameObject.FindWithTag("Player");
        
    }

    private void Start()
    {
        _time = TimeRate;
        _sword = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (_time <= 0)
        {
            min = _camera.ViewportToWorldPoint(new Vector3(0, 0));
            max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
            if (!delete && !isMove)
            {
                FindEnemy();
                DirEnemy();
            }
            
        }
        else
        {  
         
            temp = Mathf.Lerp(temp, -90, Time.deltaTime * speedAngle);
            transform.rotation = Quaternion.Euler(0, 0, temp);
            transform.position = Vector2.MoveTowards(transform.position, _moveToward.transform.position, Time.deltaTime * 4f);
            _time -= Time.deltaTime;
        }

        if (!delete && !isMove)
        {
            FindEnemy();
        }

    }
    
    public void RegIndex(GameObject go)
    {
        _moveToward = go;
    }
    
    
    private List<GameObject> _enemyList = new List<GameObject>();
    private Vector3 min;
    private Vector3 max;
    
    void FindEnemy()
    {
        if (currentEnemy == null)
        {
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Vector2 pos = enemy.transform.position;
                if (pos.x < max.x && pos.x > min.x && pos.y > min.y && pos.y < max.y)
                {

                    _enemyList.Add(enemy);
                }
            }
            if (_enemyList.Count > 0)
            {
                GameObject go = _enemyList[Random.Range(0, _enemyList.Count)];
                if (go != null) currentEnemy = go;

            }
            if(enemies.Length == 0 || _enemyList.Count == 0)
            {
                temp = Mathf.Lerp(temp, -90, Time.deltaTime * speedAngle);
                    transform.rotation = Quaternion.Euler(0, 0, temp);
                    transform.position = Vector2.MoveTowards(transform.position, _moveToward.transform.position, Time.deltaTime * 4f);
                
            }
        }
    }

    float temp = -45;
    private float rotZ;
    private Vector3 dir;
    private bool flag;
    void DirEnemy()
    {
        isDamage = true;
        if (currentEnemy != null && !isMove)
        {
            if (!flag)
            {
                dir = currentEnemy.transform.position - transform.position;
                Debug.DrawRay(transform.position,dir,Color.black);
                rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                temp = Mathf.Lerp(temp, rotZ, Time.deltaTime * speedAngle);
                if ((int)temp == (int)rotZ)
                {
                    flag = true;
                }
                transform.position =
                    Vector3.Slerp(transform.position, currentEnemy.transform.position, Time.deltaTime / 10);
                transform.rotation = Quaternion.Euler(0, 0, temp);
                
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                StartCoroutine(Move());
            }
            
            
            
        }
    }

    public bool isMove;
    private bool isDeleteStart = true;
    IEnumerator Move()
    {
        if (_sword != null)
        {
            isMove = true;
            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            _sword.GetComponent<Rigidbody2D>().velocity =
                dir.normalized * _swordBack.BulletForce;
            if (isDeleteStart)
            {
                StartCoroutine(DeleteSword());
                isDeleteStart = false;
            }
        }

    }

    private bool delete;
    IEnumerator DeleteSword()
    {
        
        yield return new WaitForSeconds(5f);
        isDamage = false;
            Destroy(_sword);
            currentEnemy = null;
            transform.position = _player.transform.position;
            isMove = false;
            flag = false;
            delete = true;
            _enemyList.Clear();
            StartCoroutine(CreateSword());

        
    }

    IEnumerator CreateSword()
    {
        yield return new WaitForSeconds(3f);
        transform.position = _player.transform.position;
        _sword = Instantiate(prefabSword, gameObject.transform);
        _sword.transform.position = transform.position;
        delete = false;
        isDeleteStart = true;
        _time = TimeRate;
    }
    




   

}
