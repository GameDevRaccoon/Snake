using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    int score, highScore;
    public GameObject boardPart;
    GameObject[,] board = new GameObject[50, 50];
    Renderer[,] boardRenders = new Renderer[50, 50];
    Player player_ = new Player();
    Color standardColor = Color.grey;
    Color fruitColour = Color.yellow;
    Color wallColour = Color.black;
    Vector2 fruitLocation = new Vector2(15, 20);
    bool initialised = false;
    bool gameOver = false;

    public bool GameOver
    {
        get { return gameOver; }
    }

    /// <summary>
    /// Public accessor for score, useful for displaying the score.
    /// </summary>
    public int Score
    {
        get { return score; }
    }

    /// <summary>
    /// Adds the grid of gameobjects to a 2D array for easier access.
    /// Caches each gameobjects renderer for quicker access later.
    /// </summary>
    void GenerateBoard()
    {
        for (int x = 0; x < 25; x++)
            for (int y = 0; y < 25; y++)
            {
                board[x, y] = boardPart.transform.FindChild("Z" + y).GetChild(x).gameObject;
                boardRenders[x, y] = board[x, y].GetComponent<Renderer>();
            }
    }

    /// <summary>
    /// Starts (and restarts) the game.
    /// Invokes the update player method to run every 0.3 seconds to give the gradual progress of the snake.
    /// </summary>
    public void BeginGame()
    {
        score = 0;
        GenerateBoard();
        player_.Init();
        initialised = true;
        gameOver = false;
        InvokeRepeating("UpdatePlayer", 0.0f, 0.3f);
    }

    public void StopGame()
    {
        CancelInvoke("UpdatePlayer");
        initialised = false;
        gameOver = true;
    }

    /// <summary>
    /// Creates a new fruit on the board.
    /// Uses recursion if the fruit generates in the player.
    /// </summary>
    void GenerateFruit()
    {
        Vector2 fruitPos = GeneratePosition();
        bool flag = false;
        for (int i = 0; i < player_.Length; i++)
        {
            if (fruitPos == player_.SnakeParts[i].Location) flag = true;
        }
        if (flag) GenerateFruit();
        else fruitLocation = fruitPos;
    }

    /// <summary>
    /// Uses unitys random to generate a vector within the boundaries.
    /// </summary>
    /// <returns></returns>
    Vector2 GeneratePosition()
    {
        int x, y;
        x = Random.Range(1, 23);
        y = Random.Range(1, 23);
        return new Vector2(x, y);
    }

    /// <summary>
    /// Handles the input of the player, uses command pattern.
    /// </summary>
    /// <returns></returns>
    Command HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) return new MoveWestCommand();
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) return new MoveEastCommand();
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) return new MoveSouthCommand();
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) return new MoveNorthCommand();
        return null;
    }

    /// <summary>
    /// Used by invoke repeating in BeginGame().
    /// </summary>
    void UpdatePlayer()
    {
        player_.Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialised) return; // Don't update unless the game is running.
        Command command = HandleInput();
        if (command != null) command.Execute(player_);

        //Handle if the player goes into the edges of the map.
        if (player_.SnakeParts[0].Location.x == 0 || player_.SnakeParts[0].Location.x == 24 || player_.SnakeParts[0].Location.y == 0 || player_.SnakeParts[0].Location.y == 24)
        {
            StopGame();
            Invoke("BeginGame", 5f);
            return;
        }
        if (fruitLocation == new Vector2(-1, -1)) GenerateFruit(); //Generate a new fruit when one has been consumed.
        //Handle if the player eats the fruit.
        if (player_.SnakeParts[0].Location == fruitLocation)
        {
            fruitLocation = new Vector2(-1, -1);
            player_.CosumeFruit();
            score += 100;
        }

        //Set the board to the regular colour and height, set the outer edges to be raised and coloured.
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                boardRenders[x, y].material.color = standardColor;
                board[x, y].transform.position = new Vector3(board[x, y].transform.position.x, 0, board[x, y].transform.position.z);
                if (x == 0 || y == 0 || x == 24 || y == 24)
                {
                    boardRenders[x, y].material.color = wallColour;
                    board[x, y].transform.position = new Vector3(board[x, y].transform.position.x, 0.2f, board[x, y].transform.position.z);
                }

            }
        }
        // if the fruit is on the board raise and colour the tile.
        if (fruitLocation != new Vector2(-1, -1))
        {
            boardRenders[(int)fruitLocation.x, (int)fruitLocation.y].material.color = fruitColour;
            board[(int)fruitLocation.x, (int)fruitLocation.y].transform.position = new Vector3(board[(int)fruitLocation.x, (int)fruitLocation.y].transform.position.x, 0.5f, board[(int)fruitLocation.x, (int)fruitLocation.y].transform.position.z);
        }
        // for each part of the snake if it's colliding stop the game. render the snakes new location.
        for (int i = 0; i < player_.Length; i++)
        {
            if (i != 0 && player_.SnakeParts[0].Location == player_.SnakeParts[i].Location)
            {
                StopGame();
                Invoke("BeginGame", 5f);
                return;
            }
            int x = (int)player_.SnakeParts[i].Location.x, y = (int)player_.SnakeParts[i].Location.y;
            boardRenders[x, y].material.color = player_.Colour;
            board[x, y].transform.position = new Vector3(board[x, y].transform.position.x, 0.5f, board[x, y].transform.position.z);
        }

    }
}
