using UnityEngine;
using System.Collections;
using System;

public class PlayerControls : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    public float bulletKnockBack;
    public float rof;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public CircleCollider2D cc;
    public GameObject bulletPrefab;
    private bool isJumping;
    private int lastDirection;
    private float timeUntilNextShot;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        isJumping = false;
        lastDirection = -1;
        timeUntilNextShot = 0;
    }
	
	// Update is called once per tick
	void FixedUpdate () {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0), ForceMode2D.Force);
        if (!isJumping)
        {
            rb.AddForce(new Vector2(0, Input.GetAxis("Jump") * jumpSpeed), ForceMode2D.Impulse);
        }
        if (cc.IsTouchingLayers())
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }

        if(Input.GetAxis("Horizontal") < 0)
        {
            lastDirection = 1;
            sr.flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            lastDirection = -1;
            sr.flipX = false;
        }

        if (Input.GetAxis("Fire1") == 1 && timeUntilNextShot <= 0)
        {
            rb.AddForce(new Vector2(bulletKnockBack * lastDirection, 0), ForceMode2D.Impulse);
            GameObject shot = (GameObject)Instantiate(bulletPrefab);
            shot.GetComponent<Rigidbody2D>().position = new Vector2(transform.position.x,transform.position.y) + new Vector2(0.25f * -lastDirection, 0);
            shot.GetComponent<Rigidbody2D>().rotation = lastDirection;
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(25f * -lastDirection, 0);
            Destroy(shot, 2);
            timeUntilNextShot = 60 / rof;
        }
        if (timeUntilNextShot > 0)
        {
            timeUntilNextShot--;
        }

    }
}
