using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSwordWeaponSpriteRender : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        StartCoroutine(Delete());
    }


    IEnumerator Delete()
    {
        for (float i = _lineRenderer.startWidth; i > 0; i-=0.005f)
        {
            _lineRenderer.startWidth = i;
            yield return new WaitForSeconds(0.05f);
        }
        
        Destroy(gameObject);
    }
}
