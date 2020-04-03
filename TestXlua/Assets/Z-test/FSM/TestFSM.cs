using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFSM : MonoBehaviour
{
    public FSMManager fsm;
    public FSMState state;
    private void Start()
    {
        fsm.BindFsm(FSMState.Idle, new FSMIdle());
        fsm.BindFsm(FSMState.Walk, new FSMWalk());
        fsm.BindFsm(FSMState.Attack, new FSMAttack());
        fsm.BindFsm(FSMState.Frozen, new FSMFrozen());
        fsm.BindFsm(FSMState.Vertigo, new FSMVertigo());
        fsm.BindFsm(FSMState.Posioning, new FSMPosioning());
        fsm.BindFsm(FSMState.NetCatch, new FSMNetCatch());
        fsm.BindFsm(FSMState.Sheep, new FSMSheep());
        fsm.BindFsm(FSMState.None, new FSMNone());
    }


    public void OnGUI()
    {

        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            fsm.Change(state);
        }
    }
}
