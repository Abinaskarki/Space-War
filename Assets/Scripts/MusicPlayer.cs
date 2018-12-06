using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    void Awake()
    {
        int CountMusicPlayer = FindObjectsOfType(GetType()).Length;

        if(CountMusicPlayer > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
