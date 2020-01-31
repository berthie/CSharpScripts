using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.05f;
    public float slowDownLeanght = 2f;


    private void Update()
    {

        Time.timeScale += (1f / slowDownLeanght) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void doSlowMotion()
    {

        Time.timeScale = slowDownFactor;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;       Change RigidBody to Interpolerar
    }


}
