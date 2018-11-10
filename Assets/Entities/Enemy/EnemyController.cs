using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public GameObject enemyShot;
    public AudioClip pew;
    public AudioClip rip;
    public static float fireRate = 0.25f;
    public static float shotSpeed = 3.0f;
    public int scoreValue = 100;

    private ScoreKeeper scoreKeeper;
    private int _health = 15;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        float probability = Time.deltaTime * fireRate;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    private void Fire()
    {
        float shotY = transform.position.y - 0.2f;
        GameObject shot = Instantiate(enemyShot, new Vector3(transform.position.x, shotY), Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -shotSpeed, 0);
        AudioSource.PlayClipAtPoint(pew, this.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            _health -= projectile.GetDamage();
            projectile.Hit();
            if (_health <= 0)
            {
                scoreKeeper.UpdateScore(scoreValue);
                AudioSource.PlayClipAtPoint(rip, this.transform.position);
                Destroy(gameObject);
            }
        }
    }
}
