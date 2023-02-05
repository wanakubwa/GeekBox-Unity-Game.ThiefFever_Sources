using System.Collections;
using UnityEngine;

public class ExitArea : Interactable
{
    #region Fields

    [SerializeField]
    private float waitDelayS = 1f;

    #endregion

    #region Propeties

    private Coroutine WaitCoroutine { get; set; } = null;

    #endregion

    #region Methods

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if (IsInteractable == true)
        {
            Player.BubbleUI.SetExitInfoVisible(true);
            WaitCoroutine = StartCoroutine(_WaitEndRemoveFromStack());

            IsInteractable = false;
        }
    }

    protected override void OnPlayerExit(PlayerCharacterController player)
    {
        base.OnPlayerExit(player);

        Player.BubbleUI.SetExitInfoVisible(false);
        if (WaitCoroutine != null)
        {
            StopCoroutine(WaitCoroutine);
            WaitCoroutine = null;
        }

        IsInteractable = true;
    }

    private void OnLvlWin()
    {
        GamePlayManager.Instance.LvlSuccess();
    }

    private IEnumerator _WaitEndRemoveFromStack()
    {
        float currentTimeS = Constants.DEFAULT_VALUE;
        while (true)
        {
            if (currentTimeS >= waitDelayS)
            {
                OnLvlWin();
                yield break;
            }

            currentTimeS += Time.deltaTime;
            Player.BubbleUI.UpdateExitProgress(currentTimeS / waitDelayS);
            yield return null;
        }
    }

    #endregion

    #region Enums



    #endregion
}
