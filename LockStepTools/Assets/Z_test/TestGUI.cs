using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logging = LockStep.Logging;
public class TestGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(Mathf.Sin(Mathf.PI * 0.5f));
        print(Mathf.Sin(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            Logging.Debug.LogError("ceshi");
        }
    }
}
