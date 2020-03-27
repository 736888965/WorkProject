using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Eat : Action
{
    // 变量：食物。会在行为树编辑器中赋值
    public SharedString food;

    public override TaskStatus OnUpdate()
    {
        Debug.Log("eat: " + food.GetValue());
        return TaskStatus.Success;
    }
}