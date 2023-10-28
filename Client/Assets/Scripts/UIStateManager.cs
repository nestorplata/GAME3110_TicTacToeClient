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

    public NetworkClient networkClient;

    public UIBaseState currentState;
    UILoginState loginState = new UILoginState();
    UICreateState createState = new UICreateState();
    UILobbyState lobbyState = new UILobbyState();
    UIRoomState RoomState = new UIRoomState();
    UIGameState GameState = new UIGameState();

    public Button ContinueButton;
    public InputField InputUserName;
    public InputField InputPassword;

    public Text ButtonText;
    public Text TittleText;
    public Text TypeText;

    void Start()
    {
        networkClient = GameObject.Find("EventSystem").GetComponent<NetworkClient>();

        stateList.Add(loginState);
        stateList.Add(createState);
        stateList.Add(lobbyState);
        stateList.Add(RoomState);
        stateList.Add(GameState);
            foreach(UIBaseState state in stateList)
        {
            state.StartState(this);
        }


        currentState = stateList[0];

        InputUserName = transform.Find("username").GetComponent<InputField>();
        InputPassword =transform.Find("password").GetComponent<InputField>();

    }

    // Update is called once per frame


    public void OnReturn()
    {
        currentState.OnReturn(this);

        ChangeState(currentState.EnumStateToReturn);


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

        char[] characters = InputUserName.text.ToCharArray();
        string InvalidString = "/\\?%*:|\"<>";
        char[] Invalid = InvalidString.ToCharArray();
 
        if (InputUserName.text!="")
        {
            foreach (char c in characters)
            {
                foreach (char I in Invalid)
                {
                    if(c==I)
                    {
                        Debug.Log("\""+c+"\"" + " Invalid character");
                        return;
                    }
                }
            }
            if(InputPassword.text != "")
            {
                string message = currentState.EnumState.ToString()  +','+
                    InputUserName.text + ',' + InputPassword.text + ",0,"+
                    networkClient.GetNetworkConnectionID();

                networkClient.SendMessageToServer(message);
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
        CheckForResponse(0.5f);
    }

    public void CheckForResponse(float time)
    {
        StartCoroutine(WaitForServerResponse(time));

    }
    IEnumerator WaitForServerResponse(float time)
    {
        yield return new WaitForSeconds(time);
        string[] message = networkClient.GetMesssage().Split(',');

        if (message[0] == "success")
        {
            //currentState.OnContinue(this);g
            ChangeState(currentState.EnumStateToContinue);
        }


    }


}



