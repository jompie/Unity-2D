using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shape : MonoBehaviour
{

    public static float Speed = 1f;

    public float LastMoveDown = 0;


	// Use this for initialization
	void Start () {
	    if (!IsInGrid())
	    {
	        SoundManager.Instance.PlayOneShot(SoundManager.Instance.gameOver);
	        Invoke("OpenGameOverScene", 1f);
	    }
	    Invoke("IncreaseSpeed", 2.0f);
	}

    void OpenGameOverScene()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    void IncreaseSpeed()
    {
        Shape.Speed -= .0001f;
    }

	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("a"))
	    {
	        transform.position += new Vector3(-1, 0, 0);
	        Debug.Log(transform.position);
	        if (!IsInGrid())
	        {
                transform.position += new Vector3(1, 0, 0);
                
            }
            else
            {
                UpdateGameBoard();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
            }
        }
        if (Input.GetKeyDown("d"))
        {
            transform.position += new Vector3(1, 0, 0);
            Debug.Log(transform.position);
            if (!IsInGrid())
            {
                transform.position += new Vector3(-1, 0, 0);
                
            }
            else
            {
                UpdateGameBoard();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
            }
        }
	    if (Input.GetKeyDown("s") || Time.time - LastMoveDown >= Shape.Speed)
        {
            transform.position += new Vector3(0, -1, 0);
            Debug.Log(transform.position);
            if (!IsInGrid())
            {
                
                transform.position += new Vector3(0, 1, 0);

                bool rowDeleted = GameGrid.DeleteAllFullRows();
                if (rowDeleted)
                {
                    GameGrid.DeleteAllFullRows();
                    //TODO change score
                    IncreaseScore();
                }

                enabled = false;
                
                FindObjectOfType<ShapeSpawner>().SpawnShape();
                

            }
            else
            {
                UpdateGameBoard();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);

            }
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
           
            LastMoveDown = Time.time;
        }
        if (Input.GetKeyDown("w"))
        {
            transform.Rotate(0, 0, 90);
            Debug.Log(transform.position);
            if (!IsInGrid())
            {
                transform.Rotate(0, 0, -90);
                
            }
            else
            {
                UpdateGameBoard();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
            }
        }
    }

    public bool IsInGrid()
    {
        int childCount = 0;
        foreach (Transform childBlock in transform )
        {
            Vector2 vector = RoundVector(childBlock.position);
            childCount++;

            if (!IsInBorder(vector))
            {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeStop);

                return false;
                
            }

            if (GameGrid.GameGridTransforms[(int) vector.x, (int) vector.y] != null &&
                GameGrid.GameGridTransforms[(int) vector.x, (int) vector.y].parent != transform)
            {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeStop);
                return false;
                
            }
        }
        return true;
    }

    public Vector2 RoundVector(Vector2 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }

    public static bool IsInBorder(Vector2 pos)
    {
        return ((int) pos.x >= 0 &&
                (int) pos.x <= 9 &&
                (int) pos.y >= 0);
    }

    public void UpdateGameBoard()
    {
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (GameGrid.GameGridTransforms[x, y] != null &&
                    GameGrid.GameGridTransforms[x, y].parent == transform)
                {
                    GameGrid.GameGridTransforms[x, y] = null;
                }
            }
        }
        foreach (Transform childBlock in transform)
        {
            Vector2 vector = RoundVector(childBlock.position);
         
            GameGrid.GameGridTransforms[(int) vector.x, (int) vector.y] = childBlock;
            Debug.Log("Cube At: " + (int) vector.x + " " + (int) vector.y);
           
        }
    }

    public void IncreaseScore()
    {
        var TextUIComponent = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(TextUIComponent.text);
        score += 1000;
        TextUIComponent.text = score.ToString();

    }
}
