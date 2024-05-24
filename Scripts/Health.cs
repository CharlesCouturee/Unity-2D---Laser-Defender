using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void ShakeCamera()
    {
        if (cameraShake && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect)
        {
            ParticleSystem instance = Instantiate(
                hitEffect,
                transform.position,
                Quaternion.identity
                );
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public int GetHealth() {  return health; }
}
