
using System.IO;
using UnityEngine;

public class UICreateState : UIBaseState
{
    public override void StartState()
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
        foreach (GameObject garbage in manager.HidablesList)
        {
            garbage.SetActive(true);
        }
    }
    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.create+ ","+ClientMessageType.OnContinue
            + "," + manager.Input1.text + "_" + manager.Input2.text;
        manager.SendMessageToServer(message);
    }

    public override void OnReturn(UIStateManager manager)
    {
        manager.ChangeState(EnumStateToContinue);
    }

    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        switch (int.Parse(msg))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = "Account Created";
                manager.ChangeState(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.BasicFailure:
                message = "Wrong Username";
                break;

        }
        return message;

    }
}
