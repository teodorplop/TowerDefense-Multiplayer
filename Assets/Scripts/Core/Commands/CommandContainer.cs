using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Command container is a Master Class that merge
/// more uses of the Command Pattern.
/// 
/// Uses: This is a specialised case of command who have
/// more commands. The only purpose for it is to run its commands
/// until the queue of commands ended. Note: the actual update
/// must be called from exterior (in game loop).
/// </summary>

public class CommandContainer : Command {

	// There are more ways how to use it. Here is described
	// only one case, when a subcommand failed, it could stop,
	// abort respective command or abort all commands from the
	// command hierarchy. Actually this was never used
	// but catch a lot of cases in a complex application.

	public enum Flag { Perpetual = 0, ContinueOnFail };

	private Queue<Command> _commandQueue = new Queue<Command> ();
	private int _flags = 0;

	private bool HasFlag(Flag checkFlag) {
		return ((_flags & ((int)checkFlag)) != 0);
	}

	/// <summary>
	/// Run current command once per frame. If there
	/// are no more commands do nothing.
	/// </summary>

	public virtual void Update() {

		if(_commandQueue.Count == 0) {
			if(!HasFlag(Flag.Perpetual)) {
				_currentState = CommandState.Finished;
			}

			return ;
		}

		// Take current command from the queue.
		Command _currentCommand = _commandQueue.Peek ();

		// If the command was never runed, execute it for
		// the first time.
		if(_currentCommand.CurrentState == Command.CommandState.Idle) {
			_currentCommand.Execute ();
		}

		// If the command is still running, update it.
		if(_currentCommand.CurrentState == Command.CommandState.Running) {
			_currentCommand.Update ();
		}

		// If the command has finished, remove it from the queue.
		if(_currentCommand.CurrentState == Command.CommandState.Finished) {
			_commandQueue.Dequeue ();
		}

		// If the command has failed, check the flags
		if(_currentCommand.CurrentState == Command.CommandState.Failed) {
			if(HasFlag(Flag.ContinueOnFail)) {
				// If it is supposed to continue, just remove the failed command from the queue
				_commandQueue.Dequeue ();
			} else {
				// Else abort all commands, and set the state of the CommandContainer as Failed.
				while(_commandQueue.Count != 0) {
					_commandQueue.Peek().Abort();
					_commandQueue.Dequeue ();
				}

				_currentState = CommandState.Failed;
			}
		}
	}

	/// <summary>
	/// Push a command to the commands queue.
	/// </summary>

	public virtual void AddCommand(Command command) {
		_commandQueue.Enqueue (command);
	}

	/// <summary>
	/// Clear stuff if the execution was halted.
	/// </summary>

	public virtual void Abort() {
		while(_commandQueue.Count != 0) {
			_commandQueue.Peek().Abort ();
			_commandQueue.Dequeue ();
		}

		_currentState = Command.CommandState.Aborted;
	}
}
