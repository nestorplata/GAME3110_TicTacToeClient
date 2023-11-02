
using System.IO;
using UnityEngine;

public class UICreateState : UIBaseState
{
    public override void StartState(UIStateManager manager)
    {
        EnumState = UIStates.create;
        EnumStateToReturn = UIStates.login;
        EnumStateToContinue = UIStates.lobby;


        tittleText = "CREATE ACCOUNT";
        ButtonText = "TO LOG ON ACCOUNT";
        TypeText = "Enter Password:";


    }

    public override void EnterState(UIStateManager manager)
    {

    }

    public override void OnReturn(UIStateManager manager)
    {

    }

}
