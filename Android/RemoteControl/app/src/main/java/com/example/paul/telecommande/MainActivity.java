package com.example.paul.telecommande;

import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.media.MediaPlayer;
import android.support.v4.app.FragmentActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;

import com.example.paul.remotecontrol.R;
import com.github.nkzawa.emitter.Emitter;
import com.github.nkzawa.socketio.client.IO;
import com.github.nkzawa.socketio.client.Socket;

import org.json.JSONObject;

import java.net.URISyntaxException;


public class MainActivity extends FragmentActivity {

    private MediaPlayer mPlayer = null;

    private final int minView = 1;
    private final int maxView = 6;
    private Socket socket;
    private int currentViewTable = 0;

    private String url = "http://10.41.10.205:8080";

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
        loadView(minView);
        loadSoundButton();
        connectionServer();
    }

//Buttons
    public void onPreviousView(View v)
    {
        if(currentViewTable >minView){
            loadView(currentViewTable-1);
            forcePush(MessagesServer.nextView);
        }
    }

    public void onNextView(View v)
    {
        if(currentViewTable < maxView){
            loadView(currentViewTable+1);
            forcePush(MessagesServer.previousView);
        }
    }

//Sound
    public void loadSoundButton(){
        Button btn_sound_encouragement = (Button) findViewById(R.id.buttonEncouragement);
        btn_sound_encouragement.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendSound(MessagesServer.soundEncouragement);
            }

        });

        Button btn_sound_felicitation = (Button) findViewById(R.id.buttonFelicitation);
        btn_sound_felicitation.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendSound(MessagesServer.soundCongratulation);
            }

        });

        Button btn_sound_essaieEncore = (Button) findViewById(R.id.buttonEssaieEncore);
        btn_sound_essaieEncore.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendSound(MessagesServer.soundTryAgain);
            }

        });
    }

    public void loadView(int view)
    {
        if(currentViewTable != view ) {
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

            switch (view) {
                case 1: //Vue roue
                    fragmentTransaction.replace(R.id.fragment, new ViewWheel());

                    break;
                case 2: //Vue choix piece
                    fragmentTransaction.replace(R.id.fragment, new ChoicePlace());
                    break;
                case 3: //Vue choix atelier
                    fragmentTransaction.replace(R.id.fragment, new ChoiceActivity());
                    break;
                case 4: //Vue trouver les objets avec les aides
                    fragmentTransaction.replace(R.id.fragment, new FindObject());
                    break;
                case 5: //Vue ordonnancer les objets
                    fragmentTransaction.replace(R.id.fragment, new Scheduling());
                    break;
                case 6: //Vue afficher les vidéos
                    fragmentTransaction.replace(R.id.fragment, new DisplayVideo());
                    break;
                default:
                    break;
            }
            currentViewTable = view;
            fragmentTransaction.addToBackStack(null);
            fragmentTransaction.commit();
        }
    }









//Partie Server
    public void connectionServer(){
        try {
        socket = IO.socket(url);
        } catch (URISyntaxException e) {
            e.printStackTrace();
        }

        socket.on(Socket.EVENT_CONNECT, new Emitter.Listener() {
            @Override
            public void call(Object... args) {
                sendAskView(MessagesServer.whichView);
            }
        }).on(Socket.EVENT_DISCONNECT, new Emitter.Listener() {
            @Override
            public void call(Object... args) {}

        }).on(MessagesServer.changeView, new Emitter.Listener() {
            @Override
            public void call(Object... args) {
                    currentViewTable = (int) args[0];
                    Log.e("Server" , "Result : " + currentViewTable);
                    loadView(currentViewTable);
                }
        });
        socket.connect();
    }

//Envoie des messages
    public JSONObject sendAskView(String msg){
        socket.emit(MessagesServer.view, msg);
        return null;
    }

//Envoie des messages
    public JSONObject sendSound(String msg){
        socket.emit(MessagesServer.sound, msg);
        return null;
    }

//Event
    public JSONObject sendClignoter (String msg){
        socket.emit(MessagesServer.clignoter, msg);
        return null;
    }

    public JSONObject sendFlash(String msg){
        socket.emit(MessagesServer.flash, msg);
        return null;
    }

    public JSONObject sendZoom(String msg){
        socket.emit(MessagesServer.zoom, msg);
        return null;
    }
//Aide
    public JSONObject sendAide(String msg){
        socket.emit(MessagesServer.help, msg);
        return null;
    }

//Aide Atelier
    public JSONObject sendAideAtelier(String msg){
        socket.emit(MessagesServer.helpAtelier, msg);
        return null;
    }

//Aide Place
    public JSONObject sendAidePlace(String msg){
        socket.emit(MessagesServer.helpPlace, msg);
        return null;
    }

    //Aide Action
    public JSONObject sendAideAction(String msg){
        socket.emit(MessagesServer.helpAction, msg);
        return null;
    }

//ForcePush
    public JSONObject forcePush(String msg){
        socket.emit(MessagesServer.forcePush, msg);
        return null;
    }

}
