using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VFXPositionElement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Image img;
    [SerializeField]
    private float scaleMultiplier = 1.1f;
    [SerializeField]
    private float animationTimeS = 2f;

    #endregion

    #region Propeties

    public Image Img {
        get => img;
    }

    public bool IsVisible { get; private set; } = false;

    #endregion

    #region Methods

    public void SetImage(Sprite sprite)
    {
        IsVisible = true;
        Img.sprite = sprite;

        DoAnimation();
    }

    private void DoAnimation()
    {
        gameObject.SetActive(true);

        transform.DOPunchScale(Vector3.one * scaleMultiplier, animationTimeS / 2f, 5);
        transform.rotation = RandomRangeRotationZUnity(-35, 35);
        img.DOFade(1f, animationTimeS).OnComplete(() => 
        {
            IsVisible = false;
            gameObject.SetActive(false);
        });
    }

    private Quaternion RandomRangeRotationZUnity(float minValue, float maxValue)
    {
        return Quaternion.Euler(0f, RandomMath.RandomRangeUnity(minValue, maxValue), 0f);
    }

    #endregion

    #region Enums



    #endregion
}