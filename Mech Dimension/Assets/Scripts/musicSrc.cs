using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class musicSrc : MonoBehaviour
{
    public AudioClip iceTheme;
    public AudioClip SiFiTheme;
    public AudioClip hubTheme;
    public AudioClip forestTheme;

    public AudioClip BadGuyIntro;
    public AudioClip badGuyTheme;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            playIceTheme();
        }


    }


    public void playIceTheme()
    {
        gameObject.GetComponent<AudioSource>().clip = iceTheme;
        //gameObject.GetComponent<AudioSource>().Play
    }

    public void playHubTheme()
    {

    }


    public void playSiFiTheme()
    {

    }

    public void playForestTheme()
    {

    }

    public void playBossTheme()
    {

    }


}
