using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSingletonBase<T> : MonoBehaviour, IManager where T : MonoBehaviour
{
	#region Fields

	private static T instance;

	#endregion
	#region Propeties

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (T)FindObjectOfType(typeof(T));
			}
			return instance;
		}
		private set
		{
			instance = value;
		}
	}

	#endregion
	#region Methods

	/// <summary>
	/// Ladowanie elementow sceny po przejsciu na nowa scene.
	/// </summary>
	public virtual void LoadContent()
	{

	}

	/// <summary>
	/// Rozladowanie trzymanych elementow po opuszczeniu swojej sceny.
	/// </summary>
	public virtual void UnloadContent()
	{
		
	}

	/// <summary>
	/// Inicjalizacja po utworzeniu managera.
	/// Miejsce na zaciagniecie dependencji oraz DB.
	/// </summary>
	public virtual void Initialize()
	{
		AttachEvents();
	}

	public virtual void AttachEvents()
	{
		
	}

	public string GetLocalizedKey()
	{
		ManagersContentSetup managersContent = ManagersContentSetup.Instance;
		if (managersContent != null)
		{
			return managersContent.GetLocalizedKeyByType(GetType());
		}

		return string.Empty;
	}

	public bool HasContentOnScene(SceneLabel sceneLabel)
	{
		ManagersContentSetup managersContent = ManagersContentSetup.Instance;
		if(managersContent != null)
		{
			SceneLabel label = managersContent.GetSceneLabelByManagerType(GetType());
			if(label.HasFlag(SceneLabel.NO_SET) || label.HasFlag(sceneLabel))
			{
				return true;
			}
		}

		return false;
	}

	public SceneLabel GetSceneLifeLabel()
	{
		SceneLabel label = SceneLabel.NO_SET;
		ManagersContentSetup managersContent = ManagersContentSetup.Instance;
		if (managersContent != null)
		{
			label = managersContent.GetSceneLabelByManagerType(GetType());
		}

		return label;
	}

	protected virtual void Awake()
	{
		instance = GetComponent<T>();
	}

	protected virtual void Start()
	{
		BrodcastEvents();
	}

	protected virtual void OnEnable()
	{
		SceneManager.sceneLoaded += ActualSceneChanged;
	}

	protected virtual void OnDisable()
	{
		SceneManager.sceneLoaded -= ActualSceneChanged;
		DetachEvents();
	}

	protected virtual void DetachEvents()
	{
		
	}

	protected virtual void BrodcastEvents()
	{

	}

	protected virtual void OnSceneSwitched(Scene scene)
	{

	}

	private void ActualSceneChanged(Scene scene, LoadSceneMode mode)
	{
		OnSceneSwitched(scene);
	}

	#endregion
	#region Handlers

	#endregion
}
