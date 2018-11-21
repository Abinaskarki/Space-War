using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    WaveConfig waveConfig;
 
    List<Transform> waypoints;
    int wayIndex = 0;


	// Use this for initialization
	void Start () {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[wayIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        EnemyMovePath();
	}

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void EnemyMovePath()
    {
       if(wayIndex <= waypoints.Count - 1)
        {
            var targetWay = waypoints[wayIndex].transform.position;
            var MovePerFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetWay, MovePerFrame);
            if(transform.position == targetWay)
            {
                wayIndex++;
            }    
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
