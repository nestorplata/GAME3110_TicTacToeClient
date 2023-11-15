using System.IO;
using UnityEngine;

public class UILobbyState : UIBaseState
{
    public override void StartState()
    {
        EnumState = UIStates.lobby;
        EnumStateToReturn = UIStates.login;
        EnumStateToContinue = UIStates.room;
    }
    // Start is called before the first frame update

    public override void EnterState(UIStateManager manager)
    {
        manager.GetInputFieldComponent(InputNumber.Input1).enabled = true;
        manager.GetInputFieldComponent(InputNumber.Input2).enabled = true;
        manager.GetButtonComponent(ClientMessageType.OnContinue).enabled = true;

        manager.InputHolder1.SetActive(false);
        manager.Buttons[ClientMessageType.OnSpecial].SetActive(false);

        manager.TittleText.text = "JOIN/CREATE THE LOBBY";
        manager.GetButtonText(ClientMessageType.OnReturn).text = "LOG OFF";
        manager.GetDescriptionText(InputNumber.Input2).text = "Enter lobby ID:";
        manager.GetButtonText(ClientMessageType.OnContinue).text = "JOIN/CREATE";
    }

    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.lobby + "," + ClientMessageType.OnContinue
            + "," + manager.GetInputFieldText(InputNumber.Input2).text;
        manager.SendMessageToServer(message);

    }

    public override void OnReturn(UIStateManager manager)
    {
        message = ClientToServerSignifiers.lobby + "," + ClientMessageType.OnReturn;
        manager.SendMessageToServer(message);

    }
    public override void OnSpecial(UIStateManager manager)
    {

    }
    public override void MessageRecieved(UIStateManager manager, string[] msg)
    {
        switch (int.Parse(msg[0]))
        {
            case ServerToClientSignifiers.ContinueSuccess:
                message = "Gameroom Created";
                manager.ChangeStateTo(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.ReturnSuccess:
                message = "Logged Off";
                manager.ChangeStateTo(EnumStateToReturn);
                break;
        }


    }
}
