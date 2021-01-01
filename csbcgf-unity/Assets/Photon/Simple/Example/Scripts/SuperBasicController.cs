//Copyright 2019, Davin Carten, All rights reserved

using UnityEngine;
using Photon.Pun.Simple;
using emotitron.Utilities;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if PUN_2_OR_NEWER
using Photon.Pun;
#endif

public class SuperBasicController : NetComponent
{

    public bool is2D = false;

    [Range(0, 300)]
    public float turnSpeed = 150f;
    [Range(0, 4f)]
    public float moveSpeed = 4f;

    public bool autoMove = true;


#if PUN_2_OR_NEWER

    /// Store transform data from the last fixedUpdate
	private Vector3 targRotDelta, targPosDelta;
    private float appliedDeltaT;

    private Animator animator;
    private SyncAnimator syncAnimator;
    private SyncTransform syncTransform;
    private SyncCannon syncLauncher;
    private SyncContactScan syncHitscan;

    private bool triggerJump;
    private bool triggerFade;
    private bool triggerTurnLeft;
    private bool triggerUpperBodyRun;
    private bool triggerUpperBodyIdle;
    private bool triggerTeleport;
    private bool freakingOut;
    private bool triggerHitscan;
    private bool triggerProjectile;
    private bool triggerBlend;


    // Start is called before the first frame update
    public override void OnAwake()
    {
        base.OnAwake();
        animator = transform.GetNestedComponentInChildren<Animator, NetObject>(true);
        syncAnimator = transform.GetNestedComponentInChildren<SyncAnimator, NetObject>(true);
        syncTransform = GetComponent<SyncTransform>();
        syncLauncher = transform.GetNestedComponentInChildren<SyncCannon, NetObject>(true);
        syncHitscan = transform.GetNestedComponentInChildren<SyncContactScan, NetObject>(true);
    }


    private void Update()
    {

        if (!IsMine)
            return;

        float t = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        Interpolate(t);

        if (Input.GetKeyDown(KeyCode.Space))
            triggerJump = true;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            triggerFade = true;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            triggerTurnLeft = true;


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (freakingOut)
                triggerUpperBodyIdle = true;
            else
                triggerUpperBodyRun = true;

            freakingOut = !freakingOut;
        }

        if (Input.GetKeyDown(KeyCode.F))
            triggerProjectile = true;

        if (Input.GetKeyDown(KeyCode.R))
            triggerHitscan = true;

        if (Input.GetKeyDown(KeyCode.T))
            triggerTeleport = true;

        if (Input.GetKeyDown(KeyCode.B))
            triggerBlend = true;
    }


    void FixedUpdate()
    {

        if (!IsMine)
            return;

        Vector3 move = new Vector3(0, 0, 0);
        Vector3 turn = new Vector3(0, 0, 0);


        if (animator && animator.isActiveAndEnabled)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("walking", true);
                animator.SetFloat("speed", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("walking", true);
                animator.SetFloat("speed", -.5f);
            }
            else
            {
                animator.SetBool("walking", false);
                animator.SetFloat("speed", 0);
            }

            if (triggerJump)
            {
                if (syncAnimator)
                    syncAnimator.SetTrigger("jump");

                triggerJump = false;
            }

            else if (triggerTurnLeft)
            {
                if (syncAnimator)
                    syncAnimator.SetTrigger("turnLeft");

                triggerTurnLeft = false;
            }

            if (triggerFade)
            {
                if (syncAnimator)
                    syncAnimator.CrossFadeInFixedTime("Jump", .25f);

                triggerFade = false;
            }

            if (triggerBlend)
                animator.SetFloat("blender", Mathf.Abs(Mathf.Sin(Time.time)));
            else
                animator.SetFloat("blender", -1);

            if (triggerUpperBodyRun)
            {
                if (syncAnimator)
                    syncAnimator.SetTrigger("upperBodyRun");

                triggerUpperBodyRun = false;
            }
            else if (triggerUpperBodyIdle)
            {
                if (syncAnimator)
                    syncAnimator.SetTrigger("upperBodyIdle");

                triggerUpperBodyIdle = false;
            }
        }

        if (!animator || !animator.applyRootMotion)
        {
            if (Input.GetKey(KeyCode.W))
            {
                move += Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                move -= Vector3.forward;
            }
        }

        if (Input.GetKey(KeyCode.A))
            move -= Vector3.right;

        if (Input.GetKey(KeyCode.D))
            move += Vector3.right;

        if (Input.GetKey(KeyCode.E))
            turn += Vector3.up;

        if (Input.GetKey(KeyCode.Q))
            turn -= Vector3.up;

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Vector2 normTouch = new Vector2(touch.rawPosition.x / Screen.width, touch.rawPosition.y / Screen.height);

            if (normTouch.y > .66f)
            {
                if (normTouch.x > .66f)
                    triggerHitscan = true;
                else if (normTouch.x < .33f)
                    triggerJump = true;
            }
            else if (normTouch.y < .33f)
            {
                if (normTouch.x > .66f)
                    move += Vector3.right;
                else if (normTouch.x < .33f)
                    move -= Vector3.right;
                else
                {
                    if (animator)
                    {
                        animator.SetBool("walking", true);
                        animator.SetFloat("speed", -0.5f);
                    }
                }
            }
            else
            {
                if (normTouch.x > .66f)
                    turn += Vector3.up;
                else if (normTouch.x < .33f)
                    turn -= Vector3.up;
                else
                {
                    if (animator)
                    {
                        animator.SetBool("walking", true);
                        animator.SetFloat("speed", 1f);
                    }
                }
            }
        }

        /// Apply some inputs if we don't have focus and the above would generate no inputs.
        if (autoMove && !Application.isFocused)
        {
            turn += new Vector3(0, Mathf.Sin(Time.time * .5f), 0);
            if (animator)
            {
                animator.SetBool("walking", true);
                animator.SetFloat("speed", Mathf.Sin(Time.time) * .5f);
            }
        }

        Interpolate(1);

        Move(move, turn);

        //Interpolate(1);

        appliedDeltaT = 0;

        if (triggerHitscan)
        {
            if (syncHitscan)
                syncHitscan.QueueTrigger();

            triggerHitscan = false;
        }

        if (triggerProjectile)
        {
            if (syncLauncher)
                syncLauncher.QueueTrigger();

            triggerProjectile = false;
        }


        if (triggerTeleport)
        {
            //triggerTeleport = true;
            //transform.position = new Vector3(0, 0, 0);

            /// TODO: typically this isn't called directly. For a real implementation this would notify all IOnTeleport components.
            if (syncTransform)
            {
                syncTransform.FlagTeleport();
                transform.localPosition = new Vector3();
                transform.localRotation = new Quaternion();
            }

            triggerTeleport = false;
        }
    }

    private void OnAnimatorMove()
    {
        if (!IsMine)
            return;

        animator.ApplyBuiltinRootMotion();

        transform.rotation = animator.rootRotation;
        transform.position = animator.rootPosition;
    }


    private void Move(Vector3 move, Vector3 turn)
    {
        if (is2D)
        {
            move = new Vector3(move.x, move.z, 0);
            turn = new Vector3(0, 0, turn.y);
        }

        targRotDelta = turn * turnSpeed * Time.fixedDeltaTime;
        targPosDelta = move * moveSpeed * Time.fixedDeltaTime;
    }

    void Interpolate(float t)
    {
        t -= appliedDeltaT;

        appliedDeltaT += t;

        transform.rotation = (transform.rotation * Quaternion.Euler(targRotDelta * t));
        transform.position += transform.rotation * (targPosDelta * t);
    }

#endif


#if UNITY_EDITOR

    [CustomEditor(typeof(SuperBasicController))]
    [CanEditMultipleObjects]
    public class SuperBasicControllerEditor : SampleCodeHeaderEditor
    {
        protected override string Instructions
        {
            get
            {
                return "Test controller used for demos. We don't recommend actually using this for anything of your own.";
            }
        }
    }

#endif
}

