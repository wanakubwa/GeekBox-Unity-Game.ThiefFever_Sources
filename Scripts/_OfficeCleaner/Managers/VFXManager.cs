using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : ManagerSingletonBase<VFXManager>
{
    #region Fields

    [SerializeField]
    private int footprintPoolSize = 25;
    [SerializeField]
    private FootstepParticleVFX footprintPrefab;
    [SerializeField]
    private Transform footprintParent;

    #endregion

    #region Propeties

    private Stack<FootstepParticleVFX> FootstepsParticlesPool { get; set; } = new Stack<FootstepParticleVFX>();

    #endregion

    #region Methods

    public void SpawnFootprintParticle(Transform source, Vector3 scaleModifier, float sidePositionDeviation)
    {
        FootstepParticleVFX footPrint = null;
        if(FootstepsParticlesPool.Count > Constants.DEFAULT_VALUE)
        {
            footPrint = FootstepsParticlesPool.Pop();
        }
        else
        {
            footPrint = GetNewFootprint();
        }

        Vector3 footPrintPosition = source.position;
        footPrint.transform.position = footPrintPosition;
        footPrint.transform.localPosition = footPrint.transform.localPosition + new Vector3(0f, 0f, scaleModifier.x * sidePositionDeviation);

        Vector3 footPrintRotationEuler = footPrint.transform.rotation.eulerAngles;
        footPrint.transform.rotation = Quaternion.Euler(footPrintRotationEuler.x, source.rotation.eulerAngles.y, footPrintRotationEuler.z);

        // Ustawienie skali w odniesieniu do prefabu poniewaz zespawnowany obiekt z poola mogl byc wczesniej zmieniony.
        footPrint.transform.localScale = footprintPrefab.transform.localScale.Mul(scaleModifier);
        footPrint.gameObject.SetActive(true);
        footPrint.Init(OnFootprintParticleDisabled);
    }

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < footprintPoolSize; i++)
        {
            FootstepParticleVFX footPrint = GetNewFootprint();
            FootstepsParticlesPool.Push(footPrint);
        }
    }

    private FootstepParticleVFX GetNewFootprint()
    {
        FootstepParticleVFX footPrint = Instantiate(footprintPrefab, footprintParent);
        footPrint.gameObject.SetActive(false);

        return footPrint;
    }

    private void OnFootprintParticleDisabled(FootstepParticleVFX footstepParticle)
    {
        footstepParticle.gameObject.SetActive(false);
        FootstepsParticlesPool.Push(footstepParticle);
    }

    #endregion

    #region Enums



    #endregion
}