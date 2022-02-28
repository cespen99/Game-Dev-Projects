using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public float sightRadius;
    public GameObject player;
    public float dist2Player;
    private enum enemyType {GOBLIN, MUSHROOM, BOSS};
    [SerializeField]private enemyType? monsterType;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        if (monsterType == null)
            monsterType = enemyType.GOBLIN;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.activeSelf)
        {
            dist2Player = Vector2.Distance(player.transform.position, transform.position);
            if (dist2Player/4 < sightRadius && GetComponent<GoblinBehaviour>().state == 0)
            {
                GetComponent<GoblinBehaviour>().StartCoroutine("Agro");
            }
        }
    }
}
