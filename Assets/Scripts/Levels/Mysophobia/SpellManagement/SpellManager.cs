using System.Collections.Generic;
using static Spell;
public class SpellManager
{
    public Dictionary<InputAction.Spell, Spell> SpellDict;

    public SpellManager()
    {
        SpellDict = new Dictionary<InputAction.Spell, Spell>();
    }

    public void Reset()
    {
        foreach (KeyValuePair<InputAction.Spell, Spell> kvp in SpellDict)
        {
            kvp.Value.Reset();
        }
    }

    public void Add(InputAction.Spell type, Spell spell)
    {
        SpellDict[type] = spell;
    }
    public void AttachDisplay(InputAction.Spell spell, string container)
    {
        SpellDict[spell].AttachDisplay(container);
    }
    public Spell Get(InputAction.Spell spell)
    {
        return SpellDict[spell];
    }
    public bool Activate(InputAction.Spell spell)
    {
        return SpellDict[spell].Activate();
    }

    public bool IsAvailable(InputAction.Spell spell)
    {
        return SpellDict[spell].IsAvailable();
    }

    public void Cancel(InputAction.Spell spell)
    {
        SpellDict[spell].Cancel();
    }
    public bool IsActivated(InputAction.Spell spell)
    {
        return SpellDict[spell].Activated;
    }

    public void Deactivate(InputAction.Spell spell)
    {
        SpellDict[spell].Deactivate();
    }
    public void IncreaseTurn()
    {
        foreach (KeyValuePair<InputAction.Spell, Spell> kvp in SpellDict)
        {
            kvp.Value.IncreaseTurn();
        }
    }
}
