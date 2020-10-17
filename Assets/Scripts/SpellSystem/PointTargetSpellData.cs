using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Projectiles;

namespace AfterlifeProject.Assets.Scripts.SpellSystem.SO
{
[CreateAssetMenu(fileName = "point_target_spell_data", menuName = "Spells/Point Target Spell Data", order = 54)]
    public class PointTargetSpellData : SpellData
    {
        [SerializeField] private float _damage;

        [SerializeField] private float _radius;

        [SerializeField] private float _lifetime;

        [SerializeField] List<SpellBehaviour> _modifiers = new List<SpellBehaviour>(); 

        public float Damage => _damage;

        public float Radius => _radius;

        public float Lifetime => _lifetime;

        public List<SpellBehaviour> Modifiers => _modifiers;

        public override void Cast(Transform transform, List<Transform> targets)
        {
            Spell spellInstance = Instantiate(SpellPrefab, targets.FirstOrDefault().position,
                targets.FirstOrDefault().transform.rotation);

            spellInstance.Initialize(this, transform);
        }
    }
}