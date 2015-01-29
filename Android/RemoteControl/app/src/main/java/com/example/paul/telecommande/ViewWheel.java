package com.example.paul.telecommande;

import android.app.Fragment;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RelativeLayout;

import com.example.paul.remotecontrol.R;

/**
 * Created by Paul on 08/01/2015.
 */
public class ViewWheel extends Fragment {

    private Context context;

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

        String uriImg = "@drawable/touchme";
        Actions[] actions = new Actions[] { Actions.image, Actions.text, Actions.son, Actions.zoom};

        CustomButton buttonTouchHere = new CustomButton(context, actions , uriImg);

        RelativeLayout rl = (RelativeLayout)v.findViewById(R.id.wheelView);

        rl.addView(buttonTouchHere);

        return v;
    }


}
/*
        ImageButton touchHere = (ImageButton) v.findViewById(R.id.longPress);

        touchHere.setOnLongClickListener(new View.OnLongClickListener() {
            @Override
            public boolean onLongClick(View v) {
                Log.e("test", "touche");
                return true;
            }
        });*/