using UnityEngine;
using System.Collections;

public enum FaceSide { None, forward, left, right, backward };

public class FSPath : MonoBehaviour
{


    private FaceSide dir = FaceSide.forward;
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private GameObject nextStep;

    #region Getters/Setters

    /// <summary>
    /// Gets the direction the cam should face.
    /// </summary>
    /// <value>The dir.</value>
    public FaceSide Dir
    {
        get { return this.dir; }
    }

    /// <summary>
    /// Gets the speed.
    /// </summary>
    /// <value>The speed.</value>
    public float Speed
    {
        get { return this.speed; }
    }

    /// <summary>
    /// Gets the next step.
    /// </summary>
    /// <value>The next step.</value>
    public GameObject NextStep
    {
        get { return this.nextStep; }
        set { this.nextStep = value; }
    }
    #endregion
}
