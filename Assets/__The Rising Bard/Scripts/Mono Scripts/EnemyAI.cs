using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private enum State
    {
        Idle,
        Following,
        Attaking,
        Die

    }
    [SerializeField] private Transform target;
    [SerializeField] private float targetRangeForFollow;
    [SerializeField] private float targetRangeForAttack;

    private State currentState;

    private void Awake()
    {
        currentState = State.Idle;
    }


    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Idle:

                break;
            case State.Following:

                break;
            case State.Attaking:

                break;
            case State.Die:

                break;
        }
        if (Vector2.Distance(transform.position, target.position) <= targetRangeForFollow)
        {

        }

        else if (Vector2.Distance(transform.position, target.position) <= targetRangeForAttack)
        {

        }
    }
}
