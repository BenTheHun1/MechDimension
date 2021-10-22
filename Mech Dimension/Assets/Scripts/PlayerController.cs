using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TempSystem tempSystemScript;
    public float iHaveThisManyCrystals = 0;

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

    public static bool hasFuelUpgrade1;
    public static bool hasFuelUpgrade2;
    public static bool hasHPUpgrade1;
    public static bool hasHPUpgrade2;

    public static bool hasCrystal1;
    public static bool hasCrystal2;
    public static bool hasCrystal3;


    public Material lit;
    public Material def;

    public int maxJumps;
    public int jumpsLeft;

    public GameObject Gun;
    public GameObject Light;
    public GameObject Legs;
    public GameObject Rocket;
    public Rigidbody2D rb;
    public musicSrc music;

    public bool debugJump;
    public bool debugLight;

    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        tempSystemScript = GameObject.Find("tempatureBarBackground").GetComponent<TempSystem>();
        music = GameObject.Find("musicManager").GetComponent<musicSrc>();

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
            tempSystemScript.mechHasTempControlMod = true;
        }
        else
        {
            tempSystemScript.mechHasTempControlMod = false;
        }
        if (hasRocketJump || debugJump)
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
        if (hasLight || debugLight)
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

        if (Input.GetKeyDown(KeyCode.Space) && (hasJump || debugJump) && jumpsLeft > 0 && !isFrozen)
        {
            jumpsLeft--;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            Legs.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().SetBool("jumping", true);
            Legs.GetComponent<Animator>().SetBool("jumping", true);
            Gun.GetComponent<Animator>().SetBool("jumping", true);
            Rocket.GetComponent<Animator>().SetBool("jumping", true);

            if (PlayerController.hasRocketJump && jumpsLeft == 0)
            {
                Rocket.GetComponent<Animator>().SetBool("fire", true);
            }

        }

        if (rb.velocity.x > 2 || rb.velocity.x < -2)
        {
            isMoving = true;
            gameObject.GetComponent<Animator>().SetBool("running", true);
            Legs.GetComponent<Animator>().SetBool("running", true);
            Gun.GetComponent<Animator>().SetBool("running", true);
            Rocket.GetComponent<Animator>().SetBool("running", true);
        }
        else
        {
            isMoving = false;
            gameObject.GetComponent<Animator>().SetBool("running", false);
            Legs.GetComponent<Animator>().SetBool("running", false);
            Gun.GetComponent<Animator>().SetBool("running", false);
            Rocket.GetComponent<Animator>().SetBool("running", false);
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

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -7f, 7f), rb.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            jumpsLeft = maxJumps;
            isOnGround = true;
            gameObject.GetComponent<Animator>().SetBool("jumping", false);
            Legs.GetComponent<Animator>().SetBool("jumping", false);
            Gun.GetComponent<Animator>().SetBool("jumping", false);
            Rocket.GetComponent<Animator>().SetBool("jumping", false);
            Rocket.GetComponent<Animator>().SetBool("fire", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision.transform.parent.parent.gameObject.name);
            collision.gameObject.transform.parent.parent.gameObject.GetComponent<Enemy>().damageEnemy(20f);
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
            music.playSiFiTheme();
        }
        else if (collision.gameObject.CompareTag("ToggleLight"))
        {
            Debug.Log("Change to Def");
            gameObject.GetComponent<SpriteRenderer>().material = def;
            Legs.GetComponent<SpriteRenderer>().material = def;
            Gun.GetComponent<SpriteRenderer>().material = def;
            Rocket.GetComponent<SpriteRenderer>().material = def;
            music.playHubTheme();
        }
        if (collision.gameObject.CompareTag("ToggleHeat"))
        {
            tempSystemScript.mechIsInHotArea = true;
            tempSystemScript.mechIsInRegularArea = false;
            music.playForestTheme();
        }
        if (collision.gameObject.CompareTag("ToggleCold"))
        {
            tempSystemScript.mechIsInColdArea = true;
            tempSystemScript.mechIsInRegularArea = false;
            music.playIceTheme();
        }
        if (collision.gameObject.CompareTag("ToggleNormal"))
        {
            tempSystemScript.mechIsInHotArea = false;
            tempSystemScript.mechIsInColdArea = false;
            tempSystemScript.mechIsInRegularArea = true;
            music.playHubTheme();
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
