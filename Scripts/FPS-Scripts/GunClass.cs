using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunClass : MonoBehaviour
{
    public string gunName;
    public float dmg;
    public float firerate;
    public bool fullauto;

    public int clipSize;
    public int maxAmmo;
    public int currAmmo;
    public int backAmmo;

    void Start()
    {
        currAmmo = clipSize;
        backAmmo = clipSize * 2;
        maxAmmo = 240;
    }
}
