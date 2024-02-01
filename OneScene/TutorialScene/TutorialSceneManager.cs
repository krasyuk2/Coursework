using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class TutorialSceneManager : MonoBehaviour
{
    public GameObject player;
    public Move move;
    public Animator animatorKillHim;
    public GameObject[] WSDA;
    public Animator animatorDialogPanel;
    public DialogManager dialogManager;
    public Dialog dialog;
    public AudioSource makeWeaponSound;
    public GameObject animationPrefabMakeWeapon;
    public GameObject cursor;
    public GameObject shadowCursor;
    public GameObject weapon;
    public GameObject enemy;
    public Animator animationCamera;

    public Camera camera;
    public GameObject enemySpawnPrefab;
    public GameObject animationPointEnemySpawnPrefab;
    
    public float timeRateSpawn;

    public GameObject wall;

    public GameObject textScore;
    public TMP_Text textCountScore;


    
    private float _time = 4f;
    private DinoTutorial _dinoTutorial;
    private SpriteRenderer _cursorRenderer;
    private bool _isSpawnEnemy = true;
    private Statistics _statistics;
    Vector2 _topRight = new Vector2(-12.21f, 6.33f);
    Vector2 _leftDown = new Vector2(12.13f, -6.57f);
    
    private void Awake()
    {
        _cursorRenderer = cursor.GetComponent<SpriteRenderer>();
        _dinoTutorial = enemy.GetComponent<DinoTutorial>();
        _statistics = FindObjectOfType<Statistics>();

    }

    IEnumerator Start()
    {
     
        yield return new WaitForSeconds(1.1f);
        
        if (PlayerPrefs.HasKey("TutorialScore"))
        {
            if (PlayerPrefs.GetInt("TutorialScore") < 10)
            {
                yield return StartCoroutine(IfKillMinFive());
                yield return StartCoroutine(OneLvl());
                yield return StartCoroutine(TwoLvl());
            }
            else
            {
                StartCoroutine(TwoLvl());
            }
        }
        else
        {
            yield return StartCoroutine(OneLvl());
            yield return StartCoroutine(TwoLvl());
        }
        
      
        //animationCamera.SetBool("Shake",true);
        
        
        yield return null;
    }

    IEnumerator IfKillMinFive()
    {
        dialogManager.StartDialog(dialog);
        StartCoroutine(dialogManager.TypeSentence("Oh, is it you again?"));
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(dialogManager.TypeSentence("What? Failed to get at least 10 kills?"));
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(dialogManager.TypeSentence("Well then you'll have to train again)"));
        yield return new WaitForSeconds(2.5f);
        dialogManager.EndDialog();
        yield return new WaitForSeconds(2f);
        
        
    }
    IEnumerator OneLvl()
    {
        dialogManager.StartDialog(dialog);
        StartCoroutine(dialogManager.TypeSentence("Hello"));
        yield return new WaitForSeconds(1f);
        StartCoroutine(dialogManager.TypeSentence("Let's see if you can walk"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(dialogManager.TypeSentence("Find the WSDA button")); 
        yield return new WaitForSeconds(2f);
        dialogManager.EndDialog();
        yield return StartCoroutine(ButtonWsda()); // Ждем пока нажмет на wsda
        yield return new WaitForSeconds(0.2f);
        dialogManager.StartDialog(dialog);
        StartCoroutine(dialogManager.TypeSentence("Well"));
        yield return new WaitForSeconds(0.5f);
        WSDA[0].gameObject.transform.parent.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.12f);
        StartCoroutine(dialogManager.TypeSentence("Okay, now take this"));
        yield return new WaitForSeconds(1.5f);
        makeWeaponSound.Play();
        animationPrefabMakeWeapon.transform.position = player.transform.position;
        animationPrefabMakeWeapon.SetActive(true);
        weapon.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        dialogManager.EndDialog();
        enemy.SetActive(true);
        animatorKillHim.SetBool("Start",true);
        _cursorRenderer.enabled = true;
        shadowCursor.SetActive(true);
        shadowCursor.transform.position = cursor.transform.position;
        yield return StartCoroutine(WaitTrueDeadDino()); // Ждем пока убьем цель
        shadowCursor.SetActive(false);
        animatorKillHim.SetBool("End",true);
    }
    IEnumerator TwoLvl()
    {
       
        _dinoTutorial.isDead = true;
        dialogManager.StartDialog(dialog);
        StartCoroutine(dialogManager.TypeSentence("Well done, you completed your training and now..."));
        yield return new WaitForSeconds(3f);
        dialogManager.EndDialog();
        _cursorRenderer.enabled = true;
        animationPrefabMakeWeapon.transform.position = player.transform.position;
        animationPrefabMakeWeapon.SetActive(true);
        weapon.SetActive(true);
        textScore.SetActive(true); // Счетчик убийств
        textCountScore.gameObject.SetActive(true); // Счетчик убийств
        yield return StartCoroutine(WaitForTeenKills()); // ждем пока убьет 10 врагов
        _isSpawnEnemy = false;
        yield return StartCoroutine(WaitAllDeadDino()); // ждем пока уюьет всех врагов 
        dialogManager.StartDialog(dialog);
        StartCoroutine(dialogManager.TypeSentence("Well"));
        yield return new WaitForSeconds(2f);
        dialogManager.EndDialog();
        AddSizeWall(); // смещаем стены 
        _topRight = new Vector2(-16.18f, 8.61f);
        _leftDown = new Vector2(16.06f, -8.52f);
        timeRateSpawn = 1f;
        yield return StartCoroutine(CameraAnimation()); // увеличваем камеру 
        _isSpawnEnemy = true;
    }

    private bool isSceneLoad;
    void Update()
    {
        if (shadowCursor.activeSelf && enemy != null)
        {
            shadowCursor.transform.position =
                Vector2.MoveTowards(shadowCursor.transform.position, enemy.transform.position, Time.deltaTime * 7f);
            if (Vector2.Distance(shadowCursor.transform.position, enemy.transform.position) < 1f)
            {
                shadowCursor.transform.position = cursor.transform.position;
            }
        } // Двигает курсор к врагу 

        if (_dinoTutorial.isDead && _isSpawnEnemy)
        {
            if (_time <= 0)
            {
                StartCoroutine(EnemySpawn());
                _time = timeRateSpawn;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        } // Спавнер 

        textCountScore.text = _statistics.Kills + "";
        if (move.Heal <= 0 && !isSceneLoad)
        {
            SceneManager.LoadScene("BarScene");
            isSceneLoad = true;
        }

     
    }

    IEnumerator ButtonWsda()
    {
        WSDA[0].gameObject.transform.parent.gameObject.SetActive(true);
        bool w, s, d, a;
        w = s = d = a = false;
        while (!w || !s || !d || !a)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                PaintGreenW(0);
                w = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                PaintGreenW(1);
                s = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                PaintGreenW(2);
                d = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                PaintGreenW(3);
                a = true;
            }
            yield return null;
        }

        yield return null;
    }

    IEnumerator WaitTrueDeadDino()
    {
        while (_dinoTutorial.isDead == false)
        {
           
            yield return null;
        }

        yield return null;
    }
    void PaintGreenW(int index) =>  WSDA[index].GetComponent<TMP_Text>().color = Color.green;
    
    IEnumerator EnemySpawn()
    {
        Vector2 randomPosSpawn =
            new Vector2(Random.Range(_topRight.x, _leftDown.x), Random.Range(_topRight.y, _leftDown.y));
        GameObject plus = Instantiate(animationPointEnemySpawnPrefab, randomPosSpawn, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(enemySpawnPrefab, randomPosSpawn, Quaternion.identity);
        DeletePlus(plus);
    }

    void DeletePlus(GameObject plus)
    {
        Destroy(plus);
    }

    IEnumerator CameraAnimation()
    {
        while (camera.orthographicSize <= 10f)
        {
            camera.orthographicSize += 0.02f;
            yield return new WaitForSeconds(0.001f);
        }

        yield return null;
    }

    IEnumerator WaitForTeenKills()
    {
        while (_statistics.Kills < 10)
        {
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitAllDeadDino()
    {
        while (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
        {
            yield return null;
        }
        yield return null;
    }

    void AddSizeWall()
    {
        wall.transform.GetChild(0).transform.localScale = new Vector3(1,20.05f,1);
        wall.transform.GetChild(0).transform.position = new Vector3(18.4200001f, 0.0399999991f, 0.199676841f);
        
        wall.transform.GetChild(1).transform.localScale = new Vector3(1,20.05f,1);
        wall.transform.GetChild(1).transform.position = new Vector3(-18.4099998f,-0.0500000007f,0.199676841f);
        
        wall.transform.GetChild(2).transform.localScale = new Vector3(1,35.65f,1);
        wall.transform.GetChild(2).transform.position = new Vector3(0.25f, 9.44999981f, 0.199676841f);
        
        wall.transform.GetChild(3).transform.localScale = new Vector3(1,35.65f,1);
        wall.transform.GetChild(3).transform.position = new Vector3(-0.119999997f, -10.71f, 0.199676841f);


    }


 

    public void SaveKillsScore()
    {
        if (PlayerPrefs.HasKey("TutorialScore"))
        {
            if (PlayerPrefs.GetInt("TutorialScore") < _statistics.Kills)
            {
                PlayerPrefs.SetInt("TutorialScore",_statistics.Kills);
            }
        }else  PlayerPrefs.SetInt("TutorialScore",_statistics.Kills);
    }
}
