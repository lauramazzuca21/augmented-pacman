using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{    public void Start()
    {
        SceneManager.LoadScene(Constants.S_1, LoadSceneMode.Single);
    }
}
