using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBaseState
{
    public UIStates EnumState;
    public string tittleText;
    public string ButtonText;

    public string Name;
    public string Password;

    public List<GameObject> hidables;


    public abstract void EnterState(UIStateManager manager);
    //public abstract void EState(UIStateManager manager);

    public abstract void UpdateState(UIStateManager manager);

    public abstract void Continue();

    public string GetTittleText()
    {
        return tittleText;
    }
    public string GetButtonText()
    {
        return ButtonText;
    }
    public UIStates GetState()
    {
        return EnumState;

    }

    public void SetText(string name, string password)
    {
        Name = name;
        Password = password;
    }
    public void SetHidables (List<GameObject> gameObjects)
    {
        hidables = gameObjects;
    }

}
