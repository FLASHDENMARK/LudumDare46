﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class Utility
    {
        internal static float GetAngle(Transform transform)
        {
            Vector3 target = transform.position;
            target.y = 0;
            Vector3 tra = transform.position;
            tra.y = 0;

            float cosAngle = Vector3.Dot((target - tra).normalized, transform.forward);
            float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

            if (float.IsNaN(angle))
            {
                angle = 0;
            }

            return angle;
        }

        public static float GetAngle(Transform from, Transform to)
        {
            Vector3 target = to.position;
            target.y = 0;
            Vector3 tra = from.position;
            tra.y = 0;

            float cosAngle = Vector3.Dot((target - tra).normalized, from.forward);
            float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

            if (float.IsNaN(angle))
            {
                angle = 0;
            }

            return angle;
        }

        internal static void PlayAudio(AudioClip[] audio, GameObject gameObject)
        {
            if (audio.Length == 0)
            {
                Debug.LogWarning("Calling play audio with not audio array");
                return;
            }

            System.Random r = new System.Random();

            int index = r.Next(0, audio.Length);
            PlayAudio(audio[index], gameObject);
        }

        public static void PlayAudio (AudioClip audio, GameObject gameObjectToPlay)
        {
            if (audio == null)
            {
                Debug.LogWarning("Calling play audio with not audio array");
                return;
            }

            gameObjectToPlay.GetComponent<AudioSource>().PlayOneShot(audio);
        }
    }
}
