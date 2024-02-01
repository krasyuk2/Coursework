using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemyLvlTwo : MonoBehaviour
{
    public int CurretnLvl;
    public int MaxLvl;
    public GameObject[] enemies;
 
    private Camera _camera;
    private string _currentLvl;

    public bool isPause;
    public GameObject prefabStartSpawn;
    public GameObject textPressESpawn;
    public float distance;
    private GameObject _player;
    private HoldWeaponLvlTwo _holdWeaponLvlTwo;
    private ShopManager _shopManager;
    private PauseLvlTwo _pauseLvlTwo;

    private void Awake()
    {
        if (Camera.main != null) _camera = Camera.main;
        _player = GameObject.FindWithTag("Player");
        _holdWeaponLvlTwo = FindObjectOfType<HoldWeaponLvlTwo>();
        _shopManager = FindObjectOfType<ShopManager>();
        _pauseLvlTwo = FindObjectOfType<PauseLvlTwo>();
        // _winLvl = FindObjectOfType<WinLvl>();
    }

    private void Start()
    {
        Pause();
    }

    private void Update()
    {
        if (isPause && !_shopManager._isShop && !_pauseLvlTwo.isPause)
        {
            if (Vector2.Distance(prefabStartSpawn.transform.position, _player.transform.position) < distance)
            {
                textPressESpawn.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
               
                    AnimationPrefab();
                    _holdWeaponLvlTwo.EnableWeapon(true);
                    NextLvl();
                    isPause = false;
                }
            }
            else
            {
                textPressESpawn.SetActive(false);
            }
        }
        else
        {
            textPressESpawn.SetActive(false);
        }
    }

    private bool _isWin = true;
    void FixedUpdate()
    {
        
        if (isSpawnDone)
        {
            if (CurretnLvl < MaxLvl)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    Pause();
                    isSpawnDone = false;
                }
            }
            else
            {
                if (_isWin && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                   print("Win");
                }
            }
        }
    }
    
    void Spawn()
    {
        switch (CurretnLvl)
        {
            case 1:
                StartCoroutine(ContentEnemyLvl(10));
                break;
            case 2:
                StartCoroutine(ContentEnemyLvl(10,10));
                break;
                
         
            
        }

    }

    public List<GameObject> listCurrentCountEnemies = new List<GameObject>();
    

    private bool isSpawnDone;
    IEnumerator ContentEnemyLvl(params int[] countEnemies )
    {
        isSpawnDone = false;
        for (int i = 0; i < countEnemies.Length; i++)
        {
            for (int j = 0; j < countEnemies[i]; j++)
            {
                GameObject enemy = Instantiate(enemies[i], PosSpawnEnemy(), Quaternion.identity);
                listCurrentCountEnemies.Add(enemy);
                yield return new WaitForSeconds(0.5f);
                
            }
        }
        print("done");
        isSpawnDone = true;
    }

    Vector2 PosSpawnEnemy()
    {
        int randomNum = Random.Range(0, 4);
         
        Vector2 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
        Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));

        Vector2 posLeft = new Vector2(Random.Range(min.x - 5f, min.x - 1.5f), Random.Range(min.y - 1f, max.y + 1));
        Vector2 posTop =  new Vector2(Random.Range(min.x - 1.5f, max.x + 1.5f), Random.Range(max.y + 5f, max.y + 1.5f));
        Vector2 posRight =  new Vector2(Random.Range(max.x + 5f, max.x + 1.5f), Random.Range(min.y - 1f, max.y + 1));
        Vector2 posDown =  new Vector2(Random.Range(min.x - 1.5f, max.x + 1.5f), Random.Range(min.y - 5f, min.y - 1.5f));
        switch (randomNum)
        {
            case 0:
                return posLeft;
            case 1:
                return posTop;
            case 2:
                return posRight;
            case 3:
                return posDown;
            default:
                return Vector2.zero;
        }
    }

    void NextLvl()
    {

        CurretnLvl += 1;
        Spawn();
        
    }

    void Pause()
    {
        isPause = true;
        _holdWeaponLvlTwo.EnableWeapon(false);
    
        
    }

    void AnimationPrefab()
    {
        
    }

}
