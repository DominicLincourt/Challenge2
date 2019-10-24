using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float speed;
    public Text score;
    public Text lives;
    public Text winText;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    private int scoreValue = 0;
    private int livesValue = 3;
    private Rigidbody2D rd2d;

    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 3);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


     private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 5)
                {
                    transform.position = new Vector2(65.0f, 55.0f);
                    livesValue = 3;
                    lives.text = "Lives: " + livesValue.ToString();
            }

            if (scoreValue == 10)
                {
                speed = 0;
                winText.text = "You Win! Game created by Dominic Lincourt";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
        }
        if (collision.collider.tag == "enemy")
        {
            livesValue = livesValue - 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);

            if (livesValue <= 0)
            {
                speed = 0;
                winText.text = "You Lose!";
                anim.SetInteger("State", 4);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 1);
            }
        }
    }
}
