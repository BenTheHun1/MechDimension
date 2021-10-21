using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TempSystem tempSystemScript;


    public float speed;
    public float jumpHeight;
    public bool left; //otherwise, right
    public bool isOnGround;
    public bool isFrozen;

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
    public GameObject Rocket;
    public Rigidbody2D rb;

    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        tempSystemScript = GameObject.Find("tempatureBarBackground").GetComponent<TempSystem>();

        gameObject.GetComponent<SpriteRenderer>().material = def;
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
            Rocket.SetActive(true);
        }
        else
        {
            Rocket.SetActive(false);
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
        if (Input.GetKey(KeyCode.A) && !isFrozen)
        {
            rb.AddForce(Vector2.left * speed * Time.deltaTime);
            gameObject.transform.localScale = new Vector3(1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            left = true;
        }
        else if (Input.GetKey(KeyCode.D) && !isFrozen)
        {
            rb.AddForce(Vector2.right * speed * Time.deltaTime);
            gameObject.transform.localScale = new Vector3(-1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            left = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && hasJump && jumpsLeft > 0 && !isFrozen)
        {
            jumpsLeft--;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            Legs.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().SetBool("jumping", true);
            Legs.GetComponent<Animator>().SetBool("jumping", true);
            Gun.GetComponent<Animator>().SetBool("jumping", true);
            Rocket.GetComponent<Animator>().SetBool("jumping", true);


        }

        if (rb.velocity.x > 2 || rb.velocity.x < -2)
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

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5f, 5f), rb.velocity.y); //change 10f to whatever the real max is
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().damageEnemy(20);
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            Legs.GetComponent<AudioSource>().Play();
        }

        if (collision.gameObject.CompareTag("ToggleDarkness")) //Should probably be a drop, as going back and forth/jumping messes it up OR, have 2 triggers, that ontriggerexit changes things, instead of toggling back and forht with one trigger.
        {
            Debug.Log("Change to Lit");
            gameObject.GetComponent<SpriteRenderer>().material = lit;
            Legs.GetComponent<SpriteRenderer>().material = lit;
            Gun.GetComponent<SpriteRenderer>().material = lit;
            Rocket.GetComponent<SpriteRenderer>().material = lit;
        }
        else if (collision.gameObject.CompareTag("ToggleLight"))
        {
            Debug.Log("Change to Def");
            gameObject.GetComponent<SpriteRenderer>().material = def;
            Legs.GetComponent<SpriteRenderer>().material = def;
            Gun.GetComponent<SpriteRenderer>().material = def;
            Rocket.GetComponent<SpriteRenderer>().material = def;
        }
        if (collision.gameObject.CompareTag("Untagged"))
        {
            Rocket.GetComponent<AudioSource>().Play();

        }
        if (collision.gameObject.CompareTag("Lamp"))
        {
            collision.gameObject.GetComponent<Animator>().SetBool("NearbyPlayer", true);
            tempSystemScript.mechIsInRegularArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            isOnGround = false;
            
        }
        if (collision.gameObject.CompareTag("Lamp"))
        {
            collision.gameObject.GetComponent<Animator>().SetBool("NearbyPlayer", false);
            if (!hasTempControl)
            {
                tempSystemScript.mechIsInRegularArea = false;
            }
        }
    }
}
