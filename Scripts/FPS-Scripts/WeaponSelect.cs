using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour
{
    public GameObject[] guns;
    public GameObject curGun;
    public int itemIndex;
    public Text ammoIndicator, weaponIndicator;
    // Start is called before the first frame update
    void Start()
    {
        itemIndex = 0;
        curGun = guns[0];
        //foreach(GameObject gun in guns)
        //    curGun.transform.position -= new Vector3(0, 0, 2);
        curGun.SetActive(true);
        ammoIndicator.text = curGun.GetComponent<GunClass>().currAmmo + " / " + curGun.GetComponent<GunClass>().backAmmo;
        weaponIndicator.text = curGun.GetComponent<GunClass>().gunName;
    }

    // Update is called once per frame
    void Update()
    {
        ammoIndicator.text = curGun.GetComponent<GunClass>().currAmmo + " / " + curGun.GetComponent<GunClass>().backAmmo;
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            Equip(++itemIndex);
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            Equip(--itemIndex);

    }

    public void Equip(int i)
    {
        if (i < 0)
            itemIndex = 2;
        if (i > 2)
            itemIndex = 0;
        curGun.SetActive(false);
        curGun = guns[itemIndex];
        curGun.SetActive(true);
        
        weaponIndicator.text = curGun.GetComponent<GunClass>().gunName;
    }
}
