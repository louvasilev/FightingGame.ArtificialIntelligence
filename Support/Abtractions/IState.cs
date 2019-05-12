
using AI.Support.Abstractions;

namespace AI.Support
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Interface defining the behavior of a state in the
    /// Finite State Machine (FSM) part of the Artificial
    /// Intelligence system.
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
    public interface IState
    {
        /// <summary>
        /// A method for actions to be taken when the FSM
        /// the state. 
        /// </summary>
        void Enter();

        /// <summary>
        /// A method for executing a set of actions while the FSM
        /// is in the state.
        /// </summary>
        /// <param name="intelligentCharacter">The AI controller character.</param>
        void Execute(IIntelligentCharacter intelligentCharacter);

        /// <summary>
        /// A method for actions to be taken when the FSM
        /// exits the state.
        /// </summary>
        void Exit();
    }
}