using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    public static Transform[,] GameGridTransforms = new Transform[10,20];

    public static bool DeleteAllFullRows()
    {
        bool rowsDeleted = false;
        for (int row = 0; row < 20; row++)
        {
            if (IsRowFull(row))
            {
                DeleteRow(row);
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rowDelete);
                rowsDeleted = true;
            }
        }
        if (rowsDeleted == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsRowFull(int row)
    {
        for (int col = 0; col < 10; col++)
        {
            if (GameGridTransforms[col, row] == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void DeleteRow(int row)
    {
        for (int col = 0; col < 10; col++)
        {
            Destroy(GameGridTransforms[col, row].gameObject);
            GameGridTransforms[col, row] = null;

        }
        row++;
        for (int j = row; j < 20; j++)
        {
            for (int col = 0; col < 10; col++)
            {
                if(GameGridTransforms[col, j] != null)
                {
                    GameGridTransforms[col, j - 1] = GameGridTransforms[col, j];
                    GameGridTransforms[col, j] = null;
                    GameGridTransforms[col, j - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

}
