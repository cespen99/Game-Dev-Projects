using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pace : MonoBehaviour
{
    public bool isPacing = false;
    private bool didBreak = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("pacearound");
    }
    private void FixedUpdate()
    {
        //print(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
    }
    // Update is called once per frame
    void Update()
    {
        if (didBreak)
        {
            didBreak = false;
            StartCoroutine("pacearound");
        }
        if (gameObject.GetComponent<GoblinBehaviour>().state == 0 || gameObject.GetComponent<Rigidbody2D>().velocity.magnitude == 0)
        {
            isPacing = gameObject.GetComponent<Animator>().GetBool("moving");
            if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude != Vector2.zero.magnitude)
            {
                
                gameObject.GetComponent<Animator>().SetBool("moving", true);

            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("moving", false);

            }
        }

    }

    IEnumerator pacearound()
    {
        while (true && gameObject.GetComponent<GoblinBehaviour>().state == 0)
        {
            if(gameObject.GetComponent<GoblinBehaviour>().state != 0)
            {
               // gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Animator>().SetBool("moving", true);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                didBreak = true;
                break;
                
            }
            int timer = Random.Range(1, 5);
            yield return new WaitForSeconds(timer);
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            
            Vector2 targetVelocity = new Vector2(Vector3.right.magnitude, 0);
            gameObject.GetComponent<Rigidbody2D>().velocity = targetVelocity * 1;
            timer = Random.Range(1, 5);
            if (gameObject.GetComponent<GoblinBehaviour>().state != 0)
            {
                // gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Animator>().SetBool("moving", true);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                didBreak = true;

                break;
            }
            yield return new WaitForSeconds(timer);
            gameObject.GetComponent<Animator>().SetBool("moving", false);
            timer = Random.Range(1, 5);
            if (gameObject.GetComponent<GoblinBehaviour>().state != 0)
            {
                // gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Animator>().SetBool("moving", true);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                didBreak = true;

                break;
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            yield return new WaitForSeconds(timer);
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (gameObject.GetComponent<GoblinBehaviour>().state != 0)
            {
                // gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Animator>().SetBool("moving", true);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                didBreak = true;

                break;
            }
            Vector2 targetVelocity1 = new Vector2(-Vector3.right.magnitude, 0);
            gameObject.GetComponent<Rigidbody2D>().velocity = targetVelocity1 * 1;
            timer = Random.Range(1, 5);
            yield return new WaitForSeconds(timer);
            gameObject.GetComponent<Animator>().SetBool("moving", false);
            if (gameObject.GetComponent<GoblinBehaviour>().state != 0)
            {
                // gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Animator>().SetBool("moving", true);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                didBreak = true;

                break;
            }
            timer = Random.Range(1, 5);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            yield return new WaitForSeconds(timer);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //yield break;
        }
        didBreak = true;
    }
}
