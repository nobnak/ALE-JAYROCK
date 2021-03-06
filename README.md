ALE-JAYROCK for Unity
===========
Unity で Jayrock (JSON-RPC) を Node.js ぽく(ALE のイベントループ)使う

## Example
Jayrock で TcpListener 使った[サンプル](https://groups.google.com/d/topic/jayrock/BcxUGhFfrds/discussion)も以下のように書ける

```c#
ALE.Tcp.Net.CreateTcpSocketServer((e, client) => {
	using (var stream = client.GetStream())
	using (var reader = new StreamReader(stream, Encoding.UTF8))
	using (var writer = new StreamWriter(stream, new UTF8Encoding(false))) {
		Service service = new Service();
		JsonRpcDispatcher dispatcher = new JsonRpcDispatcher(service);
		dispatcher.Process(reader, writer);
		writer.Flush();
	}
}).Listen(hostname, listenPort, "http://localhost");
```

## Tips
 - 以下のDLLをUnityからPluginsフォルダ等にコピーしておく
   - Mono.Web
   - System.Configuration
   - System.Drawing
   - System.EnterpriseService
   - System.Security
   - System.Web
 - API Compatibility Level を ".NET 2.0" にする
 - IP address を 0.0.0.0 (Any) にする(どのアドレスでの接続も許可)
