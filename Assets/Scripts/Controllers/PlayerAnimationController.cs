using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    public Animator animator;
    public PlayerInput playerInput;
	
	// Update is called once per frame
	void Update () {
        this.animator.SetBool("IsRunning", this.playerInput.direction != Vector3.zero);
    }
}
