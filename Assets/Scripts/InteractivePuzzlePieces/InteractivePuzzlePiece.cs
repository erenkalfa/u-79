using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractivePuzzlePiece<TComponent> : BaseInteractivePuzzlePiece
where TComponent : Component
{
    public TComponent physicsComponent;
}


public abstract class BaseInteractivePuzzlePiece : MonoBehaviour
{
    public Rigidbody rb;
}