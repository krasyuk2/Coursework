using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public string itemId;
    
    public GameObject prefabCardText;
    private GameObject _canvas;
    private Animator _animator;
    public int Prise;
    public string nameId;
    public float[] floatStep;
    public int[] stepPrice;
    public int indexStep;
    private Kills _kills;
 
    public bool isInt;
    [TextArea]
    public string textCardStr;
    private void Awake()
    {
        _canvas = GameObject.FindWithTag("Canvas");
        _animator = GetComponent<Animator>();
        _kills = FindObjectOfType<Kills>();
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        if(PlayerPrefs.HasKey(itemId)) indexStep = PlayerPrefs.GetInt(itemId);
        _animator.SetInteger(Animator.StringToHash("Step"),indexStep);
     
    }

    private GameObject card;
    public void OnClick()
    {
        GameObject textCard = GameObject.Find("CardText(Clone)");
        if(textCard != null) Destroy(textCard);
        card = Instantiate(prefabCardText, _canvas.transform);
        if (indexStep == 5)
        {
            card.GetComponent<CardTextManager>().RegItemId(this, "MAX", isInt, textCardStr);
        }
        else
        {
            card.GetComponent<CardTextManager>().RegItemId(this, Convert.ToString(stepPrice[indexStep]), isInt, textCardStr);
        }

    }
    
    public void ClickTextCard()
    {
        if(indexStep < 5) {
            if (_kills.KillCount >= Convert.ToDouble(stepPrice[indexStep]))
            {

                print(_kills.KillCount + " + " + stepPrice[indexStep]);
                _kills.KillCount -= (float)Convert.ToDouble(stepPrice[indexStep]);
                PlayerPrefs.SetFloat("Kills", _kills.KillCount);


                PlayerPrefs.SetFloat(nameId, floatStep[indexStep]); // Сохраниние значения улучшения 
                print($"{nameId} + {floatStep[indexStep]}");
                indexStep++;
                if (indexStep == 5)
                {
                    card.GetComponent<CardTextManager>().SetTextPrice("MAX");
                }
                else
                {
                    card.GetComponent<CardTextManager>().SetTextPrice(Convert.ToString(stepPrice[indexStep]));
                }

                _animator.SetInteger(Animator.StringToHash("Step"), indexStep);
                PlayerPrefs.SetInt(itemId, indexStep); // Сохранение lvl покупки
            }
        }



    }

  

}
