using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float force = 3.0f;
    [SerializeField] float flySpeed = 2f;

    [SerializeField] AudioClip flySound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip scoreSound;

    [SerializeField] GameObject hitEffect;

    [SerializeField] float rotateSmooth = 5f;
    Quaternion downRotation = Quaternion.Euler(0, 0, -60);
    Quaternion forwardRotaion = Quaternion.Euler(0, 0, 45);

    AudioSource audioSource;

    int lastScore = 0;
    int currentScore = 1;

    float balanceForce;
    float balanceMass;

    float delayTime = 0.0f;
    float clickDelaySpeed = 1.5f;

    int deadCount = 0;

    GameManager gameManager;
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentScore = gameManager.score;
        balanceMass = force;
    }

    // Update is called once per frame
    void Update()
    {
        //print("current score: " + currentScore);
        //print("last score: " + lastScore);
        switch (gameManager.gameState)
        {
            case GameManager.GameState.OnPlaying:
                hitEffect.SetActive(false);
                deadCount = 0;
                MoveForward();
                Score();
                if (gameManager.isOutSide == false)
                {
                    Fly();
                }
                break;
            case GameManager.GameState.GetReady:
                hitEffect.SetActive(false);
                flySpeed = 2f;
                MoveForward();
                if (gameManager.isOutSide == false)
                {
                    Fly();
                }
                break;
            case GameManager.GameState.GameOver:
                OnBeingHit();
                break;
            default:
                hitEffect.SetActive(false);
                MoveForward();
                break;
        }
    }

    void Fly()
    {
        delayTime -= Time.deltaTime;

        if (delayTime <= 0f)
        {
            OnPress();
        }
        transform.position += Vector3.up * balanceForce * Time.deltaTime;
        BalanceForce();
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, rotateSmooth * Time.deltaTime);
    }

    void OnPress()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddForce();
            transform.rotation = forwardRotaion;
            playerAnimator.SetTrigger("Flap");
            delayTime = 1f / clickDelaySpeed;
            audioSource.PlayOneShot(flySound);
        }
    }

    void Score()
    {
        currentScore = gameManager.score;
        if (currentScore > lastScore)
        {
            audioSource.PlayOneShot(scoreSound);
            flySpeed += 0.1f;
            lastScore = currentScore;
        }
    }

    void MoveForward()
    {
        transform.position += Vector3.right * flySpeed * Time.deltaTime;
    }

    void AddForce()
    {
        balanceForce = force;
    }
    void BalanceForce()
    {
        balanceForce -= balanceMass * Time.deltaTime;
        if (balanceForce <= 0)
        {
            balanceForce = 0;
        }
    }

    void OnBeingHit()
    {
        if (deadCount <= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            audioSource.PlayOneShot(deathSound);
            playerAnimator.SetTrigger("Die");
            hitEffect.SetActive(true);
            deadCount++;
        }
    }
}
