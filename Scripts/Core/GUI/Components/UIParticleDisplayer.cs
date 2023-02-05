using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class UIParticleDisplayer : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Image particleElementPrefab;
    [SerializeField]
    private int maxParticles = 50;

    [Space]
    [SerializeField]
    private float moveTimeS = 0.25f;
    [SerializeField]
    private float fadeToMoveFactor = 0.35f;
    [SerializeField]
    private float rootScaleAtEndMultiply = 1.1f;
    [SerializeField]
    private float rootScaleAtEndDurationS = 0.1f;

    #endregion

    #region Propeties

    public Image ParticleElementPrefab { get => particleElementPrefab;}
    public int MaxParticles { get => maxParticles; }
    public float MoveTimeS { get => moveTimeS; }
    public float FadeToMoveFactor { get => fadeToMoveFactor; }
    public float RootScaleAtEndMultiply { get => rootScaleAtEndMultiply; }
    public float RootScaleAtEndDurationS { get => rootScaleAtEndDurationS; }

    // Variables.
    private Stack<Image> PooledParticles { get; set; } = new Stack<Image>();
    private Camera CurrentCamera { get; set; }
    private int ParticlesToShow { get; set; }
    private Coroutine DisplayParticlesHandler { get; set; } = null;

    #endregion

    #region Methods

    private void Awake()
    {
        CurrentCamera = Camera.main;

        for (int i = 0; i < MaxParticles; i++)
        {
            Image particle = Instantiate(ParticleElementPrefab, transform);
            particle.gameObject.SetActive(false);
            PooledParticles.Push(particle);
        }
    }

    private void OnEnable()
    {
        if (GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnCollectMoney += OnCollectMoneyHandler;
        }
    }

    private void OnDisable()
    {
        if(GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnCollectMoney -= OnCollectMoneyHandler;
        }
    }

    private void OnCollectMoneyHandler(Vector3 worldPosition, int ammount)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(CurrentCamera, worldPosition);
        DisplayParticles(screenPoint, ammount / 2);
    }

    private void DisplayParticles(Vector3 startPosition, int ammount)
    {
        ParticlesToShow += ammount;
        ParticlesToShow = Mathf.Clamp(ammount, 0, PooledParticles.Count);

        if(DisplayParticlesHandler != null)
        {
            StopCoroutine(DisplayParticlesHandler);
        }

        DisplayParticlesHandler = StartCoroutine(_ShowParticles(startPosition));
    }

    private IEnumerator _ShowParticles(Vector3 position)
    {
        for (int i = 0; i < ParticlesToShow; i++)
        {
            Image particle = PooledParticles.Pop();
            particle.transform.position = position;
            particle.gameObject.SetActive(true);

            particle.color = new Color(particle.color.r, particle.color.g, particle.color.b, 0f);
            particle.DOFade(1f, MoveTimeS * FadeToMoveFactor);

            particle.transform.DOMove(transform.position, MoveTimeS).OnComplete(() =>
            {
                particle.gameObject.SetActive(false);
                PooledParticles.Push(particle);
                transform.DOKill(true);
                transform.DOScale(RootScaleAtEndMultiply, RootScaleAtEndDurationS).SetLoops(2, LoopType.Yoyo);
            });

            yield return null;
        }

        yield return null;
    }

    #endregion

    #region Enums



    #endregion
}
