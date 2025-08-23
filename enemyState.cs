using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class enemyState : MonoBehaviour
{
    enum State
    {
        Standing,
        Walking,
        Following
    }

    [SerializeField] State action = State.Walking;
    [SerializeField] float vel = 0.04f;
    [SerializeField] GameObject playerRef;

    int stepCount;
    int standingCount;

    const int maxSteps = 250;
    const int timeStill = 200;

    float walkVel;

    float xDist;
    float zDist;
    float vDist;

    Vector2 randomPos;

    float followSpdX;
    float followSpdZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkVel = vel;
    }

    // Update is called once per frame
    void Update()
    {
        xDist = Math.Abs(this.transform.position.x - playerRef.transform.position.x);
        zDist = Math.Abs(this.transform.position.z - playerRef.transform.position.z);
        vDist = xDist + zDist;

        switch (action)
        {
            case State.Following:
                HandleFollow();
                break;

            case State.Standing:
                ++standingCount;

                break;

            case State.Walking:
                HandleWalking();
                break;

            default: break;
        }
        
        updateState();
    }

    void HandleFollow()
    {
        if (this.transform.position.x > playerRef.transform.position.x) followSpdX = vel * -1;
        else if (this.transform.position.x < playerRef.transform.position.x) followSpdX = vel;
        else followSpdX = 0;

        if (this.transform.position.z > playerRef.transform.position.z) followSpdZ = vel * -1;
        else if (this.transform.position.z < playerRef.transform.position.z) followSpdZ = vel;
        else followSpdZ = 0;

        this.transform.position += new Vector3(followSpdX, 0.0f, followSpdZ);
    }

    void HandleWalking()
    {
        if (stepCount == 0)
        {
            randomPos.x = UnityEngine.Random.Range(walkVel * -1, walkVel);
            randomPos.y = UnityEngine.Random.Range(walkVel * -1, walkVel);
        }

        transform.position += new Vector3(randomPos.x, 0.0f, randomPos.y);
        ++stepCount;
    }

    void updateState()
    {
        if (stepCount == maxSteps)
        {
            walkVel *= -1;
            stepCount = 0;
            action = State.Standing;
        }

        if (standingCount == timeStill)
        {
            standingCount = 0;
            action = State.Walking;
        }

        if (vDist <= 8)
        {
            action = State.Following;
        }
        else if (action == State.Following)
        {
            action = State.Standing;
        }
    }

}
