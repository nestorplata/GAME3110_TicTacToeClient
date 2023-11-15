
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class UICreateState : UIBaseState
{
    public override void StartState()
    {
        EnumState = UIStates.create;
        EnumStateToReturn = UIStates.login;
        EnumStateToContinue = UIStates.lobby;

    }

    public override void EnterState(UIStateManager manager)
    {
        manager.HidablesList[0].SetActive(true);
        manager.HidablesList[1].SetActive(false);

        manager.TittleText.text = "CREATE ACCOUNT";
        manager.GetButtonText(ClientMessageType.OnReturn).text = "TO LOG ON ACCOUNT";
        manager.GetDescriptionText(InputNumber.Input1).text = "Enter Password:";
    }
    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.create+ ","+ClientMessageType.OnContinue
            + "," + manager.GetInputFieldText(InputNumber.Input1).text
            + "_" + manager.GetInputFieldText(InputNumber.Input2).text;
        manager.SendMessageToServer(message);
    }

    public override void OnReturn(UIStateManager manager)
    {
        manager.ChangeStateTo(EnumStateToReturn);
    }

    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        switch (int.Parse(msg))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = "Account Created";
                manager.ChangeStateTo(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.BasicFailure:
                message = "Wrong Username";
                break;

        }
        return message;

    }
}
