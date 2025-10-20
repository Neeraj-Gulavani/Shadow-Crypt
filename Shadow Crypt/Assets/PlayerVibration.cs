using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerVibration : MonoBehaviour
{
    public static PlayerVibration instance; // so you can call it from anywhere
    private void Awake()
    {
        instance = this;
    }

    public void Vibrate(float lowFreq, float highFreq, float duration)
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(lowFreq, highFreq);
            StartCoroutine(StopVibrationAfter(duration, gamepad));
        }
    }

    private IEnumerator StopVibrationAfter(float time, Gamepad gamepad)
    {
        yield return new WaitForSeconds(time);
        gamepad.SetMotorSpeeds(0, 0);
    }
}
