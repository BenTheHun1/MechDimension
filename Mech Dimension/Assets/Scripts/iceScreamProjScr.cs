using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceScreamProjScr : MonoBehaviour
{
    private float freezeTime = 0.5f;

    private float speed = 1f;

    private float damage = 10f;

    private HealthSystem healthSystemScript;





    // Start is called before the first frame update
    void Start()
    {
        healthSystemScript = GameObject.Find("HealthBarBackground").GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        moveScream();
    }


    void moveScream()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthSystemScript.damageMech(damage);
            Destroy(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }



}
