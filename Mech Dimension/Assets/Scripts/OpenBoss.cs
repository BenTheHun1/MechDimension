using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenBoss : MonoBehaviour
{
    public GameObject displayCrystal1;
    public GameObject displayCrystal2;
    public GameObject displayCrystal3;

    private bool doorEnabled;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.hasCrystal1)
        {
            displayCrystal1.SetActive(true);
        }
        else
        {
            displayCrystal1.SetActive(false);
        }
        if (PlayerController.hasCrystal2)
        {
            displayCrystal2.SetActive(true);
        }
        else
        {
            displayCrystal2.SetActive(false);
        }
        if (PlayerController.hasCrystal3)
        {
            displayCrystal3.SetActive(true);
        }
        else
        {
            displayCrystal3.SetActive(false);
        }
    }

    // Update is called once per frame
    public void UpdateCrystal()
    {
        if (PlayerController.hasCrystal1)
        {
            displayCrystal1.SetActive(true);
        }
        else
        {
            displayCrystal1.SetActive(false);
        }
        if (PlayerController.hasCrystal2)
        {
            displayCrystal2.SetActive(true);
        }
        else
        {
            displayCrystal2.SetActive(false);
        }
        if (PlayerController.hasCrystal3)
        {
            displayCrystal3.SetActive(true);
        }
        else
        {
            displayCrystal3.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerController.hasCrystal1 && PlayerController.hasCrystal2 && PlayerController.hasCrystal3)
        {
            doorEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        doorEnabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && doorEnabled)
        {
            SceneManager.LoadScene("BossScene");
        }
    }
}
