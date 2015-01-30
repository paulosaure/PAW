package com.example.paul.telecommande;

import android.app.Fragment;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Point;
import android.os.Bundle;
import android.view.LayoutInflater;
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
    private Actions[] actions = new Actions[] { Actions.image, Actions.text, Actions.son, Actions.zoom};

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        context = this.getActivity().getApplicationContext();

        // Inflate the layout for this fragment
        View v =  inflater.inflate(R.layout.wheel_view, container, false);

        Bitmap img = drawButton(v);
        drawPetals(v, img);


        return v;
    }

    public Bitmap drawButton(View v)
    {
        CustomButton buttonTouchHere = new CustomButton(context , R.drawable.touchme, false, 1);
        LinearLayout rl = (LinearLayout)v.findViewById(R.id.wheelView);
        rl.addView(buttonTouchHere);

        return buttonTouchHere.getBitmap();
    }

    public void drawPetals(View v, Bitmap img)
    {
        int cpt = 1;
        for(Actions act: actions) {
            new Petals(context, act, img, cpt);
        }
    }

}