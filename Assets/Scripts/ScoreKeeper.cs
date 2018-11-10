using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public Text Score;
    public static int _score;

    public 

    void Start()
    {
        Reset();
        Score.text = _score.ToString();
    }

    public void UpdateScore(int points)
    {
        _score += points;
        Score.text = _score.ToString();
    }

    public static void Reset()
    {
        _score = 0;   
    }
}
