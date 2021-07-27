using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Diagnostics;
using System.IO;
using System;

namespace PerformanceSDK
{
	/// <summary>
	/// Monitors the performance of your scene by gathring FPS data on specified samnple rate.
	/// </summary>
   public class PerformanceMonitor : MonoBehaviour
   {
		[Tooltip("The rate at which data should be collected. Measured in secconds.")]
		[SerializeField] private float sampleRate = 1;

      private PerformanceReportData performanceReport;
      private string fileName = "performanceData.json";
      private string filePath;
      private FpsCounter fpsCounter;

      private static PerformanceMonitor _instance;
      public static PerformanceMonitor instance { get { return _instance; } }
      public void Awake()
      {
         if (_instance != null && _instance != this)
         {
            Destroy(this.gameObject);
         }
         else
         {
            _instance = this;
         }
         DontDestroyOnLoad(gameObject);
      }

      private void Start()
      {
         InvokeRepeating("GatherData", 1, sampleRate);
         fpsCounter = gameObject.AddComponent<FpsCounter>();
         performanceReport = new PerformanceReportData();
         filePath = Application.persistentDataPath + fileName;
         ReadReportData();
      }

      /// <summary>
      /// Returns a report of Performance data in a JSON format. 
      /// </summary>
      /// <returns>JSON data is in the form of a PerformanceReport, see PerformanceReport.cs for more details.</returns>
      [ContextMenu("Test")]
      public string GetPerformanceJSONData()
      {
         performanceReport.EndReport();
         UnityEngine.Debug.Log(JsonUtility.ToJson(performanceReport));
         return JsonUtility.ToJson(performanceReport);
      }

      /// <summary>
      /// Returns a report of Performance data.
      /// </summary>
      /// <returns>PerformanceReport</returns>
      public PerformanceReportData GetPerformanceData()
      {
         performanceReport.EndReport();
         return performanceReport;
      }

		/// <summary>
		/// Set the rate at which data is gathered.
		/// </summary>
		/// <param name="rate">float number representing secconds</param>
		public void SetSampleRate(float rate)
		{
			CancelInvoke();
			sampleRate = rate;
			InvokeRepeating("GatherData", 0, sampleRate);
		}


		private void GatherData()
      {
         PerformanceData data = new PerformanceData();
         data.fps = fpsCounter.avgFps;
         data.frameCount = Time.frameCount;

         performanceReport.reports.Add(data.GetTimeStamp(), data);
      }

      private void SaveReportData()
      {
         CheckIfFileExistance();
         string dataString = JsonUtility.ToJson(performanceReport);
         File.WriteAllText(filePath, dataString);
      }

      private void ReadReportData()
      {
         if (CheckIfFileExistance())
         {
            string fileData = File.ReadAllText(filePath);
            if(!string.IsNullOrEmpty(fileData))
               performanceReport = JsonUtility.FromJson<PerformanceReportData>(fileData);
         }
      }

      private bool CheckIfFileExistance()
      {
         if (!File.Exists(filePath))
         {
            File.Create(filePath).Close();
            return false;
         }

         return true;
      }

      private void OnDestroy()
      {
         SaveReportData();
      }
   }
}
