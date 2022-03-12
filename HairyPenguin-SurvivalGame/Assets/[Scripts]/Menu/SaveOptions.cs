using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveOptions : MonoBehaviour
{
    public static bool isInverted = false;
    public static bool isLoadSelected;

    

    public void setLoading(bool loading)
    {
        isLoadSelected = loading;
    }
    public bool getLoading()
    {
        return isLoadSelected;

    }
    public bool getInverted()
    {
        return isInverted;

    }
    public void setInverted(bool inverted)
    {
        isInverted = inverted;
    }

}
