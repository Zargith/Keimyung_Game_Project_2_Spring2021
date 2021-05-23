using System.Collections.Generic;
using static Spell;
public class SpellManager
{
    public Dictionary<InputAction.Spell, Spell> spellDict;

    public SpellManager()
    {
        spellDict = new Dictionary<InputAction.Spell, Spell>()
        {
            {InputAction.Spell.ACCELERATION, new Spell(InputAction.Spell.ACCELERATION, ReloadType.COOLDOWN, 10, false) },
            {InputAction.Spell.DELETE_VIRUS, new Spell(InputAction.Spell.DELETE_VIRUS, ReloadType.USAGE, 2, true) }
        };
        spellDict[InputAction.Spell.ACCELERATION].AttachDisplay("AccelerationSpell");
        spellDict[InputAction.Spell.DELETE_VIRUS].AttachDisplay("AntivirusSpraySpell");
    }

    public Spell Get(InputAction.Spell spell)
    {
        return spellDict[spell];
    }
    public bool Activate(InputAction.Spell spell)
    {
        return spellDict[spell].Activate();
    }

    public bool isAvailable(InputAction.Spell spell)
    {
        return spellDict[spell].IsAvailable();
    }

    public void Cancel(InputAction.Spell spell)
    {
        spellDict[spell].Cancel();
    }
    public bool IsActivated(InputAction.Spell spell)
    {
        return spellDict[spell].activated;
    }

    public void Deactivate(InputAction.Spell spell)
    {
        spellDict[spell].Deactivate();
    }
    public void IncreaseTurn()
    {
        foreach (KeyValuePair<InputAction.Spell, Spell> kvp in spellDict)
        {
            kvp.Value.IncreaseTurn();
        }
    }
}
