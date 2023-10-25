
using System.IO;
using UnityEngine;

public class UICreateState : UIBaseState
{
    public override void EnterState(UIStateManager manager)
    {
        EnumState = UIStates.create;
        tittleText = "CREATE ACCOUNT";
        ButtonText = "TO LOG ON ACCOUNT";


    }
    public override void UpdateState(UIStateManager manager)
    {

    }

    public override void Continue()
    {
        Debug.Log(Name +',' + Password);
        string pathfile = "Accounts\\" + Name + ".txt";
        if (!File.Exists(pathfile))
        {
            using (StreamWriter sw = new StreamWriter(pathfile))
            {
                sw.WriteLine(Password);
            }
        }
        else
        {
            Debug.Log("Account Already Exists");
        }

    }

}
