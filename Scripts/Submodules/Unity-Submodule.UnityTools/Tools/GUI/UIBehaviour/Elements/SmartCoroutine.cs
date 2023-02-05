using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SmartCoroutine
{
    #region Fields



    #endregion

    #region Propeties

    public Coroutine ManagedCoroutine
    {
        get;
        private set;
    }

    public IEnumerator WorkerHandler
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public SmartCoroutine(IEnumerator worker)
    {
        WorkerHandler = worker;
    }

    public void SetCoroutine(Coroutine coroutine)
    {
        ManagedCoroutine = coroutine;
    }

    public bool WorkerEqual(IEnumerator enumerator)
    {
        if(WorkerHandler.Equals(enumerator) == true)
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Handlers



    #endregion
}

