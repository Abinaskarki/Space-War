using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    TMP_Text text;
    GameSession gameSession;

	// Use this for initialization
	void Start () {
        text = GetComponent<TMP_Text>();
        gameSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = gameSession.GetScore().ToString();
	}


}
