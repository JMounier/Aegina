using UnityEngine;
using System.Collections;

public class Craft
    {
    private ItemStack product;
    private ItemStack[] consume;
    private bool fire, workbench, forge, brewer;
    private int id;
    private bool secret;
    public enum Type
    {
        None,
        Elementary,
        WorkTop,
        Consumable,
        Tools,
        Armor,
    }
    private Type what;

    /// <summary>
    /// instencie un craft 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="product"></param>
    /// <param name="fire"></param>
    /// <param name="workbench"></param>
    /// <param name="forge"></param>
    /// <param name="brewer"></param>
    /// <param name="consume"></param>
    public Craft(int id, ItemStack product, bool fire, bool workbench, bool forge, bool brewer,bool secret, Type what, params ItemStack[] consume)
    {
        this.id = id;
        this.product = product;
        this.consume = consume;
        this.fire = fire;
        this.workbench = workbench;
        this.forge = forge;
        this.brewer = brewer;
        this.what = what;
        this.secret = secret;
    }
    public Craft(Type what)
    {
        this.id = -1;
        this.product = new ItemStack();
        this.consume = new ItemStack[0];
        this.fire = false;
        this.workbench = false;
        this.forge = false;
        this.brewer = false;
        this.what = what;
        this.secret = false;
    }
    // Getters
    public int ID
    {
        get { return this.id; }
    }
    public ItemStack Product
    {
        get { return this.product; }
    }
    public ItemStack[] Consume
    {
        get { return this.consume; }
    }
    public bool Fire
    {
        get { return this.fire; }
    }
    public bool Workbench
    {
        get { return this.workbench; }
    }
    public bool Forge
    {
        get { return this.forge; }
    }
    public bool Brewer
    {
        get { return this.brewer; }
    }
    public bool Secret
    {
        get { return this.secret; }
    }
    public Type What
    {
        get { return this.what; }
    }
}
