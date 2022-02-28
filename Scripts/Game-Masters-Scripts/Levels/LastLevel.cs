using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public static string PreviousLevel { get; private set; }
    [SerializeField] private Transform first; // spawn points
    [SerializeField] private Transform second;
    [SerializeField] private Transform third;
    GameObject player;



    void Start()
    {
        PreviousLevel = PlayerPrefs.GetString("PreviousLevel");
        player =  GameObject.FindGameObjectWithTag("Player");

        if (PreviousLevel == "Sewer")
        {
            player.GetComponent<Transform>().position = third.position;
        }
        else if (PreviousLevel == "CastleFloor2")
        {
            player.GetComponent<Transform>().position = second.position;

        }
        else if (PreviousLevel == "CaveEnd")
        {
            player.GetComponent<Transform>().position = first.position;

        }
        else if (PreviousLevel == "CastleFloor1")
        {
            player.GetComponent<Transform>().position = second.position;

        }
        else if (PreviousLevel == "Forest")
        {
            player.GetComponent<Transform>().position = second.position;

        }
        else if (PreviousLevel == "Cave")
        {
            player.GetComponent<Transform>().position = first.position;

        }
        else if (PreviousLevel == "CastleFloor2")
        {
            player.GetComponent<Transform>().position = first.position;

        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        PlayerPrefs.SetString("PreviousLevel", gameObject.scene.name);
    }
}
