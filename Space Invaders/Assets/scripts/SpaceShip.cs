using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    public float speed = 30;

    public GameObject bullet;

    void FixedUpdate()
    {
        float horzMove = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(horzMove, 0) * speed;
    }

	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Jump"))
	    {
	        // fire bullet and play fire sound
	        Instantiate(bullet, transform.position, Quaternion.identity);

	        SoundManager.Instance.PlayOneShot(SoundManager.Instance.bulletFire);
	    }
	}
}
