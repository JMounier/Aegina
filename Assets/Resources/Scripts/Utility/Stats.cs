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
    private static Dictionary<int,uint> destroyed;
    private static Dictionary<int, uint> put;
    private static Dictionary<int, uint> used;

    //Craft
    private static Dictionary<int, uint> crafted;

    public static ulong TimePlayer()
    {
        return timePlayed;
    }

    public static void IncrementTimePlayer()
    {
        timePlayed++;
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
        destroyed[element.ID]++;
    }

    public static uint Destroyed(Element element)
    {
        return destroyed[element.ID];
    }

    public static void AddPut(WorkTop worktop)
    {
        put[worktop.ID]++;
    }

    public static uint Put(WorkTop worktop)
    {
        return put[worktop.ID];
    }

    public static void AddUsed(Consumable consumable)
    {
        used[consumable.ID]++;
    }

    public static uint Used(Consumable consumable)
    {
        return used[consumable.ID];
    }

    public static void AddCrafted(Craft craft)
    {
        crafted[craft.ID]++;
    }

    public static uint Crafted(Craft craft)
    {
        return crafted[craft.ID];
    }
}
