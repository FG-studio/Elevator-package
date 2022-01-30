using System;

namespace FGElevator
{
	public enum ElevatorStateType
	{
		HIGHEST = 2,
		HIGH_TRANSFORM = 1,
		LOW_TRANSFORM = -1,
		LOWEST = -2,
	}

	interface IElevatorStateListener
	{
		public void OnChangeState(ElevatorState state);
	}

	interface IElevatorObserver
	{
		public void OnChangeState(ElevatorStateType state, bool switchState);
	}

	class ElevatorContext : IElevatorStateListener
	{
		private ElevatorState _state = null;
		private IElevatorObserver _observer = null;
		public ElevatorContext(ElevatorStateType initState, IElevatorObserver obs)
		{
			_observer = obs;
			ElevatorState newState = StateFactory.getInstance().createState(initState);
			this.OnChangeState(newState);
		}
		public void ChangeState()
		{
			if (this._state == null)
			{
				throw new Exception("ELEVATOR_NOT_INITED_STATE");
			}
			this._state.ChangeState();
		}
		public void OnChangeState(ElevatorState state)
		{
			if (_state != null)
			{
				this._state.ListenerUnsubcribe();
			}

			this._state = state;
			this._state.ListenerSubcribe(this);
			if (this._observer != null)
			{
				this._observer.OnChangeState(this._state.Type, this._state.SwitchDisabled);
			}
		}
	}

	class StateFactory
	{
		static public StateFactory instance = null;
		static public StateFactory getInstance()
		{
			if (StateFactory.instance == null)
			{
				StateFactory.instance = new StateFactory();
			}

			return StateFactory.instance;
		}

		private StateFactory() { }

		public ElevatorState createState(ElevatorStateType type)
		{
			switch (type)
			{
				case ElevatorStateType.HIGHEST:
					return new ElevatorHighestState();
				case ElevatorStateType.HIGH_TRANSFORM:
					return new ElevatorHighTransformState();
				case ElevatorStateType.LOW_TRANSFORM:
					return new ElevatorLowTransformState();
				case ElevatorStateType.LOWEST:
					return new ElevatorLowestState();
				default:
					throw new Exception("UNSUPPORTED_STATE_TYPE");
			}
		}
	}

	abstract class ElevatorState
	{
		protected IElevatorStateListener _listener;
		protected ElevatorStateType _type;
		public void ListenerUnsubcribe()
		{
			this._listener = null;
		}
		public void ListenerSubcribe(IElevatorStateListener listener)
		{
			this._listener = listener;
		}

		public ElevatorStateType Type
		{
			get { return _type; }
		}

		public bool SwitchDisabled
        {
			get { return _switchDisable;  }
        }

		public abstract void ChangeState();

		protected bool _switchDisable = false;
	}

	class ElevatorHighestState : ElevatorState
	{
		public ElevatorHighestState()
		{
			_switchDisable = false;
			_type = ElevatorStateType.HIGHEST;
		}

		public override void ChangeState()
		{
			ElevatorState newState = StateFactory.getInstance().createState(ElevatorStateType.LOW_TRANSFORM);
			if (this._listener != null)
			{
				this._listener.OnChangeState(newState);
			}
		}
	}

	class ElevatorHighTransformState : ElevatorState
	{
		public ElevatorHighTransformState()
		{
			_switchDisable = true;
			_type = ElevatorStateType.HIGH_TRANSFORM;
		}

		public override void ChangeState()
		{
			ElevatorState newState = StateFactory.getInstance().createState(ElevatorStateType.HIGHEST);
			if (this._listener != null)
			{
				this._listener.OnChangeState(newState);
			}
		}
	}

	class ElevatorLowTransformState : ElevatorState
	{
		public ElevatorLowTransformState()
		{
			_switchDisable = true;
			_type = ElevatorStateType.LOW_TRANSFORM;
		}

		public override void ChangeState()
		{
			ElevatorState newState = StateFactory.getInstance().createState(ElevatorStateType.LOWEST);
			if (this._listener != null)
			{
				this._listener.OnChangeState(newState);
			}
		}
	}

	class ElevatorLowestState : ElevatorState
	{
		public ElevatorLowestState()
		{
			_switchDisable = true;
			_type = ElevatorStateType.LOWEST;
		}

		public override void ChangeState()
		{
			ElevatorState newState = StateFactory.getInstance().createState(ElevatorStateType.HIGH_TRANSFORM);
			if (this._listener != null)
			{
				this._listener.OnChangeState(newState);
			}
		}
	}
}
