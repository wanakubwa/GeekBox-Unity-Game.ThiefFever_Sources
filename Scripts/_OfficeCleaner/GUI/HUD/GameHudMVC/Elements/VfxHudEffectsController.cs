using System.Collections;
using UnityEngine;
using System.Linq;

public class VfxHudEffectsController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private VFXPositionElement[] textEffectsCollection;
    [SerializeField]
    private Sprite[] textSprites;

    #endregion

    #region Propeties

    #endregion

    #region Methods

    private void Start()
    {
        GameplayEvents.Instance.OnFurnitureCollapsed += OnFurnitureCollapsedHandler;
    }

    private void OnFurnitureCollapsedHandler()
    {
        if(TryGetFreePosition(out VFXPositionElement position) == true && textSprites.Length > Constants.DEFAULT_VALUE)
        {
            position.SetImage(textSprites.GetRandomElement());
        }
    }

    private void OnDestroy()
    {
        if (GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnFurnitureCollapsed -= OnFurnitureCollapsedHandler;
        }
    }

    private bool TryGetFreePosition(out VFXPositionElement position)
    {
        position = textEffectsCollection.Where(x => x.IsVisible == false).GetRandomElement();
        return position != null;
    }

    #endregion

    #region Enums



    #endregion
}