using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.TextCore.Text;

namespace ECSTest
{
	public class MonoTest : MonoBehaviour
	{
		public GameObject gameObj;

		public int Count = 0;

		public float offset = 0;

		public float radius = 10;

		List<GameObject> list = new List<GameObject>();

		private int fps = 0;
		private int frameNumber = 0;
		private float lastShowFPSTime = 0f;
		private GUIStyle textStyle = new GUIStyle();

		private void Awake()
		{
			QualitySettings.vSyncCount = 0;//�رմ�ֱͬ��
			Application.targetFrameRate = -1;//���֡������
		}

		// Start is called before the first frame update
		void Start()
		{

			textStyle.normal.textColor = Color.red;
			textStyle.fontSize = 60;

			System.Random rand = new System.Random();

			//ƽ���ֲ���һ��Բ��
			for (int i = 0; i < Count; i++)
			{
				float randRadius = (float)(rand.NextDouble() * (radius - 1) + 1);

				GameObject gameObject = GameObject.Instantiate(gameObj);
				gameObject.transform.SetParent(transform, false);
				float angle = ((i / (float)Count) * 360f + offset);
				gameObject.transform.position = ToVector2(angle) * randRadius;
				gameObject.transform.eulerAngles = new Vector3(randRadius, angle, 0);
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
				trans.position = ToVector2(angle) * trans.eulerAngles.x;
				trans.eulerAngles = new Vector3(trans.eulerAngles.x, angle, 0);
			}
			Profiler.EndSample();

			frameNumber += 1;//ÿ��֡���ۼ�
			float time = Time.realtimeSinceStartup - lastShowFPSTime;
			if (time >= 1)//����һ���
			{
				fps = (int)(frameNumber / time);//����֡��
				frameNumber = 0;//����
				lastShowFPSTime = Time.realtimeSinceStartup;
			}
		}


		private void OnGUI()
		{
			GUILayout.Label(fps.ToString(), textStyle);
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