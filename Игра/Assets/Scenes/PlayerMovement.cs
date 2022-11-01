using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public GameManager gm;

    public float strafeSpeed = 500f;
    public float runSpeed = 500f;
    public float jumpForse = 10f;

    protected bool strafeLeft = false;
    protected bool strafeRight = false;
    protected bool doJump = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            gm.EndGame();

            Debug.Log("Конец игры!");
        }
    }

    void Update()
    {
        if(Input.GetKey("d")){
            strafeLeft = true;
        } else {
            strafeLeft = false;
        }

        if(Input.GetKey("a")){
            strafeRight = true;
        } else {
            strafeRight = false;
        
        }

        if(Input.GetKeyDown("space")){
            doJump = true;
        }

        if(transform.position.y < -5f)
        {
            gm.EndGame();

            Debug.Log("Конец игры!");
        }
    }

    void FixedUpdate(){
        //rb.AddForce(0, 0, runSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + Vector3.forward * runSpeed * Time.deltaTime);

        if (strafeLeft)
        {
            rb.AddForce(strafeSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (strafeRight)
        {
            rb.AddForce(-strafeSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (doJump)
        {
            rb.AddForce(Vector3.up * jumpForse, ForceMode.Impulse);

            transform.DORewind();
            transform.DOShakeScale(.5f, .5f, -50);

            doJump = false;
        }
    }

}
