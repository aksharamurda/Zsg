using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    public class CameraHandler : MonoBehaviour
    {
        public Transform camTrans;
        public Transform target;
        public Transform pivot;
        public Transform mTranform;
        public bool leftPivot;
        float delta;

        float mouseX;
        float mouseY;
        float smoothX;
        float smoothY;

        float smoothXVelocity;
        float smoothYVelocity;

        float lookAngle;
        float tiltAngle;

        public CameraValues values;

        StatesManager states;

        public void Init(InputHandler inp)
        {
            mTranform = this.transform;
            states = inp.states;
            target = states.mTransform;
        }

        public void FixedTick(float d)
        {
            delta = d;
            if (target == null)
                return;

            HandlePositions();
            HandleRotation();

            float speed = values.aimSpeed;
            if (states.states.isAiming)
                speed = values.aimSpeed;

            Vector3 targetPosition = Vector3.Lerp(mTranform.position, target.position, delta * speed);
            mTranform.position = targetPosition;
        }

        void HandlePositions()
        {
            float targetX = values.normalX;
            float targetZ = values.normalZ;
            float targetY = values.normalY;

            if (states.states.isCrouching)
                targetY = values.crouchY;

            if (states.states.isAiming)
            {
                targetX = values.aimX;
                targetZ = values.aimZ;
            }

            if (leftPivot)
                targetX = -targetX;

            Vector3 newPivotPosition = pivot.localPosition;
            newPivotPosition.x = targetX;
            newPivotPosition.y = targetY;

            Vector3 newCamPositon = camTrans.localPosition;
            newCamPositon.z = targetZ;

            float t = delta * values.adaptSpeed;
            pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
            camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCamPositon, t);
        }

        void HandleRotation()
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            if(values.turnSmooth > 0)
            {
                smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, values.turnSmooth);
                smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, values.turnSmooth);
            }
            else
            {
                smoothX = mouseX;
                smoothY = mouseY;
            }

            lookAngle += smoothX * values.y_rotate_speed;
            Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
            mTranform.rotation = targetRot;

            tiltAngle -= smoothY * values.x_rotate_speed;
            tiltAngle = Mathf.Clamp(tiltAngle, values.minAngle,  values.maxAngle);
            pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
        }
    }
}
