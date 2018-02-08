import socket

from flask import Flask, request

app = Flask(__name__)


@app.route("/")
def main():
    return """<script>
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function() {{ 
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            console.log("Flask told me " + xmlHttp.responseText);
    }}
    var source = new EventSource("https://streaming-graph.facebook.com/{live_id}/live_comments/?access_token={access_token}");
    source.onmessage = function(event) {{
        console.log("FB event: " + event.data);
        xmlHttp.open("GET", "http://{ADDR[0]}:{ADDR[1]}/gateway/?vars=" + event.data, true); // true for asynchronous 
        xmlHttp.send(null);
    }};
</script>""".format(
        ADDR=ADDR,
        access_token=access_token,
        live_id=live_id
    )


@app.route("/gateway/")
def gateway():
    c.send((str(request.args["vars"]) + "\n").encode())

    return str(request.args)


if __name__ == "__main__":

    ADDR = ('127.0.0.1', 54321)
    SOCKET_ADDR = ('127.0.0.1', 12345)

    access_token = input("Access token? ")
    live_id = input("Live ID? ")

    s = socket.socket()
    s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.bind(SOCKET_ADDR)
    s.listen(-1)

    print("Waiting for the C# client to log in...")
    c = s.accept()[0]
    print("Success!")

    try:
        app.run(ADDR[0], ADDR[1])
    finally:
        s.close()
