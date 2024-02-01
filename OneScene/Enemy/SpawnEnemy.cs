using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    public int CurretnLvl;
    private string _currentLvl;
    public int MaxLvl;
    public GameObject[] enemies;
    public TMP_Text TextCurrentLvl;
    private Animator _animatorText;

    public TMP_Text TextTimer;
    private Animator _animatorTextTimer;

    [Header("DisableObject")]
    public GameObject Timer;
    
    public GameObject[] spawnPos;
    
    private Timer _timer;

    private WinLvl _winLvl;
    private void Awake()
    {
        _animatorText = TextCurrentLvl.gameObject.GetComponent<Animator>();
        _animatorTextTimer = TextTimer.gameObject.GetComponent<Animator>();
        _timer = FindObjectOfType<Timer>();
        _winLvl = FindObjectOfType<WinLvl>();
    }

    private void Start()
    {
        StartCoroutine(NextLvl());
    }

    private bool _isWin = true;
    void FixedUpdate()
    {
        
        PrintLvl();
        if (isSpawnDone)
        {
            if (CurretnLvl < MaxLvl)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    StartCoroutine(NextLvl());
                    isSpawnDone = false;
                }
            }
            else
            {
                if (_isWin && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    _winLvl.Win(); 
                    _isWin = false;
                }
            }
        }
    }
    
    void Spawn()
    {
        switch (CurretnLvl)
        {
            case 1:
                StartCoroutine(ContentEnemyLvl(10,0,0,0,0,0,0,0,0,0,1));
                break;
            case 2:
                StartCoroutine(ContentEnemyLvl(15,5,2,0,0,0,0,0,0,0,1));
                break;
            case 3:
                StartCoroutine(ContentEnemyLvl(10,10,4,2,0,0,0,0,0,0,1));
                break;
            case 4:
                StartCoroutine(ContentEnemyLvl(10,10,5,5,0,0,0,0,0,0,1));
                break;
            case 5:
                StartCoroutine(ContentEnemyLvl(10,10,5,10,5,0,0,0,0,0,1));
                break;
            case 6:
                StartCoroutine(ContentEnemyLvl(10,10,5,10,5,5));
                break;
            case 7:
                StartCoroutine(ContentEnemyLvl(10,10,5,10,5,5,5));
                break;
            case 8:
                StartCoroutine(ContentEnemyLvl(10,10,5,10,10,6,10,1));
                break;
            case 9:
                StartCoroutine(ContentEnemyLvl(10,10,5,10,10,6,10,2,2));
                break;
            case 10:
                StartCoroutine(ContentEnemyLvl(10,10,5,10,10,6,10,2,2,5));
                break;
            
        }
        Timer.SetActive(true);
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
                GameObject enemy = Instantiate(enemies[i],
                    spawnPos[i].transform.GetChild(Random.Range(0, 4)).transform.position, Quaternion.identity);
                listCurrentCountEnemies.Add(enemy);
                AddHealAndDamageEnemy(enemy);
                yield return new WaitForSeconds(0.5f);
                
            }
        }
        print("done");
        isSpawnDone = true;
    }

    void AddHealAndDamageEnemy(GameObject enemy) // Добавляем хп + 1 за каждые 10 секунд игрового времени 
    {
        int add = (int)_timer.allTime / 10;
        enemy.GetComponent<Enemy>().Heal += add;

    }
    
    IEnumerator NextLvl()
    {

        Timer.SetActive(false);
        yield return StartCoroutine(TextAnimation());
        yield return StartCoroutine(SecToStart());
        Spawn();
        
    }

    IEnumerator TextAnimation()
    {
        _animatorText.SetBool(Animator.StringToHash("Start"),true);
        yield return new WaitForSeconds(0.7f);
        CurretnLvl += 1;
        yield return new WaitForSeconds(0.3f);
        _animatorText.SetBool(Animator.StringToHash("Start"),false);
    }

    IEnumerator SecToStart()
    {
     
        for (int i = 3; i > 0; i--)
        {
            TextTimer.text = i + "";
            _animatorTextTimer.SetBool(Animator.StringToHash("Start"),true);
            yield return new WaitForSeconds(1f);
            _animatorTextTimer.SetBool(Animator.StringToHash("Start"), false);
            yield return new WaitForSeconds(0.3f);
         
        }
    }

    void PrintLvl()
    {
        if (CurretnLvl < 10)
        {
            _currentLvl = $"0{CurretnLvl}/{MaxLvl}";
        }
        else
        {
            _currentLvl = $"{CurretnLvl}/{MaxLvl}";
        }

        TextCurrentLvl.text = _currentLvl;
    }

  
}
