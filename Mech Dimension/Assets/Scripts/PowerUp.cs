using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    PlayerController pl;
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        if ((PlayerController.hasGun && type == "gun") || (PlayerController.hasJump && type == "jump") || (PlayerController.hasLight && type == "light") || (PlayerController.hasRocketJump && type == "rocket") || (PlayerController.hasTempControl && type == "temp"))
        {
            Destroy(gameObject);
        }
        pl = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (type == "gun")
            {
                PlayerController.hasGun = true;
                pl.Gun.SetActive(true);
            }
            
            Destroy(gameObject);
        }
    }

}
