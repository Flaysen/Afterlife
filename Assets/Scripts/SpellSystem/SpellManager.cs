using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
using System;
using Combat;
using Enemies;

namespace SpellSystem
{
    public class SpellManager : MonoBehaviour
    {
        [SerializeField] SpellData [] _spells;

        private InputHandler _input;

        private PlayerController _contoller;

        public event Action<int, SpellData> OnSpellAdded;

        public event Action<int, SpellData> OnCooldownChanged;

        private void Awake()
        {
            _input = GetComponent<InputHandler>();

            _contoller = GetComponent<PlayerController>();

            _input.OnAnySpellCast += CastSpell;

            EnemyHealthBehaviour.OnDamageTaken += ReduceCooldowns;       
        }

        private void Start()
        {
            _spells = new SpellData [4];
            // for (int i = 0; i < _spells.Count; i++)
            // {
            //     _spells[i].DamageToCast = 0;
            //     OnSpellAdded?.Invoke(i, _spells[i]);
            // }
        }

        private void CastSpell(int spellIndex)
        {
            SpellData spellToCast = _spells[spellIndex - 1];
 
            if(spellToCast.DamageToCast <= 0)
            {
                spellToCast.Cast(transform, GetSpellTarget(spellToCast.CastType));

                spellToCast.DamageToCast = spellToCast.Cooldown;

                OnCooldownChanged?.Invoke(spellIndex - 1, spellToCast);

                ReduceCooldowns(0);
            }    
        }

        public List<Transform> GetSpellTarget(CastType castType)
        {     
            List<Transform> targets = new List<Transform>(); 

            switch(castType)
            {
                case CastType.Projectile:
                {
                    Turret [] turrets = GetComponentsInChildren<Turret>(); 
                    foreach(Turret turret in turrets)
                    {
                        targets.Add(turret.transform);
                    }
                    return targets;
                }                
                case CastType.PointTarget:    
                {
                    GameObject pointer = new GameObject(); 
                    pointer.transform.position = _contoller.PointToLook;            
                    targets.Add(pointer.transform);
                    Destroy(pointer);
                    return targets;              
                }               
                case CastType.SelfCast:
                {
                    targets.Add(transform);
                    return targets;
                }
                case CastType.GlobalCast:
                {
                    EnemyHealthBehaviour [] enemiese = FindObjectsOfType<EnemyHealthBehaviour>();
                    foreach(EnemyHealthBehaviour enemy in enemiese)
                    {
                        targets.Add(enemy.transform);
                    }
                    return targets;
                }
                 default:
                    throw new Exception("No valid target!");
            }
        }

        private void ReduceCooldowns(float damage)
        {
            for(int i = 0; i < 4; i++)
            {
                if(_spells[i] != null)
                {
                    _spells[i].DamageToCast = (_spells[i].DamageToCast - damage > 0) ?
                                        _spells[i].DamageToCast - damage : 0;
 
                     OnCooldownChanged?.Invoke(i, _spells[i]);    
                }              
            }       
        }

        public void AddSpell(ISpellsProvider provider, int index)
        {
            foreach(SpellData spell in provider.Spells)
            {
                _spells[index] = spell;
                OnCooldownChanged?.Invoke(index, _spells[index]);
            }
        }
    }
}
