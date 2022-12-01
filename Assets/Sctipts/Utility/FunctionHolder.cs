using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionHolder : MonoBehaviour
{
    [SerializeField] private UnityEvent OnActivate;
    public bool ToActivate;
    private bool Activated;
    private void Update()
    {
        if(ToActivate && !Activated)
        {
            Activate();
            Activated = true;
        }
    }
    public void Activate()
    {
        OnActivate?.Invoke();
    }
}
