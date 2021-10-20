using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempSystem : MonoBehaviour
{
    public float mechTempature = 0.50f;

    public bool mechIsInColdArea;            //these you only enable in the Ice/Forest areas and disable when out of them
    public bool mechIsInHotArea;        
    public bool mechIsInRegularArea;                //this is what you change when the mech enters a safe area (heat slime or shadow) and disable when exiting
    public bool mechHasTempControlMod;
    public bool mechHasCoolingOn;
    public bool mechHasWarmingOn;

    private bool mechhasEnabledTempControlModDisplayFirstTime;
    private bool isCurrentlyHurtingPlayer;
    

    public GameObject mechTempatureDisplay;
    public GameObject mechTempatureControlModDisplay;
    public GameObject EToCoolBackdropDisplay;
    public GameObject QToWarmBackdropDisplay;

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
                if (mechTempature >= 0.85f)
                {
                    mechTempature -= Time.deltaTime / 5;       //go back to normal super fast here
                }
                else
                {
                    mechTempature -= Time.deltaTime / 15;
                }
            }
            else
            {
                if (mechTempature <= 0.25f)
                {
                    mechTempature += Time.deltaTime / 5;       //go back to normal super fast here
                }
                else
                {
                    mechTempature += Time.deltaTime / 15;
                }
            }
        } else if (!mechIsInRegularArea && mechIsInColdArea && mechTempature > 0.05f)
        {
            mechTempature -= Time.deltaTime / 25;            //* modifier if needed
        } else if (!mechIsInRegularArea && mechIsInHotArea && mechTempature < 1f)
        {
            mechTempature += Time.deltaTime / 25;            //* modifier if needed
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





        //if the mech has the TempControlMod, the display is out and the input system is implemented
        if (mechHasTempControlMod)
        {
            //enable tempcontroldisplay on first run through
            if (!mechhasEnabledTempControlModDisplayFirstTime)
            {
                mechTempatureControlModDisplay.gameObject.SetActive(true);
            }

            //check for warming/cooling buttons
            updateCoolingAndWarmingInputs();
        }








    }


    void updateCoolingAndWarmingInputs()
    {
        //if E is pressed enable/disable cooling and disable warming
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!mechHasCoolingOn)
            {
                mechHasCoolingOn = true;
                mechHasWarmingOn = false;
                EToCoolBackdropDisplay.gameObject.GetComponent<Image>().color = Color.cyan;
                QToWarmBackdropDisplay.gameObject.GetComponent<Image>().color = Color.black;
                if (mechIsInHotArea)
                {
                    mechIsInRegularArea = true;
                } else
                {
                    mechIsInRegularArea = false;
                }
            }
            else
            {
                mechHasCoolingOn = false;
                EToCoolBackdropDisplay.gameObject.GetComponent<Image>().color = Color.black;
                mechIsInRegularArea = false;
            }

            //if Q is pressed enable/disable warming and disable cooling
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!mechHasWarmingOn)
            {
                mechHasWarmingOn = true;
                mechHasCoolingOn = false;
                QToWarmBackdropDisplay.gameObject.GetComponent<Image>().color = Color.red;
                EToCoolBackdropDisplay.gameObject.GetComponent<Image>().color = Color.black;
                if (mechIsInColdArea)
                {
                    mechIsInRegularArea = true;
                } else
                {
                    mechIsInRegularArea = false;
                }
            }
            else
            {
                mechHasWarmingOn = false;
                QToWarmBackdropDisplay.gameObject.GetComponent<Image>().color = Color.black;
                mechIsInRegularArea = false;
            }
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
        healthSystemScript.damageMech(2f);

        yield return new WaitForSeconds(0.35f);
        mechTempatureDisplay.gameObject.GetComponent<Image>().color = defaultMechTempDisplyColor;
        healthSystemScript.damageMech(2f);                                                          //adjust the damage here <<<<<<<<<<<<<
        isCurrentlyHurtingPlayer = false;
    }


    void updateTempatureDisplay()
    {
        mechTempatureDisplay.gameObject.GetComponent<Image>().fillAmount = mechTempature;
    }


}
