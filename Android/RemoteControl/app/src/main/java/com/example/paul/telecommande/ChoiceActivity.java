package com.example.paul.telecommande;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.paul.remotecontrol.R;

/**
 * Created by Paul on 16/01/2015.
 */
public class ChoiceActivity extends Fragment {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {


        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.choice_activity, container, false);
    }
}
