using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
namespace ECSTest
{
	public class MonoTest : MonoBehaviour
	{
		public GameObject gameObj;

		public int Count = 0;

		public float offset = 0;

		public float radius = 10;

		List<GameObject> list = new List<GameObject>();

		// Start is called before the first frame update
		void Start()
		{
			//ƽ���ֲ���һ��Բ��
			for (int i = 0; i < Count; i++)
			{
				GameObject gameObject = GameObject.Instantiate(gameObj);
				gameObject.transform.SetParent(transform, false);
				float angle = ((i / (float)Count) * 360f + offset);
				gameObject.transform.position = ToVector2(angle) * radius;
				gameObject.transform.eulerAngles = new Vector3(0, angle, 0);
				list.Add(gameObject);
			}
		}

		// Update is called once per frame
		void Update()
		{
			Profiler.BeginSample("SDHK MonoTest");
			for (int i = 0; i < list.Count; i++)
			{
				Transform trans = list[i].transform;
				float angle = (trans.eulerAngles.y + offset);
				trans.position = ToVector2(angle) * radius;
				trans.eulerAngles = new Vector3(0, angle, 0);
			}
			Profiler.EndSample();
		}

		/// <summary>
		/// �Ƕ�ת����
		/// </summary>
		public Vector2 ToVector2(float angle) => new Vector2(Mathf.Sin(angle / Mathf.Rad2Deg), Mathf.Cos(angle / Mathf.Rad2Deg));

		/// <summary>
		/// ����ת�Ƕ� +-180
		/// </summary>
		public float ToAngle(Vector2 vector) => Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
	}
}