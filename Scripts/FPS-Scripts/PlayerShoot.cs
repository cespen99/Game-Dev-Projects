using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public Transform fpsCamera;
    public float timer;
    public bool canShoot;
    public int points;
    public Text pointIndicator;
    public GameObject muzzleflash;
    public AudioSource gunSound;
    
    // Start is called before the first frame update
    void Start()
    {
        gunSound = GetComponentInChildren<AudioSource>();
        canShoot = true;
        points += 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        pointIndicator.text = points.ToString("D8");
        if (timer < 0)
            canShoot = true;
        Debug.DrawRay(fpsCamera.position, fpsCamera.forward);
        if (GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().currAmmo <= 0)
        {
            canShoot = false;
        }
        if (GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().fullauto == false)
        {
            if (canShoot && Input.GetKeyDown(KeyCode.Mouse0))
            {
                GetComponent<WeaponSelect>().curGun.GetComponent<AudioSource>().Play();
                StartCoroutine("Flash");
                Shoot();
            }
        }
        else
        {
            if (canShoot && Input.GetKeyDown(KeyCode.Mouse0))
            {
                GetComponent<WeaponSelect>().curGun.GetComponent<AudioSource>().Play();
            }
            if (canShoot && Input.GetKey(KeyCode.Mouse0))
            {
                muzzleflash.SetActive(true);
                Shoot();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                muzzleflash.SetActive(false);
                GetComponent<WeaponSelect>().curGun.GetComponent<AudioSource>().Pause();
            }
        }

        
        if (GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().currAmmo < GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().clipSize)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("Reload");
            }
        }
    }

    public void Shoot()
    {
        GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().currAmmo -= 1;
        canShoot = false;
        timer = GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().firerate;
        if (Physics.Raycast(fpsCamera.position, fpsCamera.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.tag == "Enemy")
            {
                GameObject target = hitInfo.collider.gameObject;
                hitInfo.collider.GetComponent<EnemyDamage>().Hit(GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().dmg);
                points += 50;
            }
        }
    }

    public IEnumerator Flash()
    {
        muzzleflash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        muzzleflash.SetActive(false);
    }
    public IEnumerator Reload()
    {
        canShoot = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        int newClip = (GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().clipSize - GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().currAmmo);
        if (newClip <= GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().backAmmo)
        {
            GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().backAmmo -= newClip;
            GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().currAmmo += newClip;
        }
        else
        {
            GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().backAmmo -= GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().backAmmo;
            GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().currAmmo += GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().backAmmo;
        }
        canShoot = true;
    }

}
