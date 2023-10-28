
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UILoginState : UIBaseState
{
    public override void StartState(UIStateManager manager)
    {
        EnumState = UIStates.login;
        EnumStateToReturn = UIStates.create;
        EnumStateToContinue = UIStates.lobby;


        tittleText = "LOGIN";
        ButtonText = "TO MAKE ACCOUNT";
        TypeText = "Enter Password:";

    }
    public override void EnterState(UIStateManager manager)
    {
        foreach (GameObject garbage in manager.HidablesList)
        {
            garbage.SetActive(true);
        }


}
public override void OnContinue(UIStateManager manager)
    {

    }

    public override void OnReturn(UIStateManager manager)
    {
        



    }


}
