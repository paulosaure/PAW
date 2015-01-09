package com.example.paul.remotecontrol;

import android.media.MediaPlayer;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.ImageButton;


public class MainActivity extends ActionBarActivity {

    private MediaPlayer mPlayer = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        loadData();
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    public void loadData(){
        loadSoundButton();
    }

    public void loadSoundButton(){
        Button btn_sound_encouragement = (Button) findViewById(R.id.buttonEncouragement);
        btn_sound_encouragement.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                playSound(R.raw.encouragement);
            }

        });

        Button btn_sound_felicitation = (Button) findViewById(R.id.buttonFelicitation);
        btn_sound_felicitation.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                playSound(R.raw.bravo);
            }

        });

        Button btn_sound_erreur = (Button) findViewById(R.id.buttonErreur);
        btn_sound_erreur.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {

                playSound(R.raw.erreur);
            }

        });

        Button btn_sound_essaieEncore = (Button) findViewById(R.id.buttonEssaieEncore);
        btn_sound_essaieEncore.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                playSound(R.raw.essaie_encore);
            }

        });

        ImageButton btn_next_view = (ImageButton) findViewById(R.id.imageButton);
        btn_sound_essaieEncore.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {

            }

        });
    }

    //Play Sound
    @Override
    public void onPause() {
        super.onPause();
        if(mPlayer != null) {
            mPlayer.stop();
            mPlayer.release();
        }
    }

    private void playSound(int resId) {
        if(mPlayer != null) {
            mPlayer.stop();
            mPlayer.release();
        }
        mPlayer = MediaPlayer.create(this, resId);
        mPlayer.start();
    }


}
