using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game States
public enum GameState
{
    MAIN_MENU,
    MAP1,
    MAP2,
    MAP3
}

public class GameManager : MonoBehaviour
{
    

    public GameState gameState { get; private set; }

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
    public GameObject scorePoints;
    public GameObject[] livesImgs;

    //ref to PlayerStats
    [SerializeField] private int score = 0; 
    [SerializeField] private int lives = 3;

    public bool x = false;

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        //OnStateChange();
    }

    public void OnApplicationQuit()
    {
        GameManager._instance = null;
    }


    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lives == 0)
        {
            Debug.Log("hai perso!");
            //menu di restart e funzione restart
        }

        //test
        if (x)
        {
            x = false;
            ArrestedPlayer();
        }
    }

    //GAMEPLAY FUNCTION
    public void ArrestedPlayer()
    {
        lives--;
        livesImgs[lives].SetActive(false);
    }
}

//https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74
//https://hub.packtpub.com/creating-simple-gamemanager-using-unity3d/