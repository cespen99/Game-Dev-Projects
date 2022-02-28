using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    [SerializeField] private Material green, blue;
    public GameObject tower;
    private TowerGenerator tg;
    private TowerUpgrade tu;

    private void Start()
    {
        tower = null;
        tg = GameObject.Find("GameManager").GetComponent<TowerGenerator>();
        tu = GameObject.Find("GameManager").GetComponent<TowerUpgrade>();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] spots = GameObject.FindGameObjectsWithTag("TowerSpot");
            foreach (GameObject s in spots)
            {
                s.GetComponent<MeshRenderer>().material = green;
            }
            GetComponent<MeshRenderer>().material = blue;

            if (tower == null)
            {
                tu.UnSelect();
                tg.SelectTower(gameObject);
            }
            else
            {
                tg.UnSelect();
                tu.UpgradeSelect(tower);
            }
        }
    }

    public void PlaceTower(GameObject t)
    {
        tower = Instantiate(t, transform.position, Quaternion.Euler(270, 0, 0));
    }

    public bool HasTower()
    {
        return tower != null;
    }
}
