using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour {
    TextMesh txt;
    static public int score;
	// Use this for initialization
	void Start () {
        txt = GetComponent<TextMesh>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = "Score: " + score;
	}

    static void IncreaseScore()
    {
        score++;
    }
}
