using System;
using UnityEngine;

[Serializable]
public class FleetSettings
{
    public float BaseAttack;

    public float MovementSpeed;

    public float RotationSpeed;
    
}

[CreateAssetMenu(menuName = "Settings/FleetSettings", fileName = "FleetSettings")]
public class FleetSettingsObject : ScriptableObject
{
    [SerializeField] private FleetSettings _settings;
    public FleetSettings Settings => _settings;
}