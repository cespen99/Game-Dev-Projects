using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public List<SpriteRenderer> runes;
    public float lerpSpeed;
    public bool touched = false;
    private Color curColor;
    private Color targetColor;
    public static int numAltars = 0;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !touched)
        {
            if(numAltars != 5)
            gameObject.GetComponent<AudioSource>().Play();
            touched = true;
            numAltars++;
            targetColor = new Color(1, 1, 1, 1);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //targetColor = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

        foreach (var r in runes)
        {
            r.color = curColor;
        }
    }

    
}
