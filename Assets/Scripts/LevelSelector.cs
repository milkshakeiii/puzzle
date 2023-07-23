using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public bool dontDestroyOnLoad = true;

    // Start is called before the first frame update
    void Start()
    {
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this);
    }

    public void EnableChild(int childNumber)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == childNumber);
        }
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
