using UnityEngine;

namespace GeekBox.Scripts.Generic
{
	public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
		#region Fields

		private static T instance;

		#endregion

		#region Propeties



		#endregion

		#region Methods

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

		#region Enums



		#endregion

	}
}
