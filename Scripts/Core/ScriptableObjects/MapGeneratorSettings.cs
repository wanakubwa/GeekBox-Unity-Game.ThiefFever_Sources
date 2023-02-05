using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "MapGeneratorSettings.asset", menuName = "Settings/MapGeneratorSettings")]
public class MapGeneratorSettings : ScriptableObject
{
    #region Fields

    private static MapGeneratorSettings instance;

    [SerializeField]
    private Options settings = new Options();

    [Space]
    [SerializeField]
    private LvlMap[] mapsCollection;

    #endregion

    #region Propeties

    public static MapGeneratorSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<MapGeneratorSettings>("Settings/MapGeneratorSettings");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public LvlMap[] MapsCollection {
        get => mapsCollection;
    }
    public Options Settings {
        get => settings;
    }

    #endregion

    #region Methods

    public LvlMap GetMapForLvl(int lvl)
    {
        if(MapsCollection.Length == Constants.DEFAULT_VALUE)
        {
            return null;
        }

        int fixedIndex = (lvl - 1) % MapsCollection.Length;
        return MapsCollection.GetElementAtIndexSafe(fixedIndex);
    }

    #endregion

    #region Enums

    [Serializable]
    public class Options
    {
        [field: SerializeField]
        public bool IsUsingSegments { get; private set; } = true;
    }

    #endregion
}
