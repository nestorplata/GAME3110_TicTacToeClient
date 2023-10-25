
using System.IO;
using UnityEngine;

public class UILoginState : UIBaseState
{
    public override void EnterState(UIStateManager manager)
    {
        EnumState = UIStates.login;
        tittleText = "LOGIN";
        ButtonText = "TO MAKE ACCOUNT";

    }
    public override void UpdateState(UIStateManager manager)
    {

    }

    public override void Continue()
    {
        



    }


}
