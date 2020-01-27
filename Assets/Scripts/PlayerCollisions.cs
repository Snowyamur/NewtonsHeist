using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class PlayerCollisions : MonoBehaviour
{
  [Header("Offsets of Player")]
  [SerializeField] Vector2 bottomOffset = new Vector2(0.11f, 0);
  [SerializeField] Vector2 rightOffset = new Vector2(0.11f, 0);
  [SerializeField] Vector2 leftOffset = new Vector2(0.11f, 0);
  [SerializeField] float collisionRadius = 0.25f;

  [Space]

  [Header("Collision Checks")]
  [SerializeField] bool onGround;
  [SerializeField] bool onCeiling;
  [SerializeField] bool onWall;
  [SerializeField] bool inAir;
  //[SerializeField] bool onRightWall;
  //[SerializeField] bool onLeftWall;
  //[SerializeField] int wallSide;
  [SerializeField] Transform groundTransform;

  [Space]

  [Header("Layers")]
  [SerializeField] LayerMask enemyLayer;
  [SerializeField] LayerMask groundLayer;
  [SerializeField] LayerMask ceilingLayer;
  [SerializeField] LayerMask wallLayer;

  Rigidbody2D rb;

  PlayerMechanics mechanics;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    mechanics = GetComponent<PlayerMechanics>();
  }

  void Start()
  {
      groundTransform = transform.Find("groundTransform");
      groundLayer = LayerMask.GetMask("Ground");
      wallLayer = LayerMask.GetMask("Wall");
      ceilingLayer = LayerMask.GetMask("Ceiling");
      enemyLayer = LayerMask.GetMask("Enemy");
  }

  void Update()
  {
      onGround = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, collisionRadius, groundLayer);

      onCeiling = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, collisionRadius, ceilingLayer);

      onWall = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, collisionRadius, wallLayer);
      /*onRightWall = Physics2D.OverlapCircle((Vector2)transform.position+rightOffset, collisionRadius, groundLayer);
      onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position+leftOffset, collisionRadius, groundLayer);

      wallSide = onRightWall ? 1 : -1;*/

      if(onGround || onCeiling || onWall) //If the player is standing on ground
      {
          inAir = false;
          mechanics.isJumping = false;
          mechanics.isIdle = true;
      }
  }


  void OnTriggerEnter2D(Collider2D other)
  {
      if(other.gameObject.tag == "Enemy")
      {
          //If hit with an enemy, dies
          Destroy(this);
      }
  }

  public bool IsInAir() //Checks if currently in air
  {
      return inAir;
  }
  public bool IsOnGround()
  {
      return onGround;
  }
  public bool IsOnWall()
  {
      return onWall;
  }
  public bool IsOnCeiling()
  {
      return onCeiling;
  }


}
