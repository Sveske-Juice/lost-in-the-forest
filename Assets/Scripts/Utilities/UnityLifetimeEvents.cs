using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityLifetimeEvents : MonoBehaviour
{
    public UnityEvent OnStart;
    public UnityEvent OnEnabled;
    public UnityEvent OnObjDestroy;

    private void Start()
    {
        OnStart?.Invoke();
    }

    private void OnEnable()
    {
        OnEnabled?.Invoke();
    }

    private void OnDestroy()
    {
        OnObjDestroy?.Invoke();
    }
}
