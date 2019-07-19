using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int score;
    private int lives;

    public float speed;
    public float jumpforce;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    public Object player;
    public AudioClip impact;

    AudioSource audioSource;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score = 0;
        lives = 3;
        winText.text = "";
        SetScoreText();
        SetLivesText();
    }


    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void FixedUpdate()
    {
        float movehorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(movehorizontal, 0);

        rb2d.AddForce(movement * speed);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
    }

    void SetScoreText()

        {
            scoreText.text = "Score: " + score.ToString();
            if (score == 4)
            {
                winText.text = "You win!";
                audioSource = GetComponent<AudioSource>();
                audioSource.Play(0);
                Debug.Log("started");
            }
        }

    void SetLivesText()
        {
            livesText.text = "Lives: " + lives.ToString();
            if (lives == 0)
            {
                winText.text = "You Died!";
                Destroy(player);

            }
        }

    }