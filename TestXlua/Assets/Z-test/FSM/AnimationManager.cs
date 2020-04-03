using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager 
{
    public Animation anim;
    private string currentName;
    public void Play(string name)
    {
        currentName = name;
        anim.CrossFade(name, 0.1f);
    }

    public void Stop()
    {
        anim.Stop();
    }
    /// <summary>
    /// 设置当前播放的速度
    /// </summary>
    /// <param name="speed"></param>
    public void PlaySpeed(float speed)
    {
        if (speed > 1)
            speed = 1;
        anim[currentName].speed = speed;
    }
}
