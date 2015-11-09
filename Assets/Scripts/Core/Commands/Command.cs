using UnityEngine;
using System.Collections;

/// <summary>
/// The base class of the Command used in Command Pattern 
/// (http://en.wikipedia.org/wiki/Command_pattern).
/// 
/// It is supposed that anybody that want to use it, will
/// have to specialise this class for his purposes.
/// </summary>

public class Command {

	// Every command need to have a state:
	// a) Idle: Was never executed OR was rewinded
	// b) Running: Currently need to call Update to do his job
	// c) Finished: Was executed and was successfully finished
	// d) Aborted: Was executed and was stoped at running time
	// e) Failed: An error occured while running

	public enum CommandState { Idle = 0, Running, Finished, Aborted, Failed };

	protected CommandState _currentState = CommandState.Idle;
	public CommandState CurrentState {
		get { return _currentState; }
	}

	/// <summary>
	/// Do stuff before actually start running.
	/// </summary>

	public virtual void Execute() {
		_currentState = CommandState.Running;
	}

	/// <summary>
	/// Do stuff once per frame.
	/// </summary>

	public virtual void Update() {
		_currentState = CommandState.Finished;
	}

	/// <summary>
	/// Clear stuff if the execution was halted.
	/// </summary>

	public virtual void Abort() {
		_currentState = CommandState.Aborted;
	}
}
