using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CarBaf : MonoBehaviour
{
    private AudioSource _audioSource;
    public TMP_Text _text;
    public string textContent;
    public int idCard;
    private Color _textColor;
    private float temp = 0;
    private float tempTime = 0;
    private CardBafManager _cardBafManager;
    [SerializeField]
    public Color colorActive = Color.white;

    private GameObject _groupAddPrefabLvlBaf;
    public void Awake()
    {
        _cardBafManager = FindObjectOfType<CardBafManager>();
        _audioSource = transform.GetChild(0).GetComponent<AudioSource>();
        _text = transform.GetChild(1).GetComponent<TMP_Text>();
        _groupAddPrefabLvlBaf = transform.GetChild(3).gameObject;
    }

    void Start()
    { 
        print(gameObject.name);
        _audioSource.Play();
        _textColor = _text.color;
        _text.color = new Color(0, 0, 0, 0);
        temp = 0;
        tempTime = 0;
        _cardBafManager.Randomize(this,out idCard);
        gameObject.GetComponent<Image>().color = colorActive;
        GetLvlBaf();

   
    }

    private void Update()
    {
        if(_text.color.a < 1f)  TextCreate();
    }

    public void Click()
    {
        _cardBafManager.EnterMethod(idCard);
        _cardBafManager.videoPrefab.SetActive(false);
    }
    private void TextCreate()
    {
       
        if (Time.unscaledTime >= tempTime + 0.1f) //Так как TimeScale = 0 мир на паузе и каротины не работают
        {
            tempTime = Time.unscaledTime;
            temp++;
        }
        _text.color = new Color(_textColor.r, _textColor.g, _textColor.b, temp/10f);
    }

    public int currentLvlBaf = 0;
    
    void GetLvlBaf()
    {
        if (idCard != -1)
        {
            if (_cardBafManager.LvlsBafList[idCard] != 0)
            {
                for (int i = 0; i <= _cardBafManager.LvlsBafList[idCard]; i++)
                {
                    var prefabLvl = Instantiate(_cardBafManager.prefabNumbersLvlBaf[i],
                        _groupAddPrefabLvlBaf.transform);
                    if (currentLvlBaf == i)
                    {
                        prefabLvl.GetComponent<Image>().color = Color.red;
                    }

                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if(_text.text != "Done");
        {
            if (idCard != -1)
            {
                if (idCard <= _cardBafManager.videoClips.Count - 1)
                {
                    _cardBafManager.videoPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = textContent;

                    _cardBafManager.videoPrefab.transform.position =
                        gameObject.transform.GetChild(2).transform.position;
                    if (_cardBafManager.videoClips[idCard] != null)
                    {
                        _cardBafManager.videoPrefab.GetComponent<VideoPlayer>().clip =
                            _cardBafManager.videoClips[idCard];
                        _cardBafManager.videoPrefab.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnMouseExit()
    {
        _cardBafManager.videoPrefab.SetActive(false);
    }
}
