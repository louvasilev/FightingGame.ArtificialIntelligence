
using AI.Dependencies.Abstractions;
using UnityEngine;

namespace AI.Support
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// This is the main class in the Finite State Machine (FSM)
    /// portion of the Artificial Intelligence (AI) system. Its
    /// responsibilities include setting the current state of the
    /// AI controller character, reverting to a previous state
    /// (if necessary), and so on.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 12-13-2015
    /// </date>
    /// <credit>
    /// KelsoMRK (http://forum.unity3d.com/threads/c-interface-question.275522/)
    /// </credit>
    public class FightersMind : MonoBehaviour
    {
        private IntelligentCharacter _intelligentCharacter;

        private IState _currentState;
        private IState _globalState;
        private IState _previousState;

        private SystemInput _tacticalMindInput;

        #region Public properties

        /// <summary>
        /// Gets or sets the current state.
        /// </summary>
        /// <value>The current state.</value>
        public IState CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
            }
        }

        /// <summary>
        /// Gets or sets the global state.
        /// </summary>
        /// <value>The global state.</value>
        public IState GlobalState
        {
            get
            {
                return _globalState;
            }
            set
            {
                _globalState = value;
            }
        }

        /// <summary>
        /// Gets or sets the previous state.
        /// </summary>
        /// <value>The previous state.</value>
        public IState PreviousState
        {
            get
            {
                return _previousState;
            }
            set
            {
                _previousState = value;
            }
        }

        /// <summary>
        /// Gets the input for the Tactical Mind, i.e. the Utility-Based
        /// system.
        /// </summary>
        /// <value>The tactical input.</value>
        public SystemInput TacticalInput
        {
            get
            {
                return _tacticalMindInput;
            }
            set
            {
                _tacticalMindInput = value;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="newState">New state.</param>
        public void ChangeState(IState newState)
        {
            if (_currentState != null)
            {
                // Keep track of the previous state
                _previousState = _currentState;
                _currentState.Exit();
            }

            _currentState = newState;
            _currentState.Enter();
        }

        /// <summary>
        /// Initialize the finite state machine (FSM) with the fighter
        /// being controlled by the AI system and the intial state
        /// (of the FSM).
        /// </summary>
        /// <param name="intelligentFighter">The AI fighter.</param>
        /// <param name="initialState">Initial state.</param>
        public void Initialize(IFighter intelligentFighter, IState initialState)
        {
            if (intelligentFighter == null)
            {
                throw new System.ArgumentException("IFighter reference not found.");
            }

            if (initialState == null)
            {
                throw new System.ArgumentException("IState reference not found.");
            }

            Initialize(intelligentFighter, initialState, null);
        }

        /// <summary>
        /// Initialize the finite state machine (FSM) with the fighter
        /// being controlled by the AI system and the intial state
        /// (of the FSM).
        /// </summary>
        /// <param name="intelligentFighter">Intelligent fighter.</param>
        /// <param name="initialState">Initial state.</param>
        /// <param name="tacticalMindInput">Tactical mind input. This can be null.</param>
        public void Initialize(IFighter intelligentFighter, IState initialState, SystemInput tacticalMindInput)
        {
            if (intelligentFighter == null)
            {
                throw new System.ArgumentException("IFighter reference not found.");
            }

            if (initialState == null)
            {
                throw new System.ArgumentException("IState reference not found.");
            }

            _intelligentCharacter = new IntelligentCharacter(intelligentFighter, this);
            _currentState = initialState;
            _tacticalMindInput = tacticalMindInput;
        }

        /// <summary>
        /// Reverts the FSM to the previous state.
        /// </summary>
        /// <credit>
        /// AI-Junkie (http://www.ai-junkie.com/architecture/state_driven/tut_state1.html)
        /// </credit>
        public void RevertToPreviousState()
        {
            ChangeState(_previousState);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        /// <credit>
        /// AI-Junkie (http://www.ai-junkie.com/architecture/state_driven/tut_state1.html)
        /// </credit>
        void Update()
        {
            // If a global state exists, execute it.
            if (_globalState != null)
            {
                _globalState.Execute(_intelligentCharacter);
            }

            // Do the same thing for the current state.
            if (_currentState != null)
            {
                _currentState.Execute(_intelligentCharacter);
            }
        }

        #endregion

    }
}