using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    public Transform cube;
    public Transform sphere;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        CollisionTriggerListener.Get(cube.gameObject).RegisterTriggerEnter(OnEnter);
    }

    private void OnEnter(Collider arg0)
    {
        print(arg0.gameObject.name + "  num "+ num);
        num--;
        if (num <= 0)
            Destroy(cube.gameObject);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
