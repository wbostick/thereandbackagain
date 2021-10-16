﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DNCST - header
public class DestroySelf : MonoBehaviour
{
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void SelfDestructAfter(float timer)
    {
        Destroy(gameObject, timer);
    }
}
