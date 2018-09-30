using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    public class StatesManager : MonoBehaviour
    {
        public ControllerStats stats;
        public ControllerStates states;

        public InputVariables inp;


        [System.Serializable]
        public class InputVariables
        {
            public float horizontal;
            public float vertical;
            public float moveAmount;
            public Vector3 moveDirection;
            public Vector3 aimPosition;
            public Vector3 rotateDirection;
        }

        [System.Serializable]
        public class ControllerStates
        {
            public bool onGround;
            public bool isAiming;
            public bool isCrouching;
            public bool isRunning;

            public bool isInteracting;
        }

        public Animator anim;
        public GameObject activeModel;
        [HideInInspector]
        public AnimatorHook a_hook;

        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public Collider controllerCollider;

        List<Collider> ragdollColliders = new List<Collider>();
        List<Rigidbody> ragdollRigids = new List<Rigidbody>();

        [HideInInspector]
        public LayerMask ignoreLayer;
        [HideInInspector]
        public LayerMask ignoreForGround;

        //[HideInInspector]
        //public Transform referencesParent;

        [HideInInspector]
        public Transform mTransform;
        public CharState curState;
        public float delta;

        public void Init()
        {
            mTransform = this.transform;
            SetupAnimator();

            rigid = GetComponent<Rigidbody>();
            rigid.isKinematic = false;
            rigid.drag = 4;
            rigid.angularDrag = 999;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            controllerCollider = GetComponent<Collider>();

            SetupRagdoll();

            ignoreLayer = ~(1 << 9);
            ignoreForGround = ~(1 << 9 | 1 << 10);

            a_hook = activeModel.AddComponent<AnimatorHook>();
            a_hook.Init(this);
        }

        void SetupAnimator()
        {
            if(activeModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                activeModel = anim.gameObject;
            }

            if (anim == null)
                anim = activeModel.GetComponent<Animator>();

            anim.applyRootMotion = false;
        }

        void SetupRagdoll()
        {
            Rigidbody[] rigids = activeModel.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody r in rigids)
            {
                if (r == null)
                    continue;

                Collider c = r.GetComponent<Collider>();
                c.isTrigger = true;

                ragdollRigids.Add(r);
                ragdollColliders.Add(c);
                r.isKinematic = true;
                r.gameObject.layer = 10;
            }
        }

        public void FixedTick(float d)
        {
            delta = d;
            switch (curState)
            {
                case CharState.normal:
                    states.onGround = OnGround();

                    if (states.isAiming)
                        MovementAiming();
                    else
                        MovementNormal();

                    RotationNormal();
                    break;
                case CharState.onAir:
                    rigid.drag = 0;
                    states.onGround = OnGround();
                    break;
                case CharState.cover:
                    break;
                case CharState.vaulting:
                    break;
                default:
                    break;
            }
        }

        void MovementNormal()
        {

            if (inp.moveAmount > 0.05f)
                rigid.drag = 0;
            else
                rigid.drag = 4;

            float speed = stats.walkSpeed;
            if (states.isRunning)
                speed = stats.runSpeed;
            if (states.isCrouching)
                speed = stats.crouchSpeed;

            Vector3 dir = Vector3.zero;
            dir = mTransform.forward * (speed * inp.moveAmount);
            rigid.velocity = dir;
        }

        void RotationNormal()
        {
            if(!states.isAiming)
                inp.rotateDirection = inp.moveDirection;

            Vector3 targetDir = inp.rotateDirection;
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = mTransform.forward;

            Quaternion lookDir = Quaternion.LookRotation(targetDir);
            Quaternion targetRot = Quaternion.Slerp(mTransform.rotation, lookDir, stats.rotateSpeed * delta);
            mTransform.rotation = targetRot;

        }

        void MovementAiming()
        {
            float speed = stats.aimSpeed;
            Vector3 v = inp.moveDirection * speed ;
            rigid.velocity = v;
        }

        public void Tick(float d)
        {
            delta = d;

            switch (curState)
            {
                case CharState.normal:
                    states.onGround = OnGround();
                    HandleAnimationAll();
                    break;
                case CharState.onAir:
                    states.onGround = OnGround();
                    break;
                case CharState.cover:
                    break;
                case CharState.vaulting:
                    break;
                default:
                    break;
            }
        }

        void HandleAnimationAll()
        {
            anim.SetBool(StaticStrings.animParamSprint, states.isRunning);
            anim.SetBool(StaticStrings.animParamAiming, states.isAiming);
            anim.SetBool(StaticStrings.animParamCrouch, states.isCrouching);

            if (states.isAiming)
            {
                HandleAnimationAiming();
            }
            else
            {
                HandleAnimationNormal();
            }
        }

        void HandleAnimationNormal()
        {
            float anim_v = inp.moveAmount;
            anim.SetFloat(StaticStrings.animParamVertical, anim_v, 0.15f, delta);
        }

        void HandleAnimationAiming()
        {
            float v = inp.vertical;
            float h = inp.horizontal;

            anim.SetFloat(StaticStrings.animParamHorizontal, h, 0.2f, delta);
            anim.SetFloat(StaticStrings.animParamVertical, v, 0.2f, delta);
        }

        bool OnGround()
        {
            Vector3 origin = mTransform.position;
            origin.y += 0.06f;

            Vector3 dir = -Vector3.up;
            float dis = 0.7f;
            RaycastHit hit;
            if(Physics.Raycast(origin, dir, out hit, dis, ignoreForGround))
            {
                Vector3 tp = hit.point;
                mTransform.position = tp;
                return true;
            }

            return false;
        }
    }

    public enum CharState
    {
        normal, onAir, cover, vaulting
    }
}
