using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.Project.Common.FSM
{
    /// <summary>
    /// (游戏状态)转换条件
    /// </summary>
    public enum Transition
    {
        NullTransition = 0,
        ToA,
        ToB,

        RIdleToSmileA,
        RSmileAToIdle,

        RIdleToEat,
        REatToIdle,

        REatToSmileB,
        RSmileBToEat,
    }

    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum StateID
    {
        NullStateID = 0,
        StateA,
        StateB,

        RoleIdle,
        RoleSmileA,//空手 笑
        RoleEat,
        RoleSmileB,//拿食 笑

    }

    /// <summary>
    /// 状态(父)类
    /// </summary>
    public abstract class FSMState
    {
        protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
        protected StateID stateID;
        public StateID ID { get { return stateID; } }

        /// <summary>
        /// 添加转换条件
        /// </summary>
        /// <param name="trans"> 切换条件 </param>
        /// <param name="id"> 目标状态 </param>
        public void AddTransition(Transition trans, StateID id)
        {
            if (trans == Transition.NullTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
                return;
            }

            if (id == StateID.NullStateID)
            {
                Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
                return;
            }


            if (map.ContainsKey(trans))
            {
                Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                               "Impossible to assign to another state");
                return;
            }
            map.Add(trans, id);
        }

        /// <summary>
        /// 删除转换条件
        /// </summary>
        /// <param name="trans"> 切换条件 </param>
        public void DeleteTransition(Transition trans)
        {

            if (trans == Transition.NullTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed");
                return;
            }

            if (map.ContainsKey(trans))
            {
                map.Remove(trans);
                return;
            }
            Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() +
                           " was not on the state's transition list");
        }


        public StateID GetOutputState(Transition trans)
        {
            if (map.ContainsKey(trans))
            {
                return map[trans];
            }
            return StateID.NullStateID;
        }


        public virtual void DoBeforeEntering() { }


        public virtual void DoBeforeLeaving() { }


        public abstract void Reason(GameObject owner);


        public abstract void Act(GameObject owner);

    }


    /// <summary>
    /// 状态机
    /// </summary>
    public class FSMSystem
    {
        private List<FSMState> states;

        private StateID currentStateID;
        public StateID CurrentStateID { get { return currentStateID; } }
        private FSMState currentState;
        public FSMState CurrentState { get { return currentState; } }

        public FSMSystem()
        {
            states = new List<FSMState>();
        }


        public void AddState(FSMState s)
        {
            if (s == null)
            {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
            }

            if (states.Count == 0)
            {
                states.Add(s);
                currentState = s;
                currentStateID = s.ID;
                return;
            }

            foreach (FSMState state in states)
            {
                if (state.ID == s.ID)
                {
                    Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
                                   " because state has already been added");
                    return;
                }
            }
            states.Add(s);
        }

        public void DeleteState(StateID id)
        {

            if (id == StateID.NullStateID)
            {
                Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
                return;
            }

            foreach (FSMState state in states)
            {
                if (state.ID == id)
                {
                    states.Remove(state);
                    return;
                }
            }
            Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                           ". It was not on the list of states");
        }


        public void PerformTransition(Transition trans)
        {

            if (trans == Transition.NullTransition)
            {
                Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
                return;
            }


            StateID id = currentState.GetOutputState(trans);
            if (id == StateID.NullStateID)
            {
                Debug.LogError("FSM ERROR: State " + currentStateID.ToString() + " does not have a target state " +
                               " for transition " + trans.ToString());
                return;
            }


            currentStateID = id;
            foreach (FSMState state in states)
            {
                if (state.ID == currentStateID)
                {
                    currentState.DoBeforeLeaving();

                    currentState = state;

                    currentState.DoBeforeEntering();
                    break;
                }
            }

        }

    }

}
