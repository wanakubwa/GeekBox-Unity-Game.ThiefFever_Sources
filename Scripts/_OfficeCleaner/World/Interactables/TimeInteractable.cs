using System.Collections;
using UnityEngine;

public abstract class TimeInteractable : Interactable
{
    #region Fields

    [SerializeField]
    private float waitTimeS = 1f;

    #endregion

    #region Propeties

    public float WaitTimeS { get => waitTimeS; }

    private Coroutine WaitCoroutine { get; set; } = null;

    #endregion

    #region Methods

    protected virtual bool CanInteract()
    {
        return IsInteractable;
    }

    protected virtual void OnTimerStarted()
    {

    }

    /// <summary>
    /// Zakonczony sukcesem.
    /// </summary>
    protected virtual void OnTimerEnded()
    {

    }

    protected virtual void OnTimerInterrupted()
    {

    }

    protected virtual void OnTimerUpdate(float currentTimeS)
    {

    }

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if (CanInteract() == true)
        {
            OnTimerStarted();

            WaitCoroutine = StartCoroutine(_WaitAndNotify());
            IsInteractable = false;
        }
    }

    protected override void OnPlayerExit(PlayerCharacterController player)
    {
        base.OnPlayerExit(player);

        if (WaitCoroutine != null)
        {
            StopCoroutine(WaitCoroutine);
            WaitCoroutine = null;
            OnTimerInterrupted();
        }

        IsInteractable = true;
    }

    private IEnumerator _WaitAndNotify()
    {
        float currentTimeS = Constants.DEFAULT_VALUE;
        while (true)
        {
            if (currentTimeS >= WaitTimeS)
            {
                OnTimerEnded();
                yield break;
            }

            currentTimeS += Time.deltaTime;
            OnTimerUpdate(currentTimeS);
            yield return null;
        }
    }


    #endregion

    #region Enums



    #endregion
}