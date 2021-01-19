using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    public float radius = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SensorySoundSystem.Instance.MakeSound(this.gameObject, radius);
        }
    }
}
