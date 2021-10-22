using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePit : MonoBehaviour
{
    Fuel fp;

    // Start is called before the first frame update
    void Start()
    {

        fp = GameObject.Find("Fuel Gauge").GetComponent<Fuel>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Player"))
        {
            fp.fuel = 0;
        }
    }
}
