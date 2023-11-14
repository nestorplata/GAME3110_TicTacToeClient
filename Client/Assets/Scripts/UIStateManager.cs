using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public enum UIStates
{
    login,
    create,
    lobby,
    room,
    game
}

public class UIStateManager : MonoBehaviour
{
    public List<GameObject> HidablesList = new List<GameObject>();

    List<UIBaseState> stateList = new List<UIBaseState>();
    public UIBaseState currentState;
    UILoginState loginState = new UILoginState();
    UICreateState createState = new UICreateState();
    UILobbyState lobbyState = new UILobbyState();
    UIRoomState RoomState = new UIRoomState();
    UIGameState GameState = new UIGameState();

    public Button ContinueButton;
    public InputField Input1;
    public InputField Input2;

    public Text ButtonText;
    public Text TittleText;
    public Text TypeText;

    void Start()
    {
        NetworkClientProcessing.SetStateManager(this);

        stateList.Add(loginState);
        stateList.Add(createState);
        stateList.Add(lobbyState);
        stateList.Add(RoomState);
        stateList.Add(GameState);
        foreach (UIBaseState state in stateList)
        {
            state.StartState();
        }
        currentState = stateList[0];

        Input1 = transform.Find("username").GetComponent<InputField>();
        Input2 =transform.Find("password").GetComponent<InputField>();

    }
    
    public void OnReturn()
    {
        currentState.OnReturn(this);
    }

    public void ChangeState(UIStates state)
    {
        if (currentState.EnumState != currentState.EnumStateToReturn)
        {
            foreach (var UI in stateList)
            {
                if (state == UI.EnumState)
                {

                    currentState = UI;
                    break;
                }
            }
            currentState.EnterState(this);

        }

        ButtonText.text = currentState.ButtonText;
        TittleText.text = currentState.tittleText;
        TypeText.text = currentState.TypeText;
    }



    public void OnContinue()
    {

        char[] characters = Input1.text.ToCharArray();
        char[] InvalidCharacters = "/\\?%*:|\"<>".ToCharArray();

        if (Input1.text != "")
        {
            foreach (char c in characters)
            {
                foreach (char I in InvalidCharacters)
                {
                    if (c == I)
                    {
                        Debug.Log("\"" + c + "\"" + " Invalid character");
                        return;
                    }
                }
            }
            if (Input2.text != "")
            {
                currentState.OnContinue(this);
            }
            else
            {
                Debug.Log("No password introduced");

            }
        }
        else
        {
            Debug.Log("No username introduced");
        }
        
    }

    public void SendMessageToServer(string msg)
    {
        NetworkClientProcessing.SendMessageToServer(msg, TransportPipeline.ReliableAndInOrder);

    }
}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int none = 0;
    public const int login = 1;
    public const int create = 2;
    public const int lobby = 3;
    public const int room = 4;
    public const int game = 5;
}

static public class ClientMessageType
{
    public const int OnContinue = 1;
    public const int OnReturn = 2;
    public const int OnSpecial = 3;
}

static public class ServerToClientSignifiers
{
    public const int BasicSuccess = 0;
    public const int SuccessA = 1;
    public const int ReturnSuccess = 2;
    public const int BasicFailure = 4;
    public const int FailureA = 5;
    public const int FailureB = 6;
}
#endregion



