using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponTwo : MonoBehaviour
{
    private Animator weaponAnimator;
    public GameObject prefabBullet;
    public int countFireBullet = 1;
    public GameObject spawn; 
    public GameObject reloadPrefab;
    private ReloadWeaponAnimation _reloadWeaponAnimation;
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

    public float scatter = 0;
    [Range(0f, 2f)] public float spawnDistance = 1f;
    private CameraGame _cameraGame;
    private bool isRecharge = false;
    private bool isPlaySoundNoAmmo = true;
    private bool isStartRecharge;

    private GameObject _cursor;

    private void OnDisable()
    {
        if (text != null)
        {
            text.gameObject.SetActive(false);
        }
   
    }

    private void OnEnable()
    {
        text.gameObject.SetActive(true);
        FireTime = 0.1f;
        isStartRecharge = false;
        isRecharge = false;
        RecharheTime = rechargeRate;

    }

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

        _cursor = GameObject.FindWithTag("Cursor");
        if (reloadPrefab == null || _reloadWeaponAnimation == null)
        {

            reloadPrefab = _player.transform.GetChild(4).gameObject;
            _reloadWeaponAnimation = reloadPrefab.GetComponent<ReloadWeaponAnimation>();
       

        }
        CreateAmmoText();

    }
    private void Start()
    {
       
  
        magazine = maxMagazine;
        RecharheTime = rechargeRate;
    }
    
    public void Update()
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
        
        
        if (FireTime < 0) FireTime = 0;

        if(isStartRecharge)RechargeWeapon();
        
        if(Input.GetKeyDown(KeyCode.R))
        { 
            if(magazine != maxMagazine)isStartRecharge = true;
        }

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
                    for(int i = 0; i <countFireBullet;i++ )   SpawnBulletFire();

                    FireSound.Play();
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
    protected virtual void SpawnBulletFire()
    {
        GameObject bullet = SpawnFire();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<BulletLvlTwo>().SetDamageWeapon(Damage);
        rb.velocity = DirFireAndSpeed();
        rb.velocity = Quaternion.AngleAxis(UnityEngine.Random.Range(-scatter,scatter), Vector3.forward) * rb.velocity; // разбросс 
    }
    
    GameObject SpawnFire()
    {
        
        return Instantiate(prefabBullet, _spawnBullet.transform.position,
            _spawnBullet.transform.rotation);
    }
    Vector2 DirFireAndSpeed()
    {
        // (_cameraGame.ray.direction * bulletForce);
        return  (_cursor.transform.position - _spawnBullet.transform.position ).normalized  * bulletForce;
    }
    
    
    void RechargeWeapon()
    {
        if ( magazine != maxMagazine)
        {
            if (!isRecharge)
            {
               
                //reloadAnimation = Instantiate(reloadPrefab, _player.transform); // Добавляет префаб к родителю // Это анимация перезарядки 
                //reloadAnimation.transform.position = spawn.transform.position;
                reloadPrefab.SetActive(true);
                _reloadWeaponAnimation.StartAnimationReload(rechargeRate);
                
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
                reloadPrefab.SetActive(false);
                //Destroy(reloadAnimation);
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
    



}
