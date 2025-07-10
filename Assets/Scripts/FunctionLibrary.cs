using UnityEngine;

// Import Mathf methods statically for cleaner syntax (e.g., use Sin instead of Mathf.Sin)
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    // Delegate type for functions that take (x, t) and return y (float)
    public delegate float Function(float x, float t);

    // enum of function names for dropdown
    public enum FunctionNames { Wave, MultiWave, Ripple }

    // Array of functions
    static Function[] functions = { Wave, MultiWave, Ripple };

    // Selects and returns a function based on the index value
    public static Function GetFunction(FunctionNames name)
    {
        return functions[(int)name];
    }

    // A basic sine wave: y = sin(π * (x + t))
    public static float Wave(float x, float t)
    {
        return Sin(PI * (x + t));
    }

    // A composite wave made of two sine waves at different frequencies and amplitudes
    public static float MultiWave(float x, float t)
    {
        float y = Sin(PI * (x + 0.5f * t));        // Slower sine wave
        y += 0.5f * Sin(2f * PI * (x + t));        // Faster sine wave with lower amplitude
        return y * (2f / 3f);                      // Normalize amplitude to stay within [-1,1]
    }

    // A ripple wave: wave that moves outward with diminishing amplitude based on distance
    public static float Ripple(float x, float t)
    {
        float d = Abs(x);                          // Distance from center (origin)
        float y = Sin(PI * (4f * d - t));          // Phase shift makes wave appear to move outward
        return y / (1f + 10f * d);                 // Dampen amplitude as distance increases
    }
}
