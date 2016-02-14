using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncCore : SyncElement {
	[SyncVar]
	private Team team;

	// GETTERS && SETTERS
	[Command]
	public void CmdSetTeam(Team team)
	{
		this.Team = team;
	}

	public Team Team{
		get{return this.team;}
		set{ this.team = value;}
	}
}
