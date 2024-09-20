using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isAI;

 [SerializeField] private float paddleSize = 1f; 
    [SerializeField] private GameObject ball;
    public float initialSpeed;
    private Rigidbody2D rb;
    private Vector2 playerMove;
    private float originalSpeed;
    private bool isSpeedBoosted = false;

    void Start()
    {
        paddleSize = PlayerPrefs.GetFloat("PaddleSize", 1f); 
        transform.localScale = new Vector3(1, paddleSize, 1);
        float ballSpeed = PlayerPrefs.GetFloat("BallSpeed", initialSpeed);
        
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if(isAI){
            AIControl();
        }else{
            PlayerControl();
        }
        
    }

    public void ActivateSpeedBoost()
{
    if (!isSpeedBoosted)
    {
        originalSpeed = movementSpeed; 
        movementSpeed *= 2;
        isSpeedBoosted = true; 
        StartCoroutine(ResetSpeedBoost());
    }
}

private IEnumerator ResetSpeedBoost()
{
    yield return new WaitForSeconds(5f); 
    movementSpeed = originalSpeed;
    isSpeedBoosted = false;
}
    private void PlayerControl(){
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void AIControl(){
        if(ball.transform.position.y>transform.position.y+0.5f){
            playerMove=new Vector2(0,1);
        }else if(ball.transform.position.y<transform.position.y -0.5f){
            playerMove = new Vector2(0,-1);
        }else{
            playerMove = new Vector2(0,0);
        }
    }
    private void FixedUpdate(){
        rb.velocity = playerMove * movementSpeed;
    }
    public void SetAISpeed(float speed)
{
    movementSpeed = speed;
}

public void UpdatePaddleSize(float value)
{
    paddleSize = value;
    // Adjust paddle size if needed
    transform.localScale = new Vector3(value, value, 1);
}

}

