﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SocketIOClient;
//using SocketIOClient.Messages;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

namespace PaintSurface
{
    class SocketManager
    {

        private string _serverURL;
        public string ServerUrl {
            get {
                return this._serverURL;
            }
            set {
                this._serverURL = value;
                this._connection();
            }
        }

        //public Client socket;
        public Socket socket;

        public SocketManager(string serverUrl)
        {
            this.ServerUrl = serverUrl;
            this.socket = null;
        }

        private void _connection()
        {
            if (this.socket != null)
            {
                System.Net.WebRequest.DefaultWebProxy = null;

                this.socket = IO.Socket(this.ServerUrl);
                /*this.socket.Opened += SocketOpened;
                this.socket.Error += SocketError;
                this.socket.Message += SocketMessage;
                */

                this.socket.On("connect", (data) =>
                {
                    Console.WriteLine("Connected to PaintServer.");
                    socket.Emit("isTable");
                });

                this.socket.On("disconnect", (data) =>
                {
                    Console.WriteLine("Disconnected from PaintServer.");
                });

                this.socket.On("reconnect", (data) =>
                {
                    Console.WriteLine("Connected to PaintServer after " + data + " attempts.");
                });

                this.socket.On("reconnect_attempt", (data) =>
                {
                    Console.WriteLine("Trying to reconnect to PaintServer.");
                });

                this.socket.On("reconnecting", (data) =>
                {
                    Console.WriteLine("Trying to connect to PaintServer - Attempt number " + data + ".");
                });

                this.socket.On("reconnect_error", (data) =>
                {
                    Console.WriteLine("An error occured during reconenction to PaintServer.");
                });

                this.socket.On("reconnect_failed", (data) =>
                {
                    Console.WriteLine("Failed to connect to PaintServer. No new attempt will be done.");
                });


                this.socket.On("newTablet", (data) =>
                {
                    Console.WriteLine(data);
                    Dictionary<string, string> dataJObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(data as string);

                    Dictionary<string, string> viewportDesc = new Dictionary<string, string>();
                    viewportDesc.Add("id", dataJObject["id"]);
                    viewportDesc.Add("width", "200");
                    viewportDesc.Add("height", "300");
                    socket.Emit("setTabletViewport", JsonConvert.SerializeObject(viewportDesc));
                });

                this.socket.Connect();
            }
        }

        /*
        public void SocketOpened(object sender, EventArgs e)
        {
            Console.WriteLine("opened event handler");
            Console.WriteLine(e.ToString());
        }

        public void SocketError(object sender, SocketIOClient.ErrorEventArgs e)
        {
            Console.WriteLine("error event handler");
            Console.WriteLine(e.Message);
        }

        public void SocketMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("message event handler");
            Console.WriteLine(e.Message);
        }

        */

        
    }
}
