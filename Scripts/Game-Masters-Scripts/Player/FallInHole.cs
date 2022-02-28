using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FallInHole : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] GameObject fallAudio;
    private Vector3 originalScale;
    [SerializeField] private Animator transition;
    //public static Vector3 loadScenePos;
    
   
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //print("collided with fall zone");
        if (other.gameObject.tag == "fallZone")
        {
            StartCoroutine("fallDown");
        }
        if (other.gameObject.tag == "correctHole")
        {
            StartCoroutine("fallDownC");
        }
        if (other.gameObject.tag == "doorWayForward")
        {
            StartCoroutine("LoadNext");
        }
        if (other.gameObject.tag == "doorWayBackwards")
        {
            StartCoroutine("LoadLast");
        }
    }

    IEnumerator fallDown()
    {

        GetComponent<Damage>().invincible = true;
        fallAudio.GetComponent<AudioSource>().Play();
        float xScale = 0;
        float yScale = 0;
        float minScale = Vector3.one.y * .2f;
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        GetComponent<Movement>().moveSpeed = 0;
        GetComponent<Movement>().speed = 0;
        while (yScale > minScale)
        {
            yScale -= .3f * Time.deltaTime;
            xScale -= .3f * Time.deltaTime;
            Vector3 scale = transform.localScale;
            scale.y = yScale;
            scale.x = xScale;
            gameObject.transform.localScale = scale;
            yield return null;
        }
       
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Cave2");

        }

        yield return new WaitForSeconds(1);

        GetComponent<Movement>().moveSpeed = 0;
        GetComponent<Movement>().speed = 5;
        transform.localScale = originalScale;   
        transform.position = spawnLocation.position;
        GetComponent<Damage>().invincible = false;
        GetComponent<Damage>().health -= 20;

        
        yield break;

    }

    IEnumerator fallDownC()
    {
        fallAudio.GetComponent<AudioSource>().Play();
        float xScale = 0;
        float yScale = 0;
        float minScale = Vector3.one.y * .2f;
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        GetComponent<Movement>().moveSpeed = 0;
        GetComponent<Movement>().speed = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        while (yScale > minScale)
        {
            yScale -= .3f * Time.deltaTime;
            xScale -= .3f * Time.deltaTime;
            Vector3 scale = transform.localScale;
            scale.y = yScale;
            scale.x = xScale;
            gameObject.transform.localScale = scale;
            yield return null;
        }
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<SaveLoadPrefs>().SaveAll();
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
             
        

        yield break;

    }
    IEnumerator LoadLast()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    IEnumerator LoadNext()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        GetComponent<SaveLoadPrefs>().SaveAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
