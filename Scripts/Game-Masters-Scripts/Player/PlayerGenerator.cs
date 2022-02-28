using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject NewPlayer()
    {
        return Instantiate(player);
    }
}
