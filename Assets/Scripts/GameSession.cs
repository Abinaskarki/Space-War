using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    int score = 100;
    [SerializeField] int playerHealth = 300;

    void Awake()
    {
        int CountGameSession = FindObjectsOfType(GetType()).Length;
        if(CountGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int ScoreValue)
    {
        score += ScoreValue;

    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
