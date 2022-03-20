using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //piano B
    public TMP_Text timerText;
    public TMP_Text timeArrestedText;
    public float timer = 0;
    public int timeArrested = 0;

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

    //ref to game
    [SerializeField] GameObject[] moneyObj;
    [SerializeField] GameObject[] powerupsObj;
    [SerializeField] int moneyLeft;

    //ref to objs
    public GameObject playerCar;
    public GameObject policeCar1;
    public GameObject policeCar2;
    public GameObject policeCar3;
    public GameObject policeCar4;

    //particles
    public GameObject policeCapturePuff;
    public GameObject fireworks;

    public bool infinityMode = false;

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
        moneyObj = GameObject.FindGameObjectsWithTag(Constants.T_MONEY);
        moneyLeft = moneyObj.Length;
        powerupsObj = GameObject.FindGameObjectsWithTag(Constants.T_POWERUP);
        //EVENTS
        EventManager.Points += ReceivePoints;
        EventManager.PoliceCaught += HandlePoliceCaught;
        EventManager.ArrestedPlayer += HandleArrestedPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.RoundToInt(timer % 60);
        string min = minutes.ToString();
        string sec = seconds.ToString();

        if (minutes < 10)
            min = "0" + minutes;
        if (seconds < 10)
            sec = "0" + seconds;

        timerText.text = min + ":" + sec;
    }

    private void ReceivePoints(GameObject obj, int pts)
    {
        score += pts;
        scorePoints.text = score.ToString();

        if (!infinityMode)
        {
            if(obj.tag == Constants.T_MONEY)
                moneyLeft--;

            if (moneyLeft == 0)
            {
                fireworks.SetActive(true);
                ShowPanelEndGame("You've successfully escaped!");
            }
        }
        else
        {
            StartCoroutine(RestoreItem(obj));
        }

        if (obj.tag != Constants.T_ENEMY)
        {
            obj.SetActive(false);
        }
            
    }

    IEnumerator RestoreItem(GameObject obj)
    {
        yield return new WaitForSeconds(10f);
        obj.SetActive(true);
    }

    //GAMEPLAY FUNCTION
    private void HandleArrestedPlayer()
    {
        if (!infinityMode)
        {
            if (lives == 0)
                return;

            livesImgs[lives-1].SetActive(false);

            lives--;
            if (lives == 0)
            {
                ShowPanelEndGame("You've been arrested!");
                return;
            }
        }
        else
        {
            timeArrested++;
            timeArrestedText.text = timeArrested.ToString();
        }
    }

    private void HandlePoliceCaught(GameObject policeCar)
    {
        ResetSettings toReset = ResetSettings.ENEMY_1;
        if (policeCar == policeCar1)
            toReset = ResetSettings.ENEMY_1;
        if (policeCar == policeCar2)
            toReset = ResetSettings.ENEMY_2;
        if (policeCar == policeCar3)
            toReset = ResetSettings.ENEMY_3;
        if (policeCar == policeCar4)
            toReset = ResetSettings.ENEMY_4;

        policeCapturePuff.SetActive(true);
        policeCapturePuff.transform.position = policeCar.transform.position;
        policeCapturePuff.GetComponent<ParticleSystem>().Play();
        EventManager.FireResetEvent(toReset);
    }

    private void ShowPanelEndGame(string msg)
    {
        playerCar.SetActive(false);
        policeCar1.SetActive(false);
        policeCar2.SetActive(false);
        policeCar3.SetActive(false);
        policeCar4.SetActive(false);
        msgEndGame.text = msg;

        panelGame.SetActive(false);
        panelEndGame.SetActive(true);        
    }

    public void RestartGame()
    {
        //StartCoroutine(Reload());
        //SceneManager.LoadScene(Constants.S_Loading, LoadSceneMode.Single);
        score = 0;
        lives = 3;
        moneyLeft = moneyObj.Length;

        foreach (GameObject m in moneyObj) { m.SetActive(true); }
        foreach (GameObject m in powerupsObj) { m.SetActive(true); }
        foreach (GameObject m in livesImgs) { m.SetActive(true); }
        policeCapturePuff.SetActive(false);
        fireworks.SetActive(false);
        panelEndGame.SetActive(false);

        panelGame.SetActive(true);
        playerCar.SetActive(true);
        policeCar1.SetActive(true);
        policeCar2.SetActive(true);
        policeCar3.SetActive(true);
        policeCar4.SetActive(true);

        EventManager.FireResetEvent(ResetSettings.ALL);
    }
}

//https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74
//https://hub.packtpub.com/creating-simple-gamemanager-using-unity3d/