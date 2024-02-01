using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text Text;
    private float time = 0;
    public float allTime;
 
    private int M;
    private int S;
    
    

    private string M_s;
    private string S_s;
  

    
    void Update()
    {
        S = (int)time;
        time += Time.deltaTime;
        allTime += Time.deltaTime;
        if ((int)time == 60)
        {
            time = 0;
            M++;
        }

        if (S < 10) S_s = "0" + S;
        else S_s = S+"";
        if (M < 10) M_s = "0" + M;
        else M_s = M+"";

            Text.text = $"{M_s}:{S_s}";

    }
}
