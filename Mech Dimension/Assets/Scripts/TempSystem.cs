using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempSystem : MonoBehaviour
{
    public float mechTempature = 0.99f;

    public bool mechIsInColdArea;
    public bool mechIsInHotArea;
    private bool isCurrentlyHurtingPlayer;

    public GameObject mechTempatureDisplay;
    private Color currentMechTempDisplayColor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //updates tempature due to area the mech is in

        if (mechIsInColdArea && mechTempature > 0.05f)
        {
            mechTempature -= Time.deltaTime / 40;            //* modifier if needed
        } else if (mechIsInHotArea && mechTempature < 0.95f)
        {
            mechTempature += Time.deltaTime / 40;            //* modifier if needed
        } else if (!mechIsInColdArea && !mechIsInHotArea && (mechTempature < 0.49f || mechTempature > 0.50f))
        {
            if(mechTempature > 0.5f)
            {
                mechTempature -= Time.deltaTime / 30;        //* modifier if needed
            } else
            {
                mechTempature += Time.deltaTime / 30;        //* modifier if needed
            }
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
            mechTempatureDisplay.gameObject.GetComponent<Image>().color = Color.white;
        }
    }



    void tempIsTooCold()
    {
        currentMechTempDisplayColor = Color.cyan;
        if (!isCurrentlyHurtingPlayer)
        {
            isCurrentlyHurtingPlayer = true;
            StartCoroutine(hurtPlayer());
        }
    }

    void tempIsTooHot()
    {
        currentMechTempDisplayColor = Color.red;
        if (!isCurrentlyHurtingPlayer)
        {
            isCurrentlyHurtingPlayer = true;
            StartCoroutine(hurtPlayer());
        }
    }

    IEnumerator hurtPlayer()
    {
        mechTempatureDisplay.gameObject.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        mechTempatureDisplay.gameObject.GetComponent<Image>().color = currentMechTempDisplayColor;
        StartCoroutine(hurtPlayer2());
    }

    IEnumerator hurtPlayer2()
    {
        yield return new WaitForSeconds(0.5f);
        mechTempatureDisplay.gameObject.GetComponent<Image>().color = Color.white;
        //damage player here                                                                                    <<< access to player healthbar
        isCurrentlyHurtingPlayer = false;
    }


    void updateTempatureDisplay()
    {
        mechTempatureDisplay.gameObject.GetComponent<Image>().fillAmount = mechTempature;
    }


}
