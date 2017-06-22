package com.neurotec.tutorials.biometrics;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.EnumSet;

import com.neurotec.biometrics.NERecord;
import com.neurotec.biometrics.NFCore;
import com.neurotec.biometrics.NFDelta;
import com.neurotec.biometrics.NFDoubleCore;
import com.neurotec.biometrics.NFMinutia;
import com.neurotec.biometrics.NFMinutiaFormat;
import com.neurotec.biometrics.NFRecord;
import com.neurotec.biometrics.NLRecord;
import com.neurotec.biometrics.NSRecord;
import com.neurotec.biometrics.NTemplate;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.util.IOUtils;

public final class ShowTemplateContent extends Activity {

	private static final String TAG = ShowTemplateContent.class.getSimpleName();
	private static final int REQUEST_CODE_GET_TEMPLATE = 1;

	private Button mButton;
	private TextView mResult;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_show_template_content);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_template);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getTemplate();
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_TEMPLATE) {
			if (resultCode == RESULT_OK) {
				try {
					show(data.getData());
				} catch (Exception e) {
					showMessage(e.getMessage());
					Log.e(TAG, "Exception", e);
				}
			}
		}
	}

	private void getTemplate() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, REQUEST_CODE_GET_TEMPLATE);
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private void show(Uri imageUri) throws IOException {
		NTemplate template = null;

		try {
			ByteBuffer templateBuffer = IOUtils.toByteBuffer(this, imageUri);
			showMessage(getString(R.string.msg_template_contains));

			template = new NTemplate(templateBuffer);
			if (template.getFingers() == null) {
				showMessage(getString(R.string.msg_0_fingers));
			} else {
				showMessage(getString(R.string.format_n_fingers, template.getFingers().getRecords().size()));
				for (NFRecord nfRec : template.getFingers().getRecords()) {
					printNFRecord(nfRec);
				}
			}
			if (template.getFaces() == null) {
				showMessage(getString(R.string.msg_0_faces));
			} else {
				showMessage(getString(R.string.format_n_faces, template.getFaces().getRecords().size()));
				for (NLRecord nlRec : template.getFaces().getRecords()) {
					printNLRecord(nlRec);
				}
			}
			if (template.getIrises() == null) {
				showMessage(getString(R.string.msg_0_irises));
			} else {
				showMessage(getString(R.string.format_n_irises, template.getIrises().getRecords().size()));
				for (NERecord neRec : template.getIrises().getRecords()) {
					printNERecord(neRec);
				}
			}
			if (template.getVoices() == null) {
				showMessage(getString(R.string.msg_0_voices));
			} else {
				showMessage(getString(R.string.format_n_voices, template.getVoices().getRecords().size()));
				for (NSRecord nsRec : template.getVoices().getRecords()) {
					printNSRecord(nsRec);
				}
			}
		} finally {
			if (template != null) template.dispose();
		}
	}

	private void printNFRecord(NFRecord nfRec) {
		showMessage(getString(R.string.format_template_g, nfRec.getG()));
		showMessage(getString(R.string.format_impression_type, nfRec.getImpressionType()));
		showMessage(getString(R.string.format_pattern_class, nfRec.getPatternClass()));
		showMessage(getString(R.string.format_cbeff_product_type, nfRec.getCBEFFProductType()));
		showMessage(getString(R.string.format_position, nfRec.getPosition()));
		showMessage(getString(R.string.format_ridge_counts_type, nfRec.getRidgeCountsType()));
		showMessage(getString(R.string.format_width, nfRec.getWidth()));
		showMessage(getString(R.string.format_height, nfRec.getHeight()));
		showMessage(getString(R.string.format_horizontal_resolution, nfRec.getHorzResolution()));
		showMessage(getString(R.string.format_vertical_resolution, nfRec.getVertResolution()));
		showMessage(getString(R.string.format_template_quality, nfRec.getQuality()));
		showMessage(getString(R.string.format_size, nfRec.getSize()));

		showMessage(getString(R.string.format_minutia_count, nfRec.getMinutiae().size()));

		EnumSet<NFMinutiaFormat> minutiaFormat = nfRec.getMinutiaFormat();

		int index = 1;
		for (NFMinutia minutia : nfRec.getMinutiae()) {
			showMessage(getString(R.string.format_minutia_n_of_m, index, nfRec.getMinutiae().size()));
			showMessage(getString(R.string.format_x, minutia.x));
			showMessage(getString(R.string.format_y, minutia.y));
			showMessage(getString(R.string.format_angle, rotationToDegrees(minutia.angle & 0xFF)));
			if (minutiaFormat.contains(NFMinutiaFormat.HAS_QUALITY)) {
				showMessage(getString(R.string.format_minutia_quality, minutia.quality & 0xFF));
			}
			if (minutiaFormat.contains(NFMinutiaFormat.HAS_G)) {
				showMessage(getString(R.string.format_minutia_g, minutia.g));
			}
			if (minutiaFormat.contains(NFMinutiaFormat.HAS_CURVATURE)) {
				showMessage(getString(R.string.format_curvature, minutia.curvature));
			}

			showMessage(System.getProperty("line.separator"));
			index++;
		}

		index = 1;
		for (NFDelta delta : nfRec.getDeltas()) {
			showMessage(getString(R.string.format_delta_n_of_m, index, nfRec.getDeltas().size()));
			showMessage(getString(R.string.format_x, delta.x));
			showMessage(getString(R.string.format_y, delta.y));
			showMessage(getString(R.string.format_angle1, rotationToDegrees(delta.angle1)));
			showMessage(getString(R.string.format_angle2, rotationToDegrees(delta.angle2)));
			showMessage(getString(R.string.format_angle3, rotationToDegrees(delta.angle3)));

			showMessage(System.getProperty("line.separator"));
			index++;
		}

		index = 1;
		for (NFCore core : nfRec.getCores()) {
			showMessage(getString(R.string.format_core_n_of_m, index, nfRec.getCores().size()));
			showMessage(getString(R.string.format_x, core.x));
			showMessage(getString(R.string.format_y, core.y));
			showMessage(getString(R.string.format_angle, rotationToDegrees(core.angle)));

			showMessage(System.getProperty("line.separator"));
			index++;
		}

		index = 1;
		for (NFDoubleCore doubleCore : nfRec.getDoubleCores()) {
			showMessage(getString(R.string.format_double_core_n_of_m, index, nfRec.getDoubleCores().size()));
			showMessage(getString(R.string.format_x, doubleCore.x));
			showMessage(getString(R.string.format_y, doubleCore.y));

			showMessage("");
			index++;
		}
	}

	private void printNLRecord(NLRecord nlRec) {
		showMessage(getString(R.string.format_template_quality, nlRec.getQuality()));
		showMessage(getString(R.string.format_size, nlRec.getSize()));
	}

	private void printNERecord(NERecord neRec) {
		showMessage(getString(R.string.format_position, neRec.getPosition()));
		showMessage(getString(R.string.format_size, neRec.getSize()));
	}

	private void printNSRecord(NSRecord nsRec) {
		showMessage(getString(R.string.format_phrase_id, nsRec.getPhraseId()));
		showMessage(getString(R.string.format_size, nsRec.getSize()));
	}

	private int rotationToDegrees(int rotation) {
		return (2 * rotation * 360 + 256) / (2 * 256);
	}

}
