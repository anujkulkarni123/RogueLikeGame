using UnityEngine;
using System.Collections;  
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private BoardManager boardScript;
    private int leel = 1;
    public int playerFood = 100;
    public bool playersTurn = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnload(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
    }

    void Update()
    {
        if (playersTurn) return;
        StartCouroutine(MoveEnemies());
    }

    IEnumerator MoveEnemies()
    {
        yield return new WaitForSeconds(0.1f);
        playersTurn = true;
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");
        enabled = false;
    }
}