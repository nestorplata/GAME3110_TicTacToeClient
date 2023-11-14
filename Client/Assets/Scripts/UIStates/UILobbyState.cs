using System.IO;
using UnityEngine;

public class UILobbyState : UIBaseState
{
    public override void StartState()
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
        manager.Input1.enabled = true;
        manager.Input2.enabled = true;
        manager.ContinueButton.enabled = true;
        foreach (GameObject garbage in manager.HidablesList)
        {
            garbage.SetActive(false);
        }


    }

    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.lobby + "," + ClientMessageType.OnContinue
            + "," + manager.Input2.text;
        manager.SendMessageToServer(message);

    }

    public override void OnReturn(UIStateManager manager)
    {
        message = ClientToServerSignifiers.lobby + "," + ClientMessageType.OnReturn;
        manager.SendMessageToServer(message);

    }

    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        switch (int.Parse(msg))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = "Gameroom Created";
                manager.ChangeState(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.SuccessA:
                message = "Gameroom Created";
                manager.ChangeState(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.ReturnSuccess:
                message = "Logged Off";
                manager.ChangeState(EnumStateToReturn);
                break;
        }
        return message;

    }
}
