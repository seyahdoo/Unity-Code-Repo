///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;

public class Timer
{
    //one time go.

    private float StartTime;
    private float ToCount;
    private bool Working;

    public bool Contunie
    {
        get
        {
            if (!Working)
                return false;

            if ((Time.time - StartTime) >= ToCount)
            {
                Working = false;
                return true;
            }

            return false;
        }
    }

    public void SetTimer(float Timer)
    {
        StartTime = Time.time;
        ToCount = Timer;
        Working = true;
    }

    public void SetActive(bool Active)
    {
        Working = Active;
    }

}
