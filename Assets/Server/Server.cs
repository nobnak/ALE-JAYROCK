﻿using UnityEngine;
using System.Collections;
using ALE;
using Jayrock.JsonRpc;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;

public class Server : MonoBehaviour {
	public string hostname = "0.0.0.0";
	public int listenPort = 3333;

	void OnEnable() {
		EventLoop.Start();
	}

	void OnDisable() {
		EventLoop.Stop();
	}

	void Start() {
		ALE.Tcp.Net.CreateTcpSocketServer((e, client) => {
			try {
				Debug.Log("Connected");
				using (var stream = client.GetStream())
				using (var reader = new StreamReader(stream, Encoding.UTF8))
				using (var writer = new StreamWriter(stream, new UTF8Encoding(false))) {
					Service service = new Service();
					JsonRpcDispatcher dispatcher = new JsonRpcDispatcher(service);
					dispatcher.Process(reader, writer);
					writer.Flush();
					stream.Flush();
				}
			} catch (System.Exception ex) {
				Debug.Log(ex);
			} finally {
				Debug.Log("Disconnected");
			}
        }).Listen(hostname, listenPort, "http://localhost");
	}
	
	class Service : JsonRpcService {
		[ JsonRpcMethod("add") ]
		public int Add(int a, int b) { return a + b; }
		[ JsonRpcMethod("env") ]
		public IDictionary GetEnvironment() { return System.Environment.GetEnvironmentVariables(); }
	}
}
