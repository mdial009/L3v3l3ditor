using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void DisableBoolAnimator(Animator anime)
    {
        anime.SetBool("IsDisplayed", false);
    }
    public void EnableBoolAnimator(Animator anime)
    {
        anime.SetBool("IsDisplayed", true);
    }
    public void NavigateTo(int scene)
    {
        Application.LoadLevel(scene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
