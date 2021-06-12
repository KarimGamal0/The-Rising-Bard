using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinaWalk : StateMachineBehaviour
{
    [SerializeField]
    float speed = 2.5f;

    [SerializeField]
    float maxAttackRange;

    [SerializeField]
    float minAttackRange;

    [SerializeField]
    float minAttackRangeTwo;

    Transform player;
    Rigidbody2D rb2d;
    BossTina boss;

    float m_distance;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BossTina>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();



        m_distance = Vector2.Distance(player.position, rb2d.position);

        if (m_distance <= maxAttackRange && m_distance >= minAttackRange && !animator.GetBool("isStateTwo"))
        {
            animator.SetBool("inRangeAttackOne", true);
            animator.SetBool("inRangeAttackTransforming", false);
            animator.SetTrigger("Attack");
        }
        else if (m_distance <= minAttackRange && !animator.GetBool("isStateTwo"))
        {
            animator.SetBool("inRangeAttackTransforming", true);
            animator.SetBool("inRangeAttackOne", false);
            animator.SetTrigger("Attack");
        }
        else if (m_distance <= minAttackRangeTwo && animator.GetBool("isStateTwo"))
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            Vector2 target = new Vector2(player.position.x, rb2d.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb2d.position, target, speed * Time.fixedDeltaTime);
            rb2d.position = newPos;
        }

        //if (m_distance <= attackRange)
        //{
        //    animator.SetBool("InRangeAttackOne", true);
        //    animator.SetTrigger("Attack");
        //}
        //else
        //{
        //    animator.SetBool("InRangeAttackOne", false);
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
