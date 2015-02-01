package com.example.paul.telecommande;

import android.app.Fragment;
import android.content.Context;
import android.graphics.Bitmap;
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
    private boolean pressLong = false;
    private View v;
    private Bitmap img;
    private LinearLayout rl;
    private CustomButton buttonTouchHere;
    private CustomButton buttonTouchHere2;
    private int nbButton = 1;
    private boolean needToResize = false;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        context = this.getActivity().getApplicationContext();

        // Inflate the layout for this fragment
        v =  inflater.inflate(R.layout.wheel_view, container, false);
        rl = (LinearLayout)v.findViewById(R.id.wheelView);
        drawButton();

        return v;
    }

    public void drawButton()
    {
        buttonTouchHere = new CustomButton(context , R.drawable.touchme, needToResize, nbButton);
        rl.setGravity(Gravity.CENTER_VERTICAL |Gravity.CENTER_HORIZONTAL);
        rl.addView(buttonTouchHere);

        buttonTouchHere.setOnLongClickListener(speakHoldListener);
       // buttonTouchHere.setOnTouchListener(speakTouchListener);
        img = buttonTouchHere.getBitmap();
    }

    public void drawPetals(Bitmap img)
    {
        int cpt = 1;
        rl.addView(new Petals(context, img, cpt));

        cpt++;
    }

    //Listener
    private View.OnLongClickListener speakHoldListener = new View.OnLongClickListener() {
        @Override
        public boolean onLongClick(View v) {
       //     Log.e("Click Listener","entre");
       //     Log.e("taille fragment", "heigth "+ rl.getHeight()+" width "+rl.getWidth());
            pressLong = true;
            drawPetals(img);
            buttonTouchHere.bringToFront();
            //buttonTouchHere.setY();
            return true;
        }
    };

   /* private View.OnTouchListener speakTouchListener = new View.OnTouchListener() {
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
                    rl.addView(buttonTouchHere);
                }
            }
            return false;
        }
    };*/
}