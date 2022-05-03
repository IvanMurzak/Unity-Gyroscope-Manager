using System;
using UnityEngine;

namespace UnityGyroscope.Manager
{ 
    public class GyroscopeManager : MonoBehaviour, IGyroscopeManager
    {
        private static  Quaternion      ToUnityQuaternion(Quaternion q) => new Quaternion(q.x, q.y, -q.z, -q.w);

        private         int             subscribers                     = 0;

        public          bool            HasGyroscope                    => SystemInfo.supportsGyroscope;
        public          int             SamplingFrequency               { get; set; } = 16;

#if ENABLE_INPUT_SYSTEM
        public          Vector3?        Gravity                         => HasGyroscope ? UnityEngine.InputSystem.GravitySensor.current?.gravity?.ReadValue()   : null;
        public          Quaternion?     Attitude                        => HasGyroscope ? UnityEngine.InputSystem.AttitudeSensor.current?.attitude?.ReadValue() : null;
#else
        public          Vector3?        Gravity                         => HasGyroscope ? (Vector3?)    Input.gyro.gravity                   : null;
        public          Quaternion?     Attitude                        => HasGyroscope ? (Quaternion?) Input.gyro.attitude                  : null;
#endif
        public          Quaternion?     AttitudeConverted               => HasGyroscope ? (Quaternion?) ToUnityQuaternion(Attitude.Value)    : null;

        private void RefreshGyroState()
	    {
            subscribers = Math.Max(0, subscribers);

            if (!HasGyroscope)
            {
                Debug.LogError("Device does not support Gyroscope");
                return;
            }

            if (subscribers > 0)
		    {
#if ENABLE_INPUT_SYSTEM
                UnityEngine.InputSystem.InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
                UnityEngine.InputSystem.Gyroscope.current.samplingFrequency = SamplingFrequency;
#else
                Input.gyro.enabled = true;
                Input.gyro.updateInterval = 1000f / SamplingFrequency;
#endif
            }
            else
		    {
#if ENABLE_INPUT_SYSTEM
                UnityEngine.InputSystem.InputSystem.DisableDevice(UnityEngine.InputSystem.Gyroscope.current);
#else
                Input.gyro.enabled = false;
#endif
            }
	    }

        public void Subscribe()
        {
            subscribers++;
            RefreshGyroState();
        }
        public void Unsubscribe()
        {
            subscribers--;
            RefreshGyroState();
        }
    }
}