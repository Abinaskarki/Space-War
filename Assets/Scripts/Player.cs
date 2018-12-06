using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {

    [Header("Health + Speed")]
    [SerializeField] float health = 1000f;
    [SerializeField] float Xspeed =10f;
    [SerializeField] float Yspeed = 10f;
    [SerializeField] float padding = .5f;

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float timer = .1f;

    [Header("Explosion")]
    [SerializeField] GameObject DeathVFX;
    [SerializeField] float DeathVFXDuration;

    [Header("SoundEffects")]
    [SerializeField] AudioClip DeathSFX;
    [SerializeField] [Range(0, 1)] float DeathSFXVolume = 0.23f;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] [Range(0, 1)] float ShootSFXVolume = 0.1f;

    Level level;
    float Xmin, Xmax, Ymin, Ymax;
    Coroutine fireCoroutinte;
    bool isFiring = false;

	// Use this for initialization
	void Start ()
    {
        level = FindObjectOfType<Level>();
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
            AudioSource.PlayClipAtPoint(ShootSFX, Camera.main.transform.position, ShootSFXVolume);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0)
        {
            ExplodePlayer();
        }
    }

    private void ExplodePlayer()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(DeathVFX, transform.position, transform.rotation) as GameObject;
        Destroy(explosion, DeathVFXDuration);
        AudioSource.PlayClipAtPoint(DeathSFX, Camera.main.transform.position, DeathSFXVolume);
        level.LoadGameOver();
    }

    public float GetHealth()
    {
        return health;
    }
}
