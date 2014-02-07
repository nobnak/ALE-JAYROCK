ALE-JAYROCK for Unity
===========
Unity で Jayrock (JSON-RPC) を Node.js ぽく(ALE のイベントループ)使う

## Example
Jayrock で TcpListener 使った[サンプル](https://groups.google.com/d/topic/jayrock/BcxUGhFfrds/discussion)も以下のように書ける

```c#
ALE.Tcp.Net.CreateTcpSocketServer((e, client) => {
	try {
		Debug.Log("Connected");
		using (NetworkStream stream = client.GetStream()) {
			Service service = new Service();
			StreamReader reader = new StreamReader(stream, Encoding.UTF8);
			StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false));
			JsonRpcDispatcher dispatcher = new JsonRpcDispatcher(service);
			dispatcher.Process(reader, writer);
			writer.Flush();
		}
	} finally {
		client.Close();
	}
}).Listen(hostname, listenPort, "http://localhost");
```
