using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;  
    [SerializeField] private BallMovement ball; 
    public GameObject difficultyButtons;  

    public void SetEasy()
    {
        player.SetAISpeed(5f);
        ball.SetBallSpeed(10f);
        StartGame();  
    }

    public void SetMedium()
    {
        player.SetAISpeed(10f);
        ball.SetBallSpeed(10f);
        StartGame();  
    }

    public void SetHard()
    {
        player.SetAISpeed(15f);
        ball.SetBallSpeed(15f);
        StartGame();
    }

    private void StartGame()
    {
        Debug.Log("Game started! Player can move.");
        difficultyButtons.SetActive(false);  
        ball.StartGame();
    }
}
