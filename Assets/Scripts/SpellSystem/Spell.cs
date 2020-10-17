using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public Transform Caster { get; private set; }

    public virtual void Initialize(SpellData data, Transform caster) {}

    public void DisplayEffect(GameObject effectPrefab)
    {
        if(effectPrefab)
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
}
