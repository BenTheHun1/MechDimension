using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform point;
    public GameObject bullet;
    public float delay;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            canShoot = false;
            Instantiate(bullet, point.position, point.rotation);
            StartCoroutine("Cooldown");
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
