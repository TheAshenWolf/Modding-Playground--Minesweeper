using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager Instance { get; set; }

    [Header("References")] 
    public GameObject startGameTab;
    public GameObject gameMap;
    public RectTransform playingFieldButtonsContainer;
    
    [Header("Prefabs")]
    public GameObject buttonPrefab;

    [Header("Icons")] 
    public Sprite cleanButtonIcon;
    public Sprite mineIcon;
    public Sprite mineIconRed;
    public Sprite flagIcon;
    public Sprite[] numberIcons;
    
    [Header("Data")] // These values would usually be all private; set to public for modding purposes
    public int rows = 9;
    public int columns = 9;
    public int mineCount = 10;
    public Field[][] playingField;


    public virtual void Awake()
    {
        // singleton constructor
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public virtual void StartGame(DifficultyEnum difficulty)
    {
        // Hide the menu tab
        startGameTab.SetActive(false);

        switch (difficulty)
        {
            case DifficultyEnum.Easy:
                // Start the game with easy difficulty
                rows = 9;
                columns = 9;
                mineCount = 10;
                break;
            case DifficultyEnum.Medium:
                // Start the game with medium difficulty
                rows = 16;
                columns = 16;
                mineCount = 40;
                break;
            case DifficultyEnum.Hard:
                // Start the game with hard difficulty
                rows = 16;
                columns = 30;
                mineCount = 99;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
        }
        
        GeneratePlayingField();
        DistributeMines();
        CalculateNumbers();
    }

    public virtual void OnRevealField(int x, int y)
    {
        Field field = playingField[x][y];
        if (field.isRevealed) return;
        field.isRevealed = true;
        UpdateFieldGraphic(x, y);
        if (field.isMine)
        {
            // TODO: Game over
            Debug.Log("Game over");
        }
        
    }
    
    public virtual void OnFlagField(int x, int y)
    {
        Field field = playingField[x][y];
        if (field.isRevealed) return;
        field.isFlagged = !field.isFlagged;
        UpdateFieldGraphic(x, y);
    }
    
    public virtual void UpdateFieldGraphic(int x, int y)
    {
        Field field = playingField[x][y];
        if (field.isRevealed)
        {
            field.graphic.sprite = field.isMine ? mineIconRed : numberIcons[field.adjacentMines];
        }
        else
        {
            field.graphic.sprite = field.isFlagged ? flagIcon : cleanButtonIcon;
        }
    }

    public virtual void GeneratePlayingField()
    {
        gameMap.SetActive(true);            
        // Generate the data layer
        playingField = new Field[rows][];
        for (int i = 0; i < rows; i++)
        {
            playingField[i] = new Field[columns];
            for (int j = 0; j < columns; j++)
            {
                Field field = GameObject.Instantiate(buttonPrefab, playingFieldButtonsContainer).GetComponent<Field>();
                field.isFlagged = false;
                field.isMine = false;
                field.isRevealed = false;
                field.adjacentMines = 0;
                playingField[i][j] = field;
                field.transform.position = new Vector3(j, i, 0);
                int x = i;
                int y = j;
                field.onReveal.AddListener(() => OnRevealField(x, y));
                field.onFlag.AddListener(() => OnFlagField(x, y));
            }
        }
        
        // Setup the container; +1 is to account for padding
        playingFieldButtonsContainer.sizeDelta = new Vector2((columns + 1) * 32, (rows + 1) * 32);
        
    }

    public virtual void DistributeMines()
    {
        if (mineCount > rows * columns) throw new Exception("Too many mines!");
        
        int remainingMines = mineCount;
        while (remainingMines > 0)
        {
            // Get a random position
            int x = UnityEngine.Random.Range(0, rows);
            int y = UnityEngine.Random.Range(0, columns);
            
            // Check if the field is already a mine
            if (playingField[x][y].isMine) continue;
            
            // Set the field to a mine
            playingField[x][y].isMine = true;
            remainingMines--;
        }
    }
    
    public virtual void CalculateNumbers()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                playingField[i][j].adjacentMines = GetMineCountInNeighbors(i, j);
            }
        }
    }
    

    public virtual int GetMineCountInNeighbors(int x, int y)
    {
        if (playingField[x][y].isMine) return -1;
        int mines = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // Check if the neighbor is within the bounds of the playing field
                if (x + i < 0 || x + i >= rows || y + j < 0 || y + j >= columns) continue;
                // Check if the neighbor is a mine
                if (playingField[x + i][y + j].isMine)
                {
                    mines++;
                }
            }
        }
        
        return mines;
    }
}