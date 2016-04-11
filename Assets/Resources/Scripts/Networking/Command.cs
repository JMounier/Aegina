using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Command
{
    // Database
    private static readonly Command Help = new Command("help", "/help [page]", false);
    private static readonly Command Msg = new Command("msg", "/msg <player> <message>", false, "m");
    private static readonly Command Seed = new Command("seed", "/seed", false);

    private static readonly Command Time = new Command("time", "/time <value>", true);
    private static readonly Command Give = new Command("give", "/give <player> <id> [quantity]", true);
    private static readonly Command Tp = new Command("tp", "/tp <player>", true);
    private static readonly Command Kick = new Command("kick", "/kick <player>", true);
    private static readonly Command Save = new Command("save", "/save", true);
    private static readonly Command Kill = new Command("kill", "/kill <player>", true, "k");
    private static readonly Command Effect = new Command("effect", "/effect <player> <id> [power]", true);
    private static readonly Command Op = new Command("op", "/op <player>", true);
    private static readonly Command DeOp = new Command("deop", "/deop <player>", true);


    /// <summary>
    /// Liste tous les biomes du jeu. (Utilisez avec foreach)
    /// </summary>
    private static IEnumerable<Command> Commands
    {
        get
        {
            yield return Help;
            yield return Msg;
            yield return Seed;

            yield return Time;
            yield return Give;
            yield return Tp;
            yield return Kick;
            yield return Save;
            yield return Kill;
            yield return Effect;
            yield return Op;
            yield return DeOp;
        }
    }


    // La command
    private bool opOnly;
    private string utilization;
    private List<string> names;

    public Command(string name, string utilization, bool opOnly, params string[] aliases)
    {
        this.opOnly = opOnly;
        this.utilization = utilization;
        this.names = new List<string>(aliases);
        this.names.Add(name);
    }

    /// <summary>
    /// Lance une commande.
    /// </summary>
    /// <param name="cmd">La commande</param>
    /// <param name="sender">Le gameibject du joueur envoyant la commande.</param>
    public static void LaunchCommand(string command, GameObject sender)
    {
        string[] cmd = command.Split(default(Char[]), StringSplitOptions.RemoveEmptyEntries);
        cmd[0] = cmd[0].Substring(1, cmd[0].Length - 1);
        foreach (Command c in Commands)
        {
            if (c.names.Contains(cmd[0].ToLower()))
            {
                if (!c.opOnly || sender.GetComponent<Social>().IsOp)
                {
                    string[] parameters = new string[cmd.Length - 1];
                    for (int i = 0; i < parameters.Length; i++)                    
                        parameters[i] = cmd[i + 1];                    
                    ExecuteCommand(c.names[0], parameters, sender);
                }
                else
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>You must be an operator to execute this command.</color>");
                return;
            }
        }
        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Unknow command. Try /help for a list of commands.</color>");
    }

    private static void ExecuteCommand(string name, string[] cmd, GameObject sender)
    {
        switch (cmd[0].ToLower())
        {
            // HELP
            case "help":
                try
                {                    
                    else
                        throw new Exception();
                }
                catch
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /help [page]</color>");
                }
                break;

            // TIME
            case "time":
                try
                {
                    int time = cmd[1].ToLower() == "day" ? 300 : cmd[1].ToLower() == "night" ? 900 : int.Parse(cmd[1]);
                    GameObject.Find("Map").GetComponent<DayNightCycle>().SetTime(time);
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        player.GetComponent<Social>().RpcReceiveMsg(this.namePlayer + " set the time to " + cmd[1] + ".");
                }
                catch
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /time <value></color>");
                }
                break;

            // GIVE
            case "give":
                try
                {
                    Item give;
                    GameObject recipient = null;
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                            recipient = player;
                    if (recipient == null)
                    {
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                        return;
                    }
                    int id;
                    switch (cmd.Length)
                    {
                        case 3:
                            if (int.TryParse(cmd[2], out id))
                                give = ItemDatabase.Find(id);
                            else
                                give = ItemDatabase.Find(cmd[2]);

                            recipient.GetComponent<Inventory>().RpcAddItemStack(give.ID, 1, null);
                            sender.GetComponent<Social>().RpcReceiveMsg("Give 1 of " + give.NameText.GetText(SystemLanguage.English) + ".");
                            if (!sender.Equals(recipient))
                                recipient.GetComponent<Social>().RpcReceiveMsg("Give 1 of " + give.NameText.GetText(SystemLanguage.English) + ".");
                            break;
                        default:
                            if (int.TryParse(cmd[2], out id))
                                give = ItemDatabase.Find(id);
                            else
                                give = ItemDatabase.Find(cmd[2]);

                            recipient.GetComponent<Inventory>().RpcAddItemStack(give.ID, int.Parse(cmd[3]), null);
                            sender.GetComponent<Social>().RpcReceiveMsg("Give " + int.Parse(cmd[3]) + " of " + give.NameText.GetText(SystemLanguage.English) + ".");
                            if (!sender.Equals(recipient))
                                recipient.GetComponent<Social>().RpcReceiveMsg("Give " + int.Parse(cmd[3]) + " of " + give.NameText.GetText(SystemLanguage.English) + ".");
                            break;
                    }
                }
                catch
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /give <player> <id> [quantity]</color>");
                }
                break;

            // MSG & M
            case "/msg":
            case "/m":
                try
                {
                    if (cmd.Length < 3)
                        throw new System.Exception();
                    if (cmd[1].ToLower() == sender.GetComponent<Social>().namePlayer.ToLower())
                    {
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Do you feel alone ?</color>");
                        return;
                    }
                    string text = "";
                    for (int i = 2; i < cmd.Length; i++)
                        text += cmd[i] + " ";
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                        {
                            player.GetComponent<Social>().RpcReceiveMsg("<color=grey><b>[" + sender.GetComponent<Social>().namePlayer + " -> You]</b></color> " + text);
                            sender.GetComponent<Social>().RpcReceiveMsg("<color=grey><b>[You -> " + player.GetComponent<Social>().namePlayer + "]</b></color> " + text);
                            return;
                        }
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                }
                catch
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /msg <player> <message></color>");
                }
                break;
            // TP
            case "/tp":
                try
                {
                    if (cmd[1].ToLower() == this.namePlayer)
                    {
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>You are already where you are...</color>");
                        return;
                    }
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                        {
                            player.GetComponent<Social>().RpcReceiveMsg(this.namePlayer + " teleported on you.");
                            sender.GetComponent<Social>().RpcReceiveMsg("You were teleported on " + cmd[1] + ".");
                            sender.GetComponent<Social>().RpcTeleport(player.GetComponentInChildren<CharacterCollision>().transform.position);
                            return;
                        }
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                }
                catch
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /tp <player></color>");
                }
                break;
            // KICK
            case "/kick":
                try
                {
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                        {
                            player.GetComponent<Social>().RpcKickPlayer();
                            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                                p.GetComponent<Social>().RpcReceiveMsg(this.namePlayer + " kick " + player.GetComponent<Social>().namePlayer + ".");
                            return;
                        }
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                }
                catch
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /kick <player></color>");
                }
                break;
            // SAVE
            case "/save":
                GameObject.Find("Map").GetComponent<Save>().SaveWorld();
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    player.GetComponent<Social>().RpcReceiveMsg("The world has been forcefully saved.");
                break;
            // SEED
            case "/seed":
                sender.GetComponent<Social>().RpcReceiveMsg("Seed : " + MapGeneration.SeedToString(GameObject.Find("Map").GetComponent<Save>().Seed));
                break;
            // KILL
            case "/kill":
                try
                {
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                        {
                            player.GetComponent<SyncCharacter>().Life = 0;
                            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                                if (this.namePlayer.ToLower() == cmd[1].ToLower())
                                    p.GetComponent<Social>().RpcReceiveMsg(this.namePlayer + " commited suicide.");
                                else
                                    p.GetComponent<Social>().RpcReceiveMsg(this.namePlayer + " kill " + player.GetComponent<Social>().namePlayer + ".");
                            return;
                        }
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                }
                catch
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /kill <player></color>");
                }
                break;
            // Effect
            case "/effect":
                try
                {
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                        if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                        {
                            SyncCharacter syncCharacter = player.GetComponent<SyncCharacter>();
                            Effect.EffectType effect = ((Effect.EffectType)int.Parse(cmd[2]));
                            int power = 1;
                            if (cmd.Length > 3)
                                power = int.Parse(cmd[3]);

                            switch (effect)
                            {
                                case Effect.EffectType.Speed:
                                    syncCharacter.Speed = power * 2;
                                    syncCharacter.CdSpeed = power * 30;
                                    sender.GetComponent<Social>().RpcReceiveMsg("Effect speed level " + power + " applied.");
                                    break;
                                case Effect.EffectType.Slowness:
                                    break;
                                case Effect.EffectType.Haste:
                                    break;
                                case Effect.EffectType.MiningFatigue:
                                    break;
                                case Effect.EffectType.Strength:
                                    break;
                                case Effect.EffectType.InstantHealth:
                                    syncCharacter.Life += 10 * power;
                                    sender.GetComponent<Social>().RpcReceiveMsg("Effect instant health level " + power + " applied.");
                                    break;
                                case Effect.EffectType.InstantDamage:
                                    break;
                                case Effect.EffectType.JumpBoost:
                                    syncCharacter.Jump = power * 5000;
                                    syncCharacter.CdJump = power * 30;
                                    sender.GetComponent<Social>().RpcReceiveMsg("Effect jump boost level " + power + " applied.");
                                    break;
                                case Effect.EffectType.Regeneration:
                                    syncCharacter.Regen = power;
                                    syncCharacter.CdRegen = power * 15;
                                    sender.GetComponent<Social>().RpcReceiveMsg("Effect regeneration level " + power + " applied.");
                                    break;
                                case Effect.EffectType.Resistance:
                                    break;
                                case Effect.EffectType.Hunger:
                                    break;
                                case Effect.EffectType.Weakness:
                                    break;
                                case Effect.EffectType.Poison:
                                    syncCharacter.Poison = power;
                                    syncCharacter.CdPoison = power * 15;
                                    sender.GetComponent<Social>().RpcReceiveMsg("Effect poison level " + power + " applied.");
                                    break;
                                case Effect.EffectType.Saturation:
                                    syncCharacter.Hunger += 10 * power;
                                    sender.GetComponent<Social>().RpcReceiveMsg("Effect saturation level " + power + " applied.");
                                    break;
                                case Effect.EffectType.Thirst:
                                    break;
                                case Effect.EffectType.Refreshment:
                                    syncCharacter.Thirst += 10 * power;
                                    sender.GetComponent<Social>().RpcReceiveMsg("Effect refreshment level " + power + " applied.");
                                    break;
                                default:
                                    break;
                            }
                            return;
                        }
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                }
                catch
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /effect <player> <id> [power]</color>");
                }
                break;
            default:
                break;
        }
    }
}
