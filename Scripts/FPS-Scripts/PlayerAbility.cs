using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    public Transform fpsCamera;
    public Text pointerIndicator;

    public int pointer;

    public int shields;
    public Text shieldIndicator;
    public int[] cost;

    public GameObject nukeSpeaker;
    public GameObject target;

    public bool freeNuke;

    private void Start()
    {
        pointer = 0;
        shields = 1;
        cost = new int[4];
        cost[0] = 250;
        cost[1] = 1000;
        cost[2] = 500;
        cost[3] = 5000;
        freeNuke = true;
    }
    void Update()
    {
        shieldIndicator.text = "Shields: " + shields;
        if (Physics.Raycast(fpsCamera.position, fpsCamera.forward, out RaycastHit hitInfo, 3))
        {
            if (hitInfo.collider.gameObject.tag == "Ammo")
            {
                pointerIndicator.text = "[E] Buy Ammo (250)";
                pointer = 1;
            }
            else if (hitInfo.collider.gameObject.tag == "Health")
            {
                pointerIndicator.text = "[E] Buy Health (1000)";
                pointer = 2;
            }
            else if (hitInfo.collider.gameObject.tag == "Sheild")
            {
                pointerIndicator.text = "[E] Buy Sheild (500)";
                pointer = 3;
            }
            else if (hitInfo.collider.gameObject.tag == "Nuke")
            {
                pointerIndicator.text = "[E] Buy Nuke (5000)";
                if (freeNuke)
                    pointerIndicator.text = "[E] Free Nuke (0)";
                pointer = 4;
            }
            else if (hitInfo.collider.gameObject.tag == "APowerUp")
            {
                pointerIndicator.text = "[E] Pick Up PowerUp";
                pointer = 5;
            }
            else if (hitInfo.collider.gameObject.tag == "APowerUp")
            {
                pointerIndicator.text = "[E] Pick Up PowerUp";
                pointer = 6;
            }
            else
            {
                pointer = 0;
                pointerIndicator.text = "";
            }
            if (pointer == 5)
                target = hitInfo.collider.gameObject;
        }
        else
        {
            pointer = 0;
            pointerIndicator.text = "";
        }

        if (pointer != 0 && Input.GetKeyDown(KeyCode.E))
        {
            if (pointer > 0 && pointer < 4 && GetComponent<PlayerShoot>().points >= cost[pointer - 1])
            {
                if (!freeNuke || pointer < 3)
                    GetComponent<PlayerShoot>().points -= cost[pointer - 1];
            }
            if (pointer == 4 && freeNuke)
            {
                freeNuke = false;
            }
            else if (pointer == 5)
            {
                Debug.Log(target.name);
                pointer = target.GetComponent<PowerUp>().power;
                Destroy(target);
            }

            switch (pointer)
            {
                case 1:
                    GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().backAmmo += GetComponent<WeaponSelect>().curGun.GetComponent<GunClass>().clipSize;
                    break;
                case 2:
                    GetComponent<PlayerDamage>().health += 50;
                    break;
                case 3:
                    shields++;
                    break;
                case 4:
                    StartCoroutine("NUKE");
                    break;
            }
        }
    }

    IEnumerator NUKE()
    {
        nukeSpeaker.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5);
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(g);
    }

    public void BreakShield()
    {
        shields--;
        if (shields < 0)
            shields = 0;
    }
    public bool HasShield()
    {
        if (shields > 0)
            return true;
        return false;
    }

}