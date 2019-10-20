using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movable Object/Settings", fileName = "MovableData")]
public class MovableSettings : ScriptableObject
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private bool useAi = false;

    public float MoveSpeed { get { return moveSpeed; } } 
    public float JumpSpeed { get { return jumpSpeed; } }
    public bool UseAi { get { return useAi; } }
}
