using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AudioSource source = animator.gameObject.GetComponent<AudioSource>();
        source.clip = Global.instance.Dead;
        source.loop = false;
        source.Play();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AudioSource source = animator.gameObject.GetComponent<AudioSource>();
        source.Stop();
    }
}