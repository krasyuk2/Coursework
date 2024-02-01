using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Debug = System.Diagnostics.Debug;
using Random = System.Random;

public class CardBafManager : MonoBehaviour
{

    public GameObject[] bafCards;
    public GameObject bafDeletCard; 
    
    public int NextBafCount = 10;
    private Statistics _statistics;
    private GameObject _canvas;
    public GameObject ButtonReload;
    private GameObject _buttonReload;
    public GameObject ButtonCancel;
    private GameObject _buttonCancel;
    public int currentLvl = 0;
    public int currentWinBoss = 0;
    
    [Header("OneWeapon\n")]
    public List<int> oneWeapon = new List<int>();
    public List<int> oneWeaponActiveUp = new List<int>(); 
    public List<int> oneWeaponActiveQ = new List<int>();
    public List<int> oneWeaponActiveE = new List<int>();
    [Header("TwoWeapon\n")]
    public List<int> twoWeapon = new List<int>();
    public List<int> twoWeaponActiveUp = new List<int>(); 
    public List<int> twoWeaponActiveQ = new List<int>();
    public List<int> twoWeaponActiveE = new List<int>();
    [Header("ThreeWeapon\n")]
    public List<int> threeWeapon = new List<int>();
    public List<int> threeWeaponActiveUp = new List<int>();  // Улучшение активных бафов
    public List<int> threeWeaponActiveQ = new List<int>();
    public List<int> threeWeaponActiveE = new List<int>();

    public List<int> indexWeaponActive = new List<int>();
    private List<int> _activeBafWeaponUp = new List<int>();

    public int idCardActiveQ;
    public int idCardActiveE;
 
    private List<int> _activeBafQ = new List<int>();
    private List<int> _activeBafE = new List<int>();
    
    
    public List<int> indexArray = new List<int>();// Идея - в спике храниться номер метода,
    // рандомно выбираеться индекс, получаем метод равный этому индексу, удаляем из списка чтобы не повторялось
    public List<int> _cardBaf = new List<int>();
    private List<int> _cardBafTemp = new List<int>(); // Штука чтобы карты не падали до появления стартовой карты; например Крутящийся шар вокруг игрока это стартовая карта
    // и карта добавить урон к этому шару чтобы они вместе не спавнились 
    
    
    public GameObject videoPrefab;
    public List<VideoClip> videoClips = new List<VideoClip>();

    public List<GameObject> prefabNumbersLvlBaf = new List<GameObject>();
    public List<int> LvlsBafList = new List<int>(); // Кол-во уровней в бафе

    public Color ColorActiveBaf;
    

    private ActiveBaf _activeBaf; // место для активных бафов
    
    private Ricochet _ricochet;
    private SphereShield _sphereShield;
    private SphereWeapon _sphereWeapon;
    private Sword _sword;
    private AddFireBullet _addFireBullet;
    private Dash _dash;
    private Freezing _freezing;
    private SetFireTo _setFireTo;
    private SwordLine _swordLine;
    private CircleBullet _circleBullet;
    private Pentagram _pentagram;
    private Book _book;
    private SwordBack _swordBack;
    private BackBullet _backBullet;
    private Death _death;
    private Coin _coin;
    private FourBullet _fourBullet;
    private ExoBaf _exoBaf;
    private FreezingCircle _freezingCircle;
    private SetFireCircle _setFireCircle;
    private Diana _diana;
    private Meteor _meteor;
    private BulletMultiply _bulletMultiply;
    private CharacteristicsWeapon _characteristicsWeapon;
    private AddBulletDamage _addBulletDamage;
    private CriticalDamage _criticalDamage;
    private ArrowDown _arrowDown;
    private ReturnBullet _returnBullet;
    private Experience _experience;
    private CircleLaser _circleLaser;
    private BowBulletFire2Add _bowBulletFire2Add;
    private WizardBulletFire2Add _wizardBulletFire2Add;
    private Katana _katana;
    private TwoBulletTrigger _twoBulletTrigger;
    private TwoBulletTriggerWeapon _twoBulletTriggerWeapon;
    private SaoBaf _saoBaf;
    private DivisionBulletSword _divisionBulletSword;
    private ThrowSwordWeapon _throwSwordWeapon;
    private AirStrike _airStrike;
    private SphereChieldSword _sphereShieldSword;
    private FreezingActive _freezingActive;
    private PushActive _pushActive;
    private TurretActive _turretActive;
    private SetToFireActive _setToFireActive;
    private CircleBulletActive _circleBulletActive;
    
    private void Awake()
    {
        _canvas = GameObject.FindWithTag("Canvas");
        
        _ricochet = FindObjectOfType<Ricochet>();
        _sphereShield = FindObjectOfType<SphereShield>();
        _sphereWeapon = FindObjectOfType<SphereWeapon>();
        _sword = FindObjectOfType<Sword>();
        _addFireBullet = FindObjectOfType<AddFireBullet>();
        _dash = FindObjectOfType<Dash>();
        _freezing = FindObjectOfType<Freezing>();
        _setFireTo = FindObjectOfType<SetFireTo>();
        _swordLine = FindObjectOfType<SwordLine>();
        _circleBullet = FindObjectOfType<CircleBullet>();
        _pentagram = FindObjectOfType<Pentagram>();
        _book = FindObjectOfType<Book>();
        _swordBack = FindObjectOfType<SwordBack>();
        _backBullet = FindObjectOfType<BackBullet>();
        _death = FindObjectOfType<Death>();
        _coin = FindObjectOfType<Coin>();
        _fourBullet = FindObjectOfType<FourBullet>();
        _exoBaf = FindObjectOfType<ExoBaf>();
        _setFireCircle = FindObjectOfType<SetFireCircle>();
        _freezingCircle = FindObjectOfType<FreezingCircle>();
        _diana = FindObjectOfType<Diana>();
        _meteor = FindObjectOfType<Meteor>();
        _bulletMultiply = FindObjectOfType<BulletMultiply>();
        _characteristicsWeapon = FindObjectOfType<CharacteristicsWeapon>();
        _addBulletDamage = FindObjectOfType<AddBulletDamage>();
        _criticalDamage = FindObjectOfType<CriticalDamage>();
        _arrowDown = FindObjectOfType<ArrowDown>();
        _returnBullet = FindObjectOfType<ReturnBullet>();
        _experience = FindObjectOfType<Experience>();
        _circleLaser = FindObjectOfType<CircleLaser>();
        _bowBulletFire2Add = FindObjectOfType<BowBulletFire2Add>();
        _wizardBulletFire2Add = FindObjectOfType<WizardBulletFire2Add>();
        _katana = FindObjectOfType<Katana>();
        _twoBulletTrigger = FindObjectOfType<TwoBulletTrigger>();
        _twoBulletTriggerWeapon = FindObjectOfType<TwoBulletTriggerWeapon>();
        _saoBaf = FindObjectOfType<SaoBaf>();
        _divisionBulletSword = FindObjectOfType<DivisionBulletSword>();
        _throwSwordWeapon = FindObjectOfType<ThrowSwordWeapon>();
        _airStrike = FindObjectOfType<AirStrike>();
        _sphereShieldSword = FindObjectOfType<SphereChieldSword>();
        _freezingActive = FindObjectOfType<FreezingActive>();
        _pushActive = FindObjectOfType<PushActive>();
        _turretActive = FindObjectOfType<TurretActive>();
        _setToFireActive = FindObjectOfType<SetToFireActive>();
        _circleBulletActive = FindObjectOfType<CircleBulletActive>();
        
        
        _activeBaf = FindObjectOfType<ActiveBaf>();
        

    }

    void Start()
    {
        _statistics = FindObjectOfType<Statistics>();
        _buttonReload = Instantiate(ButtonReload, _canvas.transform);
        _buttonCancel = Instantiate(ButtonCancel, _canvas.transform);
        
    }


    public void RegList(int indexList)
    {
        indexArray.Clear();
        
        switch (indexList)
        {
            case 1:
                foreach (var baf in oneWeapon)
                {
                    indexArray.Add(baf);
                }
                foreach (var baf in oneWeaponActiveE)
                {
                    _activeBafE.Add(baf);
                }
                foreach (var baf in oneWeaponActiveQ)
                {
                    _activeBafQ.Add(baf);
                }
                foreach (var baf in oneWeaponActiveUp)
                {
                    indexWeaponActive.Add(baf);
                }
                break;
            case 2:
                foreach (var baf in twoWeapon)
                {
                    indexArray.Add(baf);
                }
                foreach (var baf in twoWeaponActiveE)
                {
                    _activeBafE.Add(baf);
                }
                foreach (var baf in twoWeaponActiveQ)
                {
                    _activeBafQ.Add(baf);
                }
                foreach (var baf in twoWeaponActiveUp)
                {
                    indexWeaponActive.Add(baf);
                }
                break;
            case 3:
                foreach (var baf in threeWeapon)
                {
                    indexArray.Add(baf);
                }
                foreach (var baf in threeWeaponActiveE)
                {
                    _activeBafE.Add(baf);
                }
                foreach (var baf in threeWeaponActiveQ)
                {
                    _activeBafQ.Add(baf);
                }
                foreach (var baf in threeWeaponActiveUp)
                {
                    indexWeaponActive.Add(baf);
                }
                break;
        }

        if (indexWeaponActive.Count == 0) // Если у аружия нет активных бафов, пропускаем то когда они выпадают
        {
            currentWinBoss = 5;
        }
        
        foreach (var i in indexArray)
        {
            _cardBaf.Add(i);
           
        }
        foreach (var i in indexWeaponActive)
        {
            _activeBafWeaponUp.Add(i);
        }
        
        _statistics.BufMax = indexArray.Count;
    }

    private bool isSpawnCard = true;

    void Update()
    {
        if (_statistics.PlayerExp >= NextBafCount)
        {
            if (isSpawnCard)
            {
                currentLvl++;
                isActiveReload = true;
                //Instantiate(prefab, _canvas.transform);
                StartCoroutine(SpawnThreeCards());
                isSpawnCard = false;
            }

            _statistics.PlayerExp = 0;
            NextStepKilling(); // Дабовляем след цель для получения карты 
        }

    

    }

    public GameObject enBoss;

    private bool _isActiveBaf;
    public void StartBafActive()
    {
        currentWinBoss++;
        _isActiveBaf = true;
        StartCoroutine(SpawnThreeCards());

    }

    private bool isActiveReload = true;
    public void ReloadCard()
    {
        DeleteCard();
        if (_buttonReload != null) _buttonReload.SetActive(false);
        if (_buttonCancel != null) _buttonCancel.SetActive(false);
        Time.timeScale = 0;
        
        indexArray.Clear();
        foreach (var i in _cardBaf)
        {
            indexArray.Add(i);
        }

        isActiveReload = false;
        StartCoroutine(SpawnThreeCards());
    }

    public void CancelCard()
    {
        indexArray.Clear();
        foreach (var i in _cardBaf)
        {
            indexArray.Add(i);
        }
        indexWeaponActive.Clear();
        foreach (var i in _activeBafWeaponUp)
        {
            indexWeaponActive.Add(i);
        }
        DeleteCard();
    }

    IEnumerator WaitActiveUI()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        if (!_isActiveBaf && isActiveReload)
        {
            if (_buttonReload != null) _buttonReload.SetActive(true);
        }
    
        if (_buttonCancel != null) _buttonCancel.SetActive(true);

    }
    void NextStepKilling()
    {
        NextBafCount +=  5;
    }

    private List<GameObject> cards = new List<GameObject>();
    IEnumerator SpawnThreeCards()
    {
 
        StartCoroutine(WaitActiveUI());
       
        
        for (int i = 0; i < 3; i++)
        { 
          
            GameObject card = Instantiate(bafCards[i], _canvas.transform);
            cards.Add(card);
            yield return new WaitForSecondsRealtime(0.2f);
            if (i == 2)
            {
                Time.timeScale = 0; // после спавна всех карт останавливаем время
                foreach (var j in cards)
                {
                    j.GetComponent<Button>().enabled = true; // И после появления всех кард включаем кномку
                                                             // так как если тыкнуть в первую карту при их спавне начнеться удаления пока друге спавшяться и он не удалит все 
                }
                cards.Clear();
            }

        }
    }
    
    public void DeleteCard()
    {
        GameObject[] cardBaf = GameObject.FindGameObjectsWithTag("CardBaf");
        for (int i = 0; i < cardBaf.Length; i++)
        {
      
            {
                GameObject deleteCard = Instantiate(bafDeletCard, _canvas.transform);
                deleteCard.transform.position = cardBaf[i].transform.position;
                Destroy(cardBaf[i]);
            }
    
        }
        if (_buttonReload != null) _buttonReload.SetActive(false);
        if (_buttonCancel != null) _buttonCancel.SetActive(false);
        _isActiveBaf = false;
        isSpawnCard = true; // Говорим что можем спавнить карты 
        Time.timeScale = 1; // после удаления всех карт восстанавиливаем время
    }


    public void Randomize(CarBaf carBaf,out int numCard)
    {
        if (_isActiveBaf)
        {
            if (currentWinBoss == 1)
            {
                SetPrefab(carBaf,out numCard, _activeBafE);
            }
            else if (currentWinBoss == 2)
            {
                SetPrefab(carBaf,out numCard, _activeBafQ);
                
            }
            else if (currentWinBoss == 3)
            {
                SetPrefab(carBaf,out numCard, indexWeaponActive);  
            }
            else if (currentWinBoss == 4)
            {
                SetPrefab(carBaf, out numCard,indexWeaponActive);
            }
            else
            {
                
                SetPrefab(carBaf,out numCard, indexArray);
                _isActiveBaf = false;
            }
        }
        else
        {
            SetPrefab(carBaf,out numCard, indexArray);
        }
       
    }
    public void SetPrefab(CarBaf carBaf,out int numCard, List<int> list) // Забираем скрипт карты и ссылаемся на текст, сохранем число и возвравщаем в карту чтобы потом вызвать метод
    {
        
        int count = -1;
        Random random = new Random();
        
        int intRandom = random.Next(0, list.Count);
          
        if (list.Count > 0)
        {
            count = list[intRandom];
        }
        
        nextCard: // Если падает карта которая еще не может выпасть то удаляем ее из списка и возвращаемся сюда 
        print(count);
        switch (count) 
        {
            case 1:
                carBaf._text.text = "Ricochet + 1";
                carBaf.textContent = "When hitting an enemy, the bullet is reflected towards the nearest enemy";  
                break;
            case 2:
                carBaf._text.text = "SphereShield";
                carBaf.textContent = "A sphere appears that deals damage when it touches an enemy";
                break;
            case 3:
                carBaf._text.text = "SphereWeapon";
                carBaf.textContent = "A sphere appears that shoots at the nearest enemy";
                break;
            case 4:
                if (_cardBaf.Contains(2) )
                {
                    count = NextCard(4);
                    goto nextCard;
                }
                carBaf._text.text = "SphereShieldLvlUp";
                carBaf.textContent = "AddDamage\nAddSpeed\nAddCount";
                carBaf.currentLvlBaf = 1;
                break;
            case 5:
                carBaf._text.text = "SwordSphere";
                carBaf.textContent = "A sword appears that deals area damage every 5 seconds.";
                break;
            case 6:
                carBaf._text.text = "AddBulletFireStart";
                carBaf.textContent = "When fired, + 1 bullet will be added";
                break;
            case 7:
                if (_cardBaf.Contains(6) )
                {
                    count = NextCard(7);
                    goto nextCard;
                }
                carBaf._text.text = "+ bulletFire";
                carBaf.textContent = "When fired, + 1 bullet will be added";
                carBaf.currentLvlBaf = 1;
                break;
            case 8:
                carBaf._text.text = "Dash";
                break;
            case 9:
                carBaf._text.text = "FreezeBullet";
                carBaf.textContent = "When hitting an enemy, the bullet slows down the enemy";
                break;
            case 10: 
                carBaf._text.text = "SetFireBullet";
                carBaf.textContent = "When hit, the bullet sets the enemy on fire";
                break;
            case 11:
                carBaf._text.text = "SwordLine";
                break;
            case 12:
                carBaf._text.text = "CircleBullet";
                carBaf.textContent = "You're shooting around the area";
                break;
            case 13:
                carBaf._text.text = "Pentagram";
                carBaf.textContent = "An area will appear that deals damage";
                break;
            case 14:
                carBaf._text.text = "Book";
                carBaf.textContent = "Magic book)";
                break;
            case 15:
                carBaf._text.text = "SwordBack";
                carBaf.textContent = "A sword appears that selects a random victim";
                break;
            case 16:
                carBaf._text.text = "BulletBack";
                carBaf.textContent = "You start shooting and back";
                break;
            case 17:
                carBaf._text.text = "Death";
                break;
            case 18:
                carBaf._text.text = "Coin";
                carBaf.textContent = "";
                break;
            case 19:
                carBaf._text.text = "FourBullet";
                carBaf.textContent = "When hit, the bullets are divided into 4 parts";
                break;
            case 20:
                carBaf._text.text = "ExoBaf";
                carBaf.textContent = "";
                break;
            case 21:
                if (_cardBaf.Contains(14))
                {
                    count = NextCard(21);
                    goto nextCard;
                }
                carBaf._text.text = "BookLvlUp";
                carBaf.textContent = "Now there's more shooting";
                carBaf.currentLvlBaf = 1;
                break;
            case 22:
                if (_cardBaf.Contains(21)  )
                {
                    count = NextCard(22);
                    goto nextCard;
                }
                carBaf._text.text = "BookLvlUp";
                carBaf.textContent = "EVEN MORE SHOOTING";
                carBaf.currentLvlBaf = 2;
                break;
            case 23:
                carBaf._text.text = "SetFireCircle";
                carBaf.textContent = "An area will appear that sets enemies on fire";
                break;
            case 24:
                carBaf._text.text = "FreezingCircle";
                carBaf.textContent = "An area will appear that slows down enemies";
                break;
            case 25:
                carBaf._text.text = "Diana";
                carBaf.textContent = "4 spheres that deal damage";
                break;
            case 26:
                carBaf._text.text = "Meteor";
                carBaf.textContent = "A ball that shoots at a random point";
                break;
            case 27:
                if (_cardBaf.Contains(26))
                {
                    count = NextCard(27);
                    goto nextCard;
                }
                carBaf._text.text = "MeteorLvlUp";
                carBaf.textContent = "More shooting";
                carBaf.currentLvlBaf = 1;
                break;
            case 28:
                if (_cardBaf.Contains(27))
                {
                    count = NextCard(28);
                    goto nextCard;
                }
                carBaf._text.text = "MeteorLvlUp";
                carBaf.textContent = "MORE SHOOTING, BULLETS COUPLING LIKE HAIL";
                carBaf.currentLvlBaf = 2;
                break;
            case 29:
                carBaf._text.text = "BulletMultiply";
                carBaf.textContent = "A ball that falls apart into many pieces";
                break;
            case 30:
                carBaf._text.text = "WeaponLvlUpDM";
                carBaf.textContent = "AddDamage\nAddMagazine\nAddSize";
                break;
            case 31:
                carBaf._text.text = "WeaponLvlUpCD";
                carBaf.textContent = "LowCd\nLowCallDawnFire2\nLowReload";
                break;
            case 32:
                if (_cardBaf.Contains(29))
                {
                    count = NextCard(32);
                    goto nextCard;
                }
                carBaf._text.text = "BulletMultiplyLvlUp";
                carBaf.textContent = "MORE DAMAGE MORE PARTS";
                break;
            case 33:
                if (_cardBaf.Contains(13))
                {
                    count = NextCard(33);
                    goto nextCard;
                }
                carBaf._text.text = "PentagramLvlUp";
                carBaf.textContent = "Area larger more damage less CD";
                break;
            case 34:
                carBaf._text.text = "TwoBulletTrigger";
                carBaf.textContent =
                    "If you hit an enemy 2 times, an additional shot will be fired (does not reset when changing enemies)";
                break;
            case 35:
                carBaf._text.text = "TwoBulletTriggerWeapon";
                carBaf.textContent =
                    "If you hit an enemy 2 times, additional damage will be dealt (does not reset when changing enemies)";
                break;
            case 36:
                carBaf._text.text = "AddBulletDamage";
                break;
            case 37:
                carBaf._text.text = "CriticalDamage";
                break;
            case 38:
                carBaf._text.text = "ArrowDown";
                break;
            case 39:
                if (_cardBaf.Contains(38))
                {
                    count = NextCard(39);
                    goto nextCard;
                }
                carBaf._text.text = "ArrowDownLowKdSpawn";
                break;
            case 40:
                carBaf._text.text = "ReturnBullet";
                break;
            case 41:
                if (_cardBaf.Contains(40))
                {
                    count = NextCard(41);
                    goto nextCard;
                }
                carBaf._text.text = "ReturnBulletAddChance";
                break;
            case 42:
                carBaf._text.text = "AddRadiusExp";
                carBaf.textContent = "Increases the area for collecting experience";
                break;
            case 43:
                carBaf._text.text = "CircleLaser";
                break;
            case 44:
                if (_cardBaf.Contains(43))
                {
                    count = NextCard(44);
                    goto nextCard;
                }
                carBaf._text.text = "CircleLaserAddCountAttack\nCircleLaserLowKd";
                break;
            case 46:
                carBaf._text.text = "BowBulletFire2Add";
                break;
            case 47:
                carBaf._text.text = "WizardBulletFire2Add";
                carBaf.textContent = "Increases the number of projectiles";
                break;
            case 48:
                if (_cardBaf.Contains(10))
                {
                    count = NextCard(48);
                    goto nextCard;
                }
                carBaf._text.text = "SetFireToAddDamage";
                break;
            case 49:
                if (_cardBaf.Contains(10))
                {
                    count = NextCard(49);
                    goto nextCard;
                }
                carBaf._text.text = "SetFireToAddCountFire";
                break;
            case 50:
                if (_cardBaf.Contains(9))
                {
                    count = NextCard(50);
                    goto nextCard;
                }
                carBaf._text.text = "AddFreezingTime";
                break;
            case 51:
                if (_cardBaf.Contains(4))
                {
                    count = NextCard(51);
                    goto nextCard;
                }
                carBaf._text.text = "AddShieldSphere";
                carBaf.textContent = "Increases the number of spheres by 1";
                carBaf.currentLvlBaf = 2;
                break;
            case 52:
                if (_cardBaf.Contains(51))
                {
                    count = NextCard(52);
                    goto nextCard;
                }
                carBaf._text.text = "AddShieldSphere";
                break;
            case 54:
                if (_cardBaf.Contains(14))
                {
                    count = NextCard(54);
                    goto nextCard;
                }
                carBaf._text.text = "BookAddDamage\nBookLowKd";
                break;
            case 55:
                if (_cardBaf.Contains(52))
                {
                    count = NextCard(55);
                    goto nextCard;
                }
                carBaf._text.text = "AddShieldSphere";
                break;
            case 56:
                if (_cardBaf.Contains(2))
                {
                    count = NextCard(56);
                    goto nextCard;
                }
                carBaf._text.text = "LvlUpShieldSphere";
                break;
            case 57:
                if (_cardBaf.Contains(58))
                {
                    count = NextCard(57);
                    goto nextCard;
                }
                carBaf._text.text = "PiercingSphereWeapon";
                carBaf.currentLvlBaf = 2;
                break;
            case 58:
                if (_cardBaf.Contains(3))
                {
                    count = NextCard(58);
                    goto nextCard;
                }
                carBaf._text.text = "SphereWeaponLvlUpDM_CD";
                carBaf.textContent = "Increased damage and decreased CD for shots";
                carBaf.currentLvlBaf = 1;
                break;
            case 60:
                if (_cardBaf.Contains(3))
                {
                    count = NextCard(60);
                    goto nextCard;
                }
                carBaf._text.text = "SetFireToSphereWeapon";
                carBaf.textContent = "Sets enemies on fire when hit";
                break;
            case 61:
                if (_cardBaf.Contains(3))
                {
                    count = NextCard(61);
                    goto nextCard;
                }
                carBaf._text.text = "FreezingSphereWeapon";
                carBaf.textContent = "Slows enemies when hit";
              
                break;
            case 62:
                if (_cardBaf.Contains(58))
                {
                    count = NextCard(62);
                    goto nextCard;
                }
                carBaf._text.text = "FourBulletSphereWeapon";
                carBaf.textContent = "When hit, the bullet splits into 4 parts";
                carBaf.currentLvlBaf = 3;
                break;
            case 63:
                if (_cardBaf.Contains(5))
                {
                    count = NextCard(63);
                    goto nextCard;
                }
                carBaf._text.text = "AddDamageSword";
                carBaf.textContent = "Damage Increase";
                break;
            case 64:
                if ( _cardBaf.Contains(23)) // Для посоха
                {
                    count = NextCard(64);
                    goto nextCard;
                }
                carBaf._text.text = "SetFireToAddDamage";
                carBaf._text.text = "SetFireToAddCountFire";
                
                break;
            case 68:
                if ( _cardBaf.Contains(24) )  // Для посоха
                {
                    count = NextCard(68);
                    goto nextCard;
                }
                carBaf._text.text = "AddFreezingTime";
                break;
            case 70:
                if (_cardBaf.Contains(11))  
                {
                    count = NextCard(70);
                    goto nextCard;
                }
                carBaf._text.text = "AddDamageSwordLine";
                break;
            case 71:
                if (_cardBaf.Contains(11))  
                {
                    count = NextCard(71);
                    goto nextCard;
                }
                carBaf._text.text = "AddCountSwordLine";
                break;
            case 72:
                if (_cardBaf.Contains(15))  
                {
                    count = NextCard(72);
                    goto nextCard;
                }
                carBaf._text.text = "AddDamageSwordBack";
                break;
            
            // Sword;
            case 73:
                carBaf._text.text = "Katana";
                carBaf.textContent = "A katana appears, dealing damage to the area every 7 seconds.";
                break;
            case 74:
                if (_cardBaf.Contains(73))  
                {
                    count = NextCard(74);
                    goto nextCard;
                }
                carBaf._text.text = "KatanaLvl2";
                carBaf.textContent = "AddDamage\nAddDistance\nLowKd";
                carBaf.currentLvlBaf = 1;
                break;
            case 75:
                if (_cardBaf.Contains(74))  
                {
                    count = NextCard(75);
                    goto nextCard;
                }
                carBaf._text.text = "KatanaLvl3";
                carBaf.textContent = "Katana emits streams of wind";
                carBaf.currentLvlBaf = 2;
                break;
            case 76:
                carBaf._text.text = "DivisionBulletSword";
                carBaf.textContent = "If you press the blow in time, you will reflect the bullet";
                break;
            case 77:
                carBaf._text.text = "ThrowSwordWeapon";
                carBaf.textContent =
                    "Now when you press right punch you throw the sword. When you press it again, you will be teleported to him. To return the sword press left strike";
                break;
            case 78:
                if (_cardBaf.Contains(77))  
                {
                    count = NextCard(78);
                    goto nextCard;
                }
                carBaf._text.text = "ThrowSwordWeaponLvl2";
                carBaf.textContent = "Flying sword now deals damage";
                carBaf.currentLvlBaf = 1;
                break;
            case 79:
                if (_cardBaf.Contains(78))  
                {
                    count = NextCard(79);
                    goto nextCard;
                }
                carBaf._text.text = "ThrowSwordWeaponLvl3";
                carBaf.textContent = "When teleporting to the sword, you deal area damage";
                carBaf.currentLvlBaf = 2;
                break;
            case 80:
                carBaf._text.text = "SaoBaf";
                carBaf.textContent =
                    "When struck in different directions without repetition, a wave appears that causes damage";
                break;
            case 81:
                carBaf._text.text = "AirStrike";
                carBaf.textContent = "Every 3 seconds you deal damage in a wave";
                break;
            case 82:
                if (_cardBaf.Contains(81))  
                {
                    count = NextCard(82);
                    goto nextCard;
                }
                carBaf._text.text = "AirStrikeLvl2";
                carBaf.textContent = "Now every blow causes a wave";
                carBaf.currentLvlBaf = 1;
                break;
            
            
            case 83: // active sword
                carBaf._text.text = "SphereShieldSword";
                carBaf.textContent = "Press \"E\" to perform an area attack: the more enemies you hit, the stronger the shield will be";
                carBaf.colorActive = ColorActiveBaf;
                break;
            case 84: // active sword
                carBaf._text.text = "FreezingActive";
                carBaf.textContent = "Press \"Q\" to freeze enemies in the targeted area";
                carBaf.colorActive = ColorActiveBaf;
                break;
            case 85: // active sword
                carBaf._text.text = "PushActive";
                carBaf.textContent = "Press \"E\" to push enemies away";
                carBaf.colorActive = ColorActiveBaf;
                break;
            case 86: // active sword
                carBaf._text.text = "TurretActi ve";
                carBaf.textContent = "Press \"Q\" to install a turret";
                carBaf.colorActive = ColorActiveBaf;
                break;
            case 87: // active sword
                carBaf._text.text = "SetFireToActive";
                carBaf.textContent = "Press \"E\" to damage and set enemies on fire";
                carBaf.colorActive = ColorActiveBaf;
                break;
            case 88: // active sword
                carBaf._text.text = "CircleBulletActive";
                carBaf.textContent = "Press \"Q\" to fire bullets around you";
                carBaf.colorActive = ColorActiveBaf;
                break;
            
            // Active sword SphereShieldSword 
            case 89: // One - Only Damage
                if (idCardActiveE == 83 && currentWinBoss == 3)  // концепт
                {
                    carBaf._text.text = "SphereShieldSwordDamage";
                    carBaf.textContent = "Ability only deals damage";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(89,indexWeaponActive);
                    goto nextCard;
                }

                break;
            case 90: // Two - Only Shield
                if (idCardActiveE == 83 && currentWinBoss == 3)  // концепт
                {
                    carBaf._text.text = "SphereShieldSwordShield";
                    carBaf.textContent = "The ability only gives a shield";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(90,indexWeaponActive);
                    goto nextCard;
                }

                break;
            case 91: // Tree - Lvl Up
                if (idCardActiveE == 83 && currentWinBoss == 3)  // концепт
                {
                    carBaf._text.text = "SphereShieldSwordLvlUp";
                    carBaf.textContent = "Same thing only better)";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(91,indexWeaponActive);
                    goto nextCard;
                }
                break;
            
            // Active sword Push
            case 92: // One - Damage
                if (idCardActiveE == 85 && currentWinBoss == 3)  
                {
                    carBaf._text.text = "PushActiveDamage";
                    carBaf.textContent = "Deals damage";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(92,indexWeaponActive);
                    goto nextCard;
                }

                break;
            case 93: // Two - Grenade
                if (idCardActiveE == 85 && currentWinBoss == 3)  // концепт
                {
                    carBaf._text.text = "PushActiveGrenade";
                    carBaf.textContent = "He also pushes away and throws a grenade at the cursor, attracting enemies to him.";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(93,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 94: // Tree - The same
                if (idCardActiveE == 85 && currentWinBoss == 3)  // концепт
                {
                    carBaf._text.text = "PushActiveLvlUp";
                    carBaf.textContent = "Same thing only better)";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(94,indexWeaponActive);
                    goto nextCard;
                }
                break;
            
            // Active sword SetFireTo
            case 95: // One - Area
                if (idCardActiveE == 87 && currentWinBoss == 3)  
                {
                    carBaf._text.text = "SetFireToSquare";
                    carBaf.textContent = "Increase in area";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(95,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 96: // Two - Distance
                if (idCardActiveE == 87 && currentWinBoss == 3) 
                {
                    carBaf._text.text = "SetFireToDistance";
                    carBaf.textContent = "Increasing distance";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(96,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 97: // Tree - The same
                if (idCardActiveE == 87 && currentWinBoss == 3)  
                {
                    carBaf._text.text = "SetFireToLvlUp";
                    carBaf.textContent = "Same thing only better)";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(97,indexWeaponActive);
                    goto nextCard;
                }
                break;
            
            // Active sword Freezing
            case 98: // One - Area
                if (idCardActiveQ == 84 && currentWinBoss == 4)  
                {
                    carBaf._text.text = "FreezingActiveSquare";
                    carBaf.textContent = "Increase in area";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(98,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 99: // Two - Distance 
                if (idCardActiveQ == 84 && currentWinBoss == 4) 
                {
                    carBaf._text.text = "FreezingActiveDistanceDamage";
                    carBaf.textContent = "Increasing distance + Damage";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(99,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 100: // Tree - Heal 
                if (idCardActiveQ == 84 && currentWinBoss == 4)  
                {
                    carBaf._text.text = "FreezingActiveHeal";
                    carBaf.textContent = "Sphere that freezes and heals";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(100,indexWeaponActive);
                    goto nextCard;
                }
                break;
            
            // Active sword Turret
            case 101: // One - Area 
                if (idCardActiveQ == 86 && currentWinBoss == 4)  
                {
                    carBaf._text.text = "TurretActiveMore";
                    carBaf.textContent = "Shoots at everyone within range";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(101,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 102: // Two - Piercing
                if (idCardActiveQ == 86 && currentWinBoss == 4) 
                {
                    carBaf._text.text = "TurretActivePiercing";
                    carBaf.textContent = "Pierces enemies";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(102,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 103: // Tree - lowKd
                if (idCardActiveQ == 86 && currentWinBoss == 4)  
                {
                    carBaf._text.text = "TurretActiveKdDamage";
                    carBaf.textContent = "CD reduced by 3 times and damage increased";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(103,indexWeaponActive);
                    goto nextCard;
                }
                break;
                
            // Active sword CircleBullet
            case 104: // One - Piercing
                if (idCardActiveQ == 88 && currentWinBoss == 4)  
                {
                    carBaf._text.text = "CircleBulletActivePiercing";
                    carBaf.textContent = "Pierces enemies";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(104,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 105: // Two - AreaDamage 
                if (idCardActiveQ == 88 && currentWinBoss == 4) 
                {
                    carBaf._text.text = "CircleBulletActiveArea";
                    carBaf.textContent = "When a projectile hits an enemy, damage is dealt in an area";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(105,indexWeaponActive);
                    goto nextCard;
                }
                break;
            case 106: // Tree - CountDamage
                if (idCardActiveQ == 88 && currentWinBoss == 4)  
                {
                    carBaf._text.text = "CircleBulletActiveCountDamage";
                    carBaf.textContent = "Increased number of projectiles and damage";
                    carBaf.colorActive = ColorActiveBaf;
                }
                else
                {
                    count = NextCard(106,indexWeaponActive);
                    goto nextCard;
                }
                break;
            
            
        }
        numCard = count;

        list.Remove(numCard);


    }



    int NextCard(int deleteCard) 
    {
        Random random = new Random();
        _cardBafTemp = indexArray;
        _cardBafTemp.Remove(deleteCard);
        int indexRandom = random.Next(0, _cardBafTemp.Count);
        if (_cardBafTemp.Count != 0)
        {
            return _cardBafTemp[indexRandom];
        }
        return 0;
    }

    int NextCard(int deleteCard, List<int> list)
    {
        Random random = new Random();
        _cardBafTemp = list;
        _cardBafTemp.Remove(deleteCard);
        int indexRandom = random.Next(0, _cardBafTemp.Count);
        if (_cardBafTemp.Count != 0)
        {
            return _cardBafTemp[indexRandom];
        }
        return 0; 
    }

    public void EnterMethod(int idCard ) // Метод для вызова из карты при нажатии 
    {
        switch (idCard)
        {
            case 1:
                _ricochet.StartRicochet();
                break;
            case 2:
                _sphereShield.StartBafSphereShield();
                break;
            case 3:
                _sphereWeapon.StartSphereWeapon();
                break;
            case 4:
                _sphereShield.AddDamage();
                _sphereShield.AddSpeedAngle();
                _sphereShield.StartBafSphereShield();
                break;
            case 5:
                _sword.StartSword();
                break;
            case 6:
                _addFireBullet.AddBullet();
                break;
            case 7:
                _addFireBullet.AddBullet();
                break;
            case 8:
                _dash.StartBaf();
                break;
            case 9:
                _freezing.StartBaf();
                break;
            case 10: 
                _setFireTo.StartBaf();
                break;
            case 11:
                _swordLine.StartBaf();
                break;
            case 12:
                _circleBullet.StartBaf();
                break;
            case 13:
                _pentagram.StartBaf();
                break;
            case 14:
                _book.StartBaf();
                break;
            case 15:
                _swordBack.StartBaf();
                break;
            case 16:
                _backBullet.StartBaf();
                break;
            case 17:
                _death.StartBaf();
                break;
            case 18:
                _coin.StartBaf();
                break;
            case 19:
                _fourBullet.StartBaf();
                break;
            case 20:
                _exoBaf.StartBaf();
                break;
            case 21:
                _book.UpLvl();
                break;
            case 22:
                _book.UpLvl();
                break;
            case 23:
                _setFireCircle.StartBaf();
                break;
            case 24:
                _freezingCircle.StartBaf();
                break;
            case 25:
                _diana.StartBaf();
                break;
            case 26:
                _meteor.StartBaf();
                break;
            case 27:
               _meteor.UpLvl();
                break;
            case 28:
                _meteor.UpLvl();
                break;
            case 29:
                _bulletMultiply.StartBaf();
                break;
            case 30:
                _characteristicsWeapon.AddDamage();
                _characteristicsWeapon.AddMagazine();
                _characteristicsWeapon.AddSize();
                break;
            case 31:
                _characteristicsWeapon.LowCallDawnFire2();
                _characteristicsWeapon.LowReload();
                _characteristicsWeapon.LowKd();
                break;
            case 32:
                _bulletMultiply.AddDamageAndAddCount();
                break;
            case 33:
                _pentagram.AddSizeAndAddDamageAndLowKd();
                break;
            case 34:
                _twoBulletTrigger.StartBaf();
                break;
            case 35:
                _twoBulletTriggerWeapon.StartBaf();
                break;
            case 36:
                _addBulletDamage.BafStart();
                break;
            case 37:
                _criticalDamage.StartBaf();
                break;
            case 38:
                _arrowDown.StartBaf();
                break;
            case 39:
                _arrowDown.LowRate();
                break;
            case 40:
                _returnBullet.StartBaf();
                break;
            case 41:
                _returnBullet.AddChance();
                break;
            case 42:
                _experience.AddDistance();
                break;
            case 43:
                _circleLaser.StartBaf();
                break;
            case 44:
                _circleLaser.AddCountAttack();
                _circleLaser.LowKd();
                break;
            case 46:
                _bowBulletFire2Add.AddCountBullet();
                break;
            case 47:
                _wizardBulletFire2Add.AddCountBullet();
                break;
            case 48:
                _setFireTo.AddDamageSetFire();
                break;
            case 49:
                _setFireTo.AddCountSetFire();
                break;
            case 50:
                _freezing.AddTimeFreezing();
                break;
            case 51:
                _sphereShield.StartBafSphereShield();
                break;
            case 52:
                _sphereShield.StartBafSphereShield();
                break;
            case 54:
                _book.AddDamage();
                _book.LowKd();
                break;
            case 55:
                _sphereShield.StartBafSphereShield();
                break;
            case 56:
                _sphereShield.LvlUp();
                break;
            case 57:
                _sphereWeapon.StartPiercing();
                break;
            case 58:
                _sphereWeapon.AddDamage();
                _sphereWeapon.LowFireKd();
                break;
            case 60:
                _setFireTo.StartBaf();
                break;
            case 61:
                _freezing.StartBaf();
                break;
            case 62:
                _fourBullet.StartBaf();
                break;
            case 63:
                _swordLine.AddDamage();
                break;
            case 64:
                _setFireTo.AddDamageSetFire();
                _setFireTo.AddDamageSetFire();
                break;
            case 66:
                _setFireTo.AddCountSetFire();
                break;
            case 67:
                _setFireTo.AddCountSetFire();
                break;
            case 68:
                _freezing.AddTimeFreezing();
                break;
            case 69:
                _freezing.AddTimeFreezing();
                break;
            case 70:
                _swordLine.AddDamage();
                break;
            case 71:
                _swordLine.AddCountSwordLine();
                break;
            case 72:
                _swordBack.AddDamage();
                break;
            case 73:
                _katana.StartBaf();
                break;
            case 74:
                _katana.AddDamage();
                _katana.AddDistance();
                _katana.LowKd();
                break;
            case 75:
                _katana.LvlUp();
                break;
            case 76:
                _divisionBulletSword.StartBar();
                break;
            case 77:
                _throwSwordWeapon.StartBaf();
                break;
            case 78:
                _throwSwordWeapon.LvlUp();
                break;
            case 79:
                _throwSwordWeapon.LvlUp();
                break;
            case 80:
                _saoBaf.StartBaf();
                break;
            case 81:
                _airStrike.StartBaf();
                break;
            case 82:
                _airStrike.LvlUp();
                break;
            case 83:
                _activeBaf.AddActiveE(_sphereShieldSword.Baf);
                idCardActiveE = 83;
                break;
            case 84:
                _activeBaf.AddActiveQ(_freezingActive.Baf);
                idCardActiveQ = 84;
                break;
            case 85:
                _activeBaf.AddActiveE(_pushActive.Baf);
                idCardActiveE = 85;
                break;
            case 86:
                _activeBaf.AddActiveQ(_turretActive.Baf);
                idCardActiveQ = 86;
                break;
            case 87:
                _activeBaf.AddActiveE(_setToFireActive.Baf);
                idCardActiveE = 87;
                break;
            case 88:
                _activeBaf.AddActiveQ(_circleBulletActive.Baf);
                idCardActiveQ = 88;
                break;
            case 89:
                _sphereShieldSword.OneBaf();
                break;
            case 90:
                _sphereShieldSword.TwoBaf();
                break;
            case 91:
                _sphereShieldSword.TreeBaf();
                break;
            case 92:
                _pushActive.OneBaf();
                break;
            case 93:
                _pushActive.TwoBaf();
                break;
            case 94:
                _pushActive.TreeBaf();
                break;
            case 95:
                _setToFireActive.OneBaf();
                break;
            case 96:
                _setToFireActive.TwoBaf();
                break;
            case 97:
                _setToFireActive.TreeBaf();
                break;
            case 98:
                _freezingActive.OneBaf();
                break;
            case 99:
                _freezingActive.TwoBaf();
                break;
            case 100:
                _freezingActive.TreeBaf();
                break;
            case 101:
                _turretActive.OneBaf();
                break;
            case 102:
                _turretActive.TwoBaf();
                break;
            case 103:
                _turretActive.TreeBaf();
                break;
            case 104:
                _circleBulletActive.OneBaf();
                break;
            case 105:
                _circleBulletActive.TwoBaf();
                break;
            case 106:
                _circleBulletActive.TreeBaf();
                break;
        }
        
        if (_isActiveBaf)
        {
            if(currentWinBoss > 2)
            {
                _activeBafWeaponUp.Remove(idCard);
                indexWeaponActive.Clear(); 
                foreach (var i in _activeBafWeaponUp)
                {
                    indexWeaponActive.Add(i);
                }
            }
            
        }
        else
        {
            _cardBaf.Remove(idCard);
            indexArray.Clear();
            foreach (var i in _cardBaf)
            {
                indexArray.Add(i);
            }
        }
        
        _statistics.BufCurrent += 1;
 
     
        DeleteCard(); 
    }
    //  раньше были и дебафы
   
}
