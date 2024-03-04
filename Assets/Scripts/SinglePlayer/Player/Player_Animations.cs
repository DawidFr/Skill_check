using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{

    private Animator anim;
    
    public AnimationState currentState;
    public enum AnimationState{
        idle,
        walking,
        running,
        jumping
    }
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void PlayAnimation(Player_Animations.AnimationState state, bool checkIsPlaying = true){

        if(checkIsPlaying){
            if(currentState == state){
                
                Debug.Log("same animations");
                return;
            }
        }
        // Debug.Log("not same animations");
        anim.Play(state.ToString());
        currentState = state;
    }
    // TODO EVERYTHING
 
}
