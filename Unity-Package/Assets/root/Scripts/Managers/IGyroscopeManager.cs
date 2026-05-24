using UnityEngine;

namespace UnityGyroscope.Manager
{ 
	public interface IGyroscopeManager
	{
		bool		HasGyroscope		{ get; }
		int			SamplingFrequency	{ get; set; }
		Quaternion? Attitude			{ get; }
		Quaternion? AttitudeConverted	{ get; }
		Vector3?	Gravity				{ get; }
		GameObject	gameObject			{ get; }

		void SubscribeGravity();
		void UnsubscribeGravity();
		void SubscribeAttitude();
		void UnsubscribeAttitude();
	}
}