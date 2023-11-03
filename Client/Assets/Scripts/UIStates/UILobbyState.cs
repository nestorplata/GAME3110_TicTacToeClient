using System.IO;
using UnityEngine;

public class UILobbyState : UIBaseState
{
    public override void StartState(UIStateManager manager)
    {
        EnumState = UIStates.lobby;
        EnumStateToReturn = UIStates.login;
        EnumStateToContinue = UIStates.room;

        tittleText = "JOIN/CREATE THE LOBBY";
        ButtonText = "LOG OFF";
        TypeText = "Enter lobby ID:";

    }
    // Start is called before the first frame update
    public override void EnterState(UIStateManager manager)
    {
        manager.InputUserName.enabled = true;
        manager.InputPassword.enabled = true;
        manager.ContinueButton.enabled = true;
        foreach (GameObject garbage in manager.HidablesList)
        {
            garbage.SetActive(false);
        }


    }


    public override void OnReturn(UIStateManager manager)
    {
        manager.networkClient.SendMessageToServer(manager.currentState.EnumState.ToString() +
            ',' + manager.InputUserName.text + ',' + manager.InputPassword.text + ",1"
               , TransportPipeline.ReliableAndInOrder);

    }
}
