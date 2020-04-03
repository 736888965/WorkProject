using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttackType
{
    A,
    B,
    C, 
}
public interface IAttackAck
{
   void TriggerAttack(params object[] objs);
}

public abstract class SingleAck : IAttackAck
{
    public abstract void TriggerAttack(params object[] objs);
}

public class ATypeRealize : SingleAck
{
    public ATypeRealize()
    {

    }
    public override void TriggerAttack(params object[] objs)
    {
        Debug.LogError(" 触发攻击 A");
    }
}


public class AttackBase
{
    public void TriggerAttact(AttackType type,params object[] objs)
    {
        SingleAck ack = ReflectionHelper.CreateInstance<SingleAck>(type + "", "Assembly-CSharp");
        ack.TriggerAttack(objs);
    }
}



