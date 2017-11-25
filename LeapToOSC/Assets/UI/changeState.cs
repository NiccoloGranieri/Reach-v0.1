using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeState : MonoBehaviour {

    public static bool Toggle = true;

	public void Change()
    {
        if (Toggle == false)
            Toggle = true;
        else Toggle = false;
    }
}
