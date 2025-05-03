using Unity.VisualScripting;
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            currentGameState = currentGameState == GameState.Moving ? GameState.Drawing : GameState.Moving;
            SwitchState();
        }
    }

    private void SwitchState()
    {
        switch (currentGameState)   
        {
            case GameState.Paused:
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Moving:
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case GameState.Drawing:
                Cursor.lockState = CursorLockMode.None;
                break;
            default:
                break;
        }
    }
}
