using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RememberMe : MonoBehaviour {

//reference for InputField, needs to be assigned in editor
public TMPro.TMP_InputField login;
string loginText;
//reference for Toggle, needs to be assigned in editor
public Toggle rememberToggle;
bool remeberIsOn;

// Use this for initialization
void Start ()
{
    loginText = "";
    FindLogin();
}
public void FindLogin()
{
    if (File.Exists("userSetting.txt"))
    {
        using (TextReader reader = File.OpenText("userSetting.txt"))
        {
            string str;
            if ((str = reader.ReadLine()) != null) login.text = str;
        }
        //Debug.Log(loginText);
    }
}
public void remember()
{
    using (TextWriter writer = File.CreateText("userSetting.txt"))
    {
        writer.WriteLine(login.text);
    }
}
public void loginClick()
{
    remeberIsOn = rememberToggle.isOn;
    loginText = login.text;
    if (remeberIsOn)
    {
        remember();
    }
    else
    {
        using (TextWriter writer = File.CreateText("userSetting.txt"))
        {
            writer.WriteLine("");
        }
    }
}
}
