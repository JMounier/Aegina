using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Stats
{
    //General
    private static ulong timePlayed;

    //PVE
    private static uint hunt;
    private static uint death;

    //PVP
    private static uint kill;

    //Cristal
    private static uint capturedCristal;

    //Interact (clic droit)
    private static Dictionary<int, uint> destroyed;
    private static Dictionary<int, uint> put;
    private static Dictionary<int, uint> used;

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

    public static uint Hunt()
    {
        return hunt;
    }

    public static void IncrementHunt()
    {
        hunt++;
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

    public static void AddPut(WorkTop worktop)
    {
        if (put.ContainsKey(worktop.ID))
            put[worktop.ID]++;
        else
            put.Add(worktop.ID, 1);
    }

    public static uint Put(WorkTop worktop)
    {
        if (put.ContainsKey(worktop.ID))
            return put[worktop.ID];
        return 0;
    }

    public static void AddUsed(Consumable consumable)
    {
        if (used.ContainsKey(consumable.ID))
            used[consumable.ID]++;
        else
            used.Add(consumable.ID, 1);
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
        string[] vars = save.Split('|');
        timePlayed = ulong.Parse(vars[0]);
        hunt = uint.Parse(vars[1]);
        death = uint.Parse(vars[2]);
        kill = uint.Parse(vars[3]);
        capturedCristal = uint.Parse(vars[4]);

        destroyed = new Dictionary<int, uint>();
        string[] destroy = vars[5].Split(':');
        if (destroy[0] != string.Empty)
            for (int i = 0; i < destroy.Length; i += 2)
                destroyed.Add(int.Parse(destroy[i]), uint.Parse(destroy[i + 1]));

        put = new Dictionary<int, uint>();
        string[] p = vars[6].Split(':');
        if (p[0] != string.Empty)
            for (int i = 0; i < p.Length; i += 2)
                put.Add(int.Parse(p[i]), uint.Parse(p[i + 1]));

        used = new Dictionary<int, uint>();
        string[] use = vars[7].Split(':');
        if (use[0] != string.Empty)
            for (int i = 0; i < use.Length; i += 2)
                used.Add(int.Parse(use[i]), uint.Parse(use[i + 1]));

        crafted = new Dictionary<int, uint>();
        string[] craft = vars[8].Split(':');
        if (craft[0] != string.Empty)
            for (int i = 0; i < craft.Length; i += 2)
                crafted.Add(int.Parse(craft[i]), uint.Parse(craft[i + 1]));
    }

    public static string Save()
    {
        string save = timePlayed.ToString() + "|" + hunt.ToString() + "|" + death.ToString() + "|" + kill.ToString() + "|" + capturedCristal.ToString() + "|";

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

        return save;
    }
}