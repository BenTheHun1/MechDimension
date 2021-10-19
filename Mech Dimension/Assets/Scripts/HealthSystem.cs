using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float mechHealth = 50;

    public GameObject HealthBarDisplay;

    private bool isFlashing;

    private Color defaultHealthBarDisplayColor;






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
        if(mechHealth + healAmount > 100)
        {
            mechHealth = 100;
        } else
        {
            mechHealth += healAmount;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
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

    IEnumerator healthIsLowFlash()
    {
        HealthBarDisplay.gameObject.GetComponent<Image>().color = defaultHealthBarDisplayColor;

        yield return new WaitForSeconds(0.5f);
        HealthBarDisplay.gameObject.GetComponent<Image>().color = Color.magenta;                //pick a better distress flash

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
        //disable being able to move the character.
        //possibly death anim?
        //enable dead menu OR just reload scene right away
    }



}
