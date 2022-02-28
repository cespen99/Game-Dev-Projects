using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel, gm;
    [SerializeField] private GameObject tower;
    [SerializeField] private Text upgradeTxt;

    public void Awake()
    {
        upgradePanel = GameObject.Find("UpgradePanel");
        gm = GameObject.Find("GameManager");
    }

    void Start()
    {
        upgradePanel.SetActive(false);
    }
    public void UnSelect()
    {
        upgradePanel.SetActive(false);
    }

    public void UpgradeSelect(GameObject t)
    {
        upgradePanel.SetActive(true);
        tower = t;
        if (tower.GetComponent<TowerInfo>().level < 3)
            upgradeTxt.text = "Cost: " + tower.GetComponent<TowerInfo>().cost * 2;
        else
            upgradeTxt.text = "Max Level";
    }

    public void Upgrade()
    {
        if (tower != null && tower.GetComponent<TowerInfo>().level < 3 && gm.GetComponent<GameInfo>().CanAfford(tower.GetComponent<TowerInfo>().cost * 2))
        {
            gm.GetComponent<GameInfo>().TakeMoney(tower.GetComponent<TowerInfo>().cost * 2);
            tower.GetComponent<TowerInfo>().level++;
            tower.transform.localScale *= 1.25f;
            switch (tower.GetComponent<TowerInfo>().towerType)
            {
                case TOWER_TYPE.GUN:
                    tower.GetComponent<TowerInfo>().firerate /= 1.5f;
                    break;
                case TOWER_TYPE.MISSILE:
                    tower.GetComponent<TowerInfo>().range *= 1.5f;
                    break;
                case TOWER_TYPE.LASER:
                    tower.GetComponent<TowerInfo>().damage *= 1.5f;
                    break;
            }
        }
        upgradePanel.SetActive(false);
        tower = null;
    }
}
