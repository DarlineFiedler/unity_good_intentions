using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_Spirit_Run : StateMachineBehaviour
{
    public float speed = 2.5f;

    public float attackRange = 3f;

    Transform player;

    Rigidbody2D myBossRigidbody;

    Forest_Spirit boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myBossRigidbody = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Forest_Spirit>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        boss.LookAtPlayer();
        Vector2 target =
            new Vector2(player.position.x, myBossRigidbody.position.y);
        Vector2 newPos =
            Vector2
                .MoveTowards(myBossRigidbody.position,
                target,
                speed * Time.fixedDeltaTime);
        myBossRigidbody.MovePosition (newPos);

        if (
            Vector2.Distance(player.position, myBossRigidbody.position) <=
            attackRange
        )
        {
            animator.SetTrigger("Attack");
            PlayerPrefs.SetInt("bossIsAttaking", 1);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        animator.ResetTrigger("Attack");
    }
}
