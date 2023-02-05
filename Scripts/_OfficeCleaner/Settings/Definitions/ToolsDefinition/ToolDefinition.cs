using UnityEngine;

[CreateAssetMenu(fileName = "New ToolDefinition.asset", menuName = "Custom/ToolDefinition")]
public class ToolDefinition : ScriptableObject
{
    #region Fields

    [SerializeField]
    private ToolType typeOfTool;
    [SerializeField]
    private Sprite iconSprite;
    [SerializeField]
    private GameObject visualizationPrefab;

    [Header("Animations")]
    [SerializeField]
    private string runBoolName;

    #endregion

    #region Propeties

    public ToolType TypeOfTool { get => typeOfTool; }
    public Sprite IconSprite { get => iconSprite; }
    public string RunBoolName { get => runBoolName; }
    public GameObject VisualizationPrefab { get => visualizationPrefab; }

    #endregion

    #region Methods



    #endregion

    #region Enums



    #endregion
}
