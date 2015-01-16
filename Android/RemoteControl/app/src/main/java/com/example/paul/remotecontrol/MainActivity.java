package com.example.paul.remotecontrol;

import android.media.MediaPlayer;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ImageButton;

import com.github.nkzawa.emitter.Emitter;
import com.github.nkzawa.socketio.client.IO;
import com.github.nkzawa.socketio.client.Socket;

import org.json.JSONException;
import org.json.JSONObject;

import java.net.URISyntaxException;


public class MainActivity extends ActionBarActivity {

    private MediaPlayer mPlayer = null;
    Socket socket;
    String currentViewTable;
    private int viewPosition = 1;
    private String url = "http://192.168.1.2:8080";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        loadData();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        socket.disconnect();
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
        connectionServer();
    }


//Buttons
    public void onPreviousView()
    {
        if(viewPosition >1){
            viewPosition--;
            forcePush("next");
        }
    }

    public void onNextView()
    {
        if(viewPosition<6){
            viewPosition++;
            forcePush("previous");
        }
    }

    //Button Useless
    public void buttonSendAide(View v)
    {
        String msg = (((Button) v).getText()).toString();
        sendAide(msg);
    }

//Sound
    public void loadSoundButton(){
        Button btn_sound_encouragement = (Button) findViewById(R.id.buttonEncouragement);
        btn_sound_encouragement.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendSound("encouragement");
            }

        });

        Button btn_sound_felicitation = (Button) findViewById(R.id.buttonFelicitation);
        btn_sound_felicitation.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendSound("felicitation");
            }

        });

        Button btn_sound_erreur = (Button) findViewById(R.id.buttonErreur);
        btn_sound_erreur.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {

                sendSound("erreur");
            }

        });

        Button btn_sound_essaieEncore = (Button) findViewById(R.id.buttonEssaieEncore);
        btn_sound_essaieEncore.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendSound("essaieEncore");
            }

        });

        //ImageButton btn_next_view = (ImageButton) findViewById(R.id.imageButton);
        btn_sound_essaieEncore.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {

            }

        });
    }


//Action checkBox
    public void onCheckboxClicked(View view) {
        boolean checked = ((CheckBox) view).isChecked();

        switch(view.getId()) {
            case R.id.checkBoxTexte:
                if (checked){sendAide("string");}
                else{sendAide("none_string");}
                break;
            case R.id.checkBoxImage:
                if (checked){sendAide("image");}
                else{sendAide("none_image");}
                break;
        }
    }

    public void loadView(int view)
    {
        switch (view)
        {
            case 1: //Vue roue
                break;
            case 2: //Vue choix piece
                break;
            case 3: //Vue choix atelier
                break;
            case 4: //Vue trouver les objets avec les aides
                break;
            case 5: //Vue ordonnancer les objets
                break;
            case 6: //Vue afficher les vidéos
                break;
            default:
                break;
        }
    }

//Partie Server
    public void connectionServer(){
        try {
       // socket = IO.socket("http://192.168.1.6:8080");
        socket = IO.socket(url);
        } catch (URISyntaxException e) {
            e.printStackTrace();
        }

        socket.on(Socket.EVENT_CONNECT, new Emitter.Listener() {
            @Override
            public void call(Object... args) {
                socket.emit("foo", "hi"); //TODO clean when check
            }
        }).on(Socket.EVENT_DISCONNECT, new Emitter.Listener() {

            @Override
            public void call(Object... args) {}

        }).on("changeVue", new Emitter.Listener() {
            @Override
            public void call(Object... args) {
                currentViewTable = (String) args[0];
                Log.e("Server" , "Result : " + currentViewTable);
            }
        });
        socket.connect();
    }

//Envoie des messages
    public JSONObject sendSound(String msg){
        socket.emit("sound", msg);
        return null;
    }

//Event
    public JSONObject sendClignoter (String msg){
        socket.emit("clignoter", msg);
        return null;
    }

    public JSONObject sendFlash(String msg){
        socket.emit("flash", msg);
        return null;
    }

    public JSONObject sendZoom(String msg){
        socket.emit("zoom", msg);
        return null;
    }
//Aide
    public JSONObject sendAide(String msg){
        socket.emit("aide", msg);
        return null;
    }

//ForcePush
    public JSONObject forcePush(String msg){
        socket.emit("hardPush", msg);
        return null;
    }

}
