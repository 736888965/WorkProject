using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateName
{
    public const string Idle = "idle";
    public const string Walk = "walk";
    public const string Attack = "attack";
    public const string Frozen = "frozen";
    public const string Vertigo = "vertigo";
    public const string Sheep = "sheep";
}



public enum FSMState
{
    None = 0,
    Idle = 1,
    Walk = 2,
    Attack = 3,
    /// <summary>
    /// 眩晕
    /// </summary>
    Frozen = 4,
    /// <summary>
    /// 冰冻
    /// </summary>
    Vertigo = 5,

    MainSub=1000,
    /// <summary>
    /// 中毒
    /// </summary>
    Posioning = 1001,
    /// <summary>
    /// 网捕
    /// </summary>
    NetCatch = 1002,
    /// <summary>
    /// 变羊
    /// </summary>
    Sheep = 1003,
}

public class FSMStateRuleConfig
{
    public static List<FSMState> ChangeMain(FSMState state)
    {
        List<FSMState> list;
        switch (state)
        {
            case FSMState.None:
                list = new List<FSMState>() { FSMState.Idle, FSMState.Walk, FSMState.Attack, FSMState.Frozen, FSMState.Vertigo };
                break;
            case FSMState.Idle:
                list = new List<FSMState>() { FSMState.Walk, FSMState.Attack, FSMState.Frozen, FSMState.Vertigo };
                break;
            case FSMState.Walk:
                list = new List<FSMState>() { FSMState.Idle, FSMState.Attack, FSMState.Frozen, FSMState.Vertigo };
                break;
            case FSMState.Attack:
                list = new List<FSMState>() { FSMState.Walk, FSMState.Idle, FSMState.Frozen, FSMState.Vertigo };
                break;
            case FSMState.Frozen:
                list = new List<FSMState>() { FSMState.Idle };
                break;
            case FSMState.Vertigo:
                list = new List<FSMState>() { FSMState.Idle };
                break;
            default:
                list = new List<FSMState>();
                break;
        }
        return list;
    }
    /// <summary>
    /// 子状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static List<FSMState> MutexSubState(FSMState state)
    {
        List<FSMState> list;
        switch (state)
        {
            case FSMState.Posioning:
                list = new List<FSMState>();
                break;
            case FSMState.NetCatch:
                list = new List<FSMState>() { FSMState.Walk };
                break;
            case FSMState.Sheep:
                list = new List<FSMState>() {  FSMState.Attack };
                break;
            default:
                list = new List<FSMState>();
                break;
        }
        return list;
    }

}
public abstract class FSMBase
{
    public abstract void OnEnter(FSMManager fsm);
    public abstract void OnUpdate(FSMManager fsm,float time);
    public abstract void OnExit(FSMManager fsm);
    public virtual void OnContinue(FSMManager fsm) { }
}

public class FSMManager :MonoBehaviour
{

    public AnimationManager anim;
    /// <summary>
    /// 玩家
    /// </summary>
    public Unit m_Unit;
    /// <summary>
    /// 当前的主状态
    /// </summary>
    public FSMState currentState = FSMState.None;
    /// <summary>
    /// 自身所有的子状态
    /// </summary>
    public List<FSMState> listSubState = new List<FSMState>();
    public Dictionary<FSMState, FSMBase> allState = new Dictionary<FSMState, FSMBase>();

    /// <summary>
    /// 绑定
    /// </summary>
    public void BindFsm(FSMState state, FSMBase fsmBase)
    {
        if (!allState.ContainsKey(state))
            allState.Add(state, fsmBase);
    }
    /// <summary>
    /// 开始
    /// </summary>
    /// <param name="state"></param>
    private void StartState(FSMState state)
    {
        allState[state].OnEnter(this);
    }
    /// <summary>
    /// 结束
    /// </summary>
    /// <param name="state"></param>
    public void EndState(FSMState state)
    {
        allState[state].OnExit(this);
        if (state > FSMState.MainSub)
            RemoveSubState(state);
    }

    private void ContinueState(FSMState state)
    {
        allState[state].OnContinue(this);
    }
    private void UpdateState(FSMState state, float time)
    {
        allState[state].OnUpdate(this,time);
    }

    
    #region 增删子状态

    private void AddSubState(FSMState state)
    {
        if (!listSubState.Contains(state))
            listSubState.Add(state);
    }

    private void RemoveSubState(FSMState state)
    {
        listSubState.Remove(state);
    }
    /// <summary>
    /// 子状态是否可以影响主状态的切换
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    private bool IsCauseMian(FSMState state)
    {
        foreach (FSMState item in listSubState)
        {
            if (FSMStateRuleConfig.MutexSubState(item).Contains(state))
                return false;
        }
        return true;
    }
    /// <summary>
    /// 是否影响当前的主状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns> true 为 影响</returns>
    private bool IsCauseCurrent(FSMState substate,FSMState current)
    {
       return FSMStateRuleConfig.MutexSubState(substate).Contains(current);
    }
    #endregion



    public void Change(FSMState state)
    {
        if(state<FSMState.MainSub) // 为主状态 切换
        {
            //状态相同 不切换状态
            if (currentState == state)
                return;
            // 子状态是否影响主状态的切换
            if(IsCauseMian(state))  //true 不影响
            {
                List<FSMState> list = FSMStateRuleConfig.ChangeMain(currentState);
                if (list.Contains(state))
                {
                    // 如果可以切换这个状态 结束当前的状态
                    EndState(currentState);
                    StartState(state);
                    currentState = state;
                }
                else
                {
                    Debug.LogError("state is not change ... ");
                }
            }
            else  //不可切换
            {
                Debug.LogError("state is Mutex ...");
            }
        }
        else  // 是子状态
        {
            if(listSubState.Contains(state))  //目前有这个子状态  要时间上的延续
            {
                ContinueState(state);
            }
            else
            {
                // 当前不存在这个子状态
                // 是否影响当前的主状态
                if (IsCauseCurrent(state, currentState)) // 影响
                {
                    EndState(currentState);
                    StartState(FSMState.Idle);
                    currentState = FSMState.Idle;
                }
                AddSubState(state);
                StartState(state);
            }
        }
    }

    private int countFrame = 0;
    private float stepTime = 0.03f;
    private float lossTime = 0.0f;
    public float timePlay = 0.0f;
    private void Update()
    {
        lossTime += Time.deltaTime;
        if(lossTime>=stepTime)
        {
            lossTime -= stepTime;
            countFrame++;
            timePlay = countFrame * stepTime;
            if (currentState != FSMState.None)
                UpdateState(currentState, timePlay);
            for (int i = 0; i < listSubState.Count; i++)
            {
                UpdateState(listSubState[i], timePlay);
            }
        }

    }
}