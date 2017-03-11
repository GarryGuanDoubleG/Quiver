using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum State
{
    Standing,
    Moving,
    Jumping,
    Shooting,
    Praying,
    Falling,
    Sliding
};


public class Animation : MonoBehaviour
{
    #region Inspector
    public SkeletonAnimation animator;

    public string standing,
                    moving,
                    jumping,
                    shooting,
                    praying,
                    falling,
                    sliding;

    public SpineEvent events;
    #endregion

    //reference to model
    private State prevState,
                  currState;


    // Use this for initialization
    void Start () {
        animator.state.Event += HandleEvent;
	}

    void HandleEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        //if (e.Data.Name == footstepEvent)
        //{
        //    footstepAudioSource.pitch = 0.5f + Random.Range(-0.2f, 0.2f);
        //    footstepAudioSource.Play();
        //}


    }

    void PlayNewAnimation(string anim, int priority = 0, bool loop = true)
    {
        if(prevState == State.Jumping)
        {
            //landing.Play(); //play sound
        }

        //higher priorities get played
        animator.AnimationState.SetAnimation(priority, anim, loop);
    }

    void HandleNewState(State state)
    {
        switch (state)
        {
            case State.Standing:
                PlayNewAnimation(standing);
                break;
            case State.Moving:
                PlayNewAnimation(moving);
                break;
            case State.Shooting:
                PlayNewAnimation(shooting, 1, false);
                break;
            case State.Falling:               
                break;
            case State.Praying:
                PlayNewAnimation(praying, 1, false);
                break;
            case State.Jumping:
                PlayNewAnimation(jumping);
                break;
            case State.Sliding:
                PlayNewAnimation(sliding, 1, false);
                break;            
        }
    }

	// Update is called once per frame
	void Update () {		
        //currState = hero.state
        //HandleNewState(hero.state)
	}
}
