using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

namespace Player123
{
    public class FSMCar1
    {
        public IFsm<FSMCar1> m_Fsm = null;

        public FSMCar1()
        {
            FsmComponent fsmComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();
            m_Fsm = fsmComponent.CreateFsm("ActorFsm", this, new StopState(), new MoveState());
            m_Fsm.Start<StopState>();
        }
    }

    public class StopState : FsmState<FSMCar1>
    {
        protected override void OnInit(IFsm<FSMCar1> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建移动状态");
        }

        protected override void OnDestroy(IFsm<FSMCar1> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁移动状态");
        }

        protected override void OnEnter(IFsm<FSMCar1> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("进入移动状态");
        }

        protected override void OnLeave(IFsm<FSMCar1> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开移动状态");
        }

        protected override void OnUpdate(IFsm<FSMCar1> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询移动状态");
            if (Input.GetKey(KeyCode.A))
            {
                ChangeState<MoveState>(fsm);
            }
        }
    }

    public class MoveState : FsmState<FSMCar1>
    {
        protected override void OnUpdate(IFsm<FSMCar1> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (Input.GetKey(KeyCode.A))
            {
                ChangeState<StopState>(fsm);
            }
        }
    }
}