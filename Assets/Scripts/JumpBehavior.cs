using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour {


    private float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos, boss1Pos;
    public float speed;
    public bool isPlayerEnter;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss1Pos = GameObject.FindGameObjectWithTag("Boss1").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else {
            timer -= Time.deltaTime;
        }
        if (playerPos.transform.position.x <= boss1Pos.transform.position.x + 40f && playerPos.transform.position.y <= boss1Pos.transform.position.y + 40f)
        {
            isPlayerEnter = true;
        }
        if (isPlayerEnter)
        {
            Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
        }
        
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
