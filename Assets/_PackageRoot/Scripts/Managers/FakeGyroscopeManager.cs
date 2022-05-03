using System;
using UnityEngine;

namespace UnityGyroscope.Manager
{ 
    public class FakeGyroscopeManager : MonoBehaviour, IGyroscopeManager
    {
        private static  Quaternion      ToUnityQuaternion(Quaternion q) => new Quaternion(q.x, q.y, -q.z, -q.w);

        public          bool            HasGyroscope                    => settings.hasGyroscope;

        public          Quaternion?     Attitude                        => (Quaternion?)Quaternion.Euler(settings.attitude.x, settings.attitude.y, settings.attitude.z);
        public          Quaternion?     AttitudeConverted               => (Quaternion?)ToUnityQuaternion(Attitude.Value);
        public          Vector3?        Gravity                         => (Vector3?)settings.gravity;

        public          Settings        settings                        = new Settings();

        public void Subscribe() { }
        public void Unsubscribe() { }

        [Serializable]
        public class Settings
	    {
            public bool     hasGyroscope    = true;
            public Vector3  attitude        = new Vector3(0, 0, 0);
            public Vector3  gravity         = new Vector3(0, 0, 0);
	    }
    }
}