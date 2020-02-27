using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string userName;
    public string localId;
    public string password; 
    
    public User()
    {
        userName = PlayerScores.playerName;
        password = PlayerScores.passWord; 
        localId = PlayerScores.localId;
    }
}
