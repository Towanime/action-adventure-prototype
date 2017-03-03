using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerStateMachine : MonoBehaviour {
    
    StateMachine<PlayerStates> fsm;
    public PlayerInput playerInput;
    // other components
    public PlayerMovement playerMovement;
    public Disk disk;
    public Sword sword;

    void Awake()
    {
        fsm = StateMachine<PlayerStates>.Initialize(this, PlayerStates.Idle);
    }

    void Update()
    {
        Debug.Log(fsm.State);
    }

    void Idle_Enter()
    {
        playerMovement.Disabled = false;
    }

    void Idle_Update()
    {
        if (playerInput.attack)
        {
            fsm.ChangeState(PlayerStates.Attack);
        }
        if (playerInput.disk && disk.CurrentState == DiskStates.Default)
        {
            fsm.ChangeState(PlayerStates.DiskThrow);
        }
    }

    void Attack_Enter()
    {
        // enable sword trigger?
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
}
