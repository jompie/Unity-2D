using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{

    public float speed = 30;

    private Rigidbody2D rigidBody;

    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
	    rigidBody = GetComponent<Rigidbody2D>();
	    rigidBody.velocity = Vector2.right * speed;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        // left or right paddle collision
        if ((col.gameObject.name == "paddleLeft") ||
            (col.gameObject.name == "paddleRight"))
        {
            HandlePaddleHit(col);
        }


        // wall bottom or top collision
        if ((col.gameObject.name == "wallBottom") ||
            (col.gameObject.name == "wallTop"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.walBeep);
        }

        // goal left or right collision
        if ((col.gameObject.name == "goalLeft") ||
            (col.gameObject.name == "goalRight"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.goalBeep);

            // increase score
            if (col.gameObject.name == "goalLeft")
            {
                IncreaseTextUIScore("scoreLeft");
            }
            else if (col.gameObject.name == "goalRight")
            {
                IncreaseTextUIScore("scoreRight");
            }

            transform.position = new Vector2(0, 0);
        }
    }

    void HandlePaddleHit(Collision2D col)
    {
        float y = BallHitPaddleWhere(transform.position,
            col.transform.position,
            col.collider.bounds.size.y);
        Vector2 dir = new Vector2();
        if (col.gameObject.name == "paddleLeft")
        {
            dir = new Vector2(1,y).normalized;
        }
        if (col.gameObject.name == "paddleRight")
        {
            dir = new Vector2(-1, y).normalized;
        }
        rigidBody.velocity = dir * speed;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBeep);
    }

    float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }

    void IncreaseTextUIScore(string TextUIName)
    {
        var textUIComp = GameObject.Find(TextUIName).GetComponent<Text>();
        int score = int.Parse(textUIComp.text);
        score++;
        textUIComp.text = score.ToString();
    }
}
