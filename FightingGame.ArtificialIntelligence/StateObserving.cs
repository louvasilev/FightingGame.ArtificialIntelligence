
using AI.Dependencies;
using AI.Support;
using AI.Support.Abstractions;
using System;

namespace AI.FiniteStateMachine
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// During this state the AI controlled character is studying
    /// the main character while the latter is selecting techniques,
    /// in order to determine an optimal fighting strategy. This is in effect
    /// while the main character selects techniques and performs them.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 12-10-2015
    /// </date>
    /// <credit>
    /// KelsoMRK (http://forum.unity3d.com/threads/c-interface-question.275522/)
    /// </credit>
    public class StateObserving : IState
    {

        #region Public methods

        /// <summary>
        /// A method for actions to be taken when the FSM is
        /// entering the state. 
        /// </summary>
        public void Enter()
        {
            Console.WriteLine ("Entering state StateObserving...");
        }

        /// <summary>
        /// A method for executing a set of actions while the FSM
        /// is in the state.
        /// </summary>
        /// <param name="intelligentCharacter">The AI controller character.</param>
        public void Execute(IIntelligentCharacter intelligentCharacter)
        {
            if (intelligentCharacter == null)
            {
                throw new System.ArgumentException("IntelligentCharacter reference not found.");
            }

            IntelligentCharacter theIntelligentCharacter = (IntelligentCharacter)intelligentCharacter;

            if (theIntelligentCharacter.Fighter == null)
            {
                throw new System.ArgumentException("Fighter reference not found.");
            }

            Fighter fighter = (Fighter)theIntelligentCharacter.Fighter;

            if (!fighter.ObservingOpponent)
            {
                // Change FSM state to the one for strategizing.
                theIntelligentCharacter.MindOfTheFighter.ChangeState(new StateStrategizing());
            }
        }

        /// <summary>
        /// A method for actions to be taken when the FSM
        /// exits the state.
        /// </summary>
        public void Exit()
        {
            Console.WriteLine("Exiting state StateObserving...");
        }

        #endregion

    }
}