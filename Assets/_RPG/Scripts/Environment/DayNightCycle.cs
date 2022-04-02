
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    public AnimationCurve intensityMultiplier;
    public AnimationCurve reflectionMultiplier;


    private void Update()
    {
        float timeOfDay = TimeManager.DayTime();
        UpdateSunLight(timeOfDay);
        UpdateMoonLight(timeOfDay);
    }

    private void UpdateSunLight(float time)
    {
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4;
        sun.intensity = sunIntensity.Evaluate(time);
        sun.color = sunColor.Evaluate(time);

        if (sun.intensity == 0 && sun.gameObject.activeSelf)
        {
            sun.gameObject.SetActive(false);
        }
        else if (sun.intensity > 0 && !sun.gameObject.activeSelf)
        {
            sun.gameObject.SetActive(true);

        }
    }

    private void UpdateMoonLight(float time)
    {
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4;
        moon.intensity = moonIntensity.Evaluate(time);
        moon.color = moonColor.Evaluate(time);

        if (moon.intensity == 0 && moon.gameObject.activeSelf)
        {
            moon.gameObject.SetActive(false);
        }
        else if (moon.intensity > 0 && !moon.gameObject.activeSelf)
        {
            moon.gameObject.SetActive(true);

        }
    }
}
