using UnityEngine;

namespace GameMain.Scripts
{

    public class CamControl : MonoBehaviour
    {
        [Header("相机跟随")] public Transform lookAtPos; // 相机对准的位置
        [Header("相机臂"), Range(0, 10)] public float armLeghth = 5; // 相机臂长度
        [Range(-90, 90)] public float armVerticalRadius = 45; // 相机臂垂直角度
        [Range(-180, 180)] public float armHorizontalRadius = -90; // 相机臂水平角度
        [Header("相机反转")] public bool horizontalInvert = true; // 水平移动反转
        public bool verticalInvert = true; // 垂直移动反转
        public bool distanceInvert = true; // 远近移动反转
        [Header("相机移动速度")] public float horizontalSpeed = 1f; // 相机旋转速度
        public float verticalSpeed = 1f; // 相机上下速度
        public float distanceSpeed = 1f; // 相机远近速度

        private void LateUpdate()
        {
            SetCameraPos();
        }

        /// <summary>
        /// 设置相机位置
        /// 通过计算圆上点的位置确定相机位置
        /// </summary>
        void SetCameraPos()
        {
            // 获取旋转角度
            SetCameraRot();

            // 先计算 z/y 平面相机臂的位置 计算相机偏移

            // 圆心为跟随的目标
            Vector3 center = lookAtPos.position;
            // 限制垂直角度 因为 Cos 90°= 0
            armVerticalRadius = Mathf.Clamp(armVerticalRadius, 8f, 89.9f);
            // 圆心 z 轴负方向为 0°
            float x0 = center.z;
            float y0 = center.y;
            // 下个圆的半径
            float x1 = x0 + armLeghth * Mathf.Cos(armVerticalRadius * Mathf.Deg2Rad);
            // 相机 y
            float y1 = y0 + armLeghth * Mathf.Sin(armVerticalRadius * Mathf.Deg2Rad);

            // 再计算 x/z 平面相机臂的位置 计算相机旋转范围

            // 圆心 x 轴正方向为 0°
            float x2 = center.x;
            float y2 = center.z;
            // 相机 x
            float x3 = x2 + Mathf.Abs(x1 - x0) * Mathf.Cos(armHorizontalRadius * Mathf.Deg2Rad);
            // 相机 z
            float y3 = y2 + Mathf.Abs(x1 - x0) * Mathf.Sin(armHorizontalRadius * Mathf.Deg2Rad);

            // 设置相机位置
            transform.position = new Vector3(x3, y1, y3);
            transform.LookAt(lookAtPos);

            // 地面检测
            GroundTest();
        }

        /// <summary>
        /// 设置相机旋转
        /// </summary>
        void SetCameraRot()
        {
            // 鼠标锁定后才能旋转 不需要可以删除该行代码
            /*if (Cursor.lockState != CursorLockMode.Locked) return;*/

            // 左右旋转
            float horizontalRotaion = horizontalInvert ? -Input.GetAxis("Mouse X") : Input.GetAxis("Mouse X");
            armHorizontalRadius += horizontalRotaion * horizontalSpeed;
            // 旋转控制在 ±180° 以内
            if (armHorizontalRadius > 180 || armHorizontalRadius < -180) armHorizontalRadius = -armHorizontalRadius;
            armHorizontalRadius = Mathf.Clamp(armHorizontalRadius, -180, 180);

            // 上下旋转
            float verticalRotation = verticalInvert ? -Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y");
            armVerticalRadius += verticalRotation * verticalSpeed;

            // 相机远近
            float distance = distanceInvert ? -Input.GetAxis("Mouse ScrollWheel") : Input.GetAxis("Mouse ScrollWheel");

            armLeghth += distance * distanceSpeed;
            float maxDistance = 10.0f;
            float minDistance = 3f;

            if (armLeghth >= maxDistance)
                armLeghth = maxDistance;
            if (armLeghth <= minDistance)
                armLeghth = minDistance;

            // 不能穿过 LookAt 目标
            armLeghth = Mathf.Max(Camera.main.nearClipPlane, armLeghth);
        }

        /// <summary>
        /// 墙壁检测 防止穿透
        /// 平面射线版本
        /// </summary>
        void GroundTest()
        {
            // 求出相机宽高
            float _cameraHeight =
                Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // 求出最近的位置
            Vector3 _center = transform.position + transform.forward * Camera.main.nearClipPlane;

            // 辅助线 相机当前位置
            Debug.DrawLine(transform.position, _center, Color.red, 0.1f);
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);

            // 辅助线 lookAt 射线位置
            Debug.DrawLine(
                lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                Color.red, 0.1f);
            Debug.DrawLine(
                lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                Color.red, 0.1f);
            Debug.DrawLine(
                lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                Color.red, 0.1f);
            Debug.DrawLine(
                lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                Color.red, 0.1f);

            // 辅助线 射线范围
            Debug.DrawLine(
                lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                _center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(
                lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(
                lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                _center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(
                lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);

            // 从人物往摄像机射线
            if (Physics.BoxCast(lookAtPos.position, new Vector3(_cameraWidth * .5f, _cameraHeight * .5f),
                    -transform.forward, out RaycastHit _hit, Quaternion.LookRotation(-transform.forward),
                    Vector3.Distance(lookAtPos.position, _center), LayerMask.GetMask("Ground")))
            {
                // 辅助线 碰撞点
                Debug.DrawLine(lookAtPos.position, _hit.point, Color.white, 0.1f);

                // 碰撞点居中
                Vector3 _hitCenter = lookAtPos.position + Vector3.Project(_hit.point - lookAtPos.position,
                    transform.position - lookAtPos.position);

                // 辅助线 碰撞点居中位置
                Debug.DrawLine(lookAtPos.position, _hitCenter, Color.green, 0.1f);

                // 相机前移
                transform.position = transform.position + transform.forward * Vector3.Distance(_center, _hitCenter);

                // 求出最近的新位置
                _center = transform.position + transform.forward * Camera.main.nearClipPlane;

                // 辅助线 相机当前位置
                Debug.DrawLine(transform.position, _center, Color.blue, 0.1f);
                Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                    _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.blue,
                    0.1f);
                Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f,
                    _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.blue,
                    0.1f);
                Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                    _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.blue,
                    0.1f);
                Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f,
                    _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.blue,
                    0.1f);

            }
        }
    }
}
        

