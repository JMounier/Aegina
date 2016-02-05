using UnityEngine;
using System.Collections;

public class IslandCore : Element {

    private int team;
    private int level_upgrade;
    private int level_attack;
    private int level_prod;
    private int level_portal;
	public IslandCore(int team,int level_attack,int level_prod, int level_portal)
        :base()
    {
        this.team = team;
        this.level_attack = level_attack;
        this.level_portal = level_portal;
        this.level_prod = level_prod;
        this.level_upgrade = level_prod + level_portal + level_attack;
    }
    public IslandCore()
        : base()
    {
        this.team = 0;
        this.level_upgrade = 0;
    }
    // Methods
    /// <summary>
    /// augmente le niveau d'upgrade du cristal
    /// </summary>
    public void Level_up()
    {
        this.level_upgrade += 1;
    }
    public void Level_up (int num_level)
    {
        this.level_upgrade += num_level;
    }
    /// <summary>
    /// augmente le niveau d'upragde de production du cristal
    /// </summary>
    public void Level_up_prod()
    {
        this.level_prod += 1;
        this.level_upgrade += 1;
    }
    public void Level_up_prod(int num_level)
    {
        this.level_prod += num_level;
        this.level_upgrade += num_level;
    }
    /// <summary>
    /// augmente le niveau d'uprgrade d'attaque du cristal
    /// </summary>
    public void Level_up_atk()
    {
        this.level_attack += 1;
        this.level_upgrade += 1;
    }
    public void Level_up_atk(int num_level)
    {
        this.level_attack += num_level;
        this.level_upgrade += num_level;
    }
    /// <summary>
    /// augmente le niveau d'uprgrade de portail du cristal
    /// </summary>
    public void Level_up_porte()
    {
        this.level_portal += 1;
        this.level_upgrade += 1;
    }
    public void Level_up_porte(int num_level)
    {
        this.level_portal += num_level;
        this.level_upgrade += num_level;
    }
    // getters Setters
    public int Team
    {
        get { return this.team; }
        set { this.team = value; }
    }
    public int Level_tot
    {
        get { return this.level_upgrade; }
    }
    public int Level_prod
    {
        get { return this.level_prod; }
    }
    public int Level_atk
    {
        get { return this.level_attack; }
    }
    public int Level_portal
    {
        get { return this.level_portal; }
    }
}
