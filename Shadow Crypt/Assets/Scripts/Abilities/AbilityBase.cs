using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    public string abilityName;
    public KeyCode key;
    public float cooldown;
    public float damage;
    public abstract void Activate(Transform player);

}
