using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerStateMachine : MonoBehaviour {
    
    StateMachine<PlayerStates> fsm;
    public PlayerInput playerInput;
    // other components
    public PlayerMovement playerMovement;
    public ArchitectMode architectComponent;
    public Manipulator manipulationComponent;
    public CameraController cameraController;
    public Disk disk;
    public Sword sword;

    void Awake()
    {
        fsm = StateMachine<PlayerStates>.Initialize(this, PlayerStates.Idle);
    }

    void Update()
    {
    }

    void Idle_Enter()
    {
        playerMovement.Disabled = false;
    }

    void Idle_Update()
    {
        if ((playerInput.attack || playerInput.disk) && manipulationComponent.InSight())
        {
            fsm.ChangeState(PlayerStates.ManipulationMode);
        }
        /*if (playerInput.disk && disk.CurrentState == DiskStates.Default)
        {
            fsm.ChangeState(PlayerStates.DiskThrow);
        }
        if (playerInput.architectMode)
        {
            fsm.ChangeState(PlayerStates.ArchitectMode);
        }*/
    }

    void Attack_Enter()
    {
        // enable sword trigger?
        //cameraController.ActivateAimingMode();
    }

    void Attack_Exit()
    {

    }

    void DiskThrow_Enter()
    {
        playerMovement.Disabled = true;
        disk.BeginThrow();
    }

    void DiskThrow_Update()
    {
        if(disk.CurrentState == DiskStates.Thrown)
        {
            playerMovement.Disabled = false;
            fsm.ChangeState(PlayerStates.Idle);
        }
    }

    void ArchitectMode_Enter()
    {
        playerMovement.Disabled = true;
        architectComponent.Activate();
    }


    void ArchitectMode_Update()
    {
        if (playerInput.architectMode)
        {
            fsm.ChangeState(PlayerStates.Idle);
        }
    }

    void ArchitectMode_Exit()
    {
        playerMovement.Disabled = false;
        architectComponent.Deactivate();
    }

    void ManipulationMode_Enter()
    {
        playerMovement.Disabled = true;
        manipulationComponent.Disabled = false;
        manipulationComponent.Activate();
    }

    void ManipulationMode_Update()
    {
        if (manipulationComponent.IsDone())
        {
            fsm.ChangeState(PlayerStates.Idle);
        }
    }

    void ManipulationMode_Exit()
    {
        playerMovement.Disabled = false;
        manipulationComponent.Disabled = true;
    }
}
