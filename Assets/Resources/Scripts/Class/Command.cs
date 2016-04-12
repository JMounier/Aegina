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
    private static readonly Command Give = new Command("give", "/give [player] <id> [quantity]", true);
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

    private static int NbCommands(bool isOp)
    {
        int n = 0;
        foreach (Command c in Commands)
            n++;
        return n;
    }

    // La command
    private bool opOnly;
    private string utilization;
    private List<string> names;

    private Command(string name, string utilization, bool opOnly, params string[] aliases)
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
                    ExecuteCommand(c, parameters, sender);
                }
                else
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>You must be an operator to execute this command.</color>");
                return;
            }
        }
        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Unknow command. Try /help for a list of commands.</color>");
    }

    private static void ExecuteCommand(Command c, string[] parameters, GameObject sender)
    {
        string namePlayer = sender.GetComponent<Social>().PlayerName;
        bool isOp = sender.GetComponent<Social>().IsOp;

        try
        {
            // HELP
            if (c == Help)
            {
                int page = 1;
                int nbpages = (NbCommands(isOp) - 1) / 5 + 1;
                if (parameters.Length > 0)
                    page = int.Parse(parameters[0]);
                if (page < 1 || page > nbpages)
                    throw new Exception();
                string help = "<color=green>---This is the list of commands---</color>\n";
                int count = 5 * page;
                foreach (Command command in Commands)
                {
                    if (isOp || !command.opOnly)
                    {
                        if (count > 0 && count < 6)
                            help += command.utilization + "\n";
                        count--;
                    }
                }
                help += "<color=green>--- Page " + page + "/" + nbpages + " ---</color>";
                sender.GetComponent<Social>().RpcReceiveMsg(help);
            }
            // TIME
            else if (c == Time)
            {
                int time = parameters[0].ToLower() == "day" ? 300 : parameters[0].ToLower() == "night" ? 900 : int.Parse(parameters[0]);
                GameObject.Find("Map").GetComponent<DayNightCycle>().SetTime(time);
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    player.GetComponent<Social>().RpcReceiveMsg(namePlayer + " set the time to " + parameters[0] + ".");
            }
            // GIVE
            else if (c == Give)
            {
                Item give;
                GameObject recipient = sender;
                int posId = 0;
                int id = 0;
                int quantity = 1;
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                    {
                        recipient = player;
                        posId = 1;
                    }

                if (int.TryParse(parameters[posId], out id))
                    give = ItemDatabase.Find(id);
                else
                    give = ItemDatabase.Find(parameters[posId]);

                // Quantity
                if (parameters.Length - posId == 2)
                    quantity = int.Parse(parameters[posId + 1]);

                recipient.GetComponent<Inventory>().RpcAddItemStack(give.ID, quantity, null);

                sender.GetComponent<Social>().RpcReceiveMsg("Give " + quantity + " of " + give.NameText.GetText(SystemLanguage.English) + ".");
                if (!sender.Equals(recipient))
                    recipient.GetComponent<Social>().RpcReceiveMsg("Give " + quantity + " of " + give.NameText.GetText(SystemLanguage.English) + ".");
            }
            // MSG
            else if (c == Msg)
            {

                if (parameters.Length < 2)
                    throw new Exception();
                if (parameters[0].ToLower() == namePlayer.ToLower())
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Do you feel alone ?</color>");
                    return;
                }
                string text = "";
                for (int i = 1; i < parameters.Length; i++)
                    text += parameters[i] + " ";
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                    {
                        player.GetComponent<Social>().RpcReceiveMsg("<color=grey><b>[" + namePlayer + " -> You]</b></color> " + text);
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=grey><b>[You -> " + player.GetComponent<Social>().PlayerName + "]</b></color> " + text);
                        return;
                    }
                sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + parameters[0] + " doesn't find.</color>");
            }
            // TP
            else if (c == Tp)
            {
                if (parameters[0].ToLower() == namePlayer)
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>You are already where you are...</color>");
                    return;
                }
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                    {
                        player.GetComponent<Social>().RpcReceiveMsg(namePlayer + " teleported on you.");
                        sender.GetComponent<Social>().RpcReceiveMsg("You were teleported on " + player.GetComponent<Social>().PlayerName + ".");
                        sender.GetComponent<Social>().RpcTeleport(player.GetComponentInChildren<CharacterCollision>().transform.position);
                        return;
                    }
                sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + parameters[0] + " doesn't find.</color>");
            }
            // KICK
            else if (c == Kick)
            {
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                    {
                        if (!player.GetComponent<Social>().IsOp)
                        {
                            player.GetComponent<Social>().RpcKickPlayer();
                            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                                p.GetComponent<Social>().RpcReceiveMsg(namePlayer + " kick " + player.GetComponent<Social>().PlayerName + ".");
                            return;
                        }
                        else
                        {
                            sender.GetComponent<Social>().RpcReceiveMsg("<color=red>" + parameters[0] + " is op, so you can't kick him.</color>");
                            return;
                        }
                    }
                sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + parameters[0] + " doesn't find.</color>");
            }
            // SAVE
            else if (c == Save)
            {
                GameObject.Find("Map").GetComponent<Save>().SaveWorld();
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    player.GetComponent<Social>().RpcReceiveMsg("The world has been forcefully saved by " + namePlayer + ".");
            }
            // SEED
            else if (c == Seed)
                sender.GetComponent<Social>().RpcReceiveMsg("Seed : " + MapGeneration.SeedToString(GameObject.Find("Map").GetComponent<Save>().Seed));
            // KILL
            else if (c == Kill)
            {
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                    {
                        player.GetComponent<SyncCharacter>().Life = 0;
                        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                            if (namePlayer.ToLower() == parameters[0].ToLower())
                                p.GetComponent<Social>().RpcReceiveMsg(namePlayer + " commited suicide.");
                            else
                                p.GetComponent<Social>().RpcReceiveMsg(namePlayer + " killed " + player.GetComponent<Social>().PlayerName + ".");
                        return;
                    }
                sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + parameters[0] + " doesn't find.</color>");
            }
            // Effect
            else if (c == Effect)
            {
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                    {
                        SyncCharacter syncCharacter = player.GetComponent<SyncCharacter>();
                        Effect.EffectType effect = ((Effect.EffectType)int.Parse(parameters[1]));
                        int power = 1;
                        if (parameters.Length > 2)
                            power = int.Parse(parameters[2]);

                        switch (effect)
                        {
                            case global::Effect.EffectType.Speed:
                                syncCharacter.Speed = power * 2;
                                syncCharacter.CdSpeed = power * 30;
                                sender.GetComponent<Social>().RpcReceiveMsg("Effect speed level " + power + " applied.");
                                break;
                            case global::Effect.EffectType.Slowness:
                                break;
                            case global::Effect.EffectType.Haste:
                                break;
                            case global::Effect.EffectType.MiningFatigue:
                                break;
                            case global::Effect.EffectType.Strength:
                                break;
                            case global::Effect.EffectType.InstantHealth:
                                syncCharacter.Life += 10 * power;
                                sender.GetComponent<Social>().RpcReceiveMsg("Effect instant health level " + power + " applied.");
                                break;
                            case global::Effect.EffectType.InstantDamage:
                                break;
                            case global::Effect.EffectType.JumpBoost:
                                syncCharacter.Jump = power * 5000;
                                syncCharacter.CdJump = power * 30;
                                sender.GetComponent<Social>().RpcReceiveMsg("Effect jump boost level " + power + " applied.");
                                break;
                            case global::Effect.EffectType.Regeneration:
                                syncCharacter.Regen = power;
                                syncCharacter.CdRegen = power * 15;
                                sender.GetComponent<Social>().RpcReceiveMsg("Effect regeneration level " + power + " applied.");
                                break;
                            case global::Effect.EffectType.Resistance:
                                break;
                            case global::Effect.EffectType.Hunger:
                                break;
                            case global::Effect.EffectType.Weakness:
                                break;
                            case global::Effect.EffectType.Poison:
                                syncCharacter.Poison = power;
                                syncCharacter.CdPoison = power * 15;
                                sender.GetComponent<Social>().RpcReceiveMsg("Effect poison level " + power + " applied.");
                                break;
                            case global::Effect.EffectType.Saturation:
                                syncCharacter.Hunger += 10 * power;
                                sender.GetComponent<Social>().RpcReceiveMsg("Effect saturation level " + power + " applied.");
                                break;
                            case global::Effect.EffectType.Thirst:
                                break;
                            case global::Effect.EffectType.Refreshment:
                                syncCharacter.Thirst += 10 * power;
                                sender.GetComponent<Social>().RpcReceiveMsg("Effect refreshment level " + power + " applied.");
                                break;
                            default:
                                break;
                        }
                        return;
                    }
                sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + parameters[0] + " doesn't find.</color>");
            }
            // Op
            else if (c == Op)
            {
                if (parameters[0].ToLower() == namePlayer.ToLower())
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>You are already op.</color>");
                    return;
                }
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                    {
                        if (!player.GetComponent<Social>().IsOp)
                        {
                            player.GetComponent<Social>().IsOp = true;
                            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                                if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                                    p.GetComponent<Social>().RpcReceiveMsg(player.GetComponent<Social>().PlayerName + " has been op by " + namePlayer + ".");
                        }
                        else                        
                            sender.GetComponent<Social>().RpcReceiveMsg("<color=red>" + player.GetComponent<Social>().PlayerName + " is already op.</color>");                        
                        return;
                    }
                sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + parameters[0] + " doesn't find.</color>");
            }
            // DeOp
            else if (c == DeOp)
            {
                if (parameters[0].ToLower() == namePlayer.ToLower())
                {
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>You can't deop yourself.</color>");
                    return;
                }
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                    {
                        if (player.GetComponent<Social>().IsOp)
                        {
                            player.GetComponent<Social>().IsOp = false;
                            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                                if (player.GetComponent<Social>().PlayerName.ToLower() == parameters[0].ToLower())
                                    p.GetComponent<Social>().RpcReceiveMsg(player.GetComponent<Social>().PlayerName + " has been deop by " + namePlayer + ".");
                        }
                        else
                            sender.GetComponent<Social>().RpcReceiveMsg("<color=red>" + player.GetComponent<Social>().PlayerName + " isn't op.</color>");
                        return;
                    }
                sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + parameters[0] + " doesn't find.</color>");
            }
        }
        catch
        {
            sender.GetComponent<Social>().RpcReceiveMsg("<color=red>" + c.utilization + "</color>");
        }
    }
}
