using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PActionAnim
{
    private Animator animator;

    //Name of the animation in the animator
    private string _animName;

    private float _speed;
    private float _actionSpeed;
    //Length of all 3 animations together in seconds (anticipation, action, recovery)
    private float _length;

    //Anticipation Animation
    private bool _Ant_sActionSpeed = false;
    private float _Ant_length; //0.0 - 1.0 Precentage of animation (all 3 lengths must equal 1)

    private bool _Ant_easeIn = false;
    private float _Ant_easeInMag; //Cubic function pow(x, magnitude)
    private float _Ant_easeInDuration; //from 0.0 - 1.0 (percentage of animation)

    private bool _Ant_easeOut = false;
    private float _Ant_easeOutMag; //Cubic function pow(x, magnitude)
    private float _Ant_easeOutLength; //from 0.0 - 1.0 (percentage of animation)

    //Action Animation
    private bool _Act_sActionSpeed = false;
    private float _Act_length; //0.0 - 1.0 Precentage of animation (all 3 lengths must equal 1)

    private bool _Act_easeIn = false;
    private float _Act_easeInMag; //Cubic function pow(x, magnitude)
    private float _Act_easeInDuration; //from 0.0 - 1.0 (percentage of animation)

    private bool _Act_easeOut = false;
    private float _Act_easeOutMag; //Cubic function pow(x, magnitude)
    private float _Act_easeOutDuration; //from 0.0 - 1.0 (percentage of animation)

    //Recovery Animation
    private bool _Rec_sActionSpeed = false;
    private float _Rec_length; //0.0 - 1.0 Precentage of animation (all 3 lengths must equal 1)

    private bool _Rec_easeIn = false;
    private float _Rec_easeInMag; //Cubic function pow(x, magnitude)
    private float _Rec_easeInDuration; //from 0.0 - 1.0 (percentage of animation)

    private bool _Rec_easeOut = false;
    private float _Rec_easeOutMag; //Cubic function pow(x, magnitude)
    private float _Rec_easeOutDuration; //from 0.0 - 1.0 (percentage of animation)



    //Returns animation TIme in seconds
    public float GetAnimTime()
    {
        return 0;
    }
}
