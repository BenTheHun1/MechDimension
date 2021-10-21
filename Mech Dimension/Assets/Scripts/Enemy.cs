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
        forestBeetle,
        forestEnemy2,
        SiFiEnemy1,
        SiFiEnemy2,
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
    public GameObject enemyDisplayParent;
    public GameObject enemyDisplayIdle;
    public GameObject enemyDisplayMovement;
    public GameObject enemyDisplayAttack;

    private GameObject projectileToFire;

    public GameObject rightBounds;
    public GameObject leftBounds;

    private float attackAnimLength;
    private float moveAnimLength;


    //iceScream
    public GameObject iceScreamProjectilePrefab;
    public GameObject iceScreamDisplayIdle;
    public GameObject iceScreamDisplayAttack;
    private float iceScreamAttackAnimLength = 1.5f;


    //iceGoop
    public GameObject iceGoopDisplayIdle;
    public GameObject iceGoopDisplayMovement;
    public GameObject iceGoopDisplayAttack;
    //private float iceGoopMoveLength = 10000;          //work on these
    //attack and move

    //forBeetle
    public GameObject forestBeetleDisplayIdle;
    public GameObject forestBeetleDisplayMovment;
    public GameObject forestBeetleDisplayAttack;
    //same for this attack and move


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();      //not until it is in the scene

        if (thisEnemyType.Equals(enemyType.iceScream))
        {
            //variables
            health = 20;
            damage = 2;
            rateOfFire = 6;     //once every ___ seconds
            range = 4;
            sight = 8;
            attackAnimLength = iceScreamAttackAnimLength;       // do this for the other start methods!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //animation states
            var Idle = Instantiate(iceScreamDisplayIdle, enemyDisplayParent.transform);
            enemyDisplayIdle = Idle;
            var Attack = Instantiate(iceScreamDisplayAttack, enemyDisplayParent.transform);
            enemyDisplayAttack = Attack;
            enemyDisplayAttack.gameObject.SetActive(false);

            //projectiles
            projectileToFire = iceScreamProjectilePrefab;
        } else if (thisEnemyType.Equals(enemyType.iceGoop))
        {
            //variables
            health = 15;
            damage = 8;
            rateOfFire = 2;
            range = 1;
            sight = 6;
            movementSpeed = 2;

            //animation states
            var Idle = Instantiate(iceGoopDisplayIdle, enemyDisplayParent.transform);
            enemyDisplayIdle = Idle;
            var Move = Instantiate(iceGoopDisplayMovement, enemyDisplayParent.transform);
            enemyDisplayMovement = Move;
            enemyDisplayMovement.gameObject.SetActive(false);
            var Attack = Instantiate(iceGoopDisplayAttack, enemyDisplayParent.transform);
            enemyDisplayAttack = Attack;
            enemyDisplayAttack.gameObject.SetActive(false);
        } else if (thisEnemyType.Equals(enemyType.forestBeetle))
        {
            //variables
            health = 20;
            damage = 10;
            rateOfFire = 2.5f;
            range = 1;
            sight = 5;
            movementSpeed = 1.5f;

            
            //enemyDisplayIdle = forBeetleDisplayIdle;              //make sure to set it to active if its off
            //enemyDisplayMovement = forBeetleDisplayMovment;
            //enemyDisplayAttack = forBeetleDisplayAttack;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(doRangedAttack());
        }


        distanceToPlayer = Vector3.Distance(transform.position, playerControllerScript.transform.position);

        if (thisEnemyType.Equals(enemyType.iceScream))
        {
            doRangedScan();
        } else if (thisEnemyType.Equals(enemyType.iceGoop) || thisEnemyType.Equals(enemyType.forestBeetle))    //right? or?
        {
            doMeleeScan();
        }
    }




    //RANGED STUFF BEGIN --==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--
    void doRangedScan()
    {
        if (distanceToPlayer <= sight)
        {
            //makes the head look at the player
            enemyHead.gameObject.transform.LookAt(playerControllerScript.transform);

            //makes the body look in the general direction of the player
            if (playerControllerScript.transform.position.x > transform.position.x)
            {
                enemyDisplayParent.gameObject.GetComponent<SpriteRenderer>().flipX = true;    //it might be the oposite of this.
            }
            else
            {
                enemyDisplayParent.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            //attacks if in range
            if (distanceToPlayer <= range && !isAttacking)
            {
                StartCoroutine(doRangedAttack());
            }
        }
    }

    IEnumerator doRangedAttack()
    {
        isAttacking = true;
        enemyDisplayIdle.gameObject.SetActive(false);
        enemyDisplayAttack.gameObject.SetActive(true);

        yield return new WaitForSeconds(attackAnimLength / 2);


        Instantiate(projectileToFire, enemyHead.transform.position, enemyHead.transform.rotation);

        yield return new WaitForSeconds(attackAnimLength / 2);


        enemyDisplayIdle.gameObject.SetActive(true);
        enemyDisplayAttack.gameObject.SetActive(false);
        yield return new WaitForSeconds(rateOfFire - attackAnimLength);

        isAttacking = false;
    }
    //RANGED STUFF END  --==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--





    //MELEE STUFF BEGIN --==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--
    void doMeleeScan()
    {
        if ((Mathf.Abs(playerControllerScript.transform.position.y - transform.position.y) < sight / 2) && (distanceToPlayer <= sight))
        {
            if (distanceToPlayer <= range && !isAttacking && !isMoving)
            {
                StartCoroutine(doMeleeAttack());
            } else
            {
                if(transform.position.x < rightBounds.transform.position.x || transform.position.x > leftBounds.transform.position.x && !isMoving && !isAttacking)
                {
                    if (playerControllerScript.transform.position.x > transform.position.x)
                    {
                            StartCoroutine(doMove(true));   //move right        START COROUTINE!!!
                    }
                    else
                    {
                        StartCoroutine(doMove(false));  //move left
                    }
                }
            }

        }
        else
        {
            //if you are too far left or right switch and go other direction
            if(transform.position.x >= rightBounds.transform.position.x && !isMoving && !isAttacking)
            {
                //switch and start moving left
                enemyDisplayParent.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                StartCoroutine(doMove(false));
                
            } else if(transform.position.x <= leftBounds.transform.position.x && !isMoving && !isAttacking)
            {
                //switch and start moving right
                enemyDisplayParent.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                StartCoroutine(doMove(true));

                //otherwise go the direction you are facing
            } else if(!isMoving && !isAttacking)
            {
                if(enemyDisplayParent.gameObject.GetComponent<SpriteRenderer>().flipX == false)
                {
                    //move left
                    StartCoroutine(doMove(false));
                } else
                {
                    //move right
                    StartCoroutine(doMove(true));
                }
            }
        }
    }

    IEnumerator doMove(bool moveToTheRight)
    {
        isMoving = true;
        

        //use a while loop here


        //make sure to enable ismoving now and set to false when not    //which way to move is crucial, and lerp to that area
        //do move animation

        yield return new WaitForSeconds(moveAnimLength);  //idk
        isMoving = false;
    }


    IEnumerator doMeleeAttack()
    {
        isAttacking = true;

        //attack anim once
        //if player is in range damage them
        //wait till the attack ends
        //stop attack anim

        yield return new WaitForSeconds(rateOfFire);
        isAttacking = false;
    }
    //MELEE STUFF END   --==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--





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
