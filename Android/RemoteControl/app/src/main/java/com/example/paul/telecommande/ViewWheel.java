package com.example.paul.telecommande;

import android.app.Fragment;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.Point;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;

import com.example.paul.remotecontrol.R;

/**
 * Created by Paul on 08/01/2015.
 */
public class ViewWheel extends Fragment {

    private Context context;
    private View v;
    private Bitmap img;
    private RelativeLayout rl;
    private CustomButton buttonTouchHere;
    private int nbButton = 1;
    private boolean needToResize = false;
    private Petals petals;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        context = this.getActivity().getApplicationContext();

        // Inflate the layout for this fragment
        v = inflater.inflate(R.layout.wheel_view, container, false);
        rl = (RelativeLayout) v.findViewById(R.id.wheelView);
        drawButton();

      //  rl.setBackgroundColor(Color.YELLOW);
        return v;
    }

    public void drawButton() {
        buttonTouchHere = new CustomButton(context, R.drawable.touchme, needToResize, nbButton);
        rl.setGravity(Gravity.CENTER_VERTICAL | Gravity.CENTER_HORIZONTAL);
        rl.addView(buttonTouchHere);

        buttonTouchHere.setOnTouchListener(speakTouchListener);
        img = buttonTouchHere.getBitmap();

        createPetals();
    }

    public void createPetals() {
        petals = new Petals(context, img);
        petals.unDraw();
        rl.addView(petals);
    }

    private View.OnTouchListener speakTouchListener = new View.OnTouchListener() {
        @Override
        public boolean onTouch(View v, MotionEvent event) {
            int action = event.getAction();
            if (action == MotionEvent.ACTION_DOWN) {
                petals.drawPetals();
                petals.invalidate();
                buttonTouchHere.bringToFront();
            } else if (action == MotionEvent.ACTION_UP) {
                petals.unDraw();
                petals.invalidate();
            }
            return true;
        }
    };
}