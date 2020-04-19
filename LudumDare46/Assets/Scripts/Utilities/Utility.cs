using System;
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
    }
}
