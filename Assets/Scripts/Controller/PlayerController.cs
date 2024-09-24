using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed;
    Vector3 moveInput;
    private Animator animator;

    [HideInInspector]public CircleCollider2D circleCollider;

    public List<Weapon> unassigneWeapons;
    public List<Weapon> assigneWeapons;
    public int maxWeapons;
    [HideInInspector] public List<Weapon> fullyLevelledWeapons;

    void Start()
    {
        moveInput = new Vector3();
        animator = GetComponentInChildren<Animator>();
        circleCollider = transform.Find("PickupRange").GetComponent<CircleCollider2D>();
        fullyLevelledWeapons=new List<Weapon>();
    }

    
    void Update()
    {
        MoveControl();
        AnimationControl();
    }

    private void AnimationControl()
    {
        if (moveInput != Vector3.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

    private void MoveControl()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        transform.position += moveInput * moveSpeed * Time.deltaTime;
    }

    public void AddWeapon(int weaponNumber) 
    {
        if (weaponNumber < unassigneWeapons.Count) 
        {
            assigneWeapons.Add(unassigneWeapons[weaponNumber]);
            unassigneWeapons[weaponNumber].gameObject.SetActive(true);
            unassigneWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd) 
    {
        weaponToAdd.gameObject.SetActive(true);
        assigneWeapons.Add(weaponToAdd);
        unassigneWeapons.Remove(weaponToAdd);
    }
}
