package com.neurotec.samples.abis.util;

import java.util.concurrent.locks.Condition;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

public abstract class LockingTask {

	private final Lock lock;
	private final Condition idle;
	private boolean busy;

	public LockingTask() {
		lock = new ReentrantLock();
		idle = lock.newCondition();
	}

	public final boolean isBusy() {
		lock.lock();
		try {
			return busy;
		} finally {
			lock.unlock();
		}
	}

	public final void setBusy(boolean busy) {
		lock.lock();
		try {
			this.busy = busy;
			idle.signalAll();
		} finally {
			lock.unlock();
		}
	}

	public final void startAndWait() throws InterruptedException {
		lock.lock();
		try {
			if (busy) {
				performTask();
				while (busy) {
					idle.await();
				}
			}
		} finally {
			lock.unlock();
		}
	}

	public abstract void performTask();

}
