using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int numSpells, selectSpell;
    public List<int> spellsKnown;

    private void Start()
    {
        selectSpell = 0;
        numSpells = 30;
        spellsKnown = new List<int>();

        //LearnSpell(1);
    }

    public void SelectSpell(int s)
    {
        if (spellsKnown.Count == 0)
            return;
        selectSpell += s;
        if (selectSpell > spellsKnown.Count)
            selectSpell = spellsKnown[0];
        if (selectSpell < 1)
            selectSpell = spellsKnown[spellsKnown.Count-1];
    }

    public void LearnSpell(int n)
    {
        spellsKnown.Add(n);
    }
}


