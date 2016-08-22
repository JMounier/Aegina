using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public enum Dir { x, y, z };

public class SpecMode : NetworkBehaviour
{

    private Transform cam;
    private GameObject character;
    private bool spectate;

    private Orbiter orb;

    // Use this for initialization
    void Start()
    {
        this.spectate = false;
        this.character = gameObject.transform.GetChild(1).gameObject;
        this.orb = null;
        if (isLocalPlayer)
            this.cam = gameObject.transform.GetChild(0);
    }

    #region Passage Invisible
    /// <summary>
    /// Changes the gamemode (this method is called on serveur only by the social system !!!).
    /// </summary>
    /// <param name="spec">If set to <c>true</c> spec.</param>
    public void SetSpectate(bool spectate)
    {
        this.spectate = spectate;

        foreach (Renderer renderer in this.character.GetComponentsInChildren<Renderer>())
            renderer.enabled = !spectate;

        this.character.GetComponent<Rigidbody>().useGravity = !spectate;
        this.character.GetComponent<Collider>().enabled = !spectate;
        RpcSetSpectate(spectate);
    }


    [ClientRpc]
    /// <summary>
    /// Rpcs the change mode.
    /// ne prend pas en compte le changement d'arme ou d'armure mais fuck it
    /// </summary>
    /// <param name="notSpec">If set to <c>true</c> not spec.</param>
    private void RpcSetSpectate(bool spectate)
    {
        this.spectate = spectate;

        foreach (Renderer renderer in this.character.GetComponentsInChildren<Renderer>())
            renderer.enabled = !spectate;

        this.character.GetComponent<Rigidbody>().useGravity = !spectate;
        this.character.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.character.GetComponent<Collider>().enabled = !spectate;

        if (isLocalPlayer)
        {
            InputManager.seeGUI = !spectate;
            gameObject.GetComponent<Controller>().FPS = spectate;
        }
    }
    #endregion


    #region Orbiter
    public void SetOrbit()
    {
        RpcSetOrbit();
    }

    [ClientRpc]
    private void RpcSetOrbit()
    {
        if (!isLocalPlayer)
            return;
        if (this.orb != null)
            this.orb.clear();
        this.orb = new Orbiter(this.character.transform.position, this.character.transform.rotation.eulerAngles);
    }

    public void StartOrbit(bool sens)
    {
        RpcStartOrbit(sens);
    }

    [ClientRpc]
    private void RpcStartOrbit(bool sens)
    {
        if (!isLocalPlayer)
            return;
        if (this.orb != null)
        {
            this.orb.start();
            gameObject.GetComponent<Controller>().StartOrbiting(sens);
        }
    }

    public void MoveOrbit(Dir dir, int power)
    {
        RpcMoveOrbit(dir, power);
    }

    [ClientRpc]
    private void RpcMoveOrbit(Dir dir, int power)
    {
        if (!isLocalPlayer)
            return;
        Vector3 move = Vector3.zero;
        switch (dir)
        {
            case Dir.x:
                move = Vector3.right;
                break;
            case Dir.y:
                move = Vector3.up;
                break;
            case Dir.z:
                move = Vector3.forward;
                break;
            default:
                break;
        }
        this.orb.Center.transform.Translate(move * power);
    }


    public void RotateOrbit(Dir dir, int power)
    {
        RpcRotateOrbit(dir, power);
    }

    [ClientRpc]
    private void RpcRotateOrbit(Dir dir, int power)
    {
        if (!isLocalPlayer)
            return;
        Vector3 move = Vector3.zero;
        switch (dir)
        {
            case Dir.x:
                move = Vector3.right;
                break;
            case Dir.y:
                move = Vector3.up;
                break;
            case Dir.z:
                move = Vector3.forward;
                break;
            default:
                break;
        }
        this.orb.Center.transform.Rotate(move * power);
    }


    public void ShowOrbit(bool enable)
    {
        RpcShowOrbit(enable);
    }


    [ClientRpc]
    private void RpcShowOrbit(bool enable)
    {
        if (isLocalPlayer)
            this.orb.show(enable);
    }
    #endregion

    #region Gettters/Setters

    public bool isSpec
    {
        get { return this.spectate; }
    }

    public Orbiter Orbite
    {
        get { return this.orb; }
    }
    #endregion
}
