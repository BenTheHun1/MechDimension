using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_Proj_Src : MonoBehaviour
{
    private float freezeTime = 0.5f;

    private float speed = 1f;

    private float damage = 5f;

    private TempSystem tempSystemScript;
    private PlayerController playerControllerScript;

    public GameObject display;

    public bool isFireball;

    public Collider2D col;




    // Start is called before the first frame update
    void Start()
    {
        tempSystemScript = GameObject.Find("tempatureBarBackground").GetComponent<TempSystem>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveProj();
    }


    void moveProj()
    {
        //transform.position = Vector3.Lerp(transform.position, playerControllerScript.transform.position, Time.time * speed);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerControllerScript.transform.position - transform.position), 5 * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            display.gameObject.SetActive(false);
            col.enabled = false;
            StartCoroutine(hitPlayer());
        }
        else
        {
            Destroy(gameObject);
        }
    }


    IEnumerator hitPlayer()
    {
        for(int i  = 0; i < 10; i++)
        {
            if (isFireball)
            {
                tempSystemScript.mechIsInHotArea = true;
            }
            else
            {
                tempSystemScript.mechIsInColdArea = true;
            }
            yield return new WaitForSeconds(0.2f);
        }
        if (isFireball)
        {
            tempSystemScript.mechIsInHotArea = false;
        }
        else
        {
            tempSystemScript.mechIsInColdArea = false;
        }
        Destroy(gameObject);
    }


}
