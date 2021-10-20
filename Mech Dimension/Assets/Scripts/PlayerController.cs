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
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().material = def;
        rb = gameObject.GetComponent<Rigidbody2D>();
        hasJump = true;
        if (hasGun)
        {
            Gun.SetActive(true);
        }
        else
        {
            Gun.SetActive(false);
        }
        if (hasRocketJump)
        {
            maxJumps = 2;
        }
        else
        {
            maxJumps = 1;
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
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            jumpsLeft = maxJumps;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dark"))
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

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            isOnGround = false;

        }
    }*/
}
