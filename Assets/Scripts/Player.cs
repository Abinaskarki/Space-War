using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float Xspeed =10f;
    [SerializeField] float Yspeed = 10f;
    [SerializeField] float padding = .5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float timer = .1f;

    float Xmin, Xmax, Ymin, Ymax;
    Coroutine fireCoroutinte;
    bool isFiring = false;

	// Use this for initialization
	void Start ()
    {
        SetUpNewBoundaries();
    }


    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            isFiring = true;
           fireCoroutinte =  StartCoroutine(fireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
            StopCoroutine(fireCoroutinte);
        }
    }

    private IEnumerator fireContinuously()
    {
        while (true)
        { 
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(timer);
        }
    }

    private void SetUpNewBoundaries()
    {
        Camera gameCamera = Camera.main;
        Xmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        Xmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        Ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        Ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * Xspeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * Yspeed;
        var XnewPos = Mathf.Clamp(transform.position.x + deltaX, Xmin+padding, Xmax-padding);
        var YnewPos = Mathf.Clamp(transform.position.y + deltaY, Ymin + padding, Ymax -padding);
        transform.position = new Vector2(XnewPos, YnewPos);


    }
}
