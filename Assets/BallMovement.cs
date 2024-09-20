using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BallMovement : MonoBehaviour
{
     [SerializeField] private float initialSpeed = 10;
     [SerializeField] private float speedIncrease = 0.25f;
     [SerializeField] private Text playerScore;
     [SerializeField] private Text AiScore;

     private AudioSource audioSource;

    [SerializeField] private AudioClip paddleHitSound;
    [SerializeField] private AudioClip scoreSound;  

    [SerializeField] private ParticleSystem collisionEffect;

     private int hitCounter;
     private Rigidbody2D rb;

     private bool gameStarted = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        collisionEffect.gameObject.SetActive(false);
        // Invoke("StartBall",2f);
        
    }

    void FixedUpdate(){
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    public void StartGame()
{
    gameStarted = true;
    StartBall();
}

    private void StartBall(){
        if (!gameStarted) return;
        rb.velocity = new Vector2(-1,0) * (initialSpeed+speedIncrease*hitCounter);

    }
    private void ResetBall(){
        rb.velocity = new Vector2(0,0);
        transform.position = new Vector2(0,0);
        hitCounter = 0;
        Invoke("StartBall",2f);
    }

    private void PlayerBounce(Transform myObject){
        hitCounter++;
        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;
        float xDirection, yDirection;
        if(transform.position.x>0)xDirection=-1;else xDirection = 1;
        yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if(yDirection ==0){
            yDirection = 0.5f;
        }
        rb.velocity = new Vector2(xDirection,yDirection) * (initialSpeed + (speedIncrease*hitCounter));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Player" || collision.gameObject.name== "Ai"){
            PlayerBounce(collision.transform);
            PlayPaddleHitSound();
        }
    }

    private void PlayPaddleHitSound()
    {
        audioSource.PlayOneShot(paddleHitSound);
    }

    private void OnTriggerEnter2D(Collider2D collision ){
        PlayScoreSound();
        collisionEffect.transform.position = (transform.position*-1);  
        collisionEffect.gameObject.SetActive(true);
        collisionEffect.Play();

        StartCoroutine(DisableScoreEffect());
        if(transform.position.x > 0){             
            ResetBall();
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();

        }else if(transform.position.x <0){
            
            ResetBall();

            AiScore.text = (int.Parse(AiScore.text) + 1).ToString();
        }
        
    }
     private IEnumerator DisableScoreEffect()
    {
       
        yield return new WaitForSeconds(collisionEffect.main.duration);
        collisionEffect.Stop();
        collisionEffect.gameObject.SetActive(false);  
    }

private void PlayScoreSound()
    {
        audioSource.PlayOneShot(scoreSound);  
    }
    public void SetBallSpeed(float speed)
{
    initialSpeed = speed;
}


}
