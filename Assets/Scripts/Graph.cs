using UnityEngine;

public class Graph : MonoBehaviour
{
    // Prefab to represent each point on the graph
    [SerializeField]
    Transform pointPrefab;

    // Number of points (horizontal resolution of the graph)
    [SerializeField, Range(10, 100)]
    int resolution = 10;

    // Index to select the wave function from FunctionLibrary (e.g., Wave, MultiWave, Ripple)
    [SerializeField]
    FunctionLibrary.FunctionNames function;

    // Array to store all point instances
    Transform[] points;

    // Called when the script instance is being loaded
    void Awake()
    {
        float step = 2f / resolution; // Distance between points on x-axis
        var position = Vector3.zero;  // Temp variable to assign positions
        var scale = Vector3.one * step; // Uniform scale for each point

        points = new Transform[resolution]; // Initialize array based on resolution
        for (int i = 0; i < points.Length; i++)
        {
            // Instantiate point prefab and store it in the array
            Transform point = points[i] = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f; // Calculate x position centered around 0
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false); // Make point a child of the graph object
        }
    }

    // Called once per frame — updates point positions based on the selected function
    void Update()
    {
        // Get the selected mathematical function based on the slider (0 = Wave, 1 = MultiWave, etc.)
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);

        float time = Time.time; // Get current time to animate wave
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;

            // Update y position using the selected wave function and current time
            position.y = f(position.x, time);

            // Apply the updated position
            point.localPosition = position;
        }
    }
}
