var express = require('express');
var app = express();
var httpserver = require('http').Server(app);
var io = require('socket.io').listen(httpserver);

var port = 8080;

app.get('/', function (req, res) {
    res.sendFile(__dirname + '/index.html');
});

// launch the http server on given port
httpserver.listen(port);

console.log("Server listening on *:" + port);

var tableSocket = null;
var androidSocket = null;
var iosSocket = null;

var tablets = [];

io.on('connection', function (socket) {
    console.log("New Client Connection : " + socket.id);

    tablets[socket.id] = socket;

    //For test
    socket.on('sendToTablet', function (messageDescription) {
        console.log("sendToTablet");
        if (typeof (tablets[messageDescription.tabletId]) != "undefined") {
            console.log("emit : " + messageDescription.event + " to " + messageDescription.tabletId);
            console.log(JSON.parse(messageDescription.value));
            tablets[messageDescription.tabletId].emit(messageDescription.event, JSON.parse(messageDescription.value));
        }
    });

    var username = "unknown";
    var isTable = false;

    //Tablet actions
    socket.on('profil', function (profilDescription) {
        console.log("Profil description :");
        console.log(profilDescription);
        username = profilDescription.username;
    });

    socket.on('username', function (usernameDescription) {
        console.log("username");
        username = usernameDescription.username;
        if (tableSocket != null) {
            console.log("emit newTablet to SurfaceTable")
            tableSocket.emit("newTablet", { "id": socket.id, "username": username })
        }
    });

    //Surface Table actions

    socket.on('isAndroid', function () {
        console.log("isAndroid");
        isTable = true;
        tableSocket = tablets[socket.id];
        tablets[socket.id] = "";
    });


    socket.on('isTableSurface', function () {
        console.log("isTableSurface");
        isTable = true;
        tableSocket = tablets[socket.id];
        tablets[socket.id] = "";
        //tableSocket.emit(changeMode, "string")
    });


    socket.on('setTabletViewport', function (viewportDescription) {
        console.log("setTabletViewport");
        if (isTable) {
            if (typeof (tablets[viewportDescription.id]) != "undefined") {
                tablets[viewportDescription.id].emit("viewport", { "width": viewportDescription.width, "height": viewportDescription.height })
            }
        }
    });

    socket.on('disconnect', function () {
        console.log("Client disconnected : " + socket.id);
    });

    socket.on('error', function (errorData) {
        console.log("An error occurred during Client connection : " + socket.id);
        console.log(errorData);
    });

    socket.on('reconnect', function (attemptNumber) {
        console.log("Client Connection : " + socket.id + " after " + attemptNumber + " attempts.");
    });

    socket.on('reconnect_attempt', function () {
        console.log("Client reconnect attempt : " + socket.id);
    });

    socket.on('reconnecting', function (attemptNumber) {
        console.log("Client Reconnection : " + socket.id + " - Attempt number " + attemptNumber);
    });

    socket.on('reconnect_error', function (errorData) {
        console.log("An error occurred during Client reconnection : " + socket.id);
        console.log(errorData);
    });

    socket.on('reconnect_failed', function () {
        console.log("Failed to reconnect Client : " + socket.id + ". No new attempt will be done.");
    });


    socket.on('ask_for_workshop', function (side) {

        console.log("ask_for_workshop");
        
      
        
        console.log(side);
        if(side === "left"){
            socket.emit('workshop', {
            "name": "brossage de dents",
            "frieze": [
			{
			    "position": 1,
			    "image": "prendre_brossedent"
			},
			{
			    "position": 2,
			    "image": "mouiller_brosse"
			},
			{
			    "position": 3,
			    "image": "mettre_dentifrice"
			},
			{
			    "position": 4,
			    "image": "brosser"
			},
			{
			    "position": 5,
			    "image": "cracher"
			},
			{
			    "position": 6,
			    "image": "rincer_bouche"
			}
            ]
        });
            console.log("emit left workshop");
        }
         if(side === "right"){
            console.log("emit right workshop");
            socket.emit('workshop', {
            "name": "fake droit",
            "frieze": [
			{
			    "position": 1,
			    "image": "prendre_brossedent"
			},
			{
			    "position": 2,
			    "image": "mouiller_brosse"
			},
			{
			    "position": 3,
			    "image": "mettre_dentifrice"
			}
            ]
        });
        }
    });
    /*
        socket.on('changeMode', function (mode) {
    
            console.log("change_mode");
            socket.broadcast.emit('changeMode', mode);
    
        });
    
        socket.on('vue', function (vue) {
    
            console.log("change_vue");
            socket.emit('changeVue', vue);
    
        });
        */
    socket.on('aide', function (data) {
        console.log("aide" + data);
        socket.broadcast.emit('aide', data);
    });
    
    socket.on('sound', function (data) {
        console.log("sound");
        socket.broadcast.emit('sound', data);
    });

    socket.on('clignoter', function (data) {
        console.log("clignoter");
        socket.broadcast.emit('clignoter', data);
    });

    socket.on('flash', function (data) {
        console.log("flash");
        socket.broadcast.emit('flash', data);
    });

    socket.on('zoom', function (data) {
        console.log("zoom");
        socket.broadcast.emit('zoom', data);
    });

    socket.on('hardPush', function (data) {
        console.log("hardPush");
        socket.broadcast.emit('hardPush',data);
    });

    socket.on('play_video', function (video) {

        console.log("play_video");
        socket.broadcast.emit('play_video', video);

    });

});
