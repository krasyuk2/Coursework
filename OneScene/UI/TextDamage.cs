using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextDamage : MonoBehaviour
{
    private TMP_Text _text;
    private Color _startColor;
    public bool offsetPosRandom = true;
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        _startColor = _text.color;
        StartCoroutine(Invisible());
        if (offsetPosRandom)
        {
            transform.position = new Vector2(transform.position.x + Random.Range(0, 0.5f),
                transform.position.y + Random.Range(0, 0.5f));
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        }
    }

    private void Update()
    {
        if(_text.color.a <= 0) Destroy(gameObject);
    }

    IEnumerator Invisible()
    {
        for (float i = 1; i > -0.1; i -= 0.1f)
        {
            _text.color = new Color(_startColor.r, _startColor.g, _startColor.b, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
