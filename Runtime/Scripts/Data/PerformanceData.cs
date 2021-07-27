using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PerformanceSDK
{
   /// <summary>
   /// Data on apps performance for a given frame.
   /// </summary>
   [Serializable]
   public class PerformanceData : IData
   {
      public float fps;
      public int frameCount;

      public DateTime startDate { get; set; }

      public PerformanceData()
      {
         startDate = DateTime.UtcNow;
      }

      public PerformanceData(DateTime timeStampm, float fps, int frameCount)
      {
         startDate = timeStampm;
         this.fps = fps;
         this.frameCount = frameCount;
      }

      public string GetTimeStamp()
      {
         string datePatt = @"M/d/yyyy hh:mm:ss tt";
         return startDate.ToString(datePatt);
      }
   }
}
