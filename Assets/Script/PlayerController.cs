﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float pressDelayTime;
    [SerializeField] CameraController camera;
    float currentPressDelayTime;
    sbyte dieCount = 0;

    VolantAnimal bird;
    GameManager gameManager;
    #region Properties
    public VolantAnimal Bird
    {
        get { return bird; }
    }
    #endregion
    [System.Obsolete]
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        bird = VolantAnimal.Instance;
        bird.HitEffect.enableEmission = false;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        switch (gameManager.State)
        {
            case GameManager.GameState.Play:
                dieCount = 0;
                bird.MoveForward();
                if(camera.IsTargetInView)
                    PressToFly();
                bird.HitEffect.enableEmission = true;
                break;
            case GameManager.GameState.Ready:
                bird.BalanceForce = 0;
                bird.transform.rotation = Quaternion.Euler(0, 0, 5);
                bird.MoveForward();
                PressToFly();
                break;
            case GameManager.GameState.GameOver:
                // not to make die sound playing repeatedly
                if (dieCount <= 0)
                {
                    bird.BeingHit();
                    StartCoroutine(bird.StopParticle());
                    dieCount++;
                }
                break;
            default:
                bird.MoveForward();
                break;
        }
    }

    private void PressToFly()
    {
        currentPressDelayTime -= Time.deltaTime;
        if (currentPressDelayTime <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bird.ResetBalanceForce();
                bird.Transform.rotation = bird.ForwardRotation;
                bird.Animator.SetTrigger("Flap");
                currentPressDelayTime = pressDelayTime;
                bird.AudioSource.PlayOneShot(bird.FlySound);
            }
        }
        bird.Fly();
    }
}
