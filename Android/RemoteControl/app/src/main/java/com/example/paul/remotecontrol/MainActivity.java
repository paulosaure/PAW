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

    public void loadSoundButton(){
        Button btn_sound_encouragement = (Button) findViewById(R.id.buttonEncouragement);
        btn_sound_encouragement.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendMsg("sound_encouragement");
            }

        });

        Button btn_sound_felicitation = (Button) findViewById(R.id.buttonFelicitation);
        btn_sound_felicitation.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendMsg("sound_felicitation");
            }

        });

        Button btn_sound_erreur = (Button) findViewById(R.id.buttonErreur);
        btn_sound_erreur.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {

                sendMsg("sound_erreur");
            }

        });

        Button btn_sound_essaieEncore = (Button) findViewById(R.id.buttonEssaieEncore);
        btn_sound_essaieEncore.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {
                sendMsg("sound_essaie_encore");
            }

        });

        ImageButton btn_next_view = (ImageButton) findViewById(R.id.imageButton);
        btn_sound_essaieEncore.setOnClickListener(new OnClickListener() {

            @Override
            public void onClick(View v) {

            }

        });
    }

    public void connectionServer(){
        try {
        socket = IO.socket("http://134.59.214.247:8080");
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

        }).on("change_vue", new Emitter.Listener() {
            @Override
            public void call(Object... args) {
                currentViewTable = (String) args[0];
                Log.e("Server" , "Result : " + currentViewTable);
            }
        });
        socket.connect();
    }

    public JSONObject sendMsg(String msg){
        JSONObject obj = new JSONObject();
        socket.emit("isTable", msg);
        return null;
    }
}
