
using System.IO;
using UnityEngine;

public class UICreateState : UIBaseState
{
    public override void StartState(UIStateManager manager)
    {
        EnumState = UIStates.create;
        EnumStateToChange = UIStates.login;

        tittleText = "CREATE ACCOUNT";
        ButtonText = "TO LOG ON ACCOUNT";
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
