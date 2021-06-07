using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinaAttackOne : StateMachineBehaviour
{
    [SerializeField]
    float speed = 2.5f;
    [SerializeField]
    float attackRange = 3;

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
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb2d.position.y);

        //Debug.Log(Vector2.Distance(player.position, rb2d.position));

        //if (Vector2.Distance(player.position, rb2d.position) <= attackRange)
        //{
        //    //animator.SetBool("isAttacking", true);
        //}
        //else
        //{
        //    //animator.SetBool("isAttacking", false);
        //    //animator.SetBool("InRangeAttackOne", false);
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("inRangeAttackTransforming", false);
        animator.SetBool("InRangeAttackOne", false);
        animator.ResetTrigger("Attack");
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
