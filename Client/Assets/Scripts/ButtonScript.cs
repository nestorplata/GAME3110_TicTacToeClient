using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonScript : MonoBehaviour
{
     public UIStateManager stateManager;

    public void OnClickReturn()
    {
        stateManager.OnReturn();

    }

    public void OnClickContinue()
    {
        stateManager.OnContinue();

    }

    public void OnClickSpecial()
    {
        stateManager.OnSpecial();

    }



}
