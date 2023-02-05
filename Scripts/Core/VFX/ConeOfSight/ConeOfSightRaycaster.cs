using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ConeOfSightRaycaster : MonoBehaviour
{
	public GameObject ConeOfSight;
	public float SightAngle;
	public float MaxDistance;
	public bool DrawDebug;
	public float rotationDegree = 180;

    [SerializeField]
	private UnityEvent onTriggerEnterRange = new UnityEvent();
	[SerializeField]
	private UnityEvent onTriggerExitRange = new UnityEvent();

	[SerializeField]
	private LayerMask cullingMask;
	[SerializeField]
	private LayerMask triggerMask;

	private const int BUFFER_SIZE = 256;
	private Quaternion m_Rotation;
	private float[] m_aDepthBuffer;
	private Material m_ConeOfSightMat;
	private bool wasTriggerinRange = false;

    void Start()
	{
		Renderer r = ConeOfSight.GetComponent<Renderer>();
		m_ConeOfSightMat = r.material;

		m_aDepthBuffer = new float[BUFFER_SIZE];
	}

	void Update()
	{
		m_Rotation = this.transform.rotation;
		UpdateViewDepthBuffer();
	}

	void UpdateViewDepthBuffer()
	{
		float anglestep = SightAngle / BUFFER_SIZE;
		float viewangle = m_Rotation.eulerAngles.y + rotationDegree;
		int bufferindex = 0;

		bool isTriggerInRange = false;

		for (int i = 0; i < BUFFER_SIZE; i++)
		{
			float angle = anglestep * i + (viewangle - SightAngle / 2);


			Vector3 dest = GetVector(-angle * Mathf.PI / 180, MaxDistance);
			Ray r = new Ray(this.transform.position, dest);

			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(r, out hit, Mathf.Infinity, cullingMask.value))
			{
				m_aDepthBuffer[bufferindex] = (hit.distance / MaxDistance);
			}
			else
			{
				m_aDepthBuffer[bufferindex] = -1;
				if (DrawDebug)
                {
					Debug.DrawRay(this.transform.position, dest);
				}
			}

			if(hit.distance > MaxDistance
				&& !isTriggerInRange
				&& Physics.Raycast(r, out hit, MaxDistance, triggerMask.value) == true)
            {
				isTriggerInRange = true;
			}

			bufferindex++;

		}

		if (isTriggerInRange && !wasTriggerinRange)
		{
			wasTriggerinRange = true;
			onTriggerEnterRange.Invoke();
		}
		else if (!isTriggerInRange && wasTriggerinRange)
		{
			wasTriggerinRange = false;
			onTriggerExitRange.Invoke();
		}

		m_ConeOfSightMat.SetFloatArray("_SightDepthBuffer", m_aDepthBuffer);
	}

	Vector3 GetVector(float angle, float dist)
	{
		float x = Mathf.Cos(angle) * dist;
		float z = Mathf.Sin(angle) * dist;
		return new Vector3(x, 0, z);
	}

#if UNITY_EDITOR

	void OnDrawGizmos()
	{
		UnityEditor.Handles.DrawWireArc(this.transform.localPosition, this.transform.up, Vector3.right, 360, MaxDistance);

		float halfangle = SightAngle / 2 * Mathf.PI / 180;
		float viewangle = (this.transform.rotation.eulerAngles.y  + rotationDegree) * Mathf.PI / 180;

		Vector3 p1 = GetVector(-halfangle - viewangle, MaxDistance);
		Vector3 p2 = GetVector(halfangle - viewangle, MaxDistance);

		
		Debug.DrawRay(this.transform.position, p1, Color.green);
		Debug.DrawRay(this.transform.position, p2, Color.green);
	}

#endif
}