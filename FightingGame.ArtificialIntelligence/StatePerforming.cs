using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AI.Dependencies;
using AI.Dependencies.Abstractions;
using AI.Support.Abstractions;
using AI.Support;
using TaskManager;
using System;

namespace AI.FiniteStateMachine
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// While in this state, the Artificial Intelligence (AI) controlled
    /// character is executing the techniques selected in the decision
    /// making state. Those could be techniques for attacking, defending,
    /// countering-attacking, moving, crouching, etc.
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
    public class StatePerforming : IState
    {
	    // A delay before switching to the other charater's turn,
	    // after performing techniques.
	    public const float TURN_SWITCH_DELAY = 1.0f;
        // A constant for the name of the fighting bounce animation.
        private const string FIGHTING_BOUNCE_ANIMATION_NAME = "fight_idle_1";

	    private bool _executeEntered = false;
        private FightManagerBase _fightManager;
        private FightStatusManager _fightStatusManager;
	    private ITechniqueActionPicker _techniqueActionPicker;
	    private ITechniquePerformer _techniquePerformer;

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StatePerforming"/> class.
        /// </summary>
        /// <param name="fightManager">A child class of FightManagerBase for managing a
        /// martial arts fight.</param>
        /// <param name="fightStatusManager">An instance of FightStatusManager for managing
        /// the status of a martial arts fight.</param>
        /// <param name="techniqueActionPicker">An implementation of ITechniqueActionPicker for
        /// picking moves during a fight.</param>
        /// <param name="techniquePerformer">An implementation of ITechniquePerformer for
        /// performing moves during a fight.</param>
        public StatePerforming(FightManagerBase fightManager,
            FightStatusManager fightStatusManager,
		    ITechniqueActionPicker techniqueActionPicker,
		    ITechniquePerformer techniquePerformer)
	    {
            _fightManager = fightManager ?? throw new ArgumentNullException(nameof(fightManager) + " reference not found.");
		    _fightStatusManager = fightStatusManager ?? throw new ArgumentNullException(nameof(fightStatusManager) + " reference not found.");
		    _techniqueActionPicker = techniqueActionPicker ?? throw new ArgumentNullException(nameof(techniqueActionPicker) + " reference not found.");
		    _techniquePerformer = techniquePerformer ?? throw new ArgumentNullException(nameof(techniquePerformer) + " reference not found.");
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// A method for actions to be taken when the FSM is
	    /// entering the state. 
	    /// </summary>
	    public void Enter ()
	    {
            Console.WriteLine("Entering state StatePerforming...");
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

		    // Make sure the method gets called only once.
		    if (!_executeEntered)
		    {
			    _executeEntered = true;

			    AnimationManager animationManager = GameObject.Find ("ScriptConnector").GetComponent<AnimationManager> ();

			    if (animationManager != null)
			    {
				    // Set the fighting bounce animation and the active fighter
				    // properties for the opponent.
				    animationManager.FightingBounceAnimation = FIGHTING_BOUNCE_ANIMATION_NAME;
				    IList<FightingAction> fightingActions = _fightManager.FightingActions;

				    Task attacking = new Task (animationManager.PerformFightingActions ());

				    // Delegate for receiving notifications when the task finished.
				    attacking.Finished += delegate(bool manual)
				    {
					    if (manual)
					    {
                            Console.WriteLine("Task was stopped manually.");		
					    }
					    else
					    {
						    Task turnSwitchDelayTask = new Task (turnSwitchDelay ());

						    // Delegate for receiving notifications when the task finished.
						    turnSwitchDelayTask.Finished += delegate(bool manualStop)
						    {
							    if (manual)
							    {
                                    Console.WriteLine("Task was stopped manually.");		
							    }
							    else
							    {
								    // Update the status of the fight. It includes the damage
								    // incured to various body parts on each of the opponents
								    // bodies, the mental and physical conditions of of both
								    // fighters, and so on.
								    _fightStatusManager.UpdateStatus();
								    IntelligentCharacter theIntelligentCharacter = (IntelligentCharacter)intelligentCharacter;

								    if (theIntelligentCharacter.Fighter == null)
								    {
									    throw new System.ArgumentException (nameof(theIntelligentCharacter.Fighter) +  " reference not found.");
								    }

								    Fighter fighter = (Fighter)theIntelligentCharacter.Fighter;
								    fighter.ObservingOpponent = true;

								    _fightManager.SetNextTurn ();

								    // Change FSM state to the one for observing the opponent.
								    theIntelligentCharacter.MindOfTheFighter.ChangeState (new StateObserving ());

								    IList<TechniqueAction> receiveHitActions = _techniqueActionPicker.PickActions(fightingActions);
								    _techniquePerformer.PerformTechniques(receiveHitActions);
							    }
						    }; // End Finished
					    } // End else
				    };
			    } // End if
		    }
	    }
	    // End Execute

	    /// <summary>
	    /// A method for actions to be taken when the FSM
	    /// exits the state.
	    /// </summary>
	    public void Exit ()
	    {
            Console.WriteLine("Exiting state StatePerforming...");
	    }

	    #endregion

	    #region Private methods

	    /// <summary>
	    /// A short delay before switching to the main character. 
	    /// </summary>
	    /// <returns>An enumerator representing the method.</returns>
	    private IEnumerator turnSwitchDelay ()
	    {
		    yield return new WaitForSeconds (TURN_SWITCH_DELAY);
	    }

	    #endregion

    }
}