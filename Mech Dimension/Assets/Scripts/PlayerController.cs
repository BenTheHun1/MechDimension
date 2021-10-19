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

    public GameObject Gun;

    // Start is called before the first frame update
    void Start()
    {
        hasJump = hasGun = true;
        if (hasGun)
        {
            Gun.SetActive(true);
        }
        else
        {
            Gun.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
            gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            left = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            left = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && hasJump && isOnGround)
        {
            isOnGround = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            isOnGround = true;
        }
    }
}
