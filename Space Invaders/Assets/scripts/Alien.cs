using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D rigidbody;
    public Sprite StartingImage;
    public Sprite AlternativeImage;
    private SpriteRenderer spriteRenderer;
    public float SecsBeforeSpriteChange = 0.5f;
    public GameObject alienBullet;
    public float minFireRateTime = 1.0f;
    public float maxFireRateTime = 2.0f;
    public float baseFireWaitTime = 1.0f;
    public Sprite explodedShipImage;


	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(1,0) * speed;
	    spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeAlienSprite());
        baseFireWaitTime = baseFireWaitTime + Random.Range(minFireRateTime, maxFireRateTime);

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "LeftWall")
        {
            Turn(1);
            MoveDown();
        }
        if (col.gameObject.name == "RightWall")
        {
            Turn(-1);
            MoveDown();
        }
        //if (col.gameObject.tag == "Bullet")
        //{
        //    SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);
        //    Destroy(gameObject);
        //}
    }

    void FixedUpdate()
    {
        if (Time.time > baseFireWaitTime)
        {
            baseFireWaitTime = baseFireWaitTime + Random.Range(minFireRateTime, maxFireRateTime);
            Instantiate(alienBullet, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImage;
            Destroy(gameObject);
            Destroy(col.gameObject, 0.5f);
        }
    }

    //turn in oposite direction after wall collision
    void Turn(int direction)
    {
        Vector2 newVelocity = rigidbody.velocity;
        newVelocity.x = speed * direction;
        rigidbody.velocity = newVelocity;
    }

    //move down after wall collision
    void MoveDown()
    {
        Vector2 position = transform.position;
        position.y -= 1;
        transform.position = position;
    }

    public IEnumerator ChangeAlienSprite()
    {
        while (true)
        {
            if (spriteRenderer.sprite == StartingImage)
            {
                spriteRenderer.sprite = AlternativeImage;
                //SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz1);
            }
            else
            {
                spriteRenderer.sprite = StartingImage;
                //SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz2);
            }

            yield return new WaitForSeconds(SecsBeforeSpriteChange);
            
        }
    }
}
