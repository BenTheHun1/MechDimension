using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public bool left; //otherwise, right
    public bool isOnGround;

    public static bool hasGun;
    public static bool hasJump;
    public static bool hasRocketJump;
    public static bool hasLight;
    public static bool hasTempControl;

    public Material lit;
    public Material def;

    public int maxJumps;
    public int jumpsLeft;

    public GameObject Gun;
    public GameObject Light;
    public GameObject TempControl;
    public GameObject Legs;
    public Rigidbody2D rb;

    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().material = def;
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (hasGun)
        {
            Gun.SetActive(true);
        }
        else
        {
            Gun.SetActive(false);
        }
        if (hasTempControl)
        {
            TempControl.SetActive(true);
        }
        else
        {
            TempControl.SetActive(false);
        }
        if (hasRocketJump)
        {
            maxJumps = 2;
        }
        else if (hasJump)
        {
            maxJumps = 1;
        }
        else
        {
            maxJumps = 0;
            Legs.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.45f, 0.115f, 1);
        }
        if (hasLight)
        {
            Light.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
        }

    }

    public void ReloadStats()
    {
        if (hasRocketJump)
        {
            maxJumps = 2;
        }
        else if (hasJump)
        {
            maxJumps = 1;
        }
        else
        {
            maxJumps = 0;
        }
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed * Time.deltaTime);
            gameObject.transform.localScale = new Vector3(-1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            left = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed * Time.deltaTime);
            gameObject.transform.localScale = new Vector3(1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            left = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && hasJump && jumpsLeft > 0)
        {
            jumpsLeft--;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
        
        if (rb.velocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving && isOnGround)
        {
            if (!gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();

            }
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            jumpsLeft = maxJumps;
            isOnGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dark")) //Should probably be a drop, as going back and forth/jumping messes it up
        {
            if (gameObject.GetComponent<Renderer>().sharedMaterial == def)
            {
                Debug.Log("Change to Lit");
                gameObject.GetComponent<SpriteRenderer>().material = lit;
            }
            else
            {
                Debug.Log("Change to Def");
                gameObject.GetComponent<SpriteRenderer>().material = def;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            isOnGround = false;

        }
    }
}
