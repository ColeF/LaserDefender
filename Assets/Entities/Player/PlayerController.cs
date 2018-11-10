using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Text Lives;
    public GameObject playerShot;
    public AudioClip pew;
    public AudioClip shieldUp;
    public AudioClip rip;
    public float moveSpeed = 15.0f;
    public float shotSpeed;
    public float shotDelay = 0.000001f;
    public float firingRate = 0.25f;

    private int _lives;
    private int _health = 25;
    
    private float padding = 0.5f;
    float xMin;
    float xMax;

	void Start () {
        _lives = 0;
        UpdateLives();

        // Use camera to calculate min and max X position so that the player stays within the camera
        float zDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zDistance));
        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;
        AudioSource.PlayClipAtPoint(shieldUp, this.transform.position);
    }

    private void UpdateLives()
    {
        if (_lives > 0) { Lives.text = "Lives: " + _lives.ToString(); }
        else { Lives.text = ""; }
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
                Die();
            }
        }
    }

    void Die()
    {
        _lives--;
        AudioSource.PlayClipAtPoint(rip, this.transform.position);
        //UpdateLives();
        Destroy(gameObject);
        if (_lives > 0) { Respawn(); }
        else
        {
            LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            man.LoadLevel("Game_Over");
        }
    }

    void Fire()
    {
        float shotY = transform.position.y + 0.55f;
        GameObject shot = Instantiate(playerShot, new Vector3(transform.position.x, shotY), Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, shotSpeed, 0);
        AudioSource.PlayClipAtPoint(pew, this.transform.position);
    }

    void Respawn()
    {
        // TODO: Implement respawn function.
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", shotDelay, firingRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        //Restrict player movement within the game screen
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
