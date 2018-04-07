using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

    public const int NORMAL_ROW = 20;
    public const int MAX_ROW = 23;
    public const int MAX_COL = 10;
    public bool isDataUpdate = false;

    private Transform[,] map = new Transform[MAX_COL, MAX_ROW];

    private int score = 0;
    private int bestScore = 0;
    private int numbersGame = 0;

    public int Score { get { return score; } }
    public int BestScore { get { return bestScore; } }
    public int NumbersGame { get { return numbersGame; } }

    void Awake()
    {
        Screen.SetResolution(576, 1024, false);
        LoadData();
    }

    public bool IsGameOver()
    {
        for(int i = NORMAL_ROW; i < MAX_ROW; i++)
        {
            for(int j = 0; j < MAX_COL; j++)
            {
                if (map[j, i] != null)
                {
                    numbersGame++;
                    SaveData();
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsValidMapPosition(Transform t)
    {
        foreach(Transform child in t)
        {
            if(child.tag == "Block")
            {
                Vector2 pos = child.position.Round();
                if (IsInsideMap(pos) == false)  // 是否在地图内
                    return false;
                if(map[(int)pos.x, (int)pos.y] != null){ // 此位置是否已经有方块
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COL && pos.y >= 0;
    }

    public bool PutShapeInMap(Transform t)
    {
        foreach(Transform child in t)
        {
            if(child.tag == "Block")
            {
                Vector2 pos = child.position.Round();
                map[(int)pos.x, (int)pos.y] = child;
            }
        }
        return CheckMap();
    }

    // 检查是否需要消除一行
    private bool CheckMap()
    {
        int cnt = 0;
        for(int i = 0; i < MAX_ROW; i++)
        {
            bool isFull = CheckIsRowFull(i);
            if (isFull)
            {
                cnt++;
                DeleteRow(i);
                MoveDownAboveRows(i + 1);
                i--;
            }
        }
        if (cnt > 0)
        {
            score += (cnt * 100);
            if (score > bestScore)
                bestScore = score;
            isDataUpdate = true;
            return true;
        }
        return false;
    }

    private bool CheckIsRowFull(int row)
    {
        for(int i = 0; i < MAX_COL; i++)
        {
            if (map[i, row] == null)
                return false;
        }
        return true;
    }

    private void DeleteRow(int row)
    {
        for(int i = 0; i < MAX_COL; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }

    // row这行以及其上方的所有方块下移
    private void MoveDownAboveRows(int row)
    {
        for(int i = row; i < MAX_ROW; i++)
        {
            MoveDownRow(i);
        }
    }

    // 把row这一行所有方块下移
    private void MoveDownRow(int row)
    {
        for(int i = 0; i < MAX_COL; i++)
        {
            if (map[i, row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    private void LoadData()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        numbersGame = PlayerPrefs.GetInt("NumbersGame", 0);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.SetInt("NumbersGame", numbersGame);
    }

    public void Retry()
    {
        for(int i = 0; i < MAX_COL; i++)
        {
            for (int j = 0; j < MAX_ROW; j++)
            {
                if (map[i, j] != null)
                {
                    Destroy(map[i, j].gameObject);
                    map[i, j] = null;
                }
            }
        }
        score = 0;
    }
}
