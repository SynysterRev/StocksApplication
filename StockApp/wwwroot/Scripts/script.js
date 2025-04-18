const token = document.querySelector("#token").getAttribute("data-value");
const stockSymbol = document.querySelector("#StockSymbol").value;
const stockPrice = document.querySelector("#stock-price");

webSocket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

webSocket.addEventListener("open", function (event) {
    webSocket.send(JSON.stringify({
        "type": "subscribe",
        "symbol": stockSymbol
    }));
});

webSocket.onmessage = function (event) {
    try {
        const msg = JSON.parse(event.data);
        if (msg.data != null) {
            const newPrice = msg.data[0]["p"];
            stockPrice.textContent = newPrice.toFixed(2);
        }
    } catch (error) {
        console.error("Error with web socket:", error);
    }
};

window.onunload = function (event) {
    webSocket.close();
}