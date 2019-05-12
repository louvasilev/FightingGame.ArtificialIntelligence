using UnityEngine;
using System.Collections;
using Zenject;
using TaskManager;
using AI.Dependencies;
using AI.Dependencies.Abstractions;
using AI.UtilityBasedSystem;
using AI.Support;
using AI.Support.Abstractions;
using System;

namespace AI.FiniteStateMachine
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    /// 
    /// <summary>
    /// While in this state, the Artificial Intelligence (AI) controlled
    /// character is selecting which techniques are going to be used
    /// during his turn.
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
    public class StateDeciding : IState
    {
        private const float SIMULATED_DECISION_TIME = 2.0f;

        private bool _executeEntered = false;
        private MonoBehaviour _fightersMind;
        private SceneContext _sceneContext;

        #region Public methods

        /// <summary>
        /// A method for actions to be taken when the FSM is
        /// entering the state. 
        /// </summary>
        public void Enter()
        {
            if (GameObject.Find("SceneContext") == null)
            {
                throw new System.ArgumentException("SceneContext object not found.");
            }

            if (GameObject.Find("ScriptConnector") == null)
            {
                throw new System.ArgumentException("ScriptConnector object not found.");
            }

            _fightersMind = GameObject.Find("ScriptConnector").GetComponent<FightersMind>();
            _sceneContext = GameObject.Find("SceneContext").GetComponent<SceneContext>();
        }

        /// <summary>
        /// A method for executing a set of actions while the FSM
        /// is in the state.
        /// </summary>
        /// /// <param name="intelligentCharacter">The AI controller character.</param>
        public void Execute(IIntelligentCharacter intelligentCharacter)
        {
            if (intelligentCharacter == null)
            {
                throw new System.ArgumentException("IntelligentCharacter reference not found.");
            }

            // For right now, this method should only be executed one
            // time. All it needs to do is wait a pre-defined period of
            // time before switching to StatePerforming.
            if (!_executeEntered)
            {
                _executeEntered = true;

                IntelligentCharacter theIntelligentCharacter = (IntelligentCharacter)intelligentCharacter;

                Task strategizing = new Task(simulateDeciding());

                // Delegate for receiving notifications when the task finished.
                strategizing.Finished += delegate (bool manual)
                {
                    if (manual)
                    {
                        Console.WriteLine("Task was stopped manually.");		
                    }
                    else
                    {
                        // Set the input and the character for the Utility-Based system
                        // and call the method for picking actions for the character to
                        // perform.
                        TacticalMind.Input = (TacticalMindInput)((FightersMind)_fightersMind).TacticalInput;
                            TacticalMind.Opponent = intelligentCharacter;
                            TacticalMind.PickActions();

                        // Done deciding. Time to perform techniques.
                        // Change FSM state to the one for performing techniques.
                        theIntelligentCharacter.MindOfTheFighter.ChangeState(new StatePerforming(_sceneContext.Container.Resolve<FightManagerBase>(),
                                _sceneContext.Container.Resolve<FightStatusManager>(),
                                _sceneContext.Container.Resolve<ITechniqueActionPicker>(),
                                _sceneContext.Container.ResolveId<ITechniquePerformer>("MainCharacterReceiveHitPerformer")));
                    }
                };
            }
        }

        /// <summary>
        /// A method for actions to be taken when the FSM
        /// exits the state.
        /// </summary>
        public void Exit()
        {
            Console.WriteLine("Exiting state StateDeciding...");
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Simulates deciding by waiting for a pre-defined period of time. 
        /// </summary>
        /// <returns>An enumerator representing the method.</returns>
        private IEnumerator simulateDeciding()
        {
            yield return new WaitForSeconds(SIMULATED_DECISION_TIME);
        }

        #endregion

    }
}