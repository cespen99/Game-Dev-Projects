using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    public string className;
    //HEALTH
    public float health, baseHealth;
    public float healthRegen, baseHealthRegen;
    //ENERGY
    public float energy, baseEnergy;
    public float energyRegen, baseEnergyRegen;
    //ATTACK
    public float power, basePower;
    public float aim, baseAim;
    //AGILITY
    public float speed, baseSpeed;
    public float skill, baseSkill;
    //RESISTANCE
    public float burnResist, baseBurnResist;
    public float freezeResist, baseFreezeResist;
    public float stunResist, baseStunResist;
    public float poisonResist, basePoisonResist;
    public float blindResist, baseBlindResist;
    public float drainResist, baseDrainResist;
    public GameObject attackBox;

    public float burn, freeze, stun, poison, blind, drain;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Restore()
    {
        health = baseHealth;
        healthRegen = baseHealthRegen;
        energy = baseEnergy;
        energyRegen = baseEnergyRegen;
        power = basePower;
        aim = baseAim;
        speed = baseSpeed;
        skill = baseSkill;
        burnResist = baseBurnResist;
        freezeResist = baseFreezeResist;
        stunResist = baseStunResist;
        poisonResist = basePoisonResist;
        blindResist = baseBlindResist;
        drainResist = baseDrainResist;

    }

    public IEnumerator Stack()
    {
        Refresh();
        if (burn>0)
            Burn();
        if (freeze > 0)
            Freeze();
        if (stun > 0)
            Burn();
        if (poison > 0)
            Burn();
        if (blind > 0)
            Burn();
        if (drain > 0)
            Burn();
        yield return new WaitForSeconds(2);
        StartCoroutine(Stack());
    }

    public void Hit(float dmg, float f, float i, float l, float p, float b, float d)
    {
        health -= dmg;
        burn += f - burnResist + 2;
        if (burn < 0)
            burn = 0;
        freeze += i - freezeResist + 2;
        if (freeze < 0)
            freeze = 0;
        stun += l - stunResist + 2;
        if (stun < 0)
            stun = 0;
        poison += p - poisonResist + 2;
        if (poison < 0)
            poison = 0;
        blind += b - blindResist + 2;
        if (blind < 0)
            blind = 0;
        drain += d - drainResist + 2;
        if (drain < 0)
            drain = 0;
        Debug.Log("HIT, PLAYER HEALTH = " + health);
    }

    public void Refresh()
    {
        power = basePower;
        aim = baseAim;
        speed = baseSpeed;
        skill = baseSkill;
    }
    public void Cure()
    {
        burn = 0;
        freeze = 0;
        stun = 0;
        poison = 0;
        blind = 0;
        drain = 0;
    }
    public void Burn()
    {
        health -= health * .1f;
        burn--;
    }
    public void Freeze()
    {
        speed = baseSpeed / 2;
        skill = baseSkill / 2;
        freeze--;
    }
    public void Stun()
    {
        energy -= energy * .1f;
        stun--;
    }
    public void Poison()
    {
        power = basePower / 2;
        poison--;
    }
    public void Blind()
    {
        aim = baseAim / 2;
        blind--;
    }
    public void Drain()
    {
        health -= health * .05f;
        energy -= energy * .05f;
        drain--;
    }



    // Update is called once per frame
    void Update()
    {
        if (health < 0)
            Destroy(this.gameObject);
    }
}
