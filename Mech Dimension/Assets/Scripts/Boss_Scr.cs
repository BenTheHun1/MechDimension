using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Scr : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private HealthSystem healthSystemScript;
    private musicSrc musicManagerSript;

    public GameObject bossBattleUI;
    public GameObject healthBar;
    public GameObject congratsUI;

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

    public GameObject fireHandBoth;
    public GameObject iceHandBoth;
    public GameObject hitHandBoth;
    public GameObject GetHandBoth;

    public GameObject deathAnim;

    public GameObject robot;
    public GameObject plant;
    public GameObject scream;

    public GameObject spawnPosdMove;
    public GameObject spawnPodStat;
    public GameObject spawnPosStat2;


    // Start is called before the first frame update
    void Start()
    {

        musicManagerSript = GameObject.Find("musicManager").GetComponent<musicSrc>();
        musicManagerSript.playBossTheme();

        playerControllerScript = GameObject.Find("Player").gameObject.GetComponent<PlayerController>();
        healthSystemScript = GameObject.Find("HealthBarBackground").gameObject.GetComponent<HealthSystem>();

        //death anim
        deathAnim.gameObject.SetActive(false);


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

        //handBoth
        fireHandBoth.gameObject.SetActive(false);
        iceHandBoth.gameObject.SetActive(false);
        hitHandBoth.gameObject.SetActive(false);
        GetHandBoth.gameObject.SetActive(false);

        //ui in
        bossBattleUI.gameObject.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(doclapAttack());
        }

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
        attackInProgress = true;
        //body
        regularBossDisplay.gameObject.SetActive(false);
        fireBossDisplay.gameObject.SetActive(true);

        //hand1
        RegHand1.gameObject.SetActive(false);
        fireHand1.gameObject.SetActive(true);

        //hand2
        RegHand2.gameObject.SetActive(false);
        FireHand2.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        //hand1 + 2 (actual objects disable)
        Hand1.gameObject.SetActive(false);
        Hand2.gameObject.SetActive(false);

        //hands together (actual obj enable...and enable correct one)
        fireHandBoth.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        

        for(int i = 0; i < 6; i++)
        {
            Instantiate(fireProj, HandBoth.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);

        }

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < 6; i++)
        {
            Instantiate(fireProj, HandBoth.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);

        }


        //hands together (disable fire one, then disable actual obj)
        fireHandBoth.gameObject.SetActive(false);

        //hands1 + 2 (actual obj enable)
        Hand1.gameObject.SetActive(true);
        Hand2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //body
        regularBossDisplay.gameObject.SetActive(true);
        fireBossDisplay.gameObject.SetActive(false);

        //hand1
        RegHand1.gameObject.SetActive(true);
        fireHand1.gameObject.SetActive(false);

        //hand2
        RegHand2.gameObject.SetActive(true);
        FireHand2.gameObject.SetActive(false);

        yield return new WaitForSeconds(3.0f);

        attackInProgress = false;
    }

    IEnumerator doIceAttack()
    {
        attackInProgress = true;
        //body
        regularBossDisplay.gameObject.SetActive(false);
        iceBossDisplay.gameObject.SetActive(true);

        //hand1
        RegHand1.gameObject.SetActive(false);
        IceHand1.gameObject.SetActive(true);

        //hand2
        RegHand2.gameObject.SetActive(false);
        IceHand2.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        //hand1 + 2 (actual objects disable)
        Hand1.gameObject.SetActive(false);
        Hand2.gameObject.SetActive(false);

        //hands together (actual obj enable...and enable correct one)
        iceHandBoth.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 6; i++)
        {
            Instantiate(iceProj, HandBoth.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);

        }

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < 6; i++)
        {
            Instantiate(iceProj, HandBoth.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);

        }


        //hands together (disable fire one, then disable actual obj)
        iceHandBoth.gameObject.SetActive(false);

        //hands1 + 2 (actual obj enable)
        Hand1.gameObject.SetActive(true);
        Hand2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //body
        regularBossDisplay.gameObject.SetActive(true);
        iceBossDisplay.gameObject.SetActive(false);

        //hand1
        RegHand1.gameObject.SetActive(true);
        IceHand1.gameObject.SetActive(false);

        //hand2
        RegHand2.gameObject.SetActive(true);
        IceHand2.gameObject.SetActive(false);

        yield return new WaitForSeconds(3.0f);
        attackInProgress = false;
    }





    IEnumerator doBlock()
    {
        attackInProgress = true;

        yield return new WaitForSeconds(1.0f);

        //hand1 + 2 (actual objects disable)
        Hand1.gameObject.SetActive(false);
        Hand2.gameObject.SetActive(false);

        //hands together (actual obj enable...and enable correct one)
        GetHandBoth.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        healthSystemScript.damageMech(2f);
        Instantiate(iceProj, HandBoth.transform.position, transform.rotation);
        for (int i = 0; i < 120; i++)
        {
            health += 0.1f;
            healthBar.gameObject.GetComponent<Image>().fillAmount = health / 200;
            yield return new WaitForSeconds(0.01f);

        }

        yield return new WaitForSeconds(1.0f);

        healthSystemScript.damageMech(2f);
        Instantiate(fireProj, HandBoth.transform.position, transform.rotation);

        for (int i = 0; i < 120; i++)
        {
            health += 0.1f;
            healthBar.gameObject.GetComponent<Image>().fillAmount = health / 200;
            yield return new WaitForSeconds(0.01f);

        }


        yield return new WaitForSeconds(1.0f);


        //hands together (disable fire one, then disable actual obj)
        GetHandBoth.gameObject.SetActive(false);

        //hands1 + 2 (actual obj enable)
        Hand1.gameObject.SetActive(true);
        Hand2.gameObject.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        attackInProgress = false;
    }





    IEnumerator doclapAttack()
    {
        attackInProgress = true;
        //body
        regularBossDisplay.gameObject.SetActive(false);
        hitBossDisplay.gameObject.SetActive(true);

        //hand1
        RegHand1.gameObject.SetActive(false);
        HitHand1.gameObject.SetActive(true);

        //hand2
        RegHand2.gameObject.SetActive(false);
        HitHand2.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        //hand1 + 2 (actual objects disable)
        Hand1.gameObject.SetActive(false);
        Hand2.gameObject.SetActive(false);

        //hands together (actual obj enable...and enable correct one)
        hitHandBoth.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Instantiate(iceProj, HandBoth.transform.position, transform.rotation);

        yield return new WaitForSeconds(0.5f);
        //spawn in enemies          !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Instantiate(robot, spawnPosdMove.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);

        Instantiate(fireProj, HandBoth.transform.position, transform.rotation);

        //hands together (disable fire one, then disable actual obj)
        hitHandBoth.gameObject.SetActive(false);

        //hands1 + 2 (actual obj enable)
        Hand1.gameObject.SetActive(true);
        Hand2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //body
        regularBossDisplay.gameObject.SetActive(true);
        hitBossDisplay.gameObject.SetActive(false);

        //hand1
        RegHand1.gameObject.SetActive(true);
        HitHand1.gameObject.SetActive(false);

        //hand2
        RegHand2.gameObject.SetActive(true);
        HitHand2.gameObject.SetActive(false);

        yield return new WaitForSeconds(3.0f);
        attackInProgress = false;
    }
















    public void damageEnemy(float damageDelt)
    {
        health -= damageDelt;
        healthBar.gameObject.GetComponent<Image>().fillAmount = health / 200;
        if (health <= 0)
        {
            enemyDies();
        }
    }


    void enemyDies()
    {
        attackInProgress = true;
        //body
        fireBossDisplay.gameObject.SetActive(false);
        iceBossDisplay.gameObject.SetActive(false);
        hitBossDisplay.gameObject.SetActive(false);
        regularBossDisplay.gameObject.SetActive(false);

        //hand1
        fireHand1.gameObject.SetActive(false);
        IceHand1.gameObject.SetActive(false);
        HitHand1.gameObject.SetActive(false);
        RegHand1.gameObject.SetActive(false);

        //hand2
        FireHand2.gameObject.SetActive(false);
        IceHand2.gameObject.SetActive(false);
        HitHand2.gameObject.SetActive(false);
        RegHand2.gameObject.SetActive(false);
        

        //handBoth
        fireHandBoth.gameObject.SetActive(false);
        iceHandBoth.gameObject.SetActive(false);
        hitHandBoth.gameObject.SetActive(false);
        GetHandBoth.gameObject.SetActive(false);

        deathAnim.gameObject.SetActive(true);


        Destroy(gameObject, 4.0f);
        StartCoroutine(death());

    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(3.5f);
        congratsUI.gameObject.SetActive(true);
    }


}
