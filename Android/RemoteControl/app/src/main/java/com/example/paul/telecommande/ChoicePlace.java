package com.example.paul.telecommande;

import android.app.Fragment;
import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;

import com.example.paul.remotecontrol.R;

/**
 * Created by Paul on 16/01/2015.
 */
public class ChoicePlace extends Fragment {

    private Context context;
    private boolean pressLong = false;
    private View v;
    private Bitmap img;
    private RelativeLayout rl;
    private CustomButton buttonTouchHere1;
    private CustomButton buttonTouchHere2;
    private CustomButton buttonTouchHere3;
    private Petals petals;
    private boolean needToResize = true;

    private int nbButton =3;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        context = this.getActivity().getApplicationContext();

        // Inflate the layout for this fragment
        v =  inflater.inflate(R.layout.choice_place, container, false);
        rl = (RelativeLayout)v.findViewById(R.id.choicePlace);
        rl.setGravity(Gravity.CENTER_VERTICAL | Gravity.CENTER_HORIZONTAL);
        drawButton();

        // Inflate the layout for this fragment
        return v;
    }

    public void drawButton() {
        buttonTouchHere1 = new CustomButton(context, R.drawable.cuisine, needToResize, nbButton);
        buttonTouchHere2 = new CustomButton(context, R.drawable.salon, needToResize, nbButton);
        buttonTouchHere3 = new CustomButton(context, R.drawable.salledebain, needToResize, nbButton);

        rl.addView(buttonTouchHere1);
        rl.addView(buttonTouchHere2);
        rl.addView(buttonTouchHere3);

        buttonTouchHere1.setOnTouchListener(speakTouchListener);
        buttonTouchHere2.setOnTouchListener(speakTouchListener);
        buttonTouchHere3.setOnTouchListener(speakTouchListener);

        img = buttonTouchHere1.getBitmap();// Chopper le bon bouton en fonction de ce qu'on touche
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
                buttonTouchHere1.bringToFront();
            } else if (action == MotionEvent.ACTION_UP) {
                petals.unDraw();
                petals.invalidate();
            }
            return true;
        }
    };
}
