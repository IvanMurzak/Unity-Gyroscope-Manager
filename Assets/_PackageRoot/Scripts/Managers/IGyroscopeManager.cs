using UnityEngine;

namespace UnityGyroscope.Manager
{ 
	public interface IGyroscopeManager
	{
		bool		HasGyroscope		{ get; }
		Quaternion? Attitude			{ get; }
		Quaternion? AttitudeConverted	{ get; }
		Vector3?	Gravity				{ get; }
		GameObject	gameObject			{ get; }

		void Subscribe();
		void Unsubscribe();
	}
}