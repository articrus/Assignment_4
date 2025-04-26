using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
/*
 * Written By: Gianni Coladonato
 * Date Created: 19-11-2025 | Last Modified: 13-04-2025
 * 
 * This script is used to control the player and manage inputs
 */
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    //Object Components
    Vector2 moveInput;
    Rigidbody2D rigidbod;
    Animator anim;
    TouchingDirections touchingDirections;
    Damageable damageable;
    LevelLoader switcher;
    public PlayerDamage playerDamage;
    public LinkedList<GameObject> weapons;
    public GameObject baseWeapon;
    [SerializeField] GameObject currentWeapon;
    //Temporary for testing
    [SerializeField] List<GameObject> testWeapons;

    //Instance
    public static PlayerController PCInstance;

    //Player variables
    public PlayerInfo playerInfo;
    public float walkSpeed = 4.0f;
    public float runSpeed = 7.0f;
    public float jumpPower = 10.0f;
    public bool safeDestroy;
    public bool keyA, keyB;

    //These variables relate to powerups and upgrades
    public float jumpBoost = 1.0f;
    public bool hasSpellJump;
    public bool hasSpellBolt;

    //Upgrade boosts
    public int spdBoost;
    private float atkBoost;
    public float AttackBoost
    {
        get { return atkBoost; }
        set { atkBoost = value; playerDamage.ApplyDamageBoost(atkBoost); }
    }

    //If the player can move, either walk or run if the player is running
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (IsRunning) { return runSpeed; }
                    else { return walkSpeed; }
                }
                else { return 0; } //Idle speed
            }
            else { return 0; } //Lock player movement, either during an attack or cutscene
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    [SerializeField]
    private bool _isRunning = false;
    public bool _isFacingRight = true;

    public bool CanMove { get { return anim.GetBool(AnimationStrings.canMove); } }

    public bool IsMoving {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            anim.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
        private set
        {
            _isRunning = value;
            anim.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool IsAlive { get { return anim.GetBool(AnimationStrings.isAlive); } }


    //Used for when receiving knockback from an attack
    public bool LockVelocity
    {
        get { return anim.GetBool(AnimationStrings.lockVelocity); }
        set { anim.SetBool(AnimationStrings.lockVelocity, value); }
    }

    private void Awake()
    {
        if(PCInstance == null) { PCInstance = this; }
        weapons = new LinkedList<GameObject>();
        playerDamage = GetComponentInChildren<PlayerDamage>();
        rigidbod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        switcher = GameObject.FindGameObjectWithTag("Respawn").GetComponent<LevelLoader>();
    }

    private void Start()
    {
        safeDestroy = true;
        //Get the spawn position for now
        if (playerInfo.spawnPositions[switcher.lvlSceneNum] != (new Vector2(0,0)))
        {
            gameObject.transform.position = playerInfo.spawnPositions[switcher.lvlSceneNum];
        }
        damageable.HP = playerInfo.currentHP;
        damageable.MaxHP = playerInfo.maxHP;
        hasSpellBolt = playerInfo.hasSpellBolt;
        hasSpellJump = playerInfo.hasSpellJump;
        jumpBoost = playerInfo.jumpBoost;
        keyA = playerInfo.keyA;
        keyB = playerInfo.keyB;
        atkBoost = playerInfo.atkBoost;
        spdBoost = playerInfo.spdBoost;
        if(playerInfo.weapons.Count > 1)
        {
            foreach (GameObject weapon in playerInfo.weapons)
            {
                weapons.AddLast(weapon);
            }
        }
        else
        {
            weapons.AddLast(baseWeapon);
            //For testing the code
            foreach(GameObject weapon in testWeapons)
            {
                weapons.AddLast(weapon);
            }
        }
        currentWeapon = weapons.First.Value;
        SetCurrentWeapon(currentWeapon);
        anim.SetBool(AnimationStrings.hasSpellBolt, hasSpellBolt);
    }

    //Before destroying, overwrite info to pass into new scene
    private void OnDestroy()
    {
        if (safeDestroy)
        {
            if (isFacingRight) { playerInfo.spawnPositions[switcher.lvlSceneNum] = new Vector2(gameObject.transform.position.x - 2, gameObject.transform.position.y); }
            else { playerInfo.spawnPositions[switcher.lvlSceneNum] = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y); }
        }
        //Saving level position
        playerInfo.currentHP = damageable.HP;
        playerInfo.maxHP = damageable.MaxHP;
        playerInfo.hasSpellBolt = hasSpellBolt;
        playerInfo.hasSpellJump = hasSpellJump;
        playerInfo.jumpBoost = jumpBoost;
        playerInfo.keyA = keyA;
        playerInfo.keyB = keyB;
        playerInfo.spdBoost = spdBoost;
        playerInfo.atkBoost = atkBoost;
        playerInfo.weapons.Clear();
        foreach(GameObject weapon in weapons)
        {
            playerInfo.weapons.Add(weapon);
        }
    }

    private void FixedUpdate()
    {
        //If not stunnded (lockvelocity) prevent the player from moving 
        if (!damageable.LockVelocity) { rigidbod.velocity = new Vector2(moveInput.x * CurrentMoveSpeed * spdBoost, rigidbod.velocity.y); }
        anim.SetFloat(AnimationStrings.yVelocity, rigidbod.velocity.y);
    }

    private void Update()
    {
        if (!anim.GetBool(AnimationStrings.isAlive)) //Reload the scene
        {
            safeDestroy = false;
            damageable.HP = damageable.MaxHP; //Respawn with full HP
            playerInfo.maxHP = damageable.MaxHP;
            playerInfo.hasSpellBolt = hasSpellBolt;
            playerInfo.hasSpellJump = hasSpellJump;
            playerInfo.jumpBoost = jumpBoost;
            playerInfo.keyA = keyA;
            playerInfo.keyB = keyB;
            playerInfo.spdBoost = spdBoost;
            playerInfo.atkBoost = atkBoost;
            switcher.LoadNextLvl(switcher.lvlSceneNum);
        } 
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !isFacingRight) { isFacingRight = true; } //Face the right
        else if (moveInput.x < 0 && isFacingRight) { isFacingRight = false; } //Face the left
    }

    //Responging to the moveInput component, moving the player left or right
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else { IsMoving = false; }
    }

    //Responding to the moveInput component, putting the player in the run animation
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) { IsRunning = true; }
        else if (context.canceled) { IsRunning = false; }
    }

    //Responding to the moveInput component, letting the player jump
    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO check if also alive
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            anim.SetTrigger(AnimationStrings.jumpTrigger);
            rigidbod.velocity = new Vector2(rigidbod.velocity.x, jumpPower * jumpBoost);
        }
    }

    //Responding to the moveInput component, letting the attack when pressing the attack button
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started) { anim.SetTrigger(AnimationStrings.attack); }
    }

    public void OnSpell(InputAction.CallbackContext context)
    {
        if (context.started) { anim.SetTrigger(AnimationStrings.spell); }
    }

    //Responding to the damageable unity event, applying knockback on a hit
    public void OnHit(int damage, Vector2 knockback)
    {
        rigidbod.velocity = new Vector2(knockback.x, rigidbod.velocity.y + knockback.y);
    }

    //If the player ever gets stuck, restart.
    public void OnStuck(InputAction.CallbackContext context)
    {
        if(context.started) { anim.SetBool(AnimationStrings.isAlive, false); }
    }

    public void SetCurrentWeapon(GameObject currentWeapon)
    {
        WeaponStats stats = currentWeapon.GetComponent<WeaponStats>();
        playerDamage.ChangeWeaponStats(stats);
        CanvasManager.CMInstance.SetCurrentWeaponImage(currentWeapon.GetComponent<SpriteRenderer>().sprite);
        //Debug.Log($"Changed weapon to {currentWeapon.name}");
    }

    public void OnWeaponSwitchLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(weapons.Count > 1)
            {
                LinkedListNode<GameObject> navigator = weapons.Find(currentWeapon);
                if(navigator.Previous != null)
                {
                    currentWeapon = navigator.Previous.Value;
                    SetCurrentWeapon(currentWeapon);
                }else if(navigator.Previous == null && weapons.Last.Value != currentWeapon) 
                {
                    currentWeapon = weapons.Last.Value;
                    SetCurrentWeapon(currentWeapon);
                }
                else
                {
                    //Do nothing
                }
            }
            else
            {
                //Only one weapon, do nothing
            }
        }
    }

    public void OnWeaponSwitchRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(weapons.Count > 1)
            {
                LinkedListNode<GameObject> navigator = weapons.Find(currentWeapon);
                if(navigator.Next != null)
                {
                    currentWeapon = navigator.Next.Value;
                    SetCurrentWeapon(currentWeapon);
                }
                else if(navigator.Next == null && weapons.First.Value != currentWeapon)
                {
                    currentWeapon = weapons.First.Value;
                    SetCurrentWeapon(currentWeapon);
                }
                else
                {
                    //Do nothing
                }
            }
            else
            {
                //Do nothing
            }
        }
    }
}