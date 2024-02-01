using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class Weapon : MonoBehaviour
{
    private Animator weaponAnimator;
    public GameObject prefabBullet;
    public float SizeBullet;
    public GameObject spawn; 
    public GameObject reloadPrefab;
    private GameObject reloadAnimation;
    private GameObject _spawnBullet;
    private GameObject _player;
    private GameObject _spawnAmmoText;
    public AudioSource FireSound;
    public AudioSource RechargeSound;
    public AudioSource NoAmmoSound;
    public TMP_Text ammoText;
    private TMP_Text text;
    public float fireRate;
    public int Damage;
    public float bulletForce;
    public int maxMagazine = 10;
    public int magazine;                          // Текущее колчиесво потрон
    public float FireTime;
    public float rechargeRate = 2f;
    private float RecharheTime;
    public float scatter;
    [Range(0f, 2f)] public float spawnDistance = 1f;
    private CameraGame _cameraGame;
    private bool isRecharge = false;
    private bool isPlaySoundNoAmmo = true;
    private bool isStartRecharge;

    private GameObject _cursor;
    //Fire 2
    public bool isFireTwo;
    public AudioSource Fire2Sound;
    public AudioSource StartFire2Sound;
    public float Fire2Rate = 0.47f;
    private float FireTime2;
    private bool _isFire2;
    private bool isPlayStartFire2Sound = true;
    public GameObject prefabBulletFire2;
    public float bulletForceFire2 = 10f;
    [HideInInspector]
    public float CoolDawnFire2Time;
    public float CoolDawnFire2Rate = 5f;
    [HideInInspector]
    public bool _mouseFire2Down;
    public int maxBulletCountFire2;

    public delegate void FireDelegate();
    public FireDelegate fireDelegate;

    private DateSave _dateSave;

    delegate void CountBulletTrigger();
    private CountBulletTrigger _countBulletTrigger;
    private int calcFire = 0;
    private CountBulletTriggerWeapon _countBulletTriggerWeapon;
    
    private void Awake()
    {
        _spawnBullet =
            transform.GetChild(0)
                .gameObject; // Когда будешь цеплять скрипт к другим пушкам помни что точка спавна пуль первая в иерахии
        _cameraGame = FindObjectOfType<CameraGame>();
        _player = GameObject.FindWithTag("Player");
        _spawnAmmoText = transform.GetChild(1).gameObject; // Помни что второй в иерархии
        if (spawn == null) spawn = _player.transform.GetChild(0).gameObject;
        weaponAnimator = GetComponent<Animator>();
        _dateSave = FindObjectOfType<DateSave>();
        _cursor = GameObject.FindWithTag("Cursor");
        _countBulletTriggerWeapon = FindObjectOfType<CountBulletTriggerWeapon>();
        _countBulletTrigger += _countBulletTriggerWeapon.Method;

    }
    private void Start()
    {
        Damage += (int)_dateSave.GetSave("PistolDamage");
        fireRate -= _dateSave.GetSave("PistolKd");
        rechargeRate -= _dateSave.GetSave("PistolReload");

        fireDelegate += SpawnBulletFire;
        CreateAmmoText();
        magazine = maxMagazine;
        RecharheTime = rechargeRate;
    }
    
    private void Update()
    {
        if (!_mouseFire2Down)
        {
            if (FireTime <= 0)
            {
                Fire();
             
            }
            else
            {
                weaponAnimator.SetBool(Animator.StringToHash("Fire"), false);
                fire = false;
                FireTime -= Time.deltaTime;
            }
        }

        if (isFireTwo)
        {
            if (CoolDawnFire2Time <= 0)
            {
                FireTwo();
            }
            else CoolDawnFire2Time -= Time.deltaTime;
        }


        if (FireTime < 0) FireTime = 0;
        if (CoolDawnFire2Time < 0) CoolDawnFire2Time = 0;

        if(isStartRecharge)RechargeWeapon();
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(magazine != maxMagazine)isStartRecharge = true;
            
        }
        TriggerCountBullet();
    }

    private void FixedUpdate()
    {
        AmmoText(); // Рот того ебал, Если запихать это в Update то его дергает при движении но во время игры это не показывает пока не скампилируешь
    }

    public bool fire = false;
    void Fire()
    {
        if (!isRecharge) // Чтобы не стрылял пока идет перезарядка
        {
            if (Input.GetButton("Fire1"))
            {
                if (magazine > 0)
                {
                    weaponAnimator.SetBool(Animator.StringToHash("Fire"), true);
                    fire = true;
                    FireSound.Play();
                    fireDelegate?.Invoke();
                    calcFire += 1;
                    magazine--; // Забираем потроны 
                    FireTime = fireRate; // Дабаляем время для паузы между выстрелами 
                }
                else
                {
                    if (isPlaySoundNoAmmo) // сделано так чтобы при зажатии всего один раз пикнул
                    {
                        NoAmmoSound.Play();
                        isPlaySoundNoAmmo = false;
                        
                    }
                    else
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            isStartRecharge = true;
                        }
                    }
                  
                }
            }
        }
    }
    
    void SpawnBulletFire()
    {
        GameObject bullet = SpawnFire();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = DirFireAndSpeed();
       // rb.velocity = Quaternion.AngleAxis(Random.Range(-scatter,scatter), Vector3.forward) * rb.velocity; // разбросс 
    }
    
    public GameObject SpawnFire()
    {
        return Instantiate(prefabBullet, _spawnBullet.transform.position,
            _spawnBullet.transform.rotation);
    }
    public Vector2 DirFireAndSpeed()
    {
        // (_cameraGame.ray.direction * bulletForce);
        return  (_cursor.transform.position - _spawnBullet.transform.position ).normalized  * bulletForce;
    }
    
    public void FireTwo()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (magazine >= maxBulletCountFire2)
            {
                StartFire2Sound.Play();
                _mouseFire2Down = true;
                weaponAnimator.SetBool(Animator.StringToHash("Fire2"), true);
        
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            StartFire2Sound.Stop();
            weaponAnimator.SetBool(Animator.StringToHash("Fire2"),false);
            _mouseFire2Down = false;
            if (_isFire2)
            {
                Fire2Sound.Play();
                GameObject bullet = Instantiate(prefabBulletFire2, _spawnBullet.transform.position,
                    _spawnBullet.transform.rotation);
                    
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = _cameraGame.ray.direction * bulletForceFire2;
                rb.velocity = Quaternion.AngleAxis(Random.Range(-scatter,scatter), Vector3.forward) * rb.velocity; // разбросс 
                magazine -= maxBulletCountFire2;
                
                CoolDawnFire2Time = CoolDawnFire2Rate;
            }
            isPlayStartFire2Sound = true;
            FireTime2 = 0;
            _isFire2 = false;
               
                   
        }       
        
        if (_mouseFire2Down)
        {
            if (FireTime2 >= Fire2Rate)
            {
                if (isPlayStartFire2Sound)
                {
                    
                    isPlayStartFire2Sound = false;
                }

                _isFire2 = true;
            }
            else
            {
                FireTime2 += Time.deltaTime;
            }
        }
    }


  
    void RechargeWeapon()
    {
        if ( magazine != maxMagazine)
        {
            if (!isRecharge)
            {
               
                reloadAnimation = Instantiate(reloadPrefab, _player.transform); // Добавляет префаб к родителю // Это анимация перезарядки 
                reloadAnimation.transform.position = spawn.transform.position;
                isPlaySoundNoAmmo = true;
                RechargeSound.Play();
            }
            isRecharge = true; // Можно еще раз перезаредиться
          
        }
        
        if (isRecharge)
        {
            if (RecharheTime <= 0)
            {
                magazine = maxMagazine;
                isRecharge = false;
                Destroy(reloadAnimation);
                RechargeSound.Stop();
                isStartRecharge = false;
                RecharheTime = rechargeRate;
            }
            else
            {
                RecharheTime -= Time.deltaTime;
            }
        }
    }
    void CreateAmmoText()
    {
       text = Instantiate(ammoText, GameObject.FindWithTag("Canvas").transform);

    }

    [Range(1, 5f)] public float offsetLocalScaleAmmo = 1f;
    public Color colorAmmoText = Color.white;
    void AmmoText()
    {
        text.color = colorAmmoText;
        text.transform.position = _spawnAmmoText.transform.position;
        text.transform.rotation = _spawnAmmoText.transform.rotation;
        text.text = $"[{magazine}/{maxMagazine}]";
        text.gameObject.transform.localScale =
            new Vector3(transform.localScale.y * offsetLocalScaleAmmo, transform.localScale.y * offsetLocalScaleAmmo, 1);
    }

    void TriggerCountBullet()
    {
        if(calcFire == 2)
        {
            _countBulletTrigger?.Invoke();
            calcFire = 0;
        }
    }

}

