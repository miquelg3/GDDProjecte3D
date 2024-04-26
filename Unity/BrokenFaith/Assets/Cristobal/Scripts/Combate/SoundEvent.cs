using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent
{
    public Vector3 soundPosition;
    public float soundRange;

    public SoundEvent(Vector3 position, float range)
    {
        soundPosition = position;
        soundRange = range;
    }
}

public static class SoundEventManager
{
    public delegate void SoundEventDelegate(SoundEvent soundEvent);
    public static event SoundEventDelegate OnSoundEvent;

    public static void EmitSoundEvent(SoundEvent soundEvent)
    {
        OnSoundEvent?.Invoke(soundEvent);
    }
}

