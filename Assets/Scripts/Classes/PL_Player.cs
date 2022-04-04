using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Player 
{
    public float speed = 8;
    public float sprintMulti = 1.45f;
    public float jumpForce = 350;
    public float camSens = 5;
    public float yRotationLimit = 85f;
    public float maxVel = 6f; 
    public float walkingBobSpeed = 10f;
    public float bobbingSpeed = 0.05f;
    public float strafingSpeed;
    public float fallMultipler = 1.5f;
    public float lowJumpMutli = 1.0f;

    public float getSprintSpeed()
    {
        return speed * sprintMulti;
    }
}
