
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UILoginState : UIBaseState
{
    public override void StartState(UIStateManager manager)
    {
        EnumState = UIStates.login;
        EnumStateToChange = UIStates.create;

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
public override void UpdateState(UIStateManager manager)
    {

    }

    public override void Continue(UIStateManager manager)
    {
        



    }


}
