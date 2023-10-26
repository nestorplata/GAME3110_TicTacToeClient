using System.IO;
using UnityEngine;

public class UILobbyState : UIBaseState
{
    public override void StartState(UIStateManager manager)
    {
        EnumState = UIStates.lobby;
        EnumStateToChange = UIStates.login;

        tittleText = "JOIN/CREATE THE LOBBY";
        ButtonText = "LOG OFF";
        TypeText = "Enter lobby ID:";

    }
    // Start is called before the first frame update
    public override void EnterState(UIStateManager manager)
    {
        foreach (GameObject garbage in manager.HidablesList)
        {
            garbage.SetActive(false);
        }

    }
    public override void UpdateState(UIStateManager manager)
    {

    }

    public override void Continue(UIStateManager manager)
    {




    }
}
