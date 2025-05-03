using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Paused,
        Moving,
        Drawing
    }

    public GameState currentGameState = GameState.Moving;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
