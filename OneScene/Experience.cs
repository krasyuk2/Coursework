using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public int ExpValue = 1;
    public float Speed = 1;
    public float Distance;

    public void AddDistance()
    {
        Distance += 3;
    }
}
