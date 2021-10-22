using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerController pl;
    public float speed;
    public bool left; //otherwise go right
    public float timeToDestroy;

    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<PlayerController>();
        left = pl.left;
        StartCoroutine("Destroy");
    }


    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }

    void Update()
    {
        if (left)
        {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().damageEnemy(5);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
