using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ManagersContentSetup.asset", menuName = "Settings/ManagersContentSetup")]
public class ManagersContentSetup : ScriptableObject
{
    #region Fields

    private static ManagersContentSetup instance;

    [SerializeField]
    private List<ManagerElement> managersCollection = new List<ManagerElement>();

    [Space]
    [SerializeField]
    private List<SceneInfo> scenesInfoCollection = new List<SceneInfo>();

    #endregion

    #region Propeties

    public static ManagersContentSetup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<ManagersContentSetup>("Settings/ManagersContentSetup");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<ManagerElement> ManagersCollection
    {
        get => managersCollection;
        private set => managersCollection = value;
    }

    public List<SceneInfo> ScenesInfoCollection { 
        get => scenesInfoCollection; 
        private set => scenesInfoCollection = value; 
    }

    #endregion

    #region Methods

    public SceneLabel GetSceneLabelBySceneIndex(int buildIndex)
    {
        for(int i = 0; i < ScenesInfoCollection.Count; i++)
        {
            if(ScenesInfoCollection[i].SceneIndex == buildIndex)
            {
                return ScenesInfoCollection[i].Label;
            }
        }

        Debug.LogFormat("Cant find scene of build index: [{0}] !".SetColor(Color.yellow), buildIndex);
        return SceneLabel.NO_SET;
    }

    public int GetSceneIndexByLabel(SceneLabel label)
    {
        for (int i = 0; i < ScenesInfoCollection.Count; i++)
        {
            if (ScenesInfoCollection[i].Label == label)
            {
                return ScenesInfoCollection[i].SceneIndex;
            }
        }

        Debug.LogErrorFormat("Cant find scene of label: [{0}] !", label);
        return Constants.DEFAULT_INDEX;
    }

    public SceneLabel GetSceneLabelByManagerType(Type managerType)
    {
        if (ManagersCollection == null)
        {
            return SceneLabel.NO_SET;
        }

        for (int i = 0; i < ManagersCollection.Count; i++)
        {
            if (ManagersCollection[i].ManagerType == managerType.ToString())
            {
                return ManagersCollection[i].SceneExist;
            }
        }

        return SceneLabel.NO_SET;
    }

    public string GetLocalizedKeyByType(Type type)
    {
        if (ManagersCollection == null)
        {
            return string.Empty;
        }

        for(int i =0; i < ManagersCollection.Count; i++)
        {
            if(ManagersCollection[i].ManagerType.Equals(type.Name) == true)
            {
                return ManagersCollection[i].LocalizedKey;
            }
        }

        return string.Empty;
    }

    private void OnEnable()
    {

#if UNITY_EDITOR
        RefreshManagersCollection();
#endif

    }

#if UNITY_EDITOR

    [Button]
    private void RefreshManagersCollection()
    {
        List<Type> managersTypes = GetManagerTypesInAssembly();

        if (ManagersCollection == null)
        {
            foreach (Type managerType in managersTypes)
            {
                ManagersCollection.Add(new ManagerElement(managerType.ToString()));
            }
        }
        else
        {
            foreach (Type managerType in managersTypes)
            {
                if (ManagersCollection.Exists(x => x.ManagerType == managerType.ToString()) == false)
                {
                    ManagersCollection.Add(new ManagerElement(managerType.ToString()));
                }
            }

            foreach (ManagerElement element in ManagersCollection)
            {
                if (managersTypes.Exists(x => x.ToString() == element.ManagerType) == false)
                {
                    ManagersCollection.Remove(element);
                }
            }
        }
    }

    private static List<Type> GetManagerTypesInAssembly()
    {
        Type managerBaseType = typeof(ManagerSingletonBase<>);

        var lookup = typeof(ManagerSingletonBase<>);
        List<Type> output = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsInheritedFrom(lookup))
            .ToList();

        return output;
    }

#endif

    #endregion

    #region Handlers



    #endregion

    [Serializable]
    public class ManagerElement
    {
        #region Fields

        [SerializeField, Sirenix.OdinInspector.ReadOnly]
        private string managerType;
        [SerializeField]
        SceneLabel sceneExist = SceneLabel.NO_SET;
        [SerializeField]
        private string localizedKey = string.Empty;

        #endregion

        #region Propeties

        public string ManagerType
        {
            get => managerType;
            private set => managerType = value;
        }

        public string LocalizedKey
        {
            get => localizedKey;
            private set => localizedKey = value;
        }

        public SceneLabel SceneExist
        {
            get => sceneExist;
            private set => sceneExist = value;
        }

        #endregion

        #region Methods

        public ManagerElement(string type)
        {
            ManagerType = type;
        }

        #endregion

        #region Handlers



        #endregion
    }

    [Serializable]
    public class SceneInfo
    {
        #region Fields

        [SerializeField]
        private int sceneIndex;
        [SerializeField]
        private SceneLabel label;

        #endregion

        #region Propeties

        public int SceneIndex { 
            get => sceneIndex; 
            private set => sceneIndex = value; 
        }

        public SceneLabel Label { 
            get => label; 
            private set => label = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Enums



        #endregion
    }
}
