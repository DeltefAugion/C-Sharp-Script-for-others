using UnityEngine;
using System.Diagnostics;

public class PerformanceWatchdog : MonoBehaviour {
    [SerializeField] private MonoBehaviour[] targetScripts; // Ziehe hier die Verdächtigen scripts rein!!!!!!

    void Update() {
        foreach (var script in targetScripts) {
            if (script == null || !script.enabled) continue;

            Stopwatch sw = Stopwatch.StartNew();
            // Hier soll dein script rein der getestet wird 
            // script.ManualUpdate(); 
            sw.Stop();

            if (sw.Elapsed.TotalMilliseconds > 1.0f) { // Alarm bei > 1ms dann sieht man was deine frames runter macht
                UnityEngine.Debug.LogWarning($"{script.GetType().Name} frisst {sw.Elapsed.TotalMilliseconds}ms!");
            }
        }
    }
}
