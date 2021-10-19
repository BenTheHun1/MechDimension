using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fuel : MonoBehaviour
{
    public Image fuelDisplay;
    public float fuel;
    public static int fuelTanks;
    bool flashing;
    Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        if (!(fuelTanks > 2))
        {
            fuelTanks = 2;
        }

        fuel = fuelTanks / 4f;

        defaultColor = fuelDisplay.color;
    }

    // Update is called once per frame
    void Update()
    {
        fuel -= 0.005f * Time.deltaTime;
        if (fuel < 0.15f && !flashing)
        {
            StartCoroutine("Flash");
        }
        else if (fuel <= 0f || Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Ben Testing"); //Replace with "Main" later
        }


        fuelDisplay.fillAmount = fuel;

    }

    IEnumerator Flash()
    {
        flashing = true;
        Debug.Log("Flash");
        while (flashing)
        {
            yield return new WaitForSeconds(0.25f);
            fuelDisplay.color = defaultColor;
            yield return new WaitForSeconds(0.25f);
            fuelDisplay.color = Color.red;
        }
    }
}
