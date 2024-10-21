using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionCurve : MonoBehaviour
{
    public AnimationCurve emotionalCurve;
    private float timer;
    public Vector2[] keyframes;

    public Vector2 axis;

    public CurveState currentState;
    public enum CurveState
    {
        Up,
        Down,
        Normal,
    }

    public EmotionStatus emotionStatus;
    public enum EmotionStatus
    {
        Increasing,
        Decreasing,
        Stabilizing,
    }


    void Start()
    {
        if (keyframes.Length <= 1)
        {
            emotionalCurve = new AnimationCurve(
            new Keyframe(0, 0),         // 0s
            new Keyframe(15, 0f),
            new Keyframe(15, 0.5f),
            new Keyframe(30, 0.5f),
            new Keyframe(30, -1),
            new Keyframe(45, -1),
            new Keyframe(45, 1),       // 45s
            new Keyframe(60, 0.5f),
            new Keyframe(75, 1),
            new Keyframe(75, -1),
            new Keyframe(90, 0)         // 90s
        );
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        float previusEmotionValue = emotionalCurve.Evaluate(timer - Time.deltaTime);
        float currentEmotionValue = emotionalCurve.Evaluate(timer);

        // Debug.Log("Curve Axis: " + timer + " " + currentEmotionValue);
        axis.x = timer;
        axis.y = currentEmotionValue;

        currentState = UpdateState(currentEmotionValue);
        emotionStatus = UpdateStatus(previusEmotionValue, currentEmotionValue);
    }

    CurveState UpdateState(float emotionValue)
    {
        if (emotionValue >= 0.25f)
        {
            return CurveState.Up;
        }
        else if (emotionValue <= -0.25f)
        {
            return CurveState.Down;
        }
        else
        {
            return CurveState.Normal;
        }
    }

    EmotionStatus UpdateStatus(float previusEmotionValue,float currentEmotionValue)
    {
        if (currentEmotionValue > previusEmotionValue)
        {
            return EmotionStatus.Increasing;
        }
        else if (currentEmotionValue < previusEmotionValue)
        {
            return EmotionStatus.Decreasing;
        }
        else
        {
            return EmotionStatus.Stabilizing;
        }
    }

}

