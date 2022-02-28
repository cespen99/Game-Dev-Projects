using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TOWER_TYPE { GUN, MISSILE, LASER, TOWER_COUNT };
public class TowerInfo : MonoBehaviour
{
    public TOWER_TYPE towerType = TOWER_TYPE.LASER;
    public float firerate = 0.5f;
    public float damage = 10f;
    public float range = 5f;
    public float cost = 50f;
    public int level = 0;
}
