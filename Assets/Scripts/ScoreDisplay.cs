using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    //public Text text;

	// Use this for initialization
	void Start () {
        Text score = GetComponent<Text>();
        score.text = " " + ScoreKeeper._score.ToString();
        ScoreKeeper.Reset();
	}
}
