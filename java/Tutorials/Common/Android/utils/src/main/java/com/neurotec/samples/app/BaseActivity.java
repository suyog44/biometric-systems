package com.neurotec.samples.app;

import android.app.Activity;
import android.app.ProgressDialog;
import android.util.Log;

import com.neurotec.samples.util.ExceptionUtils;
import com.neurotec.samples.util.ToastManager;
import com.neurotec.samples.view.ErrorDialogFragment;
import com.neurotec.samples.view.InfoDialogFragment;

public abstract class BaseActivity extends Activity {

	// ===========================================================
	// Private fields
	// ===========================================================

	private ProgressDialog mProgressDialog;

	// ===========================================================
	// Protected methods
	// ===========================================================

	protected void showProgress(int messageId) {
		showProgress(getString(messageId));
	}

	protected void showProgress(String message) {
		hideProgress();
		mProgressDialog = ProgressDialog.show(this, "", message);
	}

	protected void hideProgress() {
		if (mProgressDialog != null && mProgressDialog.isShowing()) {
			mProgressDialog.dismiss();
		}
	}

	protected void showToast(int messageId) {
		ToastManager.show(this, messageId);
	}

	protected void showToast(String message) {
		ToastManager.show(this, message);
	}

	protected void showError(String message, boolean close) {
		ErrorDialogFragment.newInstance(message, close).show(getFragmentManager(), "error");
	}

	protected void showError(int messageId) {
		showError(getString(messageId));
	}

	protected void showError(String message) {
		showError(message, false);
	}

	protected void showError(Throwable th) {
		Log.e(getClass().getSimpleName(), "Exception", th);
		showError(ExceptionUtils.getMessage(th), false);
	}

	protected void showInfo(int messageId) {
		showInfo(getString(messageId));
	}

	protected void showInfo(String message) {
		InfoDialogFragment.newInstance(message).show(getFragmentManager(), "info");
	}

	@Override
	protected void onStop() {
		super.onStop();
		hideProgress();
	}
}
