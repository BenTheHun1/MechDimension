using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSystem : MonoBehaviour
{
    public float mechTempature = 50;
    public bool mechIsInColdArea;
    public bool mechIsInHotArea;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mechIsInColdArea)
        {
            mechTempature -= Time.deltaTime;            //* modifier if needed
        } else if (mechIsInHotArea)
        {
            //mechIsInColdArea += Time.deltaTime;
        }







        if(mechTempature >= 75)
        {
            tempIsTooHot();
        } else if (mechTempature <= 25)
        {
            tempIsTooCold();
        } else
        {
            //back to normal
        }
    }



    void tempIsTooCold()
    {

    }

    void tempIsTooHot()
    {

    }





    void updateTempatureDisplay()
    {

    }


}
