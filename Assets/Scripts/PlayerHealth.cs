using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealth : MonoBehaviour {

    TMP_Text healthText;
    Player player;

    void Start()
    {
        healthText = GetComponent<TMP_Text>();
        player = FindObjectOfType<Player>();
    }
    
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}

