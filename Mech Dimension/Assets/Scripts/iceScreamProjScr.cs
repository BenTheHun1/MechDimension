using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceScreamProjScr : MonoBehaviour
{
    private float freezeTime = 0.5f;

    private float speed = 1f;

    private float damage = 10f;

    private HealthSystem healthSystemScript;
    private PlayerController playerControllerScript;

    public bool moveRight;





    // Start is called before the first frame update
    void Start()
    {
        healthSystemScript = GameObject.Find("HealthBarBackground").GetComponent<HealthSystem>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveScream();
    }


    void moveScream()
    {
        if (moveRight)
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime);

        } else
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthSystemScript.damageMech(damage);
            playerControllerScript.isFrozen = true;
            Destroy(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }



    IEnumerator freezePlaya()
    {
        yield return new WaitForSeconds(0.75f);
        playerControllerScript.isFrozen = false;
    }


}
