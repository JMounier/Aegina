using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Stats
{
    //General
    private static ulong timePlayed;
    private static bool tutoComplete;
    //PVE
    private static uint death;
    private static bool bosskill;

    //PVP
    private static uint kill;

    //Cristal
    private static uint capturedCristal;

    //Interact (clic droit)
    private static Dictionary<int, uint> destroyed;
    private static Dictionary<int, uint> put;
    private static Dictionary<int, uint> used;
    private static Dictionary<int, uint> hunted;
    private static Dictionary<int, uint> cristalLevel;

    //Craft
    private static Dictionary<int, uint> crafted;

    public static ulong TimePlayer()
    {
        return timePlayed;
    }

    public static void IncrementTimePlayer(ulong time)
    {
        timePlayed += time;
    }

    public static void SetTutoComplete()
    {
        tutoComplete = true;
    }

    public static bool TutoComplete()
    {
        return tutoComplete;
    }

    public static uint Hunt()
    {
        uint hunt = 0;
        foreach (int key in hunted.Keys)
            hunt += hunted[key];
        return hunt;
    }

    public static void AddHunt(Mob mob)
    {
        if (hunted.ContainsKey(mob.ID))
            hunted[mob.ID]++;
        else
            hunted.Add(mob.ID, 1);
    }

    public static uint Hunted(Mob mob)
    {
        if (hunted.ContainsKey(mob.ID))
            return hunted[mob.ID];
        return 0;
    }
    public static void ChangeCristalLevel(int type, uint level)
    {
        if (hunted.ContainsKey(type) && hunted[type] < level)
            hunted[type] = level;
        else
            hunted.Add(type, level);
    }
    public static uint CristalLevel(int type)
    {
        if (cristalLevel.ContainsKey(type))
            return hunted[type];
        return 0;
    }

    public static bool BossKill
    {
        get { return bosskill; }
        set { bosskill = true; }
    }

    public static uint Death()
    {
        return death;
    }

    public static void IncrementDeath()
    {
        death++;
    }

    public static uint Kill()
    {
        return kill;
    }

    public static void IncrementKill()
    {
        kill++;
    }

    public static uint CapturedCristal()
    {
        return capturedCristal;
    }

    public static void IncrementCapturedCristal()
    {
        capturedCristal++;
    }

    public static void AddDestroyed(Element element)
    {
        if (destroyed.ContainsKey(element.ID))
            destroyed[element.ID]++;
        else
            destroyed.Add(element.ID, 1);
    }

    public static uint Destroyed(Element element)
    {
        if (destroyed.ContainsKey(element.ID))
            return destroyed[element.ID];
        return 0;
    }

    public static void AddPut(int id)
    {
        if (put.ContainsKey(id))
            put[id]++;
        else
            put.Add(id, 1);
    }

    public static uint Put(WorkTop worktop)
    {
        if (put.ContainsKey(worktop.ID))
            return put[worktop.ID];
        return 0;
    }

    public static void AddUsed(int id)
    {
        if (used.ContainsKey(id))
            used[id]++;
        else
            used.Add(id, 1);
    }

    public static uint Used(Consumable consumable)
    {
        if (used.ContainsKey(consumable.ID))
            return used[consumable.ID];
        return 0;
    }

    public static void AddCrafted(Craft craft)
    {
        if (crafted.ContainsKey(craft.ID))
            crafted[craft.ID]++;
        else
            crafted.Add(craft.ID, 1);
    }

    public static void AddCrafted(int iDCraft)
    {
        if (crafted.ContainsKey(iDCraft))
            crafted[iDCraft]++;
        else
            crafted.Add(iDCraft, 1);
    }

    public static uint Crafted(Craft craft)
    {
        if (crafted.ContainsKey(craft.ID))
            return crafted[craft.ID];
        return 0;
    }

    public static void Load(string save)
    {
        int v = 0;
        string[] vars = save.Split('|');
        timePlayed = ulong.Parse(vars[v++]);
        tutoComplete = bool.Parse(vars[v++]);
        death = uint.Parse(vars[v++]);
        kill = uint.Parse(vars[v++]);
        capturedCristal = uint.Parse(vars[v++]);
        bosskill = bool.Parse(vars[v++]);

        destroyed = new Dictionary<int, uint>();
        string[] destroy = vars[v++].Split(':');
        if (destroy[0] != string.Empty)
            for (int i = 0; i < destroy.Length; i += 2)
                destroyed.Add(int.Parse(destroy[i]), uint.Parse(destroy[i + 1]));

        put = new Dictionary<int, uint>();
        string[] p = vars[v++].Split(':');
        if (p[0] != string.Empty)
            for (int i = 0; i < p.Length; i += 2)
                put.Add(int.Parse(p[i]), uint.Parse(p[i + 1]));

        used = new Dictionary<int, uint>();
        string[] use = vars[v++].Split(':');
        if (use[0] != string.Empty)
            for (int i = 0; i < use.Length; i += 2)
                used.Add(int.Parse(use[i]), uint.Parse(use[i + 1]));

        crafted = new Dictionary<int, uint>();
        string[] craft = vars[v++].Split(':');
        if (craft[0] != string.Empty)
            for (int i = 0; i < craft.Length; i += 2)
                crafted.Add(int.Parse(craft[i]), uint.Parse(craft[i + 1]));

        hunted = new Dictionary<int, uint>();
        string[] chasse = vars[v++].Split(':');
        if (chasse[0] != string.Empty)
            for (int i = 0; i < chasse.Length; i += 2)
                hunted.Add(int.Parse(chasse[i]), uint.Parse(chasse[i + 1]));

        cristalLevel = new Dictionary<int, uint>();
        string[] levelcristal = vars[v++].Split(':');
        if (levelcristal[0] != string.Empty)
            for (int i = 0; i < levelcristal.Length; i += 2)
                cristalLevel.Add(int.Parse(levelcristal[i]), uint.Parse(levelcristal[i + 1]));       
    }

    public static string Save()
    {
        string save = timePlayed.ToString() + "|" + tutoComplete.ToString() + "|" + death.ToString() + "|" + kill.ToString() + "|" + capturedCristal.ToString() + "|" + bosskill.ToString() + "|";

        foreach (int key in destroyed.Keys)
            save += key.ToString() + ":" + destroyed[key].ToString() + ":";
        if (save.EndsWith(":"))
            save = save.Remove(save.Length - 1);

        save += "|";

        foreach (int key in put.Keys)
            save += key.ToString() + ":" + put[key].ToString() + ":";
        if (save.EndsWith(":"))
            save = save.Remove(save.Length - 1);
        save += "|";

        foreach (int key in used.Keys)
            save += key.ToString() + ":" + used[key].ToString() + ":";
        if (save.EndsWith(":"))
            save = save.Remove(save.Length - 1);
        save += "|";

        foreach (int key in crafted.Keys)
            save += key.ToString() + ":" + crafted[key].ToString() + ":";
        if (save.EndsWith(":"))
            save = save.Remove(save.Length - 1);
        save += "|";

        foreach (int key in hunted.Keys)
            save += key.ToString() + ":" + hunted[key].ToString() + ":";
        if (save.EndsWith(":"))
            save = save.Remove(save.Length - 1);
        save += "|";

        foreach (int key in cristalLevel.Keys)
            save += key.ToString() + ":" + cristalLevel[key].ToString() + ":";
        if (save.EndsWith(":"))
            save = save.Remove(save.Length - 1);
        save += "|";

        return save;
    }

    public static string Empty()
    {
        return "0|False|0|0|0|False||||||";
    }
}