using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject towerPanel, gm;
    private GameObject targetLocation;
    void Awake()
    {
        towerPanel = GameObject.Find("TowerPanel"); 
        gm = GameObject.Find("GameManager");
    }
    void Start()
    {
        towerPanel.SetActive(false);
    }
    public void UnSelect()
    {
        towerPanel.SetActive(false);
    }
    public void SelectTower(GameObject target)
    {
        targetLocation = target;
        towerPanel.SetActive(true);
    }
    public void AddTower(GameObject towerSelect)
    {
        if (targetLocation.GetComponent<TowerSpot>().HasTower() == false && gm.GetComponent<GameInfo>().CanAfford(towerSelect.GetComponent<TowerInfo>().cost))
        {
            gm.GetComponent<GameInfo>().TakeMoney(towerSelect.GetComponent<TowerInfo>().cost);
            targetLocation.GetComponent<TowerSpot>().PlaceTower(towerSelect);
        }
        
        towerPanel.SetActive(false);
    }
}
