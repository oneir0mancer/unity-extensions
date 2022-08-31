﻿using System.Collections;
using UnityEngine;

public static class CoroutineExtensions
{
    public static Coroutine DoAfterTime(this MonoBehaviour caller, float time, System.Action callback)
    {
        IEnumerator Routine()
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }

        return caller.StartCoroutine(Routine());
    }

    public static IEnumerator WaitForSeconds(float time, System.Action<float> everyFrameCallback = null, bool startNextFrame = true,
        System.Action finishCallback = null)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            if (!startNextFrame) everyFrameCallback?.Invoke(t);
            yield return null;
            if (startNextFrame) everyFrameCallback?.Invoke(t);
        }
        finishCallback?.Invoke();
    }
    
    public static IEnumerator WaitForSecondsOrCondition(float time, System.Func<bool> condition)
    {
        for (float t = 0; t < time || condition(); t += Time.deltaTime)
        {
            yield return null;
        }
    }
    
    public static IEnumerator WaitForSecondsPausable(float time, System.Func<bool> pausePredicate)
    {
        for (float t = 0; t < time; t += pausePredicate() ? 0 : Time.deltaTime)
        {
            yield return null;
        }
    }
    
    public static IEnumerator WaitForSecondsPausable(float time, System.Func<bool> pausePredicate, 
        System.Action<float> everyFrameCallback, bool callbackWhenPaused = true)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            while (pausePredicate())
            {
                yield return null;
                if (callbackWhenPaused) everyFrameCallback?.Invoke(t);
            }

            yield return null;
            everyFrameCallback?.Invoke(t);
        }
    }
}