using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void GotoMainMenu(){
        SceneManager.LoadScene(0);
    }
    public void GoToRegister(){
        SceneManager.LoadScene(1);
    }
    public void GoToLogin(){
        SceneManager.LoadScene(2);
    }
    public void GoToGame(){
        SceneManager.LoadScene(3);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
