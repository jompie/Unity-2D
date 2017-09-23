using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{

    public Ball ball;
    public float speed = 60;
    public float lerpTweak = 2f;

    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody2D>();

	}

    void FixedUpdate()
    {
        if (ball.transform.position.y > transform.position.y)
        {
            Vector2 dir = new Vector2(0, 1).normalized;

            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, 
                dir * speed, 
                lerpTweak * Time.deltaTime);
        }
        else if (ball.transform.position.y < transform.position.y)
        {
            Vector2 dir = new Vector2(0, -1).normalized;

            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, 
                dir * speed, 
                lerpTweak * Time.deltaTime);
        }
        else
        {
            Vector2 dir = new Vector2(0, 0).normalized;
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, 
                dir * speed, 
                lerpTweak * Time.deltaTime);
        }

    }
}
