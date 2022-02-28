using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Movement : MonoBehaviour
{
    private Vector2 moveRaw;        //WASD input
    public float moveSpeed, speed;
    public Vector2 moveVector;     //input * speed
    public Animator animator;
   
    private Vector2 pointer;        //mouse position in game
    public float targetAngle;       //angle from player to pointer
   [SerializeField] public bool facingRight = true;
    [SerializeField] public float oldpos;
    public float newpos;

    public AudioSource moveSound;
   
    private void Start()
    {
        speed = moveSpeed;
    }
    void Update()
    {
        if (GetComponent<PlayerAction>().paused == false)
        {
            pointer = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            targetAngle = Mathf.Atan2(transform.position.x - pointer.x, pointer.y - transform.position.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

            if (moveSpeed != 0)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            }
            //transform.Translate(moveSpeed * Vector3.up * Time.deltaTime);

            if (targetAngle > 0)
            {
                facingRight = false;
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            if (targetAngle < 0)
            {
                facingRight = true;
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }

            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

            newpos = transform.position.x;
        }
    }

    private void FixedUpdate()
    {
        moveVector = moveRaw * speed;
        GetComponent<Rigidbody2D>().AddForce(moveVector);
        oldpos = transform.position.x;
    }
    

    public void OnMove(InputValue value)
    {
        
        moveRaw = value.Get<Vector2>();

        if (moveRaw.x == 0 && moveRaw.y == 0)
        {
            moveSound.Pause();
            animator.SetInteger("AnimState", 0);
        }
        else if (facingRight && (oldpos > newpos) || !facingRight && (oldpos < newpos))
        {
            moveSound.Play();
            animator.SetInteger("AnimState", 2);
        }
        else
        {
            moveSound.Play();
            animator.SetInteger("AnimState", 1);
        }
    }
}
