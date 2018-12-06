using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("ScoreText")]
    GameSession gameSession;
    [SerializeField] int scoreValue = 100;

    [Header("Health + Shoot")]
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetnShots = 0.2f;
    [SerializeField] float maxTimeBetnShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 5f;

    [Header("Explosion")]
    [SerializeField] GameObject DeathVFX;
    [SerializeField] float DeathVFXDuration;

    [Header("SoundEffects")]
    [SerializeField] AudioClip DeathSFX;
    [SerializeField] [Range(0, 1)] float DeathSFXVolume =.75f;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] [Range(0, 1)] float ShootSFXVolume = .1f;

    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetnShots, maxTimeBetnShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetnShots, maxTimeBetnShots);

        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(ShootSFX, Camera.main.transform.position, ShootSFXVolume);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        gameSession.AddScore(scoreValue);
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            ExplodeStars();
        }
    }

    private void ExplodeStars()
    {
        GameObject EnemyExplosion = Instantiate(DeathVFX, transform.position, transform.rotation) as GameObject;
        Destroy(EnemyExplosion, DeathVFXDuration);

        AudioSource.PlayClipAtPoint(DeathSFX, Camera.main.transform.position, DeathSFXVolume);
    }
}
