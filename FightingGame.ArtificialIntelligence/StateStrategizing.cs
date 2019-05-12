using UnityEngine;
using System.Collections;
using AI.Support;
using AI.Support.Abstractions;
using TaskManager;
using System;

namespace AI.FiniteStateMachine
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// During this state the AI controlled character is
    /// analyzing information collected during the observation
    /// state.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 12-05-2015
    /// </date>
    /// <credit>
    /// KelsoMRK (http://forum.unity3d.com/threads/c-interface-question.275522/)
    /// </credit>
    public class StateStrategizing : IState
    {
        private const float SIMULATE_STRATEGIZING_TIME = 1.0f;

	    private bool _executeEntered = false;

	    #region Public methods

	    /// <summary>
	    /// A method for actions to be taken when the FSM is
	    /// entering the state.
	    /// </summary>
	    public void Enter ()
	    {

		    Console.WriteLine ("Entering state StateStrategizing...");
	    }

	    /// <summary>
	    /// A method for executing a set of actions while the FSM
	    /// is in the state.
	    /// </summary>
	    /// <param name="intelligentCharacter">The AI controller character.</param>
	    public void Execute (IIntelligentCharacter intelligentCharacter)
	    {
		    if (intelligentCharacter == null)
		    {
			    throw new System.ArgumentException (nameof(intelligentCharacter) +  " reference not found.");
		    }

		    // For right now, this method should only be executed one
		    // time. All it needs to do is wait a pre-defined period of
		    // time before switching to StateDeciding.
		    if (!_executeEntered)
		    {
			    _executeEntered = true;

			    IntelligentCharacter theIntelligentCharacter = (IntelligentCharacter)intelligentCharacter;

			    Task strategizing = new Task (simulateStrategizing ());

			    // Delegate for receiving notifications when the task finished.
			    strategizing.Finished += delegate(bool manual)
			    {
				    if (manual)
				    {
                        Console.WriteLine("Task was stopped manually.");		
				    }
				    else
				    {
                        // Done strategizing. Time to decide what techniques to use.
                        // Change FSM state to the one for deciding what techniques to use.
                        theIntelligentCharacter.MindOfTheFighter.ChangeState (new StateDeciding ());
				    }
			    };
		    }
	    }

        /// <summary>
        /// A method for actions to be taken when the FSM
        /// exits the state.
        /// </summary>
        public void Exit ()
	    {
            Console.WriteLine("Exiting state StateStrategizing...");
	    }

	    #endregion

	    #region Private methods

	    /// <summary>
	    /// Simulates strategizing by waiting for a pre-defined period of time. 
	    /// </summary>
	    /// <returns>An enumerator representing the method.</returns>
	    private IEnumerator simulateStrategizing ()
	    {
		    yield return new WaitForSeconds (SIMULATE_STRATEGIZING_TIME);
	    }

	    #endregion

    }
}