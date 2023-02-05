using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIMonoBehavior : MonoBehaviour
{
    #region Fields


    #endregion

    #region Propeties

    public List<SmartCoroutine> CoroutineCollection
    {
        get;
        private set;
    } = new List<SmartCoroutine>();

    #endregion

    #region Methods

    public virtual void OnEnable()
    {
        RefreshAllManagedCoroutines();
    }

    public virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SuspendManagedCorutines()
    {
        StopAllCoroutines();
    }

    public void StartManagedCoroutine(IEnumerator enumerator)
    {
        SmartCoroutine smartCoroutine = TryGetSmartCoroutine(enumerator);
        if(smartCoroutine != null)
        {
            RemoveManagedCoroutine(smartCoroutine);
        }

        // TODO: do usuniecia!
        StopAllCoroutines();

        smartCoroutine = new SmartCoroutine(enumerator);
        CoroutineCollection.Add(smartCoroutine);

        TryStartManagedCoroutine(smartCoroutine);
    }

    public void RefreshAllManagedCoroutines()
    {
        if (CoroutineCollection != null)
        {
            for (int i = 0; i < CoroutineCollection.Count; i++)
            {
                TryStartManagedCoroutine(CoroutineCollection[i]);
            }
        }
    }

    private void TryStartManagedCoroutine(SmartCoroutine coroutine)
    {
        if (gameObject.activeInHierarchy == false)
        {
            return;
        }

        if (coroutine != null)
        {
            Coroutine newCoroutine = StartCoroutine(ManagedCoroutine(coroutine.WorkerHandler));
            coroutine.SetCoroutine(newCoroutine);
        }
    }

    private IEnumerator ManagedCoroutine(IEnumerator worker)
    {
        yield return StartCoroutine(worker);
        RemoveCoroutineByWorker(worker);
    }

    private void RemoveCoroutineByWorker(IEnumerator enumerator)
    {
        SmartCoroutine smartCoroutine = TryGetSmartCoroutine(enumerator);
        if(smartCoroutine!= null)
        {
            RemoveManagedCoroutine(smartCoroutine);
        }
    }

    private void RemoveManagedCoroutine(SmartCoroutine coroutine)
    {
        if(coroutine != null)
        {
            if(coroutine.ManagedCoroutine != null)
            {
                StopCoroutine(coroutine.ManagedCoroutine);
            }

            CoroutineCollection.Remove(coroutine);
        }
    }

    private SmartCoroutine TryGetSmartCoroutine(IEnumerator enumerator)
    {
        for(int i =0; i < CoroutineCollection.Count; i++)
        {
            if(CoroutineCollection[i].WorkerEqual(enumerator) == true)
            {
                return CoroutineCollection[i];
            }
        }

        return null;
    }

    #endregion

    #region Handlers



    #endregion
}
