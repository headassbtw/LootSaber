using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace LootSaber.Extensions
{
    internal static class MovementExtensions
    {
        #region GameObject movement
        public static void Move(this Transform transform, Vector3 newpos, float time = 1, EasingFunction.Ease easing = EasingFunction.Ease.Linear)
        {
            LootSaberController.Instance.StartCoroutine(Mover(transform, newpos, time, easing));
        }
        public static void Move(this Transform transform, float newX, float newY, float newZ, float time = 1, EasingFunction.Ease easing = EasingFunction.Ease.Linear)
        {
            Vector3 tv3 = new Vector3(newX, newY, newZ);
            LootSaberController.Instance.StartCoroutine(Mover(transform, tv3, time, easing));
        }
        public static IEnumerator Mover(Transform transform, Vector3 newpos, float time, EasingFunction.Ease easing)
        {
            float i = 0.0f;
            float sT = Time.time;
            while (i < 1.0f)
            {
                i = (Time.time - sT) / (time);

                EasingFunction.Ease ease = easing;
                EasingFunction.Function func = EasingFunction.GetEasingFunction(ease);

                float x = func(transform.position.x, newpos.x, i);
                float y = func(transform.position.y, newpos.y, i);
                float z = func(transform.position.z, newpos.z, i);

                transform.position = new Vector3(x, y, z);
                yield return null;
            }
        }
        #endregion
        public static void GoUp(this float value, float goal, float seconds = 1, EasingFunction.Ease easing = EasingFunction.Ease.Linear)
        {
            LootSaberController.Instance.StartCoroutine(Upper(value, goal, seconds, easing));
        }
        public static void GoUp(this int value, int goal, float seconds = 1, EasingFunction.Ease easing = EasingFunction.Ease.Linear)
        {
            LootSaberController.Instance.StartCoroutine(Upper(value, goal, seconds, easing));
        }
        public static IEnumerator Upper(float value, float goal, float seconds = 1, EasingFunction.Ease easing = EasingFunction.Ease.Linear)
        {
            float orig = value;
            float i = 0.0f;
            float sT = Time.time;
            while (i < 1.0f)
            {
                i = (Time.time - sT) / (seconds);

                EasingFunction.Ease ease = easing;
                EasingFunction.Function func = EasingFunction.GetEasingFunction(ease);

                value = func(orig, goal, i);
                yield return null;
            }
        }
    }
}
