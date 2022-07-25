using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public static UpdateManager s;

    public delegate void _OnUpdate();
    public event _OnUpdate OnUpdate;

    public delegate void _OnFixedUpdate();
    public event _OnFixedUpdate OnFixedUpdate;

    private void Awake()
    {
        if (s != null && s != this)
            Debug.Log("There are multiple UpdateManagers");
        s = this;
    }

    private void OnEnable()
    {
        if (s != null && s != this)
            Debug.Log("There are multiple UpdateManagers");
        s = this;
    }
    private void OnDisable()
    {
        s = null;
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
