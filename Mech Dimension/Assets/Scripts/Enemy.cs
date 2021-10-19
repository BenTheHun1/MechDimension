using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //class variable
    enum enemyType
    {
        iceScream,
        iceEnemy2,
        SiFiEnemy1,
        SiFiEnemy2,
        ForestEnemy1,
        ForestEnemy2
    }

    //player accesss
    private PlayerController playerControllerScript;

    //generic
    private enemyType thisEnemyType;

    private float health;
    private float damage;
    private float rateOfFire;
    private float range;
    private float movementSpeed;

    private bool isAttacking;

    //iceScream
    private float slowDowntime;




    // Start is called before the first frame update
    void Start()
    {
        //playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();      //not until it is in the scene

        if (thisEnemyType.Equals(enemyType.iceScream))
        {
            health = 20;
            damage = 2;
            rateOfFire = 6;     //once every ___ seconds
            range = 4;
            movementSpeed = 0;
            slowDowntime = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (thisEnemyType.Equals(enemyType.iceScream))
        {
            iceScreamScan();
        }
    }




    void iceScreamScan()
    {
        //if the player is within 8 blocks, the  enemy starts to track them.
        //if the player is within 6 blocks, the enemy starts to shoot them IF not already attacking
    }

    IEnumerator iceScreamAttack()
    {
        isAttacking = true;
        //do attack anim once (then stop it either here or after the waitforseconds
        //instantiate the projectile and shoot it at the player aprox
        yield return new WaitForSeconds(rateOfFire);      //different?
        isAttacking = false;
    }




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
