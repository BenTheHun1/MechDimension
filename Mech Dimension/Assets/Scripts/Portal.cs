using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum typePortal
    {
        cold, hot, dark
    }

    public typePortal thisPortal;
    public BoxCollider2D sideLeft;
    public BoxCollider2D sideRight;

    void Start()
    {
        
        if (thisPortal == typePortal.cold)
        {
            sideLeft.gameObject.tag = "ToggleNormal";
            sideRight.gameObject.tag = "ToggleCold";
        }
        if (thisPortal == typePortal.hot)
        {
            sideLeft.gameObject.tag = "ToggleHeat";
            sideRight.gameObject.tag = "ToggleNormal";
        }
        if (thisPortal == typePortal.dark) //Rotate Clockwise for this to work
        {
            sideLeft.gameObject.tag = "ToggleLight";
            sideRight.gameObject.tag = "ToggleDarkness";
        }
    }
}