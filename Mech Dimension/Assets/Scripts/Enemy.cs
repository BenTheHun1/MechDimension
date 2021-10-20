using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //class variable
    public enum enemyType
    {
        iceScream,
        iceGoop,
        SiFiEnemy1,
        SiFiEnemy2,
        ForestEnemy1,
        ForestEnemy2
    }

    //player accesss
    private PlayerController playerControllerScript;

    //generic
    public enemyType thisEnemyType;

    private float health;
    private float damage;
    private float rateOfFire;
    private float range;
    private float sight;
    private float movementSpeed;
    private float distanceToPlayer = 100;

    private bool isAttacking;
    private bool isMoving;

    public GameObject enemyHead;
    public GameObject enemyDisplay;

    //iceScream
    private float slowDowntime;

    public GameObject iceScreamProjectilePrefab;




    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();      //not until it is in the scene

        if (thisEnemyType.Equals(enemyType.iceScream))
        {
            health = 20;
            damage = 2;
            rateOfFire = 6;     //once every ___ seconds
            range = 4;
            sight = 8;
            slowDowntime = 2;
        } else if (thisEnemyType.Equals(enemyType.iceGoop))
        {
            health = 15;
            damage = 2;
            rateOfFire = 2;
            range = 1;
            sight = 6;
            movementSpeed = 2;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerControllerScript.transform.position);

        if (thisEnemyType.Equals(enemyType.iceScream))
        {
            iceScreamScan();
        } else if (thisEnemyType.Equals(enemyType.iceGoop))
        {
            
        }
    }


    //ICESCREAM STUFF START
    void iceScreamScan()
    {
        if(distanceToPlayer <= sight)
        {
            //makes the head look at the player
            enemyHead.gameObject.transform.LookAt(playerControllerScript.transform);   
            
            //makes the body look in the general direction of the player
            if(playerControllerScript.transform.position.x > transform.position.x)
            {
                enemyDisplay.gameObject.GetComponent<SpriteRenderer>().flipX = true;    //it might be the oposite of this.
            } else
            {
                enemyDisplay.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            //attacks if in range
            if(distanceToPlayer <= range && !isAttacking)
            {
                StartCoroutine(iceScreamAttack());
            }
        }
    }


    IEnumerator iceScreamAttack()
    {
        isAttacking = true;
        //do attack anim once (then stop it either here or after the waitforseconds)
        Instantiate(iceScreamProjectilePrefab, enemyHead.transform.position, enemyHead.transform.rotation);
        yield return new WaitForSeconds(rateOfFire);                                                        //different?
        isAttacking = false;
    }
    //ICESCREAM STUFF END






    //ICEGOOP STUFF START
    void iceGoopScan()
    {
        if((Mathf.Abs(playerControllerScript.transform.position.y - transform.position.y) < sight / 2) && (distanceToPlayer <= sight))
        {
            if(distanceToPlayer <= range && !isAttacking && !isMoving)
            {
                StartCoroutine(iceGoopAttack());
            }
            //else, if moving doesn't make you fall, move towards player



        } else
        {
            //just  move back and forth between your bounds
        }
    }

    void iceGoopMove()
    {

    }

    IEnumerator iceGoopAttack()
    {
        isAttacking = true;
        //attack anim once
        //if player is in range damage them
        //wait till the attack ends
        //stop attack anim

        yield return new WaitForSeconds(rateOfFire);
        isAttacking = false;
    }
    //ICEGOOP STUFF START














    public void damageEnemy(float damageDelt)
    {
        health -= damageDelt;
        if(health <= 0)
        {
            enemyDies();
        }
    }

    void enemyDies()
    {
        //do enemy death animation
        //delete enemy
    }



}
