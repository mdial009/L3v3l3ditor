﻿using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen; 
    public Slider slider; 
    public TMPro.TMP_Text progressText;
    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        
        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
                      
            slider.value = progress;
            
            progressText.text = progress * 100f + "%";
            
            yield return null;
        }
    }
}
