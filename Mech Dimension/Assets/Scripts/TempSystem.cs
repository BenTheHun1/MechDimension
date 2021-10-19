using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempSystem : MonoBehaviour
{
    public float mechTempature = 0.50f;

    public bool mechIsInColdArea;           //these you only enable in the Ice/Forest areas and disable when out of them
    public bool mechIsInHotArea;        
    public bool mechIsInRegularArea;                //this is what you change when the mech enters a safe area (heat slime or shadow) and disable when exiting
    private bool isCurrentlyHurtingPlayer;

    public GameObject mechTempatureDisplay;

    private Color currentMechTempDisplayColor;
    private Color defaultMechTempDisplyColor;

    private HealthSystem healthSystemScript;


    // Start is called before the first frame update
    void Start()
    {
        healthSystemScript = GameObject.Find("HealthBarBackground").GetComponent<HealthSystem>();
        defaultMechTempDisplyColor = mechTempatureDisplay.gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //updates tempature due to area the mech is in
        if (mechIsInRegularArea && (mechTempature < 0.495f || mechTempature > 0.505f))
        {
            if (mechTempature > 0.5f)
            {
                mechTempature -= Time.deltaTime / 30;        //* modifier if needed
            }
            else
            {
                mechTempature += Time.deltaTime / 30;        //* modifier if needed
            }
        } else if (!mechIsInRegularArea && mechIsInColdArea && mechTempature > 0.05f)
        {
            mechTempature -= Time.deltaTime / 40;            //* modifier if needed
        } else if (!mechIsInRegularArea && mechIsInHotArea && mechTempature < 1f)
        {
            mechTempature += Time.deltaTime / 40;            //* modifier if needed
        } 

        //updates the display of the mech's tempature
        updateTempatureDisplay();


        //calls functions to hurt player if in hot/cold too long
        if(mechTempature >= 0.85f)
        {
            tempIsTooHot();
        } else if (mechTempature <= 0.25f)
        {
            tempIsTooCold();
        } else
        {
            mechTempatureDisplay.gameObject.GetComponent<Image>().color = defaultMechTempDisplyColor;
        }
    }



    void tempIsTooCold()
    {
        currentMechTempDisplayColor = Color.cyan;           //subject to change
        if (!isCurrentlyHurtingPlayer)
        {
            isCurrentlyHurtingPlayer = true;
            StartCoroutine(flashDangerAndHurtPlayer());
        }
    }

    void tempIsTooHot()
    {
        currentMechTempDisplayColor = Color.red;        //subject to change
        if (!isCurrentlyHurtingPlayer)
        {
            isCurrentlyHurtingPlayer = true;
            StartCoroutine(flashDangerAndHurtPlayer());
        }
    }

    IEnumerator flashDangerAndHurtPlayer()
    {
        mechTempatureDisplay.gameObject.GetComponent<Image>().color = defaultMechTempDisplyColor;

        yield return new WaitForSeconds(0.35f);
        mechTempatureDisplay.gameObject.GetComponent<Image>().color = currentMechTempDisplayColor;

        yield return new WaitForSeconds(0.35f);
        mechTempatureDisplay.gameObject.GetComponent<Image>().color = defaultMechTempDisplyColor;
        healthSystemScript.damageMech(1f);                                                          //adjust the damage here <<<<<<<<<<<<<
        isCurrentlyHurtingPlayer = false;
    }


    void updateTempatureDisplay()
    {
        mechTempatureDisplay.gameObject.GetComponent<Image>().fillAmount = mechTempature;
    }


}
