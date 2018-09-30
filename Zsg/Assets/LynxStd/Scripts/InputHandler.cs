using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    public class InputHandler : MonoBehaviour
    {
        float horizontal;
        float vertical;

        bool aimInput;
        bool runInput;
        bool shootInput;
        bool crouchInput;
        bool reloadInput;
        bool switchInput;
        bool pivotInput;

        bool isInit;

        float delta;

        public StatesManager states;
        public CameraHandler camHandler;

        private void Start()
        {
            InitInGame();
        }

        public void InitInGame()
        {
            states.Init();
            camHandler.Init(this);
            isInit = true;
        }

        private void FixedUpdate()
        {
            if (!isInit)
                return;

            delta = Time.fixedDeltaTime;

            GetInput_FixedUpdate();
            InGame_UpdateStates_FixedUpdate();
            states.FixedTick(delta);

            camHandler.FixedTick(delta);
        }

        void GetInput_FixedUpdate()
        {
            vertical = Input.GetAxis(StaticStrings.inputVertical);
            horizontal = Input.GetAxis(StaticStrings.inputHorizontal);
        }

        void InGame_UpdateStates_FixedUpdate()
        {
            states.inp.horizontal = horizontal;
            states.inp.vertical = vertical;

            states.inp.moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            Vector3 moveDir = camHandler.mTranform.forward * vertical;
            moveDir += camHandler.mTranform.right * horizontal;
            moveDir.Normalize();

            states.inp.moveDirection = moveDir;

            states.inp.rotateDirection = camHandler.mTranform.forward;
        }

        private void Update()
        {
            if (!isInit)
                return;

            delta = Time.deltaTime;

            GetInput_Update();
            InGame_UpdateStates_Update();

            states.Tick(delta);
        }

        void GetInput_Update()
        {
            aimInput = Input.GetMouseButton(1);
        }

        void InGame_UpdateStates_Update()
        {
            states.states.isAiming = aimInput;
        }
    }

    public enum GamePhase
    {
        inGame, inMenu
    }
}
