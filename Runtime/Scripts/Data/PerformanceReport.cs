using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PerformanceSDK
{
   /// <summary>
   /// Provides a list of performance reports in the form of a dictionary.
   /// Dictionary keys are represented by DateTime.UTC string timestamps.
   /// </summary>
   public class PerformanceReportData : IData
	{
      public Dictionary<string, PerformanceData> reports = new Dictionary<string, PerformanceData>();
      public DateTime startDate { get; set; }
		public DateTime endDate;
      public float averageFPS;

      public PerformanceReportData()
      {
         startDate = DateTime.UtcNow;
      }

      public void EndReport()
      {
         endDate = DateTime.UtcNow;
         foreach (var report in reports.Values)
         {
            averageFPS += report.fps;
         }
         averageFPS /= reports.Count;
      }
   }
}
