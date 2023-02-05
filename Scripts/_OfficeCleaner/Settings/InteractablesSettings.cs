using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractablesSettings.asset", menuName = "Settings/InteractablesSettings")]
public class InteractablesSettings : ScriptableObject
{
    #region Fields

    private static InteractablesSettings instance;

    [SerializeField]
    private ToolDefinition[] toolDefinitions;

    #endregion

    #region Propeties

    public static InteractablesSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<InteractablesSettings>("Settings/InteractablesSettings");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public ToolDefinition[] ToolDefinitions { 
        get => toolDefinitions;
    }

    #endregion

    #region Methods

    public ToolDefinition GetToolDefinition(ToolType type)
    {
        ToolDefinition output = null;

        for (int i = 0; i < ToolDefinitions.Length; i++)
        {
            if (ToolDefinitions[i].TypeOfTool == type)
            {
                output = ToolDefinitions[i];
                break;
            }
        }

        return output;
    }

    #endregion

    #region Enums



    #endregion
}
