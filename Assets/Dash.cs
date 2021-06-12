using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : StateMachineBehaviour
{
    [SerializeField] float dashSpeed;
    [SerializeField] float attackRange = 3;

    Transform player;
    Rigidbody2D rb2d;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Vector2.Distance(player.position, rb2d.position) <= attackRange)
        {
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            rb2d.velocity = boss.GetDirection() * dashSpeed;

            animator.SetBool("isAttacking", false);
            animator.SetBool("isCoolingDownFinished", false);
        }

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("isCoolingDownFinished", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
