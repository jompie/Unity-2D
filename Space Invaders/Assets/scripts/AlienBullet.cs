using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public float speed = 30;
    public Sprite ExplodedShipSprite;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody2D>();
	    rigidbody.velocity = Vector2.down * speed;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (col.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
            col.GetComponent<SpriteRenderer>().sprite = ExplodedShipSprite;
            Destroy(gameObject);
            DestroyObject(col.gameObject, 0.5f);
        }
        if (col.tag == "Shield")
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }

    void OnBecomeInvisable()
    {
        Destroy(gameObject);
    }
}
