using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.VersionControl.Asset;

public enum UIStates
{
    login,
    create
}

public class UIStateManager : MonoBehaviour
{
    NetworkClient networkClient;

    UIBaseState currentState;
    UILoginState loginState = new UILoginState();
    UICreateState createState = new UICreateState();
    List<UIBaseState> stateList = new List<UIBaseState>();

    InputField InputUserName;
    InputField InputPassword;

    Text currentScreen;

    void Start()
    {
        networkClient = GameObject.Find("EventSystem").GetComponent<NetworkClient>();

        loginState.EnterState(this);
        createState.EnterState(this);

        stateList.Add(loginState);
        stateList.Add(createState);

        currentState = stateList[0];
        currentScreen = transform.GetChild(0).GetComponent<Text>();

        InputUserName = transform.Find("username").GetComponent<InputField>();
        InputPassword =transform.Find("password").GetComponent<InputField>();

    }

    // Update is called once per frame
    void Update()
    {
        //currentState.UpdateState(this);

    }

    public void SetState(UIStates state)
    {
        foreach(var UI in stateList)
        {
            if (UI.GetState()==state)
            {
                currentState = UI;
                break;
            }
        }
        currentScreen.text = currentState.GetTittleText();

    }

    public UIBaseState GetState()
    {
        return currentState;
    }

    public void Continue()
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
                string message = currentState.GetState().ToString() + ',' +
                    InputUserName.text + ',' + InputPassword.text + ',' +
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
        //currentState.Continue();
    }



}



