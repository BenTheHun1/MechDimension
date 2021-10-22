using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    PlayerController pl;
    Fuel fp;
    
    public enum powerType
    {
        Gun, Jump, Light, Rocket, TempControl, FuelUpgrade1, FuelUpgrade2, HealthUpgrade1, HealthUpgrade2
    }

    public powerType thisPowerUp;



    // Start is called before the first frame update
    void Start()
    {
        if ((PlayerController.hasGun && thisPowerUp == powerType.Gun) || (PlayerController.hasJump && thisPowerUp == powerType.Jump) || (PlayerController.hasLight && thisPowerUp == powerType.Light) || (PlayerController.hasRocketJump && thisPowerUp == powerType.Rocket) || (PlayerController.hasTempControl && thisPowerUp == powerType.TempControl) || (PlayerController.hasFuelUpgrade1 && thisPowerUp == powerType.FuelUpgrade1) || (PlayerController.hasFuelUpgrade2 && thisPowerUp == powerType.FuelUpgrade2) || (PlayerController.hasHPUpgrade1 && thisPowerUp == powerType.HealthUpgrade1) || (PlayerController.hasHPUpgrade2 && thisPowerUp == powerType.HealthUpgrade2))
        {
            Destroy(gameObject);
        }
        pl = GameObject.Find("Player").GetComponent<PlayerController>();
        fp = GameObject.Find("GameManager").GetComponent<Fuel>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (thisPowerUp == powerType.Gun)
            {
                PlayerController.hasGun = true;
                pl.Gun.SetActive(true);
                Debug.Log("got gun");   
            }
            if (thisPowerUp == powerType.Light)
            {
                PlayerController.hasLight = true;
                pl.Light.SetActive(true);
            }
            if (thisPowerUp == powerType.Jump)
            {
                PlayerController.hasJump = true;
                pl.ReloadStats();
                pl.Legs.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (thisPowerUp == powerType.Rocket)
            {
                PlayerController.hasRocketJump = true;
                pl.ReloadStats();
                pl.Rocket.SetActive(true);
            }
            if (thisPowerUp == powerType.TempControl)
            {
                PlayerController.hasTempControl = true;
                pl.TempControl.SetActive(true);
            }
            if (thisPowerUp == powerType.FuelUpgrade1)
            {
                Fuel.fuelTanks++;
                fp.fuel += 0.25f;
                PlayerController.hasFuelUpgrade1 = true;
            }
            if (thisPowerUp == powerType.FuelUpgrade2)
            {
                Fuel.fuelTanks++;
                fp.fuel += 0.25f;
                PlayerController.hasFuelUpgrade2 = true;
            }
            if (thisPowerUp == powerType.HealthUpgrade1)
            {
                HealthSystem.mechMAXHealth += 25;
                PlayerController.hasHPUpgrade1 = true;
            }
            if (thisPowerUp == powerType.HealthUpgrade2)
            {
                HealthSystem.mechMAXHealth += 25;
                PlayerController.hasHPUpgrade2 = true;
            }

            Destroy(gameObject);
        }
    }

}
