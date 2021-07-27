using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PerformanceSDK
{
   /// <summary>
   /// Shows the rolling average FPS
   /// </summary>
   public class FpsCounter : MonoBehaviour
   {
      public float avgFps;

      private int historyIdx = 0;
      private float[] fpsHistory;

      private void Start()
      {
         fpsHistory = new float[50];
      }

      private void Update()
      {
         fpsHistory[historyIdx] = 1f / Time.unscaledDeltaTime;

         historyIdx++;

         if (historyIdx >= fpsHistory.Length)
         {
            historyIdx = 0;
         }

         var total = 0f;
         int numWithValue = 0;

         for (int i = 0; i < fpsHistory.Length; ++i)
         {
            total += fpsHistory[i];
            if (fpsHistory[i] > 0f)
            {
               numWithValue++;
            }
         }

         avgFps = total / numWithValue;

      }
   }
}
