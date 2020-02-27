using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;

public class PlayerScores : MonoBehaviour
{
    public InputField emailText;
    public InputField usernameText;
    public InputField passwordText;
    User user = new User();

    private string databaseURL = "https://level-editor-b91ba.firebaseio.com/users"; // database URL is put into a varialble ot readability.
    private string AuthKey = "AIzaSyDzyCxYLT2Epm028KLMgu8ALUFJN4uqC5g";
    
    public static fsSerializer serializer = new fsSerializer();
      
    public static int playerScore;
    public static string playerName;
    public static string passWord;

    private string idToken;
    
    public static string localId;

    private string getLocalId;

    private void PostToDatabase()// Posts the user in the datatbase.
    {
        User user = new User();
 
        RestClient.Put(databaseURL + "/" + localId + passWord + ".json?auth=" + idToken, user);
    }

    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response =>
            {
                user = response;
            });
    }

    public void SignUpUserButton()// runs when sign up button is pressed.
    {
        SignUpUser(emailText.text, usernameText.text, passwordText.text);
    }
    
    public void SignInUserButton()// runs when sign in button is pressed.
    {
        SignInUser(emailText.text, passwordText.text);// only need email and password to sign in.
    }
    
    private void SignUpUser(string email, string username, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;// token is generated with each sign in.
                localId = response.localId;// specifically identitfies a user
                playerName = username;
                passWord = password; 
                PostToDatabase();
                
            }).Catch(error => // Executes if request fails.
        {
            Debug.Log(error);
        });
    }
    
    private void SignInUser(string email, string password)// This function will check if the user credentials are correct, throws error if false
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                GetUsername();
            }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

    private void GetUsername()
    {
        RestClient.Get<User>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            playerName = response.userName;
        });
    }

    
    private void GetLocalId(){
        RestClient.Get(databaseURL + ".json?auth=" + idToken).Then(response =>
        {
            var username = usernameText.text;
            
            fsData userData = fsJsonParser.Parse(response.Text);
            Dictionary<string, User> users = null;
            serializer.TryDeserialize(userData, ref users);

            foreach (var user in users.Values)
            {
                if (user.userName == username)
                {
                    getLocalId = user.localId;
                    RetrieveFromDatabase();
                    break;
                }
            }
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }
}
