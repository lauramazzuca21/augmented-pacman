using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState { get; private set; }

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("GameManager is null!");
            }

            return _instance;
        }
    }

    //ref to UI
    //public UnityEngine.UI.Text scorePoints;
    public TMP_Text scorePoints;
    public TMP_Text msgEndGame;
    public GameObject[] livesImgs;
    public GameObject panelEndGame;
    public GameObject panelGame;

    //ref to PlayerStats
    [SerializeField] private int score = 0; 
    [SerializeField] private int lives = 3;

    public bool x = false;
    public bool modSharkCarActive = false;

    public void SetGameState(GameState state)
    {
        GameState = state;
        //OnStateChange();
    }

    public void OnApplicationQuit()
    {
        GameManager._instance = null;
    }
    //ref to interactions

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Points += ReceivePoints;
    }

    private void ReceivePoints(GameObject obj, int pts)
    {
        score += pts;
        scorePoints.text = score.ToString();
        Destroy(obj);
    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            Debug.Log("hai perso!");
            lives--;

            ShowPanelEndGame("Sei stato arrestato");

        }

        if (score == 100)
        {
            Debug.Log("hai vinto!");
            lives--;

            ShowPanelEndGame("Sei scampato alla cattura");

        }

        //test
        if (x)
        {
            x = false;
            ArrestedPlayer();
        }

        if (modSharkCarActive)
        {
            StartCoroutine(ResetSharkMod());
        }
    }
    IEnumerator ResetSharkMod()
    {
        yield return new WaitForSeconds(15f);
        modSharkCarActive = false;
    }

    //GAMEPLAY FUNCTION
    public void ArrestedPlayer()
    {
        lives--;
        livesImgs[lives].SetActive(false);
    }

    private void ShowPanelEndGame(string msg)
    {
        msgEndGame.text = msg;

        panelGame.SetActive(false);
        panelEndGame.SetActive(true);        
    }

    public void RestartGame()
    {
        //player stats
        lives = 3;
        score = 0;

        //UI
        scorePoints.text = "0";
        msgEndGame.text = "";
        foreach (GameObject img in livesImgs) { img.SetActive(true); }

        //panels
        panelGame.SetActive(true);
        panelEndGame.SetActive(false);

        //ref to status
        modSharkCarActive = false;
    }
}

//https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74
//https://hub.packtpub.com/creating-simple-gamemanager-using-unity3d/