using System.Runtime.CompilerServices;

namespace ProFak.UI;

// https://devblogs.microsoft.com/oldnewthing/20190329-00/?p=102373

struct ControlThreadSwitcher : INotifyCompletion
{
	internal ControlThreadSwitcher(Control control) => this.control = control;
	public ControlThreadSwitcher GetAwaiter() => this;
	public bool IsCompleted => !control.InvokeRequired;
	public void GetResult() { }
	public void OnCompleted(Action continuation) => control.BeginInvoke(continuation);
	Control control;
}

struct ThreadPoolThreadSwitcher : INotifyCompletion
{
	public ThreadPoolThreadSwitcher GetAwaiter() => this;
	public bool IsCompleted => SynchronizationContext.Current == null;
	public void GetResult() { }
	public void OnCompleted(Action continuation) => ThreadPool.QueueUserWorkItem(_ => continuation());
}

class ThreadSwitcher
{
	static public ControlThreadSwitcher ResumeForegroundAsync(Control control) => new ControlThreadSwitcher(control);
	static public ThreadPoolThreadSwitcher ResumeBackgroundAsync() => new ThreadPoolThreadSwitcher();
}