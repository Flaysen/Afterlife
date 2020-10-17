using UnityEngine;
using SpellSystem;

public class SpellPanel : MonoBehaviour
{
    const int SLOT_NUMBER = 4;

    [SerializeField] private HUDSlot _slotPrefab;
    
    private HUDSlot [] _slots = new HUDSlot [SLOT_NUMBER];

    private SpellManager _spellManager;

    void Awake()
    {
        _spellManager = FindObjectOfType<SpellManager>();

        _spellManager.OnSpellAdded += AddSpell;

        _spellManager.OnCooldownChanged += SetFade;

        InizializeSlotList();
    }

    private void SetFade(int index, SpellData spell)
    {
        _slots[index].Fade.AdjustFade(spell.DamageToCast, spell.Cooldown);
    }

    private void AddSpell(int index, SpellData spell)
    {
        //_slots[index].SetImage(spell.Image);
    }

    private void InizializeSlotList()
    {
        for (int i = 0; i < SLOT_NUMBER; i++)
        {
            HUDSlot slot = Instantiate(_slotPrefab, transform);

            slot.Id = i;
            
            _slots[i] = slot;
        }
    }
}
