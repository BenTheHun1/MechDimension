using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    PlayerController pl;
    
    public enum powerType
    {
        Gun, Jump, Light, Rocket, TempControl
    }

    public powerType thisPowerUp;



    // Start is called before the first frame update
    void Start()
    {
        if ((PlayerController.hasGun && thisPowerUp == powerType.Gun) || (PlayerController.hasJump && thisPowerUp == powerType.Jump) || (PlayerController.hasLight && thisPowerUp == powerType.Light) || (PlayerController.hasRocketJump && thisPowerUp == powerType.Rocket) || (PlayerController.hasTempControl && thisPowerUp == powerType.TempControl))
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
            if (thisPowerUp == powerType.Gun)
            {
                PlayerController.hasGun = true;
                pl.Gun.SetActive(true);
            }
            
            Destroy(gameObject);
        }
    }

}
