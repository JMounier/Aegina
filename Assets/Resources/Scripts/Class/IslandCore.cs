using UnityEngine;
using System.Collections;

/// <summary>
/// Les differentes teams
/// </summary>
public enum Team { Neutre, Orange, Red, Blue, Green };

/// <summary>
/// Creer un nouveau cristeau
/// </summary>
public class IslandCore : Element
{

    private Team t;
    private int level_upgrade;
    private int level_attack;
    private int level_prod;
    private int level_portal;

    public IslandCore() : base()
    {
        this.t = Team.Neutre;
        this.level_upgrade = 0;
        this.level_attack = 0;
        this.level_prod = 0;
        this.level_portal = 0;
    }

    public IslandCore(Team team, int level_attack, int level_prod, int level_portal) : base()
    {
        this.t = team;
        this.level_attack = level_attack;
        this.level_portal = level_portal;
        this.level_prod = level_prod;
        this.level_upgrade = level_prod + level_portal + level_attack;
    }

    // Getters & Setters

    /// <summary>
    /// La team a la quelle appartient le cristal
    /// </summary>
    public Team T
    {
        get { return this.t; }
        set
        {
            this.t = value;
            prefab.GetComponentInChildren<MeshRenderer>().materials[0] = Resources.Load<Material>("Models/Components/Islands/Materials/Team" + (int)value);
        }
    }

    /// <summary>
    /// Le niveau total du cristal
    /// </summary>
    public int Level_tot
    {
        get { return this.level_upgrade; }
        set { this.level_upgrade = value; }
    }

    /// <summary>
    /// Le niveau de production du cristal
    /// </summary>
    public int Level_prod
    {
        get { return this.level_prod; }
        set { this.level_prod = value; }
    }

    /// <summary>
    /// Le niveau d'attaque du cristal
    /// </summary>
    public int Level_atk
    {
        get { return this.level_attack; }
        set { this.level_attack = value; }
    }

    /// <summary>
    /// Le niveau de portail du cristal
    /// </summary>
    public int Level_portal
    {
        get { return this.level_portal; }
        set { this.level_portal = value; }
    }
}
