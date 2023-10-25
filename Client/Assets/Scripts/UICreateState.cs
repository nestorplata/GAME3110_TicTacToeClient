
using System.IO;
using UnityEngine;

public class UICreateState : UIBaseState
{
    public override void EnterState(UIStateManager manager)
    {
        EnumState = UIStates.create;
        tittleText = "CREATE ACCOUNT";
        ButtonText = "TO LOG ON ACCOUNT";


    }
    public override void UpdateState(UIStateManager manager)
    {

    }

    public override void Continue()
    {

    }

}
