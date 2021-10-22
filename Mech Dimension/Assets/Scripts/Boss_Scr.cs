using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Scr : MonoBehaviour
{
    private float health = 200;
    private float damage = 10f;

    public GameObject regularBossDisplay;
    public GameObject fireBossDisplay;
    public GameObject iceBossDisplay;
    public GameObject blackBossDisplay;

    public GameObject fireProj;
    public GameObject iceProj;






    public GameObject Hand1;
    public GameObject Hand2;

    public GameObject HandBoth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }











    IEnumerator doFireAttack()
    {

        yield return new WaitForSeconds(1.0f);

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
