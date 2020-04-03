using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMIdle : FSMBase
{

    public override void OnEnter(FSMManager fsm)
    {
        //fsm.anim.Play(FSMStateName.Idle);
        Log.LogColor("idle 进入");

    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("idle 退出");
    }

    public override void OnUpdate(FSMManager fsm,float time)
    {

    }
}

public class FSMWalk : FSMBase
{
    public override void OnEnter(FSMManager fsm)
    {
        Log.LogColor("walk 进入");
    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("walk 退出");
    }


    public override void OnUpdate(FSMManager fsm,float time)
    {
    }
}
public class FSMAttack : FSMBase
{
    public override void OnEnter(FSMManager fsm)
    {
        Log.LogColor("attack 进入");
    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("attack 退出");
    }

    public override void OnUpdate(FSMManager fsm,float time)
    {
    }
}
public class FSMFrozen : FSMBase
{
    public override void OnEnter(FSMManager fsm)
    {
        Log.LogColor("frozen 进入");
    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("frozen 退出");
    }

    public override void OnUpdate(FSMManager fsm,float time)
    {
    }
}
public class FSMVertigo : FSMBase
{
    public override void OnEnter(FSMManager fsm)
    {
        Log.LogColor("Vertigo 进入");
    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("Vertigo 退出");
    }

    public override void OnUpdate(FSMManager fsm,float time)
    {
    }
}

public class FSMPosioning : FSMBase
{
    public override void OnEnter(FSMManager fsm)
    {
        Log.LogColor("posioning 进入");
    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("posioning 退出");
    }

    public override void OnUpdate(FSMManager fsm,float time)
    {
    }
}
public class FSMNetCatch : FSMBase
{
    public override void OnEnter(FSMManager fsm)
    {
        Log.LogColor("netcatch 进入");
    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("netcatch 退出");
    }

    public override void OnUpdate(FSMManager fsm,float time)
    {
    }
}
public class FSMSheep : FSMBase
{
    private float enterTime;
    private float lastTime = 10f;
    public override void OnEnter(FSMManager fsm)
    {
        enterTime = fsm.timePlay;
        Log.LogColor("sheep 进入" + enterTime);

    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("sheep 退出");
    }

    public override void OnUpdate(FSMManager fsm,float time)
    {
        //持续时间结束
        if(time-enterTime>=lastTime)
        {
            fsm.EndState(FSMState.Sheep);
        }
    }
    public override void OnContinue(FSMManager fsm)
    {
        enterTime += 5;
        Log.LogColor("sheep 继续延长时间" + enterTime);
    }

}

public class FSMNone: FSMBase
{
    public override void OnEnter(FSMManager fsm)
    {
        Log.LogColor("none 进入");
    }

    public override void OnExit(FSMManager fsm)
    {
        Log.LogColor("none 退出");
    }

    public override void OnUpdate(FSMManager fsm,float time)
    {
    }
}

