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

    //ref to UI
    //public UnityEngine.UI.Text scorePoints;
    public TMP_Text scorePoints;
    public TMP_Text msgEndGame;
    public GameObject[] livesImgs;
    public GameObject panelEndGame;
    public GameObject panelGame;
    public GameObject player;

    //ref to PlayerStats
    [SerializeField] private int score = 0; 
    [SerializeField] private int lives = 3;

    //ref to game
    [SerializeField] GameObject[] moneyLeftObj;
    [SerializeField] int moneyLeft;

    //ref to objs
    public GameObject playerCar;
    [SerializeField] Vector3 playerCarInitialPos;
    public GameObject policeCar1;
    [SerializeField] Vector3 policeCar1InitialPos;
    public GameObject policeCar2;
    [SerializeField] Vector3 policeCar2InitialPos;
    public GameObject policeCar3;
    [SerializeField] Vector3 policeCar3InitialPos;
    public GameObject policeCar4;
    [SerializeField] Vector3 policeCar4InitialPos;

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
        moneyLeftObj = GameObject.FindGameObjectsWithTag("Point");
        moneyLeft = moneyLeftObj.Length;
        Debug.Log("Rimangono " + moneyLeft + " banconote");

        playerCarInitialPos = playerCar.transform.position;
        policeCar1InitialPos = policeCar1.transform.position;
        policeCar2InitialPos = policeCar2.transform.position;
        policeCar3InitialPos = policeCar3.transform.position;
        policeCar4InitialPos = policeCar4.transform.position;

        //EVENTS
        EventManager.Points += ReceivePoints;
        EventManager.PoliceCaught += HandlePoliceCaught;
        EventManager.ArrestedPlayer += HandleArrestedPlayer;
    }

    private void ReceivePoints(GameObject obj, int pts)
    {
        score += pts;
        scorePoints.text = score.ToString();

        if(obj.tag == Constants.T_MONEY)
            moneyLeft--;

        if (moneyLeft == 0)
            ShowPanelEndGame("Sei scampato alla cattura");

        if (obj.tag != Constants.T_ENEMY)
            obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //GAMEPLAY FUNCTION
    private void HandleArrestedPlayer()
    {
        lives--;
        livesImgs[lives].SetActive(false);
        if (lives == 0)
        {
            ShowPanelEndGame("Sei stato arrestato");
        }
    }

    private void HandlePoliceCaught(GameObject policeCar)
    {
        if(policeCar == policeCar1)
            policeCar1.transform.position = policeCar1InitialPos;
        if (policeCar == policeCar2)
            policeCar2.transform.position = policeCar2InitialPos;
        if (policeCar == policeCar3)
            policeCar3.transform.position = policeCar3InitialPos;
        if (policeCar == policeCar4)
            policeCar4.transform.position = policeCar4InitialPos;
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

        RestorePosition();
    }

    public void RestorePosition()
    {
        playerCar.transform.position = playerCarInitialPos;
        policeCar1.transform.position = policeCar1InitialPos;
        policeCar2.transform.position = policeCar2InitialPos;
        policeCar3.transform.position = policeCar3InitialPos;
        policeCar4.transform.position = policeCar4InitialPos;
    }
}

//https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74
//https://hub.packtpub.com/creating-simple-gamemanager-using-unity3d/