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
        forestPlant,
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
    private bool isFacingRight;

    public GameObject enemyHead;
    public GameObject enemyDisplayParent;
    public GameObject enemyDisplayIdle;
    public GameObject enemyDisplayMovement;
    public GameObject enemyDisplayAttack;

    public GameObject enemyDisplayDeath;

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

    public GameObject iceScreamDisplayDeath;


    //iceGoop
    public GameObject iceGoopDisplayIdle;
    public GameObject iceGoopDisplayMovement;
    public GameObject iceGoopDisplayAttack;
    private float iceGoopMoveLength = 2.5f;
    private float iceGoopAttackLength = 0.8f;

    public GameObject iceGoopDisplayDeath;

    //forBeetle
    public GameObject forestBeetleDisplayIdle;
    public GameObject forestBeetleDisplayMovment;
    public GameObject forestBeetleDisplayAttack;
    private float forestBeetleAttackAnimLength = 0.8f;
    private float forestBeetleMovementAnimLength = 2.5f;

    public GameObject forestBeetleDisplayDeath;


    //for Plant
    public GameObject plantProjectilePrefab;
    public GameObject plantDisplayIdle;
    public GameObject plantDisplayAttack;
    public GameObject plantDisplayDeath;
    private float plantAttackAnimLength = 1.0f;

    public GameObject forestPlantDisplayDeath;






    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        if (thisEnemyType.Equals(enemyType.iceScream))
        {
            //variables
            health = 20;
            damage = 2;
            rateOfFire = 3f;
            range = 4;
            sight = 8;
            attackAnimLength = iceScreamAttackAnimLength;

            //animation states
            var Idle = Instantiate(iceScreamDisplayIdle, enemyDisplayParent.transform);
            enemyDisplayIdle = Idle;
            var Attack = Instantiate(iceScreamDisplayAttack, enemyDisplayParent.transform);
            enemyDisplayAttack = Attack;
            enemyDisplayAttack.gameObject.SetActive(false);
            var Death = Instantiate(iceScreamDisplayDeath, enemyDisplayParent.transform);
            enemyDisplayDeath = Death;
            enemyDisplayDeath.gameObject.SetActive(false);

            //projectiles
            projectileToFire = iceScreamProjectilePrefab;
        } else if (thisEnemyType.Equals(enemyType.iceGoop))
        {
            //variables
            health = 15;
            damage = 8;
            rateOfFire = 1.5f;
            range = 0.3f;
            sight = 2;
            movementSpeed = 5f;
            attackAnimLength = iceGoopAttackLength;
            moveAnimLength = iceGoopMoveLength;


            //animation states
            var Idle = Instantiate(iceGoopDisplayIdle, enemyDisplayParent.transform);
            enemyDisplayIdle = Idle;
            var Move = Instantiate(iceGoopDisplayMovement, enemyDisplayParent.transform);
            enemyDisplayMovement = Move;
            enemyDisplayMovement.gameObject.SetActive(false);
            var Attack = Instantiate(iceGoopDisplayAttack, enemyDisplayParent.transform);
            enemyDisplayAttack = Attack;
            enemyDisplayAttack.gameObject.SetActive(false);
            var Death = Instantiate(iceGoopDisplayDeath, enemyDisplayParent.transform);
            enemyDisplayDeath = Death;
            enemyDisplayDeath.gameObject.SetActive(false);
        } else if (thisEnemyType.Equals(enemyType.forestBeetle))
        {
            //variables
            health = 20;
            damage = 10;
            rateOfFire = 2.5f;
            range = 0.5f;
            sight = 2;
            movementSpeed = 25f;
            attackAnimLength = forestBeetleAttackAnimLength;
            moveAnimLength = forestBeetleMovementAnimLength;

            //animation states
            var Idle = Instantiate(forestBeetleDisplayIdle, enemyDisplayParent.transform);
            enemyDisplayIdle = Idle;
            var Move = Instantiate(forestBeetleDisplayMovment, enemyDisplayParent.transform);
            enemyDisplayMovement = Move;
            enemyDisplayMovement.gameObject.SetActive(false);
            var Attack = Instantiate(forestBeetleDisplayAttack, enemyDisplayParent.transform);
            enemyDisplayAttack = Attack;
            enemyDisplayAttack.gameObject.SetActive(false);
            var Death = Instantiate(forestBeetleDisplayDeath, enemyDisplayParent.transform);
            enemyDisplayDeath = Death;
            enemyDisplayDeath.gameObject.SetActive(false);
        } else if (thisEnemyType.Equals(enemyType.forestPlant))
        {
            //variables
            health = 15;
            damage = 2;
            rateOfFire = 2.5f;
            range = 4;
            sight = 8;
            attackAnimLength = plantAttackAnimLength;       // do this for the other start methods!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



            //animation states
            var Idle = Instantiate(plantDisplayIdle, enemyDisplayParent.transform);
            enemyDisplayIdle = Idle;
            var Attack = Instantiate(plantDisplayAttack, enemyDisplayParent.transform);
            enemyDisplayAttack = Attack;
            enemyDisplayAttack.gameObject.SetActive(false);
            var Death = Instantiate(plantDisplayDeath, enemyDisplayParent.transform);
            enemyDisplayDeath = Death;
            enemyDisplayDeath.gameObject.SetActive(false);


            //projectiles
            projectileToFire = plantProjectilePrefab;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enemyDies();
        }



        distanceToPlayer = Vector3.Distance(transform.position, playerControllerScript.transform.position);

        if (thisEnemyType.Equals(enemyType.iceScream) || thisEnemyType.Equals(enemyType.forestPlant))
        {
            doRangedScan();
        } else if (thisEnemyType.Equals(enemyType.iceGoop) || thisEnemyType.Equals(enemyType.forestBeetle))
        {
            doMeleeScan();
        }
    }




    //RANGED STUFF BEGIN --==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--==--
    void doRangedScan()
    {
        if (!isAttacking)
        {
            if (distanceToPlayer >= sight && Mathf.Abs(playerControllerScript.transform.position.y - transform.position.y) < 1)
            {
                //just idle and look
                if (Mathf.Abs(playerControllerScript.transform.position.y - transform.position.y) < 1)
                {
                    if (playerControllerScript.transform.position.x > transform.position.x)
                    {
                        enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
                    } else
                    {
                        enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    }
                }
            } else
            {
                if (Mathf.Abs(playerControllerScript.transform.position.y - transform.position.y) < 1)
                {
                    if (playerControllerScript.transform.position.x > transform.position.x)
                    {
                        enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
                    }
                    else
                    {
                        enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    }
                    if (distanceToPlayer < range)
                    {
                        //makes the head look at the player
                        enemyHead.gameObject.transform.LookAt(playerControllerScript.transform);
                        //attack
                        StartCoroutine(doRangedAttack());
                    }
                }
            }
        }

            }

    IEnumerator doRangedAttack()
    {
        isAttacking = true;
        enemyDisplayIdle.gameObject.SetActive(false);
        enemyDisplayAttack.gameObject.SetActive(true);

        yield return new WaitForSeconds(attackAnimLength / 2);


        //Instantiate(projectileToFire, enemyHead.transform.position, enemyHead.transform.rotation);  //different 
        //enemyHead.transform.rotation.Set(enemyHead.transform.rotation.x, 0, 0, 0);
        //Debug.Log("Z = " + enemyHead.transform.rotation.z + " ...and... Y = " + enemyHead.transform.rotation.y);

        Quaternion angle = new Quaternion(0, 0, 0, 0);
        
        var r = Instantiate(projectileToFire, enemyHead.transform.position, angle);
        r.transform.Rotate(enemyHead.transform.rotation.x, 0, 0);
        if(playerControllerScript.gameObject.transform.position.x > transform.position.x)
        {
            r.gameObject.GetComponent<iceScreamProjScr>().moveRight = true;
            r.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
        }
        else
        {
            r.gameObject.GetComponent<iceScreamProjScr>().moveRight = false;
            r.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
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

        //if can't find player
        if(!isMoving && !isAttacking)
        {
            if(distanceToPlayer >= sight || Mathf.Abs(playerControllerScript.transform.position.y - transform.position.y) > 1)
            {
                defaultWalkDecider();
            } else
            {
                //if player isn't within bounds, idle
                if(playerControllerScript.transform.position.x < leftBounds.transform.position.x || playerControllerScript.transform.position.x > rightBounds.transform.position.x)
                {
                    if (playerControllerScript.transform.position.x > transform.position.x)
                    {
                        enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
                        isFacingRight = true;
                    }
                    else
                    {
                        enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                        isFacingRight = false;
                    }
                    enemyDisplayIdle.gameObject.SetActive(true);
                    enemyDisplayAttack.gameObject.SetActive(false);
                    enemyDisplayMovement.gameObject.SetActive(false);
                } else
                {
                    if(distanceToPlayer > range)
                    {
                        if(playerControllerScript.transform.position.x > transform.position.x)
                        {
                            enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
                            isFacingRight = true;
                        } else
                        {
                            enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                            isFacingRight = false;
                        }
                        defaultWalkDecider();
                    } else
                    {
                        if (playerControllerScript.transform.position.x > transform.position.x)
                        {
                            enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
                            isFacingRight = true;
                        }
                        else
                        {
                            enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                            isFacingRight = false;
                        }
                        StartCoroutine(doMeleeAttack());
                    }
                }
            }
        }
    }


    void defaultWalkDecider()
    {
        if (!isMoving && !isAttacking)
        {
            if (transform.position.x > rightBounds.transform.position.x)
            {
                //switch and move left
                enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                isFacingRight = false;
                StartCoroutine(doMove(false));
            }
            else if (transform.position.x < leftBounds.transform.position.x)
            {
                //switch and move right
                enemyDisplayParent.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
                isFacingRight = true;
                StartCoroutine(doMove(true));
            }
            else if (isFacingRight)
            {
                //move right
                StartCoroutine(doMove(true));
            }
            else
            {
                //move left
                StartCoroutine(doMove(false));
            }
        }
    }

    IEnumerator doMove(bool moveToTheRight)
    {
        isMoving = true;
        enemyDisplayMovement.gameObject.SetActive(true);
        enemyDisplayIdle.gameObject.SetActive(false);

        //do actual moving here
        for(int i = 0; i < movementSpeed; i++)
        {
            if (moveToTheRight)
            {
                transform.Translate(0.01f, 0, 0);
                yield return new WaitForSeconds(moveAnimLength / 100);
            }
            else
            {
                transform.Translate(-0.01f, 0, 0);
                yield return new WaitForSeconds(moveAnimLength / 100);
            }
        }
        //enemyDisplayMovement.gameObject.SetActive(false);
        //enemyDisplayIdle.gameObject.SetActive(true);

        //yield return new WaitForSeconds(moveAnimLength / 4);
        isMoving = false;
    }


    IEnumerator doMeleeAttack()
    {
        isAttacking = true;
        enemyDisplayAttack.gameObject.SetActive(true);
        enemyDisplayIdle.gameObject.SetActive(false);
        enemyDisplayMovement.gameObject.SetActive(false);

        yield return new WaitForSeconds(attackAnimLength / 2);


        //if player is in range damage them


        yield return new WaitForSeconds(attackAnimLength / 2);
        enemyDisplayAttack.gameObject.SetActive(false);
        enemyDisplayIdle.gameObject.SetActive(true);

        //this is to wait until the player can attack again
        yield return new WaitForSeconds(rateOfFire - attackAnimLength);
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
        enemyDisplayAttack.gameObject.SetActive(false);
        enemyDisplayIdle.gameObject.SetActive(false);
        if(enemyDisplayMovement != null)
        {
            enemyDisplayMovement.gameObject.SetActive(false);
        }
        enemyDisplayDeath.gameObject.SetActive(true);
        Destroy(gameObject, 1.2f);
    }



}
