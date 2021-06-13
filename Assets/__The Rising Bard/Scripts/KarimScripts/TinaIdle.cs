using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinaIdle : StateMachineBehaviour
{
    [SerializeField]
    float speed = 2.5f;

    [SerializeField]
    float maxAttackRange;
    [SerializeField]
    float minAttackRange;

    Transform player;
    Rigidbody2D rb2d;
    Boss boss;

    float m_distance;

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

        m_distance = Vector2.Distance(player.position, rb2d.position);

        if (m_distance <= minAttackRange)
        {
            animator.SetBool("inRangeAttackTransforming", true);
            animator.SetBool("inRangeAttackOne", false);
            animator.SetTrigger("Attack");
        }
        else if (m_distance <= maxAttackRange && m_distance >= minAttackRange)
        {
            animator.SetBool("inRangeAttackOne", true);
            animator.SetBool("inRangeAttackTransforming", false);
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetBool("inRangeAttackTransforming", false);
        //animator.SetBool("InRangeAttackOne", false);
        //animator.ResetTrigger("Attack");
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
