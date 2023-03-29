using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
 
    public static readonly Event<Vector3> onBulletHittingSurface = new Event<Vector3>();


    public static readonly Event onPlayerEnabled = new Event();


    public static readonly Event<float> onStartSetUIHPValue = new Event<float>();
    public static readonly Event<float> onPlayerHpChangeUIUpdate = new Event<float>();

    public static readonly Event onPlayerCrouchActivate = new Event();
    public static readonly Event onPlayerCrouchDisactivate = new Event();

    public static readonly Event onPlayerDeath = new Event();


    public static readonly Event<float> onAmmunitionAmountChange = new Event<float>();
}

