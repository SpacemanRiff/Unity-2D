using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    public float speed;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal")*speed,0), ForceMode2D.Force);
	}
}
