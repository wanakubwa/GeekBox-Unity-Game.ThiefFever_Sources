using System.Collections;
using UnityEngine;

public class HidableBase : Interactable
{
    #region Fields

    [SerializeField]
    private GameObject characterToShow;
    [SerializeField]
    private GameObject[] toShowOnPlayerExit;
    [SerializeField]
    private float waitTimeS = 1f;

    #endregion

    #region Propeties

    public GameObject CharacterToShow {
        get => characterToShow;
    }

    private Coroutine HideCoroutine { get; set; } = null;

    #endregion

    #region Methods

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if (IsInteractable == true)
        {
            Player.BubbleUI.SetHideInfoVisible(true);
            HideCoroutine = StartCoroutine(_WaitAndHide());

            IsInteractable = false;
        }
    }

    protected override void OnPlayerExit(PlayerCharacterController player)
    {
        base.OnPlayerExit(player);

        Player.BubbleUI.SetHideInfoVisible(false);

        Player = null;
        if (HideCoroutine != null)
        {
            StopCoroutine(HideCoroutine);
        }

        HideCoroutine = null;
        IsInteractable = true;
    }

    protected override void Awake()
    {
        base.Awake();

        CharacterToShow.SetActive(false);
        toShowOnPlayerExit.ForEach(hide => hide.gameObject.SetActive(true));
    }

    private void HidePlayer()
    {
        HideCoroutine = null;

        Player.SetVisible(false);
        CharacterToShow.gameObject.SetActive(true);
        Player.BubbleUI.SetHideInfoVisible(false);
        toShowOnPlayerExit.ForEach(hide => hide.gameObject.SetActive(false));

        // Subskrypacja eventow zeby wyjsc.
        UserInputManager.Instance.OnMousePress += OnMouseHoldHandler;
    }

    private void ShowPlayer()
    {
        // Subskrypacja eventow zeby wyjsc.
        UserInputManager.Instance.OnMousePress -= OnMouseHoldHandler;

        Player.SetVisible(true);
        CharacterToShow.gameObject.SetActive(false);
        toShowOnPlayerExit.ForEach(hide => hide.gameObject.SetActive(true));
    }

    private void OnMouseHoldHandler()
    {
        ShowPlayer();
    }

    private IEnumerator _WaitAndHide()
    {
        float currentTimeS = Constants.DEFAULT_VALUE;
        while (true)
        {
            if(currentTimeS >= waitTimeS)
            {
                HidePlayer();
                yield break;
            }

            currentTimeS += Time.deltaTime;
            Player.BubbleUI.UpdateHideProgress(currentTimeS / waitTimeS);
            yield return null;
        }
    }

    #endregion

    #region Enums



    #endregion
}