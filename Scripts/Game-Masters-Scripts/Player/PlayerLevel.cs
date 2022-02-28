using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public float lvl;
    public float xp;
    public float points;
    public Text pointText;
    public Text txtLevel;
    public Text txtXP;
    public Text txtInfo;
    void Start()
    {
        //xp = PlayerPrefs.GetFloat("PlayerXP");
        //points = PlayerPrefs.GetFloat("PlayerPoints");
    }

    void Update()
    {
        if (xp >= 100 * lvl)
        {
            lvl++;
            if (lvl == 1)
            {
                lvl = Mathf.Ceil(xp / 100);
            }
            else
            {
                points += 1;
                //PlayerPrefs.SetFloat("PlayerPoints", points);
                StartCoroutine("LevelUp");
            }
        }
        pointText.text = "Points: " + points;
        txtLevel.text = lvl.ToString();
        txtXP.text = (100 - (xp%100)).ToString();
    }

    IEnumerator LevelUp()
    {
        txtInfo.text = "LEVEL UP!!!";
        yield return new WaitForSeconds(0.5f);
        txtInfo.text = "";
        yield return new WaitForSeconds(0.3f);
        txtInfo.text = "LEVEL UP!!!";
        yield return new WaitForSeconds(0.5f);
        txtInfo.text = "";
        yield return new WaitForSeconds(0.5f);
        txtInfo.text = "Spell Point Earned!";
        yield return new WaitForSeconds(0.8f);
        txtInfo.text = "";
    }

    public void addXP(float n)
    {
        xp += n;
        //PlayerPrefs.SetFloat("PlayerXP", xp);
    }

    public void spendPoints(float n)
    {
        points -= n;
        //PlayerPrefs.SetFloat("PlayerPoints", points);
    }

    
}
