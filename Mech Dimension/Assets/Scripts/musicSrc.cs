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
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void playHubTheme()
    {
        gameObject.GetComponent<AudioSource>().clip = hubTheme;
        gameObject.GetComponent<AudioSource>().Play();
    }


    public void playSiFiTheme()
    {
        gameObject.GetComponent<AudioSource>().clip = SiFiTheme;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void playForestTheme()
    {
        gameObject.GetComponent<AudioSource>().clip = forestTheme;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void playBossTheme()
    {
        gameObject.GetComponent<AudioSource>().clip = badGuyTheme;
        gameObject.GetComponent<AudioSource>().Play();
    }


}
