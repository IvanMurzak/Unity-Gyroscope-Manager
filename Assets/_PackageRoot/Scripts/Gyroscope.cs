using UnityEngine;

namespace UnityGyroscope.Manager
{
	public class Gyroscope : MonoBehaviour
	{
		public const int DefaultSamplingFrequency = 32;

		private static IGyroscopeManager instance;
		public static IGyroscopeManager Instance
        {
			get
            {
				if (instance == null)
					instance = Init(new FakeGyroscopeManager.Settings(), Application.isEditor);
				return instance;
            }
        }

		[SerializeField] int samplingFrequency = DefaultSamplingFrequency;
		[SerializeField] bool useFakeGyroscopeInEditor = true;
		[SerializeField] FakeGyroscopeManager.Settings fakeGyroscopeSettings = new FakeGyroscopeManager.Settings();

		void Awake()
        {
			if (instance == null)
				instance = Init(fakeGyroscopeSettings, useFakeGyroscopeInEditor && Application.isEditor, samplingFrequency);
        }

		static IGyroscopeManager Init(FakeGyroscopeManager.Settings fakeGyroscopeSettings, bool fake = false, int samplingFrequency = DefaultSamplingFrequency)
		{
			Debug.Log($"Gyroscope.Init fake={fake}, samplingFrequency={samplingFrequency}");

			var gyroscopeManager = fake ?
				new GameObject().SetName("Fake Gyroscope Manager").GetOrAddComponent<FakeGyroscopeManager>() as IGyroscopeManager :
				new GameObject().SetName("Gyroscope Manager").GetOrAddComponent<GyroscopeManager>() as IGyroscopeManager;

			gyroscopeManager.SamplingFrequency = samplingFrequency;

			if (fake) ((FakeGyroscopeManager)gyroscopeManager).settings = fakeGyroscopeSettings;

			gyroscopeManager.gameObject.transform.parent = null;
			GameObject.DontDestroyOnLoad(gyroscopeManager.gameObject);
			return gyroscopeManager;
		}
	}

	static class GameObjectEx
    {
		public static GameObject SetName(this GameObject gameObject, string name)
        {
			gameObject.name = name;
			return gameObject;
        }
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
			var result = gameObject.GetComponent<T>();
			if (result == null)
				result = gameObject.AddComponent<T>();
			return result;
        }
    }
}
