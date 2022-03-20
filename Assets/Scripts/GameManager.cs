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
    public GameObject policeCapturePuff;

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
        playerCarInitialPos = playerCar.transform.position;
        policeCar1InitialPos = policeCar1.transform.position;
        policeCar2InitialPos = policeCar2.transform.position;
        policeCar3InitialPos = policeCar3.transform.position;
        policeCar4InitialPos = policeCar4.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        moneyLeftObj = GameObject.FindGameObjectsWithTag(Constants.T_MONEY);
        moneyLeft = moneyLeftObj.Length;
        moneyLeftObj = GameObject.FindGameObjectsWithTag(Constants.T_POWERUP);
        Debug.Log("Rimangono " + moneyLeft + " banconote");

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
        livesImgs[lives-1].SetActive(false);

        if (lives == 0)
        {
            ShowPanelEndGame("Sei stato arrestato");
            return;
        }
        lives--;
    }

    private void HandlePoliceCaught(GameObject policeCar)
    {
        Vector3 posToUse = new Vector3();
        if (policeCar == policeCar1)
            posToUse = policeCar1InitialPos;
        if (policeCar == policeCar2)
            posToUse = policeCar2InitialPos;
        if (policeCar == policeCar3)
            posToUse = policeCar3InitialPos;
        if (policeCar == policeCar4)
            posToUse = policeCar4InitialPos;

        policeCapturePuff.SetActive(true);
        policeCapturePuff.transform.position = policeCar.transform.position;
        policeCapturePuff.GetComponent<ParticleSystem>().Play();
        policeCar.transform.position = posToUse;
    }

    private void ShowPanelEndGame(string msg)
    {
        Destroy(playerCar);

        msgEndGame.text = msg;

        panelGame.SetActive(false);
        panelEndGame.SetActive(true);        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Constants.S_1, LoadSceneMode.Single);
    }
}

//https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74
//https://hub.packtpub.com/creating-simple-gamemanager-using-unity3d/