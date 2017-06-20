package com.neurotec.samples.devices.view;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.neurotec.images.NImage;
import com.neurotec.samples.devices.R;
import com.neurotec.view.NView;

public class ImageFragment extends Fragment {

	// ==============================================
	// Private fields
	// ==============================================

	private NView mImageView;

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_image, container, false);
		mImageView = ((NView) rootView.findViewById(R.id.view_image));
		return rootView;
	}

	public void setImage(NImage image) {
		if (mImageView.getImage() != image) {
			mImageView.setImage(image);
		}
	}
}