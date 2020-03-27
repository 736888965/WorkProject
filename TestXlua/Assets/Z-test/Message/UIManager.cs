using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase 
{
    public static UIManager instance;

    /// <summary>
    /// ui open 的栈
    /// </summary>
    private Stack<UIBase> ui_stack = new Stack<UIBase>();

    private void Awake()
    {
        instance = this;
    }

    public void OpenUI()
    {

    }

    public void OpenUI(string name ,bool beforeClose = true)
    {

    }
    public void CloseUI()
    {

    }

    public void CloseAll()
    {

    }

    public void Pop()
    {
        if(ui_stack.Count>0)
        {
            ui_stack.Pop();
        }
    }
    public void Push(UIBase ui)
    {
        ui_stack.Push(ui);
    }

}
