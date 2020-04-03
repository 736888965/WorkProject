using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimeScale : MonoBehaviour
{
    private float timeHZ = 0.3f;
    private float allTime;

    public float scale;

    // Update is called once per frame
    void Update()
    {
        //allTime += Time.deltaTime;
        //if(allTime>=timeHZ)
        //{
        //    //transform.Rotate(Vector3.up*5);
        //    Log.LogColor("time ： " + Time.realtimeSinceStartup);
        //    allTime -= timeHZ;
        //}
    }
    private void OnGUI()
    {
        if(GUILayout.Button("",GUILayout.Width(100),GUILayout.Height(50)))
        {
            //Time.timeScale = scale;
            for (int i = 0; i < 1000; i++)
            {
                Log.LogColor(string.Format("{0}的cos值为 ; {1}", i, +Mathf.Cos(i)), "red");
            }
        }
    }
}
