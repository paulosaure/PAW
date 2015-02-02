package com.example.paul.telecommande;

import android.app.Fragment;
import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.util.Log;
import android.view.GestureDetector;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewConfiguration;
import android.view.ViewGroup;
import android.widget.RelativeLayout;
import android.widget.Toast;


import com.example.paul.remotecontrol.R;

/**
 * Created by Paul on 08/01/2015.
 */
public class ViewWheel extends Fragment{

    private Context context;
    private View v;
    private Bitmap img;
    private RelativeLayout rl;
    private CustomButton buttonTouchHere;
    private int nbButton = 1;
    private boolean needToResize = false;
    private Petals petals;
    private static final String DEBUG_TAG = "Gestures";

    private GestureDetector gestureDetector;
    View.OnTouchListener gestureListener;


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        context = this.getActivity().getApplicationContext();
        gestureDetector = new GestureDetector(context, new MyGestureDetector(context));
        
        gestureListener = new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                return gestureDetector.onTouchEvent(event);
            }
        };

        v = inflater.inflate(R.layout.wheel_view, container, false);
        drawButton();

        return v;
    }


    public void drawButton() {
        buttonTouchHere = (CustomButton) v.findViewById(R.id.img);
        buttonTouchHere.setOnTouchListener(speakTouchListener);
        img = buttonTouchHere.getBitmap(); // Il faut associer le bouton touché à l'image
        createPetals(); // On crée la pétale
    }

    public void createPetals() {
        petals = new Petals(context, img);
        petals.unDraw();

        RelativeLayout.LayoutParams relativeParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MATCH_PARENT, RelativeLayout.LayoutParams.MATCH_PARENT);
        ((RelativeLayout) v.findViewById(R.id.wheelView)).addView(petals, relativeParams);
    }

    private View.OnTouchListener speakTouchListener = new View.OnTouchListener() {
        @Override
        public boolean onTouch(View v, MotionEvent event) {
            int action = event.getAction();
            if (action == MotionEvent.ACTION_DOWN) {
                petals.drawPetals(); // on dessine
                petals.invalidate(); // on lance le draw
                buttonTouchHere.bringToFront();
            } else if (action == MotionEvent.ACTION_UP) {
                petals.unDraw();
                petals.invalidate();
            }
            return false;
        }
    };
}