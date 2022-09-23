using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootLogic : MonoBehaviour
{
    public void Shoot(Func<Vector3> set)
    {
        set.Invoke();
    }
}
