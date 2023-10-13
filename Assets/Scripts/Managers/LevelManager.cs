using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class LevelManager : MonoBehaviour
{ 
    /*
    Level starts out with penguins attacking an ice castle
    Basically tower defence

    Level 1 has 5 penguins, 1 penguin appearing every 5 seconds,  (number of penguins increases by 5. Appears total number of penguins / 5) 
    Level 2 has 10 penguins, 2 penguins every 5 seconds, 
    Level 3 has 15 penguins, 3 penguins appear every 5 seconds 
    Level 4 has 20, 4 penguins appear every 5 seconds
    Level 5 has 25, 5 penguins appear every 5 seconds

    And goes on until player ends game through pause menu or if the castle is destroyed

    

    */ 
    public static LevelManager Instance { get; private set; }

    private int currentLevel;
    private float currentTime = 0;
   

    [System.Serializable]
    public class LevelData
    {
        public int enemyNumber; 
    }

    public LevelData[] levels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        GameManager.Instance.SwitchState(GameState.GAME);
    }

    private void Update()
    {
        if (GameManager.Instance.GetGameState() != GameState.GAME) return; 

        if (PlayerWinsLevel())
        {
            GameManager.Instance.SwitchState(GameState.WIN);
            return;
        }

        else
        {
            currentTime += Time.deltaTime;
        }
    }


    private void LoadLevel(int levelIndex)
    {
        // 
    }

    private bool PlayerWinsLevel()
    {
        return false;
    }

    public LevelData GetCurrentLevelData()
    {
        return levels[currentLevel];
    }

    public int GetCurrentLevel()
    {
        return 0; 
    }
}