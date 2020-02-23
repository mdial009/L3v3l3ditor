using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SignResponse
{
    public string localId;// specifically identitfies a user
    public string idToken;// token is generated with each sign in.
}
