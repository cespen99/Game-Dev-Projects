using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardAttack : MonoBehaviour
{
    public float mana, manaMax, manaRegen;
    public List<int> spellsKnown;
    public int selectSpell; //index of player spells
    public int spell; //id of spells
    public float damage; 
    public Spell_Type element;

    public Dictionary<int, string> spellName;
    Dictionary<int, float> spellDamage;
    Dictionary<int, Spell_Type> spellType;
    Dictionary<int, Spell_Pattern> spellPattern;
    Dictionary<int, float> spellCost;
    Dictionary<int, float> spellMana;
    public Text[] spellCostText;
    public enum Spell_Type { fire, ice, lightning };
    public enum Spell_Pattern { Bolt, Ball, Bomb, Grasp, Cone, Wave, Splash, Spray, Beam };

    public bool canAttack;
    public Color col;
    public GameObject hitbox;

    public Text spellInfoText;
    public AudioSource fireSound, iceSound, lightningSound;

    private void Update()
    {
        mana += manaRegen * Time.deltaTime;
        if (mana > manaMax)
            mana = manaMax;
        if (mana < 0)
            mana = 0;
    }

    void Start()
    {
        mana = manaMax;
        selectSpell = PlayerPrefs.GetInt("SpellSelect");
        spell = 0;
        spellsKnown = new List<int>();
        spellsKnown.Clear();

        spellDamage = new Dictionary<int, float>();
        spellName = new Dictionary<int, string>();
        spellType = new Dictionary<int, Spell_Type>();
        spellPattern = new Dictionary<int, Spell_Pattern>();
        spellCost = new Dictionary<int, float>();

        AddSpell(1, "Heat Touch", Spell_Type.fire, Spell_Pattern.Grasp, 1, 10);

        AddSpell(2, "Firebolt", Spell_Type.fire, Spell_Pattern.Bolt, 2, 10);
        AddSpell(3, "Flame Spit", Spell_Type.fire, Spell_Pattern.Splash, 2, 15);

        
        AddSpell(4, "Flame Thrower", Spell_Type.fire, Spell_Pattern.Spray, 3, 12);
        AddSpell(5, "Heat Blast", Spell_Type.fire, Spell_Pattern.Cone, 3, 20);
        AddSpell(6, "Fireball", Spell_Type.fire, Spell_Pattern.Ball, 3, 35);

        AddSpell(7, "Heat Wave", Spell_Type.fire, Spell_Pattern.Wave, 4, 45);
        AddSpell(8, "Plasma Beam", Spell_Type.fire, Spell_Pattern.Beam, 4, 20);

        AddSpell(9, "Firebomb", Spell_Type.fire, Spell_Pattern.Bomb, 5, 75);



        AddSpell(11, "Frost Bite", Spell_Type.ice, Spell_Pattern.Splash, 1, 4);

        AddSpell(12, "Chill Touch", Spell_Type.ice, Spell_Pattern.Grasp, 2, 10);
        AddSpell(13, "Ice Spike", Spell_Type.ice, Spell_Pattern.Bolt, 2, 8);

        AddSpell(14, "Snow Ball", Spell_Type.ice, Spell_Pattern.Ball, 3, 12);
        AddSpell(15, "Frost Spray", Spell_Type.ice, Spell_Pattern.Spray, 3, 7);
        AddSpell(16, "Cone of Cold", Spell_Type.ice, Spell_Pattern.Cone, 3, 25);

        AddSpell(17, "Ice Beam", Spell_Type.ice, Spell_Pattern.Beam, 4, 15);
        AddSpell(18, "Snow Bomb", Spell_Type.ice, Spell_Pattern.Bomb, 4, 40);

        AddSpell(19, "Blizzard", Spell_Type.ice, Spell_Pattern.Wave, 5, 65);



        AddSpell(21, "Electro Buzz", Spell_Type.lightning, Spell_Pattern.Bolt, 1, 6);

        AddSpell(22, "Static Spark", Spell_Type.lightning, Spell_Pattern.Splash, 2, 4);
        AddSpell(23, "Shocking Grasp", Spell_Type.lightning, Spell_Pattern.Grasp, 2, 8);

        AddSpell(24, "Sonic Wave", Spell_Type.lightning, Spell_Pattern.Cone, 3, 16);
        AddSpell(25, "Electro Bolt", Spell_Type.lightning, Spell_Pattern.Ball, 3, 13);
        AddSpell(26, "Spark Lightning", Spell_Type.lightning, Spell_Pattern.Spray, 3, 25);

        AddSpell(27, "Electro Bomb", Spell_Type.lightning, Spell_Pattern.Bomb, 4, 20);
        AddSpell(28, "Thunder Wave", Spell_Type.lightning, Spell_Pattern.Wave, 4, 35);

        AddSpell(29, "Lightning Strike", Spell_Type.lightning, Spell_Pattern.Beam, 5, 35);

        canAttack = true;   

        for (int i = 0; i < 3; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                for (int k = 0; k < 27; k++)
                {
                    if (PlayerPrefs.GetString("PlayerSpell" + k) == spellName[(i*10)+j])
                        LearnSpell((i * 10) + j);
                }
            }
        }
    }

    IEnumerator InfoMessage(string msg)
    {
        spellInfoText.color = col;
        spellInfoText.text = msg;
        yield return new WaitForSeconds(0.8f);
        spellInfoText.text = "";
    }

    public void SelectSpell(int s)
    {
        if (spellsKnown.Count == 0)
            return;
        selectSpell += s;
        if (selectSpell >= spellsKnown.Count)
            selectSpell = 0;
        if (selectSpell < 0)
            selectSpell = spellsKnown.Count - 1;
        spell = spellsKnown[selectSpell];

        switch (spellType[spell])
        {
            case Spell_Type.fire:
                col = Color.red;
                break;
            case Spell_Type.ice:
                col = Color.blue;
                break;
            case Spell_Type.lightning:
                col = Color.yellow;
                break;
        }
        StartCoroutine("InfoMessage", spellName[spell] + " selected");
    }

    public void LearnSpell(int n)
    {
        if (!spellsKnown.Contains(n) && !GetComponent<PlayerAction>().gameOver)
        {
            string playprefSpell = "PlayerSpell" + (spellsKnown.Count);
            //reload spells from save
            if (PlayerPrefs.GetString(playprefSpell) == spellName[n])
            {
                spellsKnown.Add(n);
                spellCostText[n].text = "LEARNED";

                if (spellsKnown.Count == 1)
                    SelectSpell(1);
            }
            else  //this looks structured weird, but i have to check if player has unlocked spells and if not see if they have enough to pay
            {
                if (GetComponent<PlayerLevel>().points >= spellCost[n])
                {
                    GetComponent<PlayerLevel>().spendPoints(spellCost[n]);
                    spellsKnown.Add(n);
                    PlayerPrefs.SetString(playprefSpell, spellName[n]);

                    spellCostText[n].text = "LEARNED";
                    StartCoroutine("InfoMessage", "Learned: " + spellName[n]);
                    if (spellsKnown.Count == 1)
                        SelectSpell(1);
                }
            }
        }
    }

    public void AddSpell(int num, string name, Spell_Type type, Spell_Pattern pattern, float cost, float damage)
    {
        spellName.Add(num, name);
        spellType.Add(num, type);
        spellPattern.Add(num, pattern);
        spellDamage.Add(num, damage);
        spellCost.Add(num, cost);
    }

    public void Spell()
    {
        if (canAttack  && selectSpell != -1 && mana >= spellCost[spell] * 10)
        {
            mana -= spellCost[spell] * 10;
            canAttack = false;
            GetComponent<Movement>().animator.SetTrigger("Block");
            damage = spellDamage[spell];
            element = spellType[spell];
            switch (element)
            {
                case Spell_Type.fire:
                    fireSound.Play();
                    break;
                case Spell_Type.ice:
                    iceSound.Play();
                    break;
                case Spell_Type.lightning:
                    lightningSound.Play();
                    break;
            }
            StartCoroutine(spellPattern[spell].ToString());

            StartCoroutine("Pause", .5f);
        }
    }


    public void Box(float size, float distance, float degree, float speed, float timer, float grow)
    {
        Movement mv = GetComponent<Movement>();                                 //wizard shit
        GameObject box = Instantiate(hitbox, transform.position + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * (mv.targetAngle + degree)) * distance, Mathf.Cos(Mathf.Deg2Rad * (mv.targetAngle + degree)) * distance, 0f), Quaternion.Euler(0,0,mv.targetAngle));        //box.GetComponent<AttackBox>().SetColor(col);
        box.GetComponent<AttackBox>().SetDamage(damage);
        box.GetComponent<AttackBox>().SetColor(col);
        box.transform.localScale = new Vector3(size, size, 0);
        box.GetComponent<BoxCollider2D>().enabled = true;
        box.GetComponent<SpriteRenderer>().enabled = true;
        box.GetComponent<AttackBox>().targetEnemy = 1;
        box.GetComponent<AttackBox>().enabled = true;
        box.GetComponent<AttackBox>().SetProjectile(speed);
        box.GetComponent<AttackBox>().SetGrow(grow);
        Destroy(box, timer);
    }

    public IEnumerator Pause(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    // ---o
    public IEnumerator Ball()
    {
        Box(1f, 1f, 0, 5, .5f, 0);
        yield return new WaitForSeconds(.03f);
    }
    public IEnumerator Bolt()
    {
        Box(.5f, 1f, 0, 10, .3f, 0);
        yield return new WaitForSeconds(.03f);
    }
    public IEnumerator Bomb()
    {
        Box(2f, 1.5f, 0, 8, .5f, 1.5f);
        yield return new WaitForSeconds(.03f);
    }

    // -o08
    public IEnumerator Grasp()
    {
        Box(1.5f, 1.25f, 0, 0, .2f, 0);
        yield return new WaitForSeconds(.1f);
        Box(2f, 3f, -15, 0, .2f, 0);
        Box(2f, 3f, 15, 0, .2f, 0);
        yield return new WaitForSeconds(.03f);
    }
    public IEnumerator Cone()
    {
        Box(1.5f, 1.25f, 0, 0, .2f, 0);
        yield return new WaitForSeconds(.1f);
        Box(2f, 3f, 0, 0, .2f, 0);
        Box(2f, 3f, -30, 0, .2f, 0);
        Box(2f, 3f, 30, 0, .2f, 0);
        yield return new WaitForSeconds(.1f);
        Box(2.5f, 5f, 0, 0, .2f, 0);
        Box(2.5f, 5f, -25, 0, .2f, 0);
        Box(2.5f, 5f, 25, 0, .2f, 0);
        Box(2.5f, 5f, -40, 0, .2f, 0);
        Box(2.5f, 5f, 40, 0, .2f, 0);
    }
    public IEnumerator Wave()
    {
        Box(1.5f, 1.25f, 0, 0, .2f, 0);
        yield return new WaitForSeconds(.1f);
        Box(2f, 3f, 0, 0, .2f, 0);
        Box(2f, 3f, -30, 0, .2f, 0);
        Box(2f, 3f, 30, 0, .2f, 0);
        yield return new WaitForSeconds(.05f);
        Box(3f, 5f, 0, 0, .2f, 0);
        Box(3f, 5f, -25, 0, .2f, 0);
        Box(3f, 5f, 25, 0, .2f, 0);
        Box(3f, 5f, -40, 0, .2f, 0);
        Box(3f, 5f, 40, 0, .2f, 0);
        yield return new WaitForSeconds(.05f);
        Box(4f, 8f, 0, 0, .2f, 0);
        Box(4f, 8f, -20, 0, .2f, 0);
        Box(4f, 8f, 20, 0, .2f, 0);
        Box(4f, 8f, -40, 0, .2f, 0);
        Box(4f, 8f, 40, 0, .2f, 0);
        Box(4f, 8f, -60, 0, .2f, 0);
        Box(4f, 8f, 60, 0, .2f, 0);
    }

    // -o8o8o
    public IEnumerator Splash()
    {
        Box(.75f, .5f, 30, 6, .5f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, -60, 6, .5f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 60, 6, .5f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, -30, 6, .5f, 0);
        yield return new WaitForSeconds(.03f);
    }
    public IEnumerator Spray()
    {
        Box(.5f, .5f, 0, 6, .75f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.5f, .5f, -15, 6, .75f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.5f, .5f, 15, 6, .75f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.5f, .5f, 0, 6, .75f, 0);
        yield return new WaitForSeconds(.04f);
        Box(.5f, .5f, -15, 6, .75f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.5f, .5f, 15, 6, .75f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.5f, .5f, 0, 6, .75f, 0);
        yield return new WaitForSeconds(.04f);
        Box(.5f, .5f, -15, 6, .75f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.5f, .5f, 15, 6, .75f, 0);
        yield return new WaitForSeconds(.02f);
        Box(.5f, .5f, 0, 6, .75f, 0);
        yield return new WaitForSeconds(.04f);
    }
    public IEnumerator Beam()
    {
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
        Box(.75f, .5f, -15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 15, 6, 3, 0);
        yield return new WaitForSeconds(.02f);
        Box(.75f, .5f, 0, 6, 3, 0);
        yield return new WaitForSeconds(.04f);
    }
}
