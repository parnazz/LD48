using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogCounterDataHolder 
{
    private static string endingType;

    public static string Type
    {
        get
        {
            return endingType;
        }
        set
        {
            endingType = value;
        }
    }

}
