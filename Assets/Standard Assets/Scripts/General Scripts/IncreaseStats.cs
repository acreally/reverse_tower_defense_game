using UnityEngine;
using System.Collections;

public class IncreaseStats : MonoBehaviour {
    // Healer script
    Healer healer;
    // Infantry script
    Infantry infantry;
    // Knight script
    Knight knight;
    // Mage script
    Mage mage;
    // Spy script
    Spy spy;
    // GUI style for the background
    public GUIStyle backgroundStyle;
    // GUI style for healer icon
    public GUIStyle healerStyle;
    // GUI style for infantry icon
    public GUIStyle infantryStyle;
    // GUI style for knight icon
    public GUIStyle knightStyle;
    // GUI style for mage icon
    public GUIStyle mageStyle;
    // GUI style for spy icon
    public GUIStyle spyStyle;
    // GUI style for text boxes
    public GUIStyle textBoxStyle;
    // GUI style for the increase stat button
    public GUIStyle upButtonStyle;
    // GUI style for the decrease stat button
    public GUIStyle downButtonStyle;
    // GUI style for the done button
    public GUIStyle doneButtonStyle;
    // The maximum health that healer units will be set to when
    // the user is done upgrading
    float healerHealth;
    // The speed that healer units will be set to when the user is
    // done updrading
    float healerSpeed;
    // The defense that healer units will be set to when the user is
    // done updrading
    float healerDefense;
    // The squad size that healer units will be set to when the user is
    // done upgrading
    int healerSquadSize;
    // The maximum health that infantry units will be set to when
    // the user is done upgrading
    float infantryHealth;
    // The speed that infantry units will be set to when the user is
    // done updrading
    float infantrySpeed;
    // The defense that infantry units will be set to when the user is
    // done updrading
    float infantryDefense;
    // The squad size that infantry units will be set to when the user is
    // done upgrading
    int infantrySquadSize;
    // The maximum health that knight units will be set to when
    // the user is done upgrading
    float knightHealth;
    // The speed that knight units will be set to when the user is
    // done updrading
    float knightSpeed;
    // The defense that knight units will be set to when the user is
    // done updrading
    float knightDefense;
    // The squad size that knight units will be set to when the user is
    // done upgrading
    int knightSquadSize;
    // The maximum health that mage units will be set to when
    // the user is done upgrading
    float mageHealth;
    // The speed that mage units will be set to when the user is
    // done updrading
    float mageSpeed;
    // The defense that mage units will be set to when the user is
    // done updrading
    float mageDefense;
    // The squad size that mage units will be set to when the user is
    // done upgrading
    int mageSquadSize;
    // The maximum health that spy units will be set to when
    // the user is done upgrading
    float spyHealth;
    // The speed that spy units will be set to when the user is
    // done updrading
    float spySpeed;
    // The defense that spy units will be set to when the user is
    // done updrading
    float spyDefense;
    // The squad size that spy units will be set to when the user is
    // done upgrading
    int spySquadSize;
    // The number of points the player can spend on upgrades
    int[] upgradePoints;

    void Awake()
    {
        healer = GetComponent<Healer>();
        infantry = GetComponent<Infantry>();
        knight = GetComponent<Knight>();
        mage = GetComponent<Mage>();
        spy = GetComponent<Spy>();
    }

    void OnEnable()
    {
        upgradePoints = GetComponent<GameInfo>().UpgradePoints;
        healerDefense = healer.Def;
        healerHealth = healer.Health;
        healerSpeed = healer.Speed;
        healerSquadSize = healer.SizeOfSquad;
        infantryDefense = infantry.Def;
        infantryHealth = infantry.Health;
        infantrySpeed = infantry.Speed;
        infantrySquadSize = infantry.SizeOfSquad;
        knightDefense = knight.Def;
        knightHealth = knight.Health;
        knightSpeed = knight.Speed;
        knightSquadSize = knight.SizeOfSquad;
        mageDefense = mage.Def;
        mageHealth = mage.Health;
        mageSpeed = mage.Speed;
        mageSquadSize = mage.SizeOfSquad;
        spyDefense = spy.Def;
        spyHealth = spy.Health;
        spySpeed = spy.Speed;
        spySquadSize = spy.SizeOfSquad;

    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "",
            backgroundStyle);
        GUI.Box(new Rect(90, 220, 64, 64), "", infantryStyle);
        GUI.Box(new Rect(290, 220, 64, 64), "", knightStyle);
        GUI.Box(new Rect(490, 220, 64, 64), "", mageStyle);
        GUI.Box(new Rect(690, 220, 64, 64), "", healerStyle);
        GUI.Box(new Rect(890, 220, 64, 64), "", spyStyle);
        // Infantry stat boxes
        GUI.Box(new Rect(40, 300, 128, 32), "Health: " + infantryHealth, 
            textBoxStyle);
        if (GUI.Button(new Rect(168, 300, 16, 16), "", upButtonStyle))
        {
            infantryHealth = IncreaseStat(infantryHealth,
                infantry.healthIncreaseAmt, infantry.Health,
                infantry.maxHealthAmt, 0);
        }
        if (GUI.Button(new Rect(168, 316, 16, 16), "", downButtonStyle))
        {
            infantryHealth = DecreaseStat(infantryHealth,
                infantry.healthIncreaseAmt, infantry.Health,
                infantry.maxHealthAmt, 0);
        }
        GUI.Box(new Rect(40, 332, 128, 32), "Defense: " + infantryDefense, 
            textBoxStyle);
        if (GUI.Button(new Rect(168, 332, 16, 16), "", upButtonStyle))
        {
            infantryDefense = IncreaseStat(infantryDefense,
                infantry.defenseIncreaseAmt, infantry.Def,
                infantry.maxDefenseAmt, 0);
        }
        if (GUI.Button(new Rect(168, 348, 16, 16), "", downButtonStyle))
        {
            infantryDefense = DecreaseStat(infantryDefense,
                infantry.defenseIncreaseAmt, infantry.Def,
                infantry.maxDefenseAmt, 0);
        }
        GUI.Box(new Rect(40, 364, 128, 32), "Speed: " + infantrySpeed, 
            textBoxStyle);
        if (GUI.Button(new Rect(168, 364, 16, 16), "", upButtonStyle))
        {
            infantrySpeed = IncreaseStat(infantrySpeed,
                infantry.speedIncreaseAmt, infantry.Speed,
                infantry.maxSpeedAmt, 0);
        }
        if (GUI.Button(new Rect(168, 380, 16, 16), "", downButtonStyle))
        {
            infantrySpeed = DecreaseStat(infantrySpeed,
                infantry.speedIncreaseAmt, infantry.Speed,
                infantry.maxSpeedAmt, 0);
        }
        GUI.Box(new Rect(40, 396, 128, 32), "Squad Size: " + 
            infantrySquadSize, textBoxStyle);
        if (GUI.Button(new Rect(168, 396, 16, 16), "", upButtonStyle))
        {
            infantrySquadSize = IncreaseStat(infantrySquadSize,
                infantry.squadSizeIncreaseAmt, infantry.SizeOfSquad,
                infantry.maxSquadSizeAmt, 0);
        }
        if (GUI.Button(new Rect(168, 412, 16, 16), "", downButtonStyle))
        {
            infantrySquadSize = DecreaseStat(infantrySquadSize,
                infantry.squadSizeIncreaseAmt, infantry.SizeOfSquad,
                infantry.maxSquadSizeAmt, 0);
        }
        GUI.Box(new Rect(40, 428, 128, 32), "Points Left: " +
            upgradePoints[0], textBoxStyle);

        // Knight stat boxes
        GUI.Box(new Rect(240, 300, 128, 32), "Health: " + knightHealth,
            textBoxStyle);
        if (GUI.Button(new Rect(368, 300, 16, 16), "", upButtonStyle))
        {
            knightHealth = IncreaseStat(knightHealth,
                knight.healthIncreaseAmt, knight.Health,
                knight.maxHealthAmt, 1);
        }
        if (GUI.Button(new Rect(368, 316, 16, 16), "", downButtonStyle))
        {
            knightHealth = DecreaseStat(knightHealth,
                knight.healthIncreaseAmt, knight.Health,
                knight.maxHealthAmt, 1);
        }
        GUI.Box(new Rect(240, 332, 128, 32), "Defense: " + knightDefense,
            textBoxStyle);
        if (GUI.Button(new Rect(368, 332, 16, 16), "", upButtonStyle))
        {
            knightDefense = IncreaseStat(knightDefense,
                knight.defenseIncreaseAmt, knight.Def,
                knight.maxDefenseAmt, 1);
        }
        if (GUI.Button(new Rect(368, 348, 16, 16), "", downButtonStyle))
        {
            knightDefense = DecreaseStat(knightDefense,
                knight.defenseIncreaseAmt, knight.Def,
                knight.maxDefenseAmt, 1);
        }
        GUI.Box(new Rect(240, 364, 128, 32), "Speed: " + knightSpeed,
            textBoxStyle);
        if (GUI.Button(new Rect(368, 364, 16, 16), "", upButtonStyle))
        {
            knightSpeed = IncreaseStat(knightSpeed,
                knight.speedIncreaseAmt, knight.Speed,
                knight.maxSpeedAmt, 1);
        }
        if (GUI.Button(new Rect(368, 380, 16, 16), "", downButtonStyle))
        {
            knightSpeed = DecreaseStat(knightSpeed,
                knight.speedIncreaseAmt, knight.Speed,
                knight.maxSpeedAmt, 1);
        }
        GUI.Box(new Rect(240, 396, 128, 32), "Squad Size: " +
            knightSquadSize, textBoxStyle);
        if (GUI.Button(new Rect(368, 396, 16, 16), "", upButtonStyle))
        {
            knightSquadSize = IncreaseStat(knightSquadSize,
                knight.squadSizeIncreaseAmt, knight.SizeOfSquad,
                knight.maxSquadSizeAmt, 1);
        }
        if (GUI.Button(new Rect(368, 412, 16, 16), "", downButtonStyle))
        {
            knightSquadSize = DecreaseStat(knightSquadSize,
                knight.squadSizeIncreaseAmt, knight.SizeOfSquad,
                knight.maxSquadSizeAmt, 1);
        }
        GUI.Box(new Rect(240, 428, 128, 32), "Points Left: " +
            upgradePoints[1], textBoxStyle);
        
        // Mage stat boxes
        GUI.Box(new Rect(440, 300, 128, 32), "Health: " + mageHealth,
            textBoxStyle);
        if (GUI.Button(new Rect(568, 300, 16, 16), "", upButtonStyle))
        {
            mageHealth = IncreaseStat(mageHealth,
                mage.healthIncreaseAmt, mage.Health,
                mage.maxHealthAmt, 2);
        }
        if (GUI.Button(new Rect(568, 316, 16, 16), "", downButtonStyle))
        {
            mageHealth = DecreaseStat(mageHealth,
                mage.healthIncreaseAmt, mage.Health,
                mage.maxHealthAmt, 2);
        }
        GUI.Box(new Rect(440, 332, 128, 32), "Defense: " + mageDefense,
            textBoxStyle);
        if (GUI.Button(new Rect(568, 332, 16, 16), "", upButtonStyle))
        {
            mageDefense = IncreaseStat(mageDefense,
                mage.defenseIncreaseAmt, mage.Def,
                mage.maxDefenseAmt, 2);
        }
        if (GUI.Button(new Rect(568, 348, 16, 16), "", downButtonStyle))
        {
            mageDefense = DecreaseStat(mageDefense,
                mage.defenseIncreaseAmt, mage.Def,
                mage.maxDefenseAmt, 2);
        }
        GUI.Box(new Rect(440, 364, 128, 32), "Speed: " + mageSpeed,
            textBoxStyle);
        if (GUI.Button(new Rect(568, 364, 16, 16), "", upButtonStyle))
        {
            mageSpeed = IncreaseStat(mageSpeed,
                mage.speedIncreaseAmt, mage.Speed,
                mage.maxSpeedAmt, 2);
        }
        if (GUI.Button(new Rect(568, 380, 16, 16), "", downButtonStyle))
        {
            mageSpeed = DecreaseStat(mageSpeed,
                mage.speedIncreaseAmt, mage.Speed,
                mage.maxSpeedAmt, 2);
        }
        GUI.Box(new Rect(440, 396, 128, 32), "Squad Size: " +
            mageSquadSize, textBoxStyle);
        if (GUI.Button(new Rect(568, 396, 16, 16), "", upButtonStyle))
        {
            mageSquadSize = IncreaseStat(mageSquadSize,
                mage.squadSizeIncreaseAmt, mage.SizeOfSquad,
                mage.maxSquadSizeAmt, 2);
        }
        if (GUI.Button(new Rect(568, 412, 16, 16), "", downButtonStyle))
        {
            mageSquadSize = DecreaseStat(mageSquadSize,
                mage.squadSizeIncreaseAmt, mage.SizeOfSquad,
                mage.maxSquadSizeAmt, 2);
        }
        GUI.Box(new Rect(440, 428, 128, 32), "Points Left: " +
            upgradePoints[2], textBoxStyle);

        // Healer stat boxes
        GUI.Box(new Rect(640, 300, 128, 32), "Health: " + healerHealth,
            textBoxStyle);
        if (GUI.Button(new Rect(768, 300, 16, 16), "", upButtonStyle))
        {
            healerHealth = IncreaseStat(healerHealth, 
                healer.healthIncreaseAmt, healer.Health, 
                healer.maxHealthAmt, 3);
        }
        if (GUI.Button(new Rect(768, 316, 16, 16), "", downButtonStyle))
        {
            healerHealth = DecreaseStat(healerHealth,
                healer.healthIncreaseAmt, healer.Health,
                healer.maxHealthAmt, 3);
        }
        GUI.Box(new Rect(640, 332, 128, 32), "Defense: " + healerDefense,
            textBoxStyle);
        if (GUI.Button(new Rect(768, 332, 16, 16), "", upButtonStyle))
        {
            healerDefense = IncreaseStat(healerDefense,
                healer.defenseIncreaseAmt, healer.Def,
                healer.maxDefenseAmt, 3);
        }
        if (GUI.Button(new Rect(768, 348, 16, 16), "", downButtonStyle))
        {
            healerDefense = DecreaseStat(healerDefense, 
                healer.defenseIncreaseAmt, healer.Def,
                healer.maxDefenseAmt, 3);
        }
        GUI.Box(new Rect(640, 364, 128, 32), "Speed: " + healerSpeed,
            textBoxStyle);
        if (GUI.Button(new Rect(768, 364, 16, 16), "", upButtonStyle))
        {
            healerSpeed = IncreaseStat(healerSpeed,
                healer.speedIncreaseAmt, healer.Speed,
                healer.maxSpeedAmt, 3);
        }
        if (GUI.Button(new Rect(768, 380, 16, 16), "", downButtonStyle))
        {
            healerSpeed = DecreaseStat(healerSpeed,
                healer.speedIncreaseAmt, healer.Speed,
                healer.maxSpeedAmt, 3);
        }
        GUI.Box(new Rect(640, 396, 128, 32), "Squad Size: " +
            healerSquadSize, textBoxStyle);
        if (GUI.Button(new Rect(768, 396, 16, 16), "", upButtonStyle))
        {
            healerSquadSize = IncreaseStat(healerSquadSize,
                healer.squadSizeIncreaseAmt, healer.SizeOfSquad,
                healer.maxSquadSizeAmt, 3);
        }
        if (GUI.Button(new Rect(768, 412, 16, 16), "", downButtonStyle))
        {
            healerSquadSize = DecreaseStat(healerSquadSize,
                healer.squadSizeIncreaseAmt, healer.SizeOfSquad,
                healer.maxSquadSizeAmt, 3);
        }
        GUI.Box(new Rect(640, 428, 128, 32), "Points Left: " +
            upgradePoints[3], textBoxStyle);

        // Spy stat boxes
        GUI.Box(new Rect(840, 300, 128, 32), "Health: " + spyHealth,
            textBoxStyle);
        if (GUI.Button(new Rect(968, 300, 16, 16), "", upButtonStyle))
        {
            spyHealth = IncreaseStat(spyHealth,
                spy.healthIncreaseAmt, spy.Health,
                spy.maxHealthAmt, 4);
        }
        if (GUI.Button(new Rect(968, 316, 16, 16), "", downButtonStyle))
        {
            spyHealth = DecreaseStat(spyHealth,
                spy.healthIncreaseAmt, spy.Health,
                spy.maxHealthAmt, 4);
        }
        GUI.Box(new Rect(840, 332, 128, 32), "Defense: " + spyDefense,
            textBoxStyle);
        if (GUI.Button(new Rect(968, 332, 16, 16), "", upButtonStyle))
        {
            spyDefense = IncreaseStat(spyDefense,
                spy.defenseIncreaseAmt, spy.Def,
                spy.maxDefenseAmt, 4);
        }
        if (GUI.Button(new Rect(968, 348, 16, 16), "", downButtonStyle))
        {
            spyDefense = DecreaseStat(spyDefense,
                spy.defenseIncreaseAmt, spy.Def,
                spy.maxDefenseAmt, 4);
        }
        GUI.Box(new Rect(840, 364, 128, 32), "Speed: " + spySpeed,
            textBoxStyle);
        if (GUI.Button(new Rect(968, 364, 16, 16), "", upButtonStyle))
        {
            spySpeed = IncreaseStat(spySpeed,
                spy.speedIncreaseAmt, spy.Speed,
                spy.maxSpeedAmt, 4);
        }
        if (GUI.Button(new Rect(968, 380, 16, 16), "", downButtonStyle))
        {
            spySpeed = DecreaseStat(spySpeed,
                spy.speedIncreaseAmt, spy.Speed,
                spy.maxSpeedAmt, 4);
        }
        GUI.Box(new Rect(840, 396, 128, 32), "Squad Size: " +
            spySquadSize, textBoxStyle);
        if (GUI.Button(new Rect(968, 396, 16, 16), "", upButtonStyle))
        {
            spySquadSize = IncreaseStat(spySquadSize,
                spy.squadSizeIncreaseAmt, spy.SizeOfSquad,
                spy.maxSquadSizeAmt, 4);
        }
        if (GUI.Button(new Rect(968, 412, 16, 16), "", downButtonStyle))
        {
            spySquadSize = DecreaseStat(spySquadSize,
                spy.squadSizeIncreaseAmt, spy.SizeOfSquad,
                spy.maxSquadSizeAmt, 4);
        }
        GUI.Box(new Rect(840, 428, 128, 32), "Points Left: " +
            upgradePoints[4], textBoxStyle);

        // Done button
        if (GUI.Button(new Rect(400, 500, 256, 64), "", doneButtonStyle))
        {
            UpdateStats();
            GetComponent<GameInfo>().UpgradePoints = upgradePoints;
            Application.LoadLevel("levelSelect");
        }
    }

    void UpdateStats()
    {
        healer.Def = healerDefense;
        healer.Health = healerHealth;
        healer.Speed = healerSpeed;
        healer.SizeOfSquad = healerSquadSize;
        infantry.Def = infantryDefense;
        infantry.Health = infantryHealth;
        infantry.Speed = infantrySpeed;
        infantry.SizeOfSquad = infantrySquadSize;
        knight.Def = knightDefense;
        knight.Health = knightHealth;
        knight.Speed = knightSpeed;
        knight.SizeOfSquad = knightSquadSize;
        mage.Def = mageDefense;
        mage.Health = mageHealth;
        mage.Speed = mageSpeed;
        mage.SizeOfSquad = mageSquadSize;
        spy.Def = spyDefense;
        spy.Health = spyHealth;
        spy.Speed = spySpeed;
        spy.SizeOfSquad = spySquadSize;
    }

    /*
     * Return stat increased by increaseAmt up to a maximum of maxStat.
     */
    float IncreaseStat(float stat, float increaseAmt, float baseStat,
        float maxStat, int index) {
            if (upgradePoints[index] > 0 && stat < maxStat)
            {
                upgradePoints[index]--;
                return Mathf.Clamp(stat + increaseAmt, baseStat, maxStat);
            }
            else
            {
                return stat;
            }
        
    }

    /*
     * Return stat increased by increaseAmt up to a maximum of maxStat.
     */
    int IncreaseStat(int stat, int increaseAmt, int baseStat, int maxStat,
        int index)
    {
        if (upgradePoints[index] > 0 && stat < maxStat)
        {
            upgradePoints[index]--;
            return Mathf.Clamp(stat + increaseAmt, baseStat, maxStat);
        }
        else
        {
            return stat;
        }
        
    }

    /*
     * Return stat increased by decreaseAmt up to a maximum of minStat.
     */
    float DecreaseStat(float stat, float decreaseAmt, float baseStat, 
        float minStat, int index)
    {
        if (stat > baseStat)
        {
            upgradePoints[index]++;
            return Mathf.Clamp(stat - decreaseAmt, baseStat, minStat);
        }
        else
        {
            return stat;
        }
        
    }

    /*
     * Return stat increased by decreaseAmt up to a maximum of minStat.
     */
    int DecreaseStat(int stat, int decreaseAmt, int baseStat, int minStat,
        int index)
    {
        if (stat > baseStat)
        {
            upgradePoints[index]++;
            return Mathf.Clamp(stat - decreaseAmt, baseStat, minStat);
        }
        else
        {
            return stat;
        }
    }
}
