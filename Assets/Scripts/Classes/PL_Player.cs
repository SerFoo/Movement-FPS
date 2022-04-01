using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Player 
{
    public float speed = 8;
    public float sprintMulti = 1.45f;

    public float jumpForce = 8;
    public float camSens = 5;
    public float yRotationLimit = 85f;
    public float maxVel = 8.5f; 
    public float walkingBobSpeed = 10f;
    public float bobbingSpeed = 0.05f;

    public float Speed {get {return speed;} set{speed = value;}}
    public float SprintMulti {get {return sprintMulti;} set{sprintMulti = value;}}
    public float JumpForce {get {return jumpForce;} set{jumpForce = value;}}
    public float CamSens {get {return camSens;} set{camSens = value;}}
    public float YRotationLimit {get {return yRotationLimit;} set{yRotationLimit = value;}}
    public float MaxVel {get {return maxVel;} set{maxVel = value;}}
    public float WalkingBobSpeed {get {return walkingBobSpeed;} set{walkingBobSpeed = value;}}
    public float BobbingSpeed {get {return bobbingSpeed;} set{bobbingSpeed = value;}}

}
