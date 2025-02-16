using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState {Menu, Game, Level, LevelComplete, GameOver}

    private GameState gameState;

    public static Action<GameState> onGameStateChanged;
    void Start()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Update()
    {
        
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }
}
