using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Scr : MonoBehaviour
{
    private PlayerController playerControllerScript;

    private bool attackInProgress;

    private float health = 200;
    private float damage = 10f;

    public GameObject regularBossDisplay;
    public GameObject fireBossDisplay;
    public GameObject iceBossDisplay;
    public GameObject hitBossDisplay;

    public GameObject fireProj;
    public GameObject iceProj;





    public GameObject Hand1;

    public GameObject fireHand1;
    public GameObject IceHand1;
    public GameObject RegHand1;
    public GameObject HitHand1;

    public GameObject Hand2;

    public GameObject FireHand2;
    public GameObject IceHand2;
    public GameObject RegHand2;
    public GameObject HitHand2;

    public GameObject HandBoth;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").gameObject.GetComponent<PlayerController>();

        //body
        fireBossDisplay.gameObject.SetActive(false);
        iceBossDisplay.gameObject.SetActive(false);
        hitBossDisplay.gameObject.SetActive(false);

        //hand1
        fireHand1.gameObject.SetActive(false);
        IceHand1.gameObject.SetActive(false);
        HitHand1.gameObject.SetActive(false);

        //hand2
        FireHand2.gameObject.SetActive(false);
        IceHand2.gameObject.SetActive(false);
        HitHand2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //turn to player
        if (playerControllerScript.transform.position.x > transform.position.x)
        {
            transform.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
        }
        else
        {
            transform.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }


        if (!attackInProgress)
        {
            int rand = Random.Range(0, 10);
            if(rand < 3)
            {
                StartCoroutine(doFireAttack());
            } else if(rand < 6)
            {
                StartCoroutine(doIceAttack());
            } else if(rand < 8)
            {
                StartCoroutine(doclapAttack());
            } else
            {
                StartCoroutine(doBlock());
            }
        }






    }











    IEnumerator doFireAttack()
    {
        //body

        //hand1

        //hand2

        yield return new WaitForSeconds(1.0f);

        //hand1 + 2
        
        //hands together




    }

    IEnumerator doIceAttack()
    {

        yield return new WaitForSeconds(1.0f);

    }

    IEnumerator doBlock()
    {


        yield return new WaitForSeconds(1.0f);


    }

    IEnumerator doclapAttack()
    {

        yield return new WaitForSeconds(1.0f);

    }
















    public void damageEnemy(float damageDelt)
    {
        health -= damageDelt;
        if (health <= 0)
        {
            enemyDies();
        }
    }


    void enemyDies()
    {
        //enemyDisplayAttack.gameObject.SetActive(false);
        //enemyDisplayIdle.gameObject.SetActive(false);
        // enemyDisplayMovement.gameObject.SetActive(false);
        //enemyDisplayDeath.gameObject.SetActive(true);
        //Destroy(gameObject, 0.8f);
    }

}
