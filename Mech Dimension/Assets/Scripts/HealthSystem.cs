using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    float mechHealth;

    public static float mechMAXHealth = 50;

    public GameObject HealthBarDisplay;

    private bool isFlashing;

    private Color defaultHealthBarDisplayColor;

    public GameObject deathMenu;

    public Fuel fp;



    // Start is called before the first frame update
    void Start()
    {
        fp = GameObject.Find("Fuel Gauge").GetComponent<Fuel>();
        Debug.Log(mechMAXHealth);
        mechHealth = mechMAXHealth;
        healMech(1f);
        defaultHealthBarDisplayColor = HealthBarDisplay.gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //signals low health by flashing
        if (mechHealth <= 25 && !isFlashing)
        {
            isFlashing = true;
            StartCoroutine(healthIsLowFlash());
        }
    }





    public void damageMech(float damage)
    {
        mechHealth -= damage;
        updateHealthBarDisplay();

        //does mech death sequence if mech health 0
        if (mechHealth <= 0.0f)
        {
            mechDies();
        }
    }


    public void healMech(float healAmount)
    {
        if (mechHealth + healAmount > mechMAXHealth)
        {
            mechHealth = mechMAXHealth;
        }
        else
        {
            mechHealth += healAmount;
        }
        updateHealthBarDisplay();
    }


    IEnumerator healthIsLowFlash()
    {
        HealthBarDisplay.gameObject.GetComponent<Image>().color = defaultHealthBarDisplayColor;

        yield return new WaitForSeconds(0.5f);
        HealthBarDisplay.gameObject.GetComponent<Image>().color = Color.black;                //pick a better distress flash

        yield return new WaitForSeconds(0.5f);
        HealthBarDisplay.gameObject.GetComponent<Image>().color = defaultHealthBarDisplayColor;
        isFlashing = false;
    }


    void updateHealthBarDisplay()
    {
        HealthBarDisplay.gameObject.GetComponent<Image>().fillAmount = mechHealth / 100;
    }


    void mechDies()
    {
        fp.fuel = 0;
    }



}
