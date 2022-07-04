using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UInputSystem = UnityEngine.InputSystem.InputSystem;
#endif

namespace UnityGyroscope.Manager
{ 
    public class GyroscopeManager : MonoBehaviour, IGyroscopeManager
    {
        private static  Quaternion      ToUnityQuaternion(Quaternion q) => new Quaternion(q.x, q.y, -q.z, -q.w);

        private         int             gravitySubscribers              = 0;
        private         int             attitudeSubscribers             = 0;

        public          bool            HasGyroscope                    => SystemInfo.supportsGyroscope;
        public          int             SamplingFrequency               { get; set; } = Gyroscope.DefaultSamplingFrequency;

#if ENABLE_INPUT_SYSTEM
        public          Vector3?        Gravity                         => HasGyroscope ? GravitySensor.current?.gravity?.ReadValue()   : null;
        public          Quaternion?     Attitude                        => HasGyroscope ? AttitudeSensor.current?.attitude?.ReadValue() : null;
#else
        public          Vector3?        Gravity                         => HasGyroscope ? (Vector3?)    Input.gyro.gravity                   : null;
        public          Quaternion?     Attitude                        => HasGyroscope ? (Quaternion?) Input.gyro.attitude                  : null;
#endif
        public          Quaternion?     AttitudeConverted               => HasGyroscope ? (Quaternion?) ToUnityQuaternion(Attitude.Value)    : null;

        private void RefreshGyroState()
	    {
            gravitySubscribers = Math.Max(0, gravitySubscribers);
            attitudeSubscribers = Math.Max(0, attitudeSubscribers);

            if (!HasGyroscope)
            {
                Debug.LogError("Device does not support Gyroscope");
                return;
            }

#if ENABLE_INPUT_SYSTEM
            if (gravitySubscribers > 0)
		    {
                if (GravitySensor.current == null)
                {
                    Debug.LogError("GravitySensor.current == null");
                    return;
                }

                if (!GravitySensor.current.enabled)
                {
                    Debug.Log($"Enabling device: {GravitySensor.current.name}");
                    UInputSystem.EnableDevice(GravitySensor.current);
                }
                GravitySensor.current.samplingFrequency = SamplingFrequency;
                Debug.Log($"RefreshGyroState GravitySensor samplingFrequency: {SamplingFrequency}");
            }
            else
		    {
                if (GravitySensor.current.enabled)
                {
                    Debug.Log($"Disabling device: {GravitySensor.current.name}");
                    UInputSystem.DisableDevice(GravitySensor.current);
                }
            }
            if (attitudeSubscribers > 0)
            {
                if (AttitudeSensor.current == null)
                {
                    Debug.LogError("GravitySensor.current == null");
                    return;
                }

                if (!AttitudeSensor.current.enabled)
                {
                    Debug.Log($"Enabling device: {AttitudeSensor.current.name}");
                    UInputSystem.EnableDevice(AttitudeSensor.current);
                }
                AttitudeSensor.current.samplingFrequency = SamplingFrequency;
                Debug.Log($"RefreshGyroState AttitudeSensor samplingFrequency: {SamplingFrequency}");
            }
            else
            {
                if (AttitudeSensor.current.enabled)
                {
                    Debug.Log($"Disabling device: {AttitudeSensor.current.name}");
                    UInputSystem.DisableDevice(AttitudeSensor.current);
                }
            }
#else
            if (gravitySubscribers + attitudeSubscribers > 0)
            {
                Debug.Log($"Enabling device: Input.gyro");
                Input.gyro.enabled = true;
                Input.gyro.updateInterval = 1f / SamplingFrequency;
                Debug.Log($"RefreshGyroState Input.gyro.updateInterval: {Input.gyro.updateInterval}");
            }
            else
            {
                Debug.Log($"Disabling device: Input.gyro");
                Input.gyro.enabled = false;
            }
#endif
        }

        public void SubscribeGravity()
        {
            gravitySubscribers++;
            RefreshGyroState();
        }
        public void UnsubscribeGravity()
        {
            gravitySubscribers--;
            RefreshGyroState();
        }
        public void SubscribeAttitude()
        {
            attitudeSubscribers++;
            RefreshGyroState();
        }
        public void UnsubscribeAttitude()
        {
            attitudeSubscribers--;
            RefreshGyroState();
        }
    }
}