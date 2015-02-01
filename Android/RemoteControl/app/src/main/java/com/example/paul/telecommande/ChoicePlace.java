package com.example.paul.telecommande;

import android.app.Fragment;
import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
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
    private boolean needToResize = true;

    private int nbButton =3;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        context = this.getActivity().getApplicationContext();

        // Inflate the layout for this fragment
        v =  inflater.inflate(R.layout.choice_place, container, false);
        rl = (RelativeLayout)v.findViewById(R.id.choicePlace);
        drawButtons();

        // Inflate the layout for this fragment
        return v;
    }

    public void drawButtons()
    {
        buttonTouchHere1 = new CustomButton(context , R.drawable.cuisine, needToResize, nbButton);
        buttonTouchHere2 = new CustomButton(context , R.drawable.salon, needToResize, nbButton);
        buttonTouchHere3 = new CustomButton(context , R.drawable.salledebain, needToResize, nbButton);

        rl.addView(buttonTouchHere1);
        rl.addView(buttonTouchHere2);
        rl.addView(buttonTouchHere3);

        buttonTouchHere1.setOnLongClickListener(speakHoldListener);
        buttonTouchHere1.setOnTouchListener(speakTouchListener);

        buttonTouchHere2.setOnLongClickListener(speakHoldListener);
        buttonTouchHere2.setOnTouchListener(speakTouchListener);

        buttonTouchHere3.setOnLongClickListener(speakHoldListener);
        buttonTouchHere3.setOnTouchListener(speakTouchListener);
    }

    public void drawPetals(Bitmap img)
    {
        int cpt = 1;
        rl.addView(new Petals(context, img));
        cpt++;
    }

    private View.OnLongClickListener speakHoldListener = new View.OnLongClickListener() {
        @Override
        public boolean onLongClick(View v) {
            pressLong = true;
            drawPetals(img);
            return true;
        }
    };

    private View.OnTouchListener speakTouchListener = new View.OnTouchListener() {
        @Override
        public boolean onTouch(View pView, MotionEvent pEvent) {
            pView.onTouchEvent(pEvent);
            // We're only interested in when the button is released.
            if (pEvent.getAction() == MotionEvent.ACTION_UP) {
                // We're only interested in anything if our speak button is currently pressed.
                if (pressLong) {
                    // Do something when the button is released.
                    pressLong = false;
                    rl.removeAllViews();
                    rl.addView(buttonTouchHere1);
                    rl.addView(buttonTouchHere2);
                    rl.addView(buttonTouchHere3);
                }
            }
            return false;
        }
    };
}
