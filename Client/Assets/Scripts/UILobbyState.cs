using System.IO;
using UnityEngine;

public class UILobbyState : UIBaseState
{
    // Start is called before the first frame update
    public override void EnterState(UIStateManager manager)
    {
        EnumState = UIStates.lobby;
        tittleText = "JOIN THE LOBBY";
        ButtonText = "LOG OFF";

    }
    public override void UpdateState(UIStateManager manager)
    {
        foreach(GameObject garbage in hidables)
        {
            garbage.SetActive(false);
        }
    }

    public override void Continue()
    {




    }
}
